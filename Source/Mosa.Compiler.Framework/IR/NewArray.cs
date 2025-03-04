// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.IR;

/// <summary>
/// NewArray
/// </summary>
/// <seealso cref="Mosa.Compiler.Framework.IR.BaseIRInstruction" />
public sealed class NewArray : BaseIRInstruction
{
	public NewArray()
		: base(2, 1)
	{
	}

	public override bool IsMemoryWrite => true;
}
