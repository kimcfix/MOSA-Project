// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Instructions;

/// <summary>
/// MovCRLoad32
/// </summary>
/// <seealso cref="Mosa.Compiler.x86.X86Instruction" />
public sealed class MovCRLoad32 : X86Instruction
{
	internal MovCRLoad32()
		: base(1, 1)
	{
	}

	public override bool IsMemoryRead => true;

	public override void Emit(InstructionNode node, OpcodeEncoder opcodeEncoder)
	{
		System.Diagnostics.Debug.Assert(node.ResultCount == 1);
		System.Diagnostics.Debug.Assert(node.OperandCount == 1);

		opcodeEncoder.Append8Bits(0x0F);
		opcodeEncoder.Append8Bits(0x20);
		opcodeEncoder.Append2Bits(0b11);
		opcodeEncoder.Append3Bits(node.Operand1.Register.RegisterCode);
		opcodeEncoder.Append3Bits(node.Result.Register.RegisterCode);
	}
}
