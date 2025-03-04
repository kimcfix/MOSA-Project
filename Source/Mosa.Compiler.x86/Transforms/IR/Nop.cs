// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.IR;

/// <summary>
/// Nop
/// </summary>
[Transform("x86.IR")]
public sealed class Nop : BaseIRTransform
{
	public Nop() : base(IRInstruction.Nop, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.Empty();

		//context.SetInstruction(X86.Nop);
	}
}
