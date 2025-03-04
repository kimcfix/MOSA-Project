// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification;

/// <summary>
/// MulSigned32ByNegative1
/// </summary>
[Transform("IR.Optimizations.Auto.Simplification")]
public sealed class MulSigned32ByNegative1 : BaseTransform
{
	public MulSigned32ByNegative1() : base(IRInstruction.MulSigned32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsResolvedConstant)
			return false;

		if (context.Operand2.ConstantUnsigned64 != 18446744073709551615)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;

		var e1 = Operand.CreateConstant(To32(0));

		context.SetInstruction(IRInstruction.Sub32, result, e1, t1);
	}
}

/// <summary>
/// MulSigned32ByNegative1_v1
/// </summary>
[Transform("IR.Optimizations.Auto.Simplification")]
public sealed class MulSigned32ByNegative1_v1 : BaseTransform
{
	public MulSigned32ByNegative1_v1() : base(IRInstruction.MulSigned32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsResolvedConstant)
			return false;

		if (context.Operand1.ConstantUnsigned64 != 18446744073709551615)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand2;

		var e1 = Operand.CreateConstant(To32(0));

		context.SetInstruction(IRInstruction.Sub32, result, e1, t1);
	}
}
