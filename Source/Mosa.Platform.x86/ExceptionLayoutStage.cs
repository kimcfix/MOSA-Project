/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using System;
using System.Collections.Generic;
using System.IO;
using Mosa.Compiler.Common;
using Mosa.Compiler.Framework;
using Mosa.Compiler.Linker;
using Mosa.Compiler.Metadata;
using Mosa.Compiler.TypeSystem;

// FIXME: Splits this class into platform dependent and independent classes. Move platform independent code into Mosa.Compiler.Framework

namespace Mosa.Platform.x86
{
	public sealed class ExceptionLayoutStage : BaseMethodCompilerStage, IMethodCompilerStage, IPipelineStage
	{
		#region Data members

		//FIXME: Assumes LittleEndian architecture (okay for x86 but not for platform independent code)
		private static readonly DataConverter LittleEndianBitConverter = DataConverter.LittleEndian;

		private List<ExceptionClauseNode> sortedClauses;

		private Dictionary<BasicBlock, ExceptionClause> blockExceptions;

		private Dictionary<ExceptionClause, List<BasicBlock>> exceptionBlocks = new Dictionary<ExceptionClause, List<BasicBlock>>();

		private ICodeEmitter codeEmitter;

		#endregion // Data members

		#region IMethodCompilerStage members

		/// <summary>
		/// Performs stage specific processing on the compiler context.
		/// </summary>
		public void Run()
		{
			codeEmitter = methodCompiler.Pipeline.FindFirst<CodeGenerationStage>().CodeEmitter;

			// Step 1 - Sort the exception clauses into postorder-traversal
			BuildSort();

			// Step 2 - Assign blocks to innermost exception clause
			AssignBlocksToClauses();

			// Step 3 - Emit table of PC ranges and the clause handler
			EmitExceptionTable();
		}

		#endregion // IMethodCompilerStage members

		private void AssignBlocksToClauses()
		{
			blockExceptions = new Dictionary<BasicBlock, ExceptionClause>();

			foreach (BasicBlock block in basicBlocks)
			{
				ExceptionClause clause = FindExceptionClause(block);

				if (clause != null)
				{
					List<BasicBlock> blocks;

					if (!exceptionBlocks.TryGetValue(clause, out blocks))
					{
						blocks = new List<BasicBlock>();
						exceptionBlocks.Add(clause, blocks);
					}

					blocks.Add(block);
					blockExceptions.Add(block, clause);
				}
			}

		}

		private ExceptionClause FindExceptionClause(BasicBlock block)
		{
			Context ctx = new Context(instructionSet, block);
			int label = ctx.Label;

			foreach (ExceptionClauseNode node in sortedClauses)
			{
				if (node.Clause.IsLabelWithinTry(label))
					return node.Clause;

				//if (node.Clause.TryEnd > label)
				//    return null; // early out
			}

			return null;
		}

		private struct ExceptionEntry
		{
			public ExceptionClauseType Kind;
			public uint Start;
			public uint Length;
			public uint Handler;

			public uint Filter;
			public RuntimeType Type;

			public uint End { get { return Start + Length - 1; } }

			public ExceptionEntry(uint start, uint length, uint handler, ExceptionClauseType kind, RuntimeType type, uint filter)
			{
				Start = start;
				Length = length;
				Kind = kind;
				Handler = handler;
				Type = type;
				Filter = filter;
			}

		}

		private void EmitExceptionTable()
		{
			List<ExceptionEntry> entries = new List<ExceptionEntry>();

			foreach (ExceptionClauseNode node in sortedClauses)
			{
				ExceptionClause clause = node.Clause;

				ExceptionEntry prev = new ExceptionEntry();

				foreach (BasicBlock block in this.exceptionBlocks[clause])
				{
					uint start = (uint)codeEmitter.GetPosition(block.Label);
					uint length = (uint)codeEmitter.GetPosition(block.Label + 0x0F000000) - start;
					uint handler = (uint)codeEmitter.GetPosition(clause.TryOffset);

					uint filter = 0;
					RuntimeType type = null;

					if (clause.Kind == ExceptionClauseType.Exception)
					{
						// Convert token to method table pointer (linker request needs to be involved)

						// Get runtime type
						type = typeModule.GetType(new Token(clause.ClassToken));
					}
					else if (clause.Kind == ExceptionClauseType.Filter)
					{
						filter = (uint)codeEmitter.GetPosition(clause.FilterOffset);
					}

					// TODO: Optimization - Search for existing exception protected region  (before or after) to merge the current block

					// Simple optimization assuming blocks are somewhat sorted by position
					if (prev.End + 1 == start && prev.Kind == clause.Kind && prev.Handler == handler && prev.Filter == filter && prev.Type == type)
					{
						// merge protected blocks sequence
						prev.Length = prev.Length + (uint)codeEmitter.GetPosition(block.Label + 0x0F000000) - start;
					}
					else
					{
						// new protection block sequence
						ExceptionEntry entry = new ExceptionEntry(start, length, handler, clause.Kind, type, filter);
						entries.Add(entry);
						prev = entry;
					}
				}
			}

			int tableSize = (entries.Count * nativePointerSize * 5) + nativePointerSize;

			string section = methodCompiler.Method.FullName + @"$etable";

			using (Stream stream = methodCompiler.Linker.Allocate(section, SectionKind.ROData, tableSize, nativePointerAlignment))
			{
				foreach (ExceptionEntry entry in entries)
				{
					// FIXME: Assumes x86 platform
					WriteLittleEndian4(stream, (uint)entry.Kind);
					WriteLittleEndian4(stream, entry.Start);
					WriteLittleEndian4(stream, entry.Length);
					WriteLittleEndian4(stream, entry.Handler);

					if (entry.Kind == ExceptionClauseType.Exception)
					{
						// TODO: Store method table pointer here
						methodCompiler.Linker.Link(LinkType.AbsoluteAddress | LinkType.I4, section, (int)stream.Position, 0, entry.Type.FullName + "$mtable", IntPtr.Zero);

						stream.Position += nativePointerSize;
					}
					else if (entry.Kind == ExceptionClauseType.Filter)
					{
						// TODO: There are no plans in the short term to support filtered exception clause - C# doesn't use them 
						stream.Position += nativePointerSize;
					}
					else
					{
						stream.Position += nativePointerSize;
					}
				}

				// Mark end of table
				stream.Position += typeLayout.NativePointerSize;
			}

		}

		static private void WriteLittleEndian4(Stream stream, uint value)
		{
			stream.Write(LittleEndianBitConverter.GetBytes(value));
		}

		#region Postorder-traversal Sort

		private class ExceptionClauseNode : IComparable<ExceptionClauseNode>
		{
			public ExceptionClause Clause { get; private set; }

			public int Start { get { return Clause.TryOffset; } }
			public int Length { get { return Clause.TryLength; } }

			public ExceptionClauseNode Parent;
			public List<ExceptionClauseNode> Children;

			public ExceptionClauseNode(ExceptionClause clause)
			{
				Clause = clause;
			}

			public bool IsInside(ExceptionClauseNode pair)
			{
				return this.Start > pair.Start && (this.Start + this.Length < pair.Start + pair.Length);
			}

			public int CompareTo(ExceptionClauseNode other)
			{
				return this.Start.CompareTo(other.Start);
			}

		}

		private void BuildSort()
		{
			sortedClauses = new List<ExceptionClauseNode>(methodCompiler.ExceptionClauseHeader.Clauses.Count);

			foreach (ExceptionClause clause in methodCompiler.ExceptionClauseHeader.Clauses)
				sortedClauses.Add(new ExceptionClauseNode(clause));

			// TODO: Sort? Seems to be missing here
		}

		private List<ExceptionClauseNode> Sort(List<ExceptionClauseNode> listToSort)
		{
			var result = new List<ExceptionClauseNode>();
			listToSort.Sort();
			foreach (var pair in listToSort)
			{
				pair.Children = this.GetChildren(pair, listToSort);
				pair.Children.Sort();
			}

			var root = listToSort[0];
			result.Add(root);
			InsertChildren(result, root);
			return result;
		}

		private void InsertChildren(List<ExceptionClauseNode> result, ExceptionClauseNode root)
		{
			result.InsertRange(result.IndexOf(root), root.Children);
			foreach (var pair in root.Children)
				InsertChildren(result, pair);
		}

		private List<ExceptionClauseNode> GetChildren(ExceptionClauseNode pair, List<ExceptionClauseNode> list)
		{
			var children = new List<ExceptionClauseNode>();
			foreach (var other in list)
			{
				if (other.IsInside(pair))
					children.Add(other);
			}

			var result = new List<ExceptionClauseNode>(children);
			foreach (var x in children)
			{
				foreach (var y in children)
				{
					if (x.IsInside(y))
						result.Remove(x);
				}
			}

			foreach (var x in result)
				x.Parent = pair;

			return result;
		}

		#endregion

	}
}