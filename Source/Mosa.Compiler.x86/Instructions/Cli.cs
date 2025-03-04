// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Instructions;

/// <summary>
/// Cli
/// </summary>
/// <seealso cref="Mosa.Compiler.x86.X86Instruction" />
public sealed class Cli : X86Instruction
{
	internal Cli()
		: base(0, 0)
	{
	}

	public override bool HasUnspecifiedSideEffect => true;

	public override void Emit(InstructionNode node, OpcodeEncoder opcodeEncoder)
	{
		System.Diagnostics.Debug.Assert(node.ResultCount == 0);
		System.Diagnostics.Debug.Assert(node.OperandCount == 0);

		opcodeEncoder.Append8Bits(0xFA);
	}
}
