// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// Truncate64x32
/// </summary>
[Transform("x64.IR")]
public sealed class Truncate64x32 : BaseIRTransform
{
	public Truncate64x32() : base(IRInstruction.Truncate64x32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.ReplaceInstruction(X64.Movzx32To64);
	}
}
