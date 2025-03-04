// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Instructions;

/// <summary>
/// Shrd32
/// </summary>
/// <seealso cref="Mosa.Compiler.x64.X64Instruction" />
public sealed class Shrd32 : X64Instruction
{
	internal Shrd32()
		: base(1, 3)
	{
	}

	public override bool IsZeroFlagModified => true;

	public override bool IsCarryFlagModified => true;

	public override bool IsSignFlagModified => true;

	public override bool IsOverflowFlagModified => true;

	public override bool IsParityFlagModified => true;

	public override void Emit(InstructionNode node, OpcodeEncoder opcodeEncoder)
	{
		System.Diagnostics.Debug.Assert(node.ResultCount == 1);
		System.Diagnostics.Debug.Assert(node.OperandCount == 3);
		System.Diagnostics.Debug.Assert(node.Result.IsCPURegister);
		System.Diagnostics.Debug.Assert(node.Operand1.IsCPURegister);
		System.Diagnostics.Debug.Assert(node.Result.Register == node.Operand1.Register);

		if (node.Operand2.IsCPURegister)
		{
			opcodeEncoder.SuppressByte(0x40);
			opcodeEncoder.Append4Bits(0b0100);
			opcodeEncoder.Append1Bit(0b0);
			opcodeEncoder.Append1Bit(node.Operand2.Register.RegisterCode >> 3);
			opcodeEncoder.Append1Bit(0b0);
			opcodeEncoder.Append1Bit(node.Result.Register.RegisterCode >> 3);
			opcodeEncoder.Append8Bits(0x0F);
			opcodeEncoder.Append8Bits(0xAD);
			opcodeEncoder.Append2Bits(0b11);
			opcodeEncoder.Append3Bits(node.Operand2.Register.RegisterCode);
			opcodeEncoder.Append3Bits(node.Result.Register.RegisterCode);
			return;
		}

		if (node.Operand2.IsConstant)
		{
			opcodeEncoder.SuppressByte(0x40);
			opcodeEncoder.Append4Bits(0b0100);
			opcodeEncoder.Append1Bit(0b0);
			opcodeEncoder.Append1Bit(node.Result.Register.RegisterCode >> 3);
			opcodeEncoder.Append1Bit(0b0);
			opcodeEncoder.Append1Bit(0b0);
			opcodeEncoder.Append8Bits(0x0F);
			opcodeEncoder.Append8Bits(0xAC);
			opcodeEncoder.Append2Bits(0b11);
			opcodeEncoder.Append3Bits(0b100);
			opcodeEncoder.Append3Bits(node.Result.Register.RegisterCode);
			opcodeEncoder.Append8BitImmediate(node.Operand2);
			return;
		}

		throw new Compiler.Common.Exceptions.CompilerException("Invalid Opcode");
	}
}
