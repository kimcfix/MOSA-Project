// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.Tweak;

/// <summary>
/// Nop
/// </summary>
[Transform("x64.Tweak")]
public sealed class Nop : BaseTransform
{
	public Nop() : base(X64.Nop, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.Empty();
	}
}
