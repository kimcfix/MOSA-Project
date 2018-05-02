// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// BranchNoCarry
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.X86Instruction" />
	public sealed class BranchNoCarry : X86Instruction
	{
		public override string AlternativeName { get { return "JNC"; } }

		public static readonly byte[] opcode = new byte[] { 0x0F, 0x83 };

		internal BranchNoCarry()
			: base(0, 0)
		{
		}

		public override FlowControl FlowControl { get { return FlowControl.ConditionalBranch; } }

		public override bool IsCarryFlagUsed { get { return true; } }

		public override BaseInstruction GetOpposite()
		{
			return X86.BranchCarry;
		}

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == 0);
			System.Diagnostics.Debug.Assert(node.OperandCount == 0);
			System.Diagnostics.Debug.Assert(node.BranchTargets.Count >= 1);
			System.Diagnostics.Debug.Assert(node.BranchTargets[0] != null);

			emitter.Write(opcode);
			(emitter as X86CodeEmitter).EmitRelativeBranchTarget(node.BranchTargets[0].Label);
		}
	}
}

