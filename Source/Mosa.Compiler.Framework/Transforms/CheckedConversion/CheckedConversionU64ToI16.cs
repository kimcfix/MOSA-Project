// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace Mosa.Compiler.Framework.Transforms.CheckedConversion;

/// <summary>
/// CheckedConversionU64ToI16
/// </summary>
public sealed class CheckedConversionU64ToI16 : BaseCheckedConversionTransform
{
	public CheckedConversionU64ToI16() : base(IRInstruction.CheckedConversionU64ToI16, TransformType.Manual | TransformType.Transform)
	{
	}

	public override int Priority => -10;

	public override bool Match(Context context, TransformContext transform)
	{
		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		CallCheckOverflow(transform, context, "U8ToI2");
	}
}
