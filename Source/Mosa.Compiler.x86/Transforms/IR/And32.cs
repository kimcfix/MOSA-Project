// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.IR;

/// <summary>
/// And32
/// </summary>
[Transform("x86.IR")]
public sealed class And32 : BaseIRTransform
{
	public And32() : base(IRInstruction.And32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.ReplaceInstruction(X86.And32);
	}
}
