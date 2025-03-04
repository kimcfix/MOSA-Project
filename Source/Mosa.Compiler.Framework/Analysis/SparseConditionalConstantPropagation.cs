﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Mosa.Compiler.Common;
using Mosa.Compiler.Framework.Trace;
using static Mosa.Compiler.Framework.BaseMethodCompilerStage;

namespace Mosa.Compiler.Framework.Analysis;

/// <summary>
/// Sparse Conditional Constant Propagation (SCCP) Optimization
/// </summary>
public sealed class SparseConditionalConstantPropagation
{
	private const int MAXCONSTANTS = 5;

	private sealed class VariableState
	{
		private enum VariableStatusType
		{ Unknown, OverDefined, SingleConstant, MultipleConstants }

		private enum ReferenceStatusType
		{ Unknown, DefinedNotNull, OverDefined }

		private VariableStatusType Status;

		private ReferenceStatusType ReferenceStatus;

		public int ConstantCount => Constants?.Count ?? 0;

		public List<ulong> Constants { get; private set; }

		public ulong ConstantUnsignedLongInteger => Constants[0];

		public long ConstantSignedLongInteger => (long)Constants[0];

		public bool ConstantsContainZero { get; set; }

		public Operand Operand { get; }

		public bool IsOverDefined
		{
			get => Status == VariableStatusType.OverDefined;
			set { Status = VariableStatusType.OverDefined; Constants = null; Debug.Assert(value); }
		}

		public bool IsUnknown => Status == VariableStatusType.Unknown;

		public bool IsSingleConstant
		{
			get => Status == VariableStatusType.SingleConstant;
			set { Status = VariableStatusType.SingleConstant; Debug.Assert(value); }
		}

		public bool HasMultipleConstants => Status == VariableStatusType.MultipleConstants;

		public bool HasOnlyConstants => Status is VariableStatusType.SingleConstant or VariableStatusType.MultipleConstants;

		public bool IsVirtualRegister { get; set; }

		public bool IsReferenceType { get; set; }

		public bool IsReferenceDefinedUnknown => ReferenceStatus == ReferenceStatusType.Unknown;

		public bool IsReferenceDefinedNotNull
		{
			get => ReferenceStatus == ReferenceStatusType.DefinedNotNull;
			set
			{
				Debug.Assert(value);
				ReferenceStatus = ReferenceStatusType.DefinedNotNull;
			}
		}

		public bool IsReferenceOverDefined
		{
			get => ReferenceStatus == ReferenceStatusType.OverDefined;
			set
			{
				Debug.Assert(value);
				ReferenceStatus = ReferenceStatusType.OverDefined;
			}
		}

		public VariableState(Operand operand)
		{
			Operand = operand;

			IsVirtualRegister = operand.IsVirtualRegister;
			IsReferenceType = operand.IsObject;
			ConstantsContainZero = false;

			if (IsVirtualRegister)
			{
				Status = VariableStatusType.Unknown;
				IsVirtualRegister = true;
			}
			else if (operand.IsUnresolvedConstant)
			{
				IsOverDefined = true;
			}
			else if (operand.IsConstant && operand.IsInteger)
			{
				AddConstant(operand.ConstantUnsigned64);
			}
			else if (operand.IsNull)
			{
				AddConstant(0);
			}
			else
			{
				IsOverDefined = true;
			}

			if (!IsReferenceType || !IsVirtualRegister)
			{
				ReferenceStatus = ReferenceStatusType.OverDefined;
			}
			else
			{
				ReferenceStatus = ReferenceStatusType.Unknown;
			}
		}

		private void AppendConstant(ulong value)
		{
			Constants.Add(value);

			if (value == 0)
			{
				ConstantsContainZero = true;
			}
		}

		public bool AddConstant(ulong value)
		{
			if (Status == VariableStatusType.OverDefined)
				return false;

			if (Constants != null)
			{
				if (Constants.Contains(value))
					return false;
			}
			else
			{
				Constants = new List<ulong>(2);
				AppendConstant(value);
				Status = VariableStatusType.SingleConstant;
				return true;
			}

			if (Constants.Count > MAXCONSTANTS)
			{
				Status = VariableStatusType.OverDefined;
				Constants = null;
				return true;
			}

			AppendConstant(value);

			Status = VariableStatusType.MultipleConstants;
			return true;
		}

		public void AddConstant(long value)
		{
			AddConstant((ulong)value);
		}

		public bool AreConstantsEqual(VariableState other)
		{
			if (!other.IsSingleConstant || !IsSingleConstant)
				return false;

			return other.ConstantUnsignedLongInteger == ConstantUnsignedLongInteger;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append($"{Operand} : {Status}");

			if (IsSingleConstant)
			{
				sb.Append($" = {ConstantUnsignedLongInteger}");
			}
			else if (HasMultipleConstants)
			{
				sb.Append($" ({Constants.Count}) =");
				foreach (var i in Constants)
				{
					sb.Append($" {i},");
				}
				sb.Length--;
			}

			sb.Append(" [null: ");
			if (IsReferenceOverDefined)
				sb.Append("OverDefined");
			else if (IsReferenceDefinedNotNull)
				sb.Append("NotNull");
			else if (IsReferenceDefinedUnknown)
				sb.Append("Unknown");
			sb.Append(']');

			return sb.ToString();
		}
	}

	private readonly bool[] blockStates;

	private readonly Dictionary<Operand, VariableState> variableStates;

	private readonly Stack<InstructionNode> instructionWorkList;
	private readonly Stack<BasicBlock> blockWorklist;

	private readonly HashSet<InstructionNode> executedStatements;

	private readonly BasicBlocks BasicBlocks;
	private readonly CreateTraceHandler CreateTrace;
	private readonly TraceLog MainTrace;

	private readonly KeyedList<BasicBlock, InstructionNode> phiStatements;

	private readonly bool Is32BitPlatform;

	public SparseConditionalConstantPropagation(BasicBlocks basicBlocks, CreateTraceHandler createTrace, bool is32BitPlatform)
	{
		// Method is empty - must be a plugged method
		if (basicBlocks.HeadBlocks.Count == 0)
			return;

		CreateTrace = createTrace;
		BasicBlocks = basicBlocks;
		Is32BitPlatform = is32BitPlatform;

		variableStates = new Dictionary<Operand, VariableState>();
		instructionWorkList = new Stack<InstructionNode>();
		blockWorklist = new Stack<BasicBlock>();
		phiStatements = new KeyedList<BasicBlock, InstructionNode>();
		executedStatements = new HashSet<InstructionNode>();

		MainTrace = CreateTrace("SparseConditionalConstantPropagation", 5);

		blockStates = new bool[BasicBlocks.Count];

		for (var i = 0; i < BasicBlocks.Count; i++)
		{
			blockStates[i] = false;
		}

		AddExecutionBlocks(BasicBlocks.HeadBlocks);
		AddExecutionBlocks(BasicBlocks.HandlerHeadBlocks);

		while (blockWorklist.Count > 0 || instructionWorkList.Count > 0)
		{
			ProcessBlocks();
			ProcessInstructions();
		}

		DumpTrace();

		// Release
		phiStatements = null;
	}

	public List<Tuple<Operand, ulong>> GetIntegerConstants()
	{
		var list = new List<Tuple<Operand, ulong>>();

		foreach (var variable in variableStates.Values)
		{
			if (variable.IsVirtualRegister && variable.IsSingleConstant)
			{
				list.Add(new Tuple<Operand, ulong>(variable.Operand, variable.ConstantUnsignedLongInteger));
			}
		}

		return list;
	}

	public List<BasicBlock> GetDeadBlocked()
	{
		var list = new List<BasicBlock>();

		for (var i = 0; i < BasicBlocks.Count; i++)
		{
			if (!blockStates[i])
			{
				list.Add(BasicBlocks[i]);
			}
		}

		return list;
	}

	private VariableState GetVariableState(Operand operand)
	{
		if (!variableStates.TryGetValue(operand, out VariableState variable))
		{
			variable = new VariableState(operand);
			variableStates.Add(operand, variable);
		}

		return variable;
	}

	private void DumpTrace()
	{
		if (MainTrace == null)
			return;

		var variableTrace = CreateTrace("Variables", 5);

		if (variableTrace == null)
			return;

		foreach (var variable in variableStates.Values)
		{
			if (variable.IsVirtualRegister)
			{
				variableTrace?.Log(variable.ToString());
			}
		}

		var blockTrace = CreateTrace("Blocks", 5);

		for (var i = 0; i < BasicBlocks.Count; i++)
		{
			blockTrace.Log($"{BasicBlocks[i]} = {(blockStates[i] ? "Executable" : "Dead")}");
		}
	}

	private void AddExecutionBlocks(List<BasicBlock> blocks)
	{
		foreach (var block in blocks)
			AddExecutionBlock(block);
	}

	private void AddExecutionBlock(BasicBlock block)
	{
		if (blockStates[block.Sequence])
			return;

		blockStates[block.Sequence] = true;
		blockWorklist.Push(block);
	}

	private void AddInstruction(InstructionNode node)
	{
		instructionWorkList.Push(node);
	}

	private void AddInstruction(VariableState variable)
	{
		foreach (var use in variable.Operand.Uses)
		{
			if (executedStatements.Contains(use))
			{
				AddInstruction(use);
			}
		}
	}

	private void ProcessBlocks()
	{
		while (blockWorklist.Count > 0)
		{
			var block = blockWorklist.Pop();
			ProcessBlock(block);
		}
	}

	private void ProcessBlock(BasicBlock block)
	{
		MainTrace?.Log($"Process Block: {block}");

		// if the block has only one successor block, add successor block to executed block list
		if (block.NextBlocks.Count == 1)
		{
			AddExecutionBlock(block.NextBlocks[0]);
		}

		ProcessInstructionsContinuiously(block.First);

		// re-analyze phi statements
		var phiUse = phiStatements.Get(block);

		if (phiUse == null)
			return;

		foreach (var index in phiUse)
		{
			AddInstruction(index);
		}
	}

	private void ProcessInstructionsContinuiously(InstructionNode node)
	{
		// instead of adding items to the worklist, the whole block will be processed
		for (; !node.IsBlockEndInstruction; node = node.Next)
		{
			if (node.IsEmpty)
				continue;

			var @continue = ProcessInstruction(node);

			executedStatements.Add(node);

			if (!@continue)
				return;
		}
	}

	private void ProcessInstructions()
	{
		while (instructionWorkList.Count > 0)
		{
			var node = instructionWorkList.Pop();

			if (node.Instruction == IRInstruction.Branch32
				|| node.Instruction == IRInstruction.Branch64
				|| node.Instruction == IRInstruction.BranchObject)
			{
				// special case
				ProcessInstructionsContinuiously(node);
			}
			else
			{
				ProcessInstruction(node);
			}
		}
	}

	private bool ProcessInstruction(InstructionNode node)
	{
		//MainTrace?.Log(node.ToString());

		var instruction = node.Instruction;

		if (instruction == IRInstruction.Move32
			|| instruction == IRInstruction.Move64
			|| instruction == IRInstruction.MoveObject)
		{
			Move(node);
		}
		else if (instruction == IRInstruction.NewObject
				 || instruction == IRInstruction.NewArray
				 || instruction == IRInstruction.NewString)
		{
			NewObject(node);
		}
		else if (instruction == IRInstruction.CallDynamic
				 || instruction == IRInstruction.CallInterface
				 || instruction == IRInstruction.CallDirect
				 || instruction == IRInstruction.CallStatic
				 || instruction == IRInstruction.CallVirtual
				 || instruction == IRInstruction.IntrinsicMethodCall)
		{
			Call(node);
		}
		else if (instruction == IRInstruction.Load32
				 || instruction == IRInstruction.Load64
				 || instruction == IRInstruction.LoadObject

				 || instruction == IRInstruction.LoadSignExtend8x32
				 || instruction == IRInstruction.LoadSignExtend16x32
				 || instruction == IRInstruction.LoadSignExtend8x64
				 || instruction == IRInstruction.LoadSignExtend16x64
				 || instruction == IRInstruction.LoadSignExtend32x64

				 || instruction == IRInstruction.LoadZeroExtend8x32
				 || instruction == IRInstruction.LoadZeroExtend16x32
				 || instruction == IRInstruction.LoadZeroExtend8x64
				 || instruction == IRInstruction.LoadZeroExtend16x64
				 || instruction == IRInstruction.LoadZeroExtend32x64

				 || instruction == IRInstruction.LoadR4
				 || instruction == IRInstruction.LoadR8

				 || instruction == IRInstruction.LoadParamSignExtend8x32
				 || instruction == IRInstruction.LoadParamSignExtend16x32
				 || instruction == IRInstruction.LoadParam32
				 || instruction == IRInstruction.LoadParam64
				 || instruction == IRInstruction.LoadParamSignExtend8x64
				 || instruction == IRInstruction.LoadParamSignExtend16x64
				 || instruction == IRInstruction.LoadParamSignExtend32x64
				 || instruction == IRInstruction.LoadParamZeroExtend8x32
				 || instruction == IRInstruction.LoadParamZeroExtend16x32
				 || instruction == IRInstruction.LoadParamZeroExtend8x64
				 || instruction == IRInstruction.LoadParamZeroExtend16x64
				 || instruction == IRInstruction.LoadParamZeroExtend32x64
				 || instruction == IRInstruction.LoadParamR4
				 || instruction == IRInstruction.LoadParamR8)
		{
			Load(node);
		}
		else if (instruction == IRInstruction.Add32
				 || instruction == IRInstruction.Add64
				 || instruction == IRInstruction.Sub32
				 || instruction == IRInstruction.Sub64
				 || instruction == IRInstruction.MulSigned32
				 || instruction == IRInstruction.MulUnsigned32
				 || instruction == IRInstruction.MulSigned64
				 || instruction == IRInstruction.MulUnsigned64
				 || instruction == IRInstruction.DivSigned32
				 || instruction == IRInstruction.DivUnsigned32
				 || instruction == IRInstruction.RemSigned32
				 || instruction == IRInstruction.RemUnsigned32
				 || instruction == IRInstruction.DivSigned64
				 || instruction == IRInstruction.DivUnsigned64
				 || instruction == IRInstruction.RemSigned64
				 || instruction == IRInstruction.RemUnsigned64
				 || instruction == IRInstruction.ShiftLeft32
				 || instruction == IRInstruction.ShiftRight32
				 || instruction == IRInstruction.ShiftLeft64
				 || instruction == IRInstruction.ShiftRight64
				 || instruction == IRInstruction.ArithShiftRight32
				 || instruction == IRInstruction.ArithShiftRight64)
		{
			IntegerOperation(node);
		}
		else if (instruction == IRInstruction.Compare32x32
				 || instruction == IRInstruction.Compare32x64
				 || instruction == IRInstruction.Compare64x32
				 || instruction == IRInstruction.Compare64x64
				 || instruction == IRInstruction.CompareObject)
		{
			CompareOperation(node);
		}
		else if (instruction.IsPhi)
		{
			Phi(node);
		}
		else if (instruction == IRInstruction.Jmp)
		{
			Jmp(node);
		}
		else if (instruction == IRInstruction.Branch32
				 || instruction == IRInstruction.Branch64
				 || instruction == IRInstruction.BranchObject)
		{
			return CompareBranch(node);
		}
		else if (instruction == IRInstruction.AddressOf)
		{
			AddressOf(node);
		}
		else if (instruction == IRInstruction.SignExtend8x32
				 || instruction == IRInstruction.SignExtend16x32
				 || instruction == IRInstruction.SignExtend8x64
				 || instruction == IRInstruction.SignExtend16x64
				 || instruction == IRInstruction.SignExtend32x64
				 || instruction == IRInstruction.ZeroExtend8x32
				 || instruction == IRInstruction.ZeroExtend16x32
				 || instruction == IRInstruction.ZeroExtend8x64
				 || instruction == IRInstruction.ZeroExtend16x64
				 || instruction == IRInstruction.ZeroExtend32x64)
		{
			SignOrZeroExtend(node);
		}
		else if (instruction == IRInstruction.Switch)
		{
			Switch(node);
		}
		else if (instruction == IRInstruction.IfThenElse32
				 || instruction == IRInstruction.IfThenElse64)
		{
			IfThenElse(node);
		}
		else if (instruction == IRInstruction.FinallyStart)
		{
			FinallyStart(node);
		}
		else if (instruction == IRInstruction.SetReturn32
				 || instruction == IRInstruction.SetReturnObject
				 || instruction == IRInstruction.SetReturn64
				 || instruction == IRInstruction.SetReturnR4
				 || instruction == IRInstruction.SetReturnR8
				 || instruction == IRInstruction.SetReturnCompound)
		{
			// nothing
		}
		else
		{
			// for all other instructions
			Default(node);
		}

		return true;
	}

	private static bool? NullComparisionCheck(ConditionCode condition, VariableState operand1, VariableState operand2)
	{
		// not null check
		if (condition == ConditionCode.Equal)
		{
			if (operand2.IsSingleConstant && operand2.ConstantSignedLongInteger == 0 && operand1.IsReferenceDefinedNotNull)
			{
				return false;
			}

			if (operand1.IsSingleConstant && operand1.ConstantSignedLongInteger == 0 && operand2.IsReferenceDefinedNotNull)
			{
				return false;
			}
		}
		else if (condition == ConditionCode.NotEqual)
		{
			if (operand2.IsSingleConstant && operand2.ConstantSignedLongInteger == 0 && operand1.IsReferenceDefinedNotNull)
			{
				return true;
			}

			if (operand1.IsSingleConstant && operand1.ConstantSignedLongInteger == 0 && operand2.IsReferenceDefinedNotNull)
			{
				return true;
			}
		}
		else if (condition == ConditionCode.UnsignedGreater)
		{
			if (operand2.IsSingleConstant && operand2.ConstantSignedLongInteger == 0 && operand1.IsReferenceDefinedNotNull)
			{
				return true;
			}
		}

		return null;
	}

	private void CompareOperation(InstructionNode node)
	{
		var result = GetVariableState(node.Result);

		if (result.IsOverDefined)
			return;

		var operand1 = GetVariableState(node.Operand1);
		var operand2 = GetVariableState(node.Operand2);

		var compare = NullComparisionCheck(node.ConditionCode, operand1, operand2);

		if (compare.HasValue)
		{
			UpdateToConstant(result, compare.Value ? 1u : 0u);
			return;
		}

		IntegerOperation(node);
	}

	private void UpdateToConstant(VariableState variable, ulong value)
	{
		Debug.Assert(!variable.IsOverDefined);

		if (variable.AddConstant(value))
		{
			MainTrace?.Log(variable.ToString());

			AddInstruction(variable);
		}
	}

	private void UpdateToOverDefined(VariableState variable)
	{
		if (variable.IsOverDefined)
			return;

		variable.IsOverDefined = true;

		MainTrace?.Log(variable.ToString());

		AddInstruction(variable);
	}

	private void AssignedNewObject(VariableState variable)
	{
		SetReferenceNotNull(variable);
	}

	private void SetReferenceOverdefined(VariableState variable)
	{
		SetReferenceNull(variable);
	}

	private void SetReferenceNull(VariableState variable)
	{
		if (variable.IsReferenceOverDefined)
			return;

		variable.IsReferenceOverDefined = true;

		MainTrace?.Log(variable.ToString());

		AddInstruction(variable);
	}

	private void SetReferenceNotNull(VariableState variable)
	{
		if (variable.IsReferenceOverDefined || variable.IsReferenceDefinedNotNull)
			return;

		variable.IsReferenceDefinedNotNull = true;

		MainTrace?.Log(variable.ToString());

		AddInstruction(variable);
	}

	private void Jmp(InstructionNode node)
	{
		if (node.BranchTargets == null || node.BranchTargetsCount == 0)
			return;

		Branch(node);
	}

	private void Move(InstructionNode node)
	{
		if (!node.Result.IsVirtualRegister)
			return;

		var result = GetVariableState(node.Result);
		var operand = GetVariableState(node.Operand1);

		CheckAndUpdateNullAssignment(result, operand);

		if (result.IsOverDefined)
			return;

		if (operand.IsOverDefined)
		{
			UpdateToOverDefined(result);
		}
		else if (operand.HasOnlyConstants)
		{
			foreach (var c in operand.Constants)
			{
				UpdateToConstant(result, c);

				if (result.IsOverDefined)
					return;
			}
		}
		else if (operand.IsUnknown)
		{
			Debug.Assert(result.IsUnknown);
		}
	}

	private void CheckAndUpdateNullAssignment(VariableState result, VariableState operand)
	{
		if (result.IsReferenceDefinedUnknown || result.IsReferenceDefinedNotNull)
		{
			if (operand.IsReferenceType)
			{
				if (operand.IsReferenceDefinedNotNull)
				{
					SetReferenceNotNull(result);
				}
				else if (operand.Operand.IsParameter)
				{
					// the this pointer can not be null
					AssignedNewObject(result);
				}
				else
				{
					SetReferenceOverdefined(result);
				}
			}
			else if (operand.IsSingleConstant && operand.ConstantUnsignedLongInteger != 0)
			{
				SetReferenceNotNull(result);
			}
			else if (operand.HasMultipleConstants && !operand.ConstantsContainZero)
			{
				SetReferenceNotNull(result);
			}
			else
			{
				SetReferenceOverdefined(result);
			}
		}
	}

	private void Call(InstructionNode node)
	{
		if (node.ResultCount == 0)
			return;

		Debug.Assert(node.ResultCount == 1);

		var result = GetVariableState(node.Result);

		//todo: go thru parameter operands, if any are out, then set overdefine on that parameter operand

		UpdateToOverDefined(result);
		SetReferenceOverdefined(result);
	}

	private void NewObject(InstructionNode node)
	{
		if (node.ResultCount == 0)
			return;

		Debug.Assert(node.ResultCount == 1);

		var result = GetVariableState(node.Result);

		UpdateToOverDefined(result);
		SetReferenceNotNull(result);
	}

	private void SignOrZeroExtend(InstructionNode node)
	{
		var result = GetVariableState(node.Result);

		if (result.IsOverDefined)
			return;

		var operand1 = GetVariableState(node.Operand1);

		if (operand1.IsOverDefined)
		{
			UpdateToOverDefined(result);
			return;
		}
		else if (operand1.IsUnknown)
		{
			Debug.Assert(result.IsUnknown);
			return;
		}
		else if (operand1.IsSingleConstant)
		{
			if (SignOrZeroExtend(node.Instruction, operand1.ConstantUnsignedLongInteger, out var value))
			{
				UpdateToConstant(result, value);
				return;
			}
			else
			{
				UpdateToOverDefined(result);
				return;
			}
		}
		else if (operand1.HasOnlyConstants)
		{
			foreach (var c1 in operand1.Constants)
			{
				if (SignOrZeroExtend(node.Instruction, c1, out var value))
				{
					UpdateToConstant(result, value);
				}
				else
				{
					UpdateToOverDefined(result);
					return;
				}

				if (result.IsOverDefined)
					return;
			}
		}
	}

	private void IntegerOperation(InstructionNode node)
	{
		var result = GetVariableState(node.Result);

		if (result.IsOverDefined)
			return;

		var operand1 = GetVariableState(node.Operand1);
		var operand2 = GetVariableState(node.Operand2);

		if (operand1.IsOverDefined || operand2.IsOverDefined)
		{
			UpdateToOverDefined(result);
			return;
		}
		else if (operand1.IsUnknown || operand2.IsUnknown)
		{
			Debug.Assert(result.IsUnknown);
			return;
		}
		else if (operand1.IsSingleConstant && operand2.IsSingleConstant)
		{
			if (IntegerOperation(node.Instruction, operand1.ConstantUnsignedLongInteger, operand2.ConstantUnsignedLongInteger, node.ConditionCode, out var value))
			{
				UpdateToConstant(result, value);
				return;
			}
			else
			{
				UpdateToOverDefined(result);
				return;
			}
		}
		else if (operand1.HasOnlyConstants && operand2.HasOnlyConstants)
		{
			foreach (var c1 in operand1.Constants)
			{
				foreach (var c2 in operand2.Constants)
				{
					if (IntegerOperation(node.Instruction, c1, c2, node.ConditionCode, out var value))
					{
						UpdateToConstant(result, value);
					}
					else
					{
						UpdateToOverDefined(result);
						return;
					}

					if (result.IsOverDefined)
						return;
				}
			}
		}
	}

	private static bool IntegerOperation(BaseInstruction instruction, ulong operand1, ulong operand2, ConditionCode conditionCode, out ulong result)
	{
		if (instruction == IRInstruction.Add32
			|| instruction == IRInstruction.Add64)
		{
			result = operand1 + operand2;
			return true;
		}
		else if (instruction == IRInstruction.Sub32
				 || instruction == IRInstruction.Sub64)
		{
			result = operand1 - operand2;
			return true;
		}
		else if (instruction == IRInstruction.MulUnsigned32
				 || instruction == IRInstruction.MulSigned32
				 || instruction == IRInstruction.MulUnsigned64
				 || instruction == IRInstruction.MulSigned64)
		{
			result = operand1 * operand2;
			return true;
		}
		else if ((instruction == IRInstruction.DivUnsigned32 || instruction == IRInstruction.DivUnsigned64) && operand2 != 0)
		{
			result = operand1 / operand2;
			return true;
		}
		else if ((instruction == IRInstruction.DivSigned32 || instruction == IRInstruction.DivSigned64) && operand2 != 0)
		{
			result = (ulong)((long)operand1 / (long)operand2);
			return true;
		}
		else if ((instruction == IRInstruction.RemUnsigned32 || instruction == IRInstruction.RemUnsigned64) && operand2 != 0)
		{
			result = operand1 % operand2;
			return true;
		}
		else if ((instruction == IRInstruction.RemSigned32 || instruction == IRInstruction.RemSigned64) && operand2 != 0)
		{
			result = (ulong)((long)operand1 % (long)operand2);
			return true;
		}
		else if (instruction == IRInstruction.ArithShiftRight32 || instruction == IRInstruction.ArithShiftRight64)
		{
			result = (ulong)((long)operand1 >> (int)operand2);
			return true;
		}
		else if (instruction == IRInstruction.ShiftRight32 || instruction == IRInstruction.ShiftRight64)
		{
			result = operand1 >> (int)operand2;
			return true;
		}
		else if (instruction == IRInstruction.ShiftLeft32 || instruction == IRInstruction.ShiftLeft64)
		{
			result = operand1 << (int)operand2;
			return true;
		}
		else if (instruction == IRInstruction.Compare32x32)
		{
			var compare = Compare32((uint)operand1, (uint)operand2, conditionCode);

			if (compare.HasValue)
			{
				result = compare.Value ? 1u : 0u;
				return true;
			}
		}
		else if (instruction == IRInstruction.Compare64x32
				 || instruction == IRInstruction.Compare64x64)
		{
			var compare = Compare64(operand1, operand2, conditionCode);

			if (compare.HasValue)
			{
				result = compare.Value ? 1u : 0u;
				return true;
			}
		}
		result = 0;
		return false;
	}

	private static bool SignOrZeroExtend(BaseInstruction instruction, ulong operand1, out ulong result)
	{
		if (instruction == IRInstruction.SignExtend8x32)
		{
			var value = (byte)operand1;
			result = (value & 0x80) == 0 ? value : value | 0xFFFFFF00;
			return true;
		}
		else if (instruction == IRInstruction.SignExtend16x32)
		{
			var value = (ushort)operand1;
			result = (value & 0x8000) == 0 ? value : value | 0xFFFF0000;
			return true;
		}
		else if (instruction == IRInstruction.SignExtend8x64)
		{
			var value = (byte)operand1;
			result = (value & 0x80) == 0 ? value : value | 0xFFFFFFFFFFFFFF00ul;
			return true;
		}
		else if (instruction == IRInstruction.SignExtend16x64)
		{
			var value = (ushort)operand1;
			result = (value & 0x8000) == 0 ? value : value | 0xFFFFFFFFFFFF0000ul;
			return true;
		}
		else if (instruction == IRInstruction.SignExtend32x64)
		{
			var value = (uint)operand1;
			result = (value & 0x80000000) == 0 ? value : value | 0xFFFFFFFF00000000ul;
			return true;
		}
		else if (instruction == IRInstruction.ZeroExtend8x32)
		{
			result = (byte)operand1;
			return true;
		}
		else if (instruction == IRInstruction.ZeroExtend16x32)
		{
			result = (byte)operand1;
			return true;
		}
		else if (instruction == IRInstruction.ZeroExtend8x64)
		{
			result = (byte)operand1;
			return true;
		}
		else if (instruction == IRInstruction.ZeroExtend16x64)
		{
			result = (ushort)operand1;
			return true;
		}
		else if (instruction == IRInstruction.ZeroExtend32x64)
		{
			result = (uint)operand1;
			return true;
		}
		result = 0;
		return false;
	}

	private void Load(InstructionNode node)
	{
		var result = GetVariableState(node.Result);
		UpdateToOverDefined(result);
		SetReferenceOverdefined(result);
	}

	private void AddressOf(InstructionNode node)
	{
		var result = GetVariableState(node.Result);
		var operand1 = GetVariableState(node.Operand1);

		UpdateToOverDefined(result);
		UpdateToOverDefined(operand1);
		SetReferenceOverdefined(result);
		SetReferenceOverdefined(operand1);
	}

	private void FinallyStart(InstructionNode node)
	{
		var result = GetVariableState(node.Result);

		UpdateToOverDefined(result);
		SetReferenceOverdefined(result);
	}

	private void Default(InstructionNode node)
	{
		if (node.ResultCount == 0)
			return;

		var result = GetVariableState(node.Result);

		UpdateToOverDefined(result);
		SetReferenceOverdefined(result);

		if (node.ResultCount == 2)
		{
			var result2 = GetVariableState(node.Result);

			UpdateToOverDefined(result2);
			SetReferenceOverdefined(result2);
		}
	}

	private bool CompareBranch(InstructionNode node)
	{
		var operand1 = GetVariableState(node.Operand1);
		var operand2 = GetVariableState(node.Operand2);

		var compareNull = NullComparisionCheck(node.ConditionCode, operand1, operand2);

		if (compareNull.HasValue)
		{
			if (compareNull.Value)
			{
				Branch(node);
			}

			return !compareNull.Value;
		}

		var is32Bit = node.Instruction == IRInstruction.Branch32 || (node.Instruction == IRInstruction.BranchObject && Is32BitPlatform);

		if (operand1.IsOverDefined || operand2.IsOverDefined)
		{
			Branch(node);
			return true;
		}
		else if (operand1.IsSingleConstant && operand2.IsSingleConstant)
		{
			var compare = is32Bit
				? Compare32((uint)operand1.ConstantUnsignedLongInteger, (uint)operand2.ConstantUnsignedLongInteger, node.ConditionCode)
				: Compare64(operand1.ConstantUnsignedLongInteger, operand2.ConstantUnsignedLongInteger, node.ConditionCode);

			if (!compare.HasValue)
			{
				// assume it always branches
				Branch(node);
				return true;
			}

			if (compare.Value)
			{
				Branch(node);
			}

			return !compare.Value;
		}
		else if (operand1.HasOnlyConstants && operand2.HasOnlyConstants)
		{
			bool? final = null;

			foreach (var c1 in operand1.Constants)
			{
				foreach (var c2 in operand2.Constants)
				{
					//bool? compare = Compare(c1, c2, node.ConditionCode);

					var compare = is32Bit
						? Compare32((uint)c1, (uint)c2, node.ConditionCode)
						: Compare64(c1, c2, node.ConditionCode);

					if (!compare.HasValue)
					{
						Branch(node);
						return true;
					}

					if (!final.HasValue)
					{
						final = compare;
						continue;
					}
					else if (final.Value == compare.Value)
					{
						continue;
					}
					else
					{
						Branch(node);
						return true;
					}
				}
			}

			if (final.Value)
			{
				Branch(node);
			}

			return !final.Value;
		}

		Branch(node);
		return true;
	}

	private static bool? Compare32(uint operand1, uint operand2, ConditionCode conditionCode)
	{
		return conditionCode switch
		{
			ConditionCode.Equal => operand1 == operand2,
			ConditionCode.NotEqual => operand1 != operand2,
			ConditionCode.GreaterOrEqual => (int)operand1 >= (int)operand2,
			ConditionCode.Greater => (int)operand1 > (int)operand2,
			ConditionCode.LessOrEqual => (int)operand1 <= (int)operand2,
			ConditionCode.Less => (int)operand1 < (int)operand2,
			ConditionCode.UnsignedGreaterOrEqual => operand1 >= operand2,
			ConditionCode.UnsignedGreater => operand1 > operand2,
			ConditionCode.UnsignedLessOrEqual => operand1 <= operand2,
			ConditionCode.UnsignedLess => operand1 < operand2,
			ConditionCode.Always => true,
			ConditionCode.Never => false,
			_ => null
		};
	}

	private static bool? Compare64(ulong operand1, ulong operand2, ConditionCode conditionCode)
	{
		return conditionCode switch
		{
			ConditionCode.Equal => operand1 == operand2,
			ConditionCode.NotEqual => operand1 != operand2,
			ConditionCode.GreaterOrEqual => (long)operand1 >= (long)operand2,
			ConditionCode.Greater => (long)operand1 > (long)operand2,
			ConditionCode.LessOrEqual => (long)operand1 <= (long)operand2,
			ConditionCode.Less => (long)operand1 < (long)operand2,
			ConditionCode.UnsignedGreaterOrEqual => operand1 >= operand2,
			ConditionCode.UnsignedGreater => operand1 > operand2,
			ConditionCode.UnsignedLessOrEqual => operand1 <= operand2,
			ConditionCode.UnsignedLess => operand1 < operand2,
			ConditionCode.Always => true,
			ConditionCode.Never => false,
			_ => null
		};
	}

	private void Branch(InstructionNode node)
	{
		//Debug.Assert(node.BranchTargets.Length == 1);

		foreach (var block in node.BranchTargets)
		{
			AddExecutionBlock(block);
		}
	}

	private void Switch(InstructionNode node)
	{
		// no optimization attempted
		Branch(node);
	}

	private void IfThenElse(InstructionNode node)
	{
		MainTrace?.Log(node.ToString());

		var result = GetVariableState(node.Result);
		var operand1 = GetVariableState(node.Operand2);
		var operand2 = GetVariableState(node.Operand3);

		if (result.IsOverDefined)
			return;

		if (operand1.IsOverDefined is true or true)
		{
			UpdateToOverDefined(result);
		}
		else if (operand1.HasOnlyConstants && operand2.HasOnlyConstants)
		{
			foreach (var c in operand1.Constants)
			{
				UpdateToConstant(result, c);

				if (result.IsOverDefined)
					return;
			}
			foreach (var c in operand2.Constants)
			{
				UpdateToConstant(result, c);

				if (result.IsOverDefined)
					return;
			}
		}
		else if (operand1.IsUnknown || operand2.IsUnknown)
		{
			Debug.Assert(result.IsUnknown);
		}
		else
		{
			UpdateToOverDefined(result);
		}
	}

	private void Phi(InstructionNode node)
	{
		MainTrace?.Log(node.ToString());

		var result = GetVariableState(node.Result);

		if (result.IsOverDefined)
			return;

		var sourceBlocks = node.PhiBlocks;
		var currentBlock = node.Block;

		MainTrace?.Log($"Loop: {currentBlock.PreviousBlocks.Count}");

		for (var index = 0; index < currentBlock.PreviousBlocks.Count; index++)
		{
			var predecessor = sourceBlocks[index];

			phiStatements.AddIfNew(predecessor, node);

			var executable = blockStates[predecessor.Sequence];

			MainTrace?.Log($"# {index}: {predecessor} {(executable ? "Yes" : "No")}");

			if (!executable)
				continue;

			if (result.IsOverDefined)
				continue;

			var op = node.GetOperand(index);

			var operand = GetVariableState(op);

			MainTrace?.Log($"# {index}: {operand}");

			CheckAndUpdateNullAssignment(result, operand);

			if (operand.IsOverDefined)
			{
				UpdateToOverDefined(result);
				continue;
			}
			else if (operand.IsSingleConstant)
			{
				UpdateToConstant(result, operand.ConstantUnsignedLongInteger);
				continue;
			}
			else if (operand.HasMultipleConstants)
			{
				foreach (var c in operand.Constants)
				{
					UpdateToConstant(result, c);

					if (result.IsOverDefined)
						break;
				}
			}
		}
	}
}
