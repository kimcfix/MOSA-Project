// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Instructions;

/// <summary>
/// Xorps
/// </summary>
/// <seealso cref="Mosa.Compiler.x86.X86Instruction" />
public sealed class Xorps : X86Instruction
{
	internal Xorps()
		: base(1, 2)
	{
	}

	public override void Emit(InstructionNode node, OpcodeEncoder opcodeEncoder)
	{
		System.Diagnostics.Debug.Assert(node.ResultCount == 1);
		System.Diagnostics.Debug.Assert(node.OperandCount == 2);
		System.Diagnostics.Debug.Assert(node.Result.IsCPURegister);
		System.Diagnostics.Debug.Assert(node.Operand1.IsCPURegister);
		System.Diagnostics.Debug.Assert(node.Result.Register == node.Operand1.Register);

		opcodeEncoder.Append8Bits(0x66);
		opcodeEncoder.Append8Bits(0x57);
		opcodeEncoder.Append2Bits(0b11);
		opcodeEncoder.Append3Bits(node.Operand1.Register.RegisterCode);
		opcodeEncoder.Append3Bits(node.Operand2.Register.RegisterCode);
	}
}
