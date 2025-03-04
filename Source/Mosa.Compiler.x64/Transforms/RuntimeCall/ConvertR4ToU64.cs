// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.RuntimeCall;

/// <summary>
/// ConvertR4ToU64
/// </summary>
[Transform("x64.RuntimeCall")]
public sealed class ConvertR4ToU64 : BaseTransform
{
	public ConvertR4ToU64() : base(IRInstruction.ConvertR4ToU64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override int Priority => -100;

	public override bool Match(Context context, TransformContext transform)
	{
		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		transform.ReplaceWithCall(context, "Mosa.Runtime.Math.Conversion", "R4ToU8");
	}
}
