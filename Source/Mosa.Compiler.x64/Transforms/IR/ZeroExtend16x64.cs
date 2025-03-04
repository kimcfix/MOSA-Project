// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// ZeroExtend16x64
/// </summary>
[Transform("x64.IR")]
public sealed class ZeroExtend16x64 : BaseIRTransform
{
	public ZeroExtend16x64() : base(IRInstruction.ZeroExtend16x64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.ReplaceInstruction(X64.Movzx16To64);
	}
}
