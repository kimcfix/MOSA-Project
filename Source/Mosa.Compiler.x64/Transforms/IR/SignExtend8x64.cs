// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// SignExtend8x64
/// </summary>
[Transform("x64.IR")]
public sealed class SignExtend8x64 : BaseIRTransform
{
	public SignExtend8x64() : base(IRInstruction.SignExtend8x64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.ReplaceInstruction(X64.Movsx8To64);
	}
}
