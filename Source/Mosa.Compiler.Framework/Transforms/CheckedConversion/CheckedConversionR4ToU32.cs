// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace Mosa.Compiler.Framework.Transforms.CheckedConversion;

/// <summary>
/// CheckedConversionR4ToU32
/// </summary>
public sealed class CheckedConversionR4ToU32 : BaseCheckedConversionTransform
{
	public CheckedConversionR4ToU32() : base(IRInstruction.CheckedConversionR4ToU32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override int Priority => -10;

	public override bool Match(Context context, TransformContext transform)
	{
		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		CallCheckOverflow(transform, context, "R4ToU4");
	}
}
