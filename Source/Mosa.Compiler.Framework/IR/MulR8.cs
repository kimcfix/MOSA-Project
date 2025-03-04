// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.IR;

/// <summary>
/// MulR8
/// </summary>
/// <seealso cref="Mosa.Compiler.Framework.IR.BaseIRInstruction" />
public sealed class MulR8 : BaseIRInstruction
{
	public MulR8()
		: base(2, 1)
	{
	}

	public override bool IsCommutative => true;
}
