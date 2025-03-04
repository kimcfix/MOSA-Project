// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.StrengthReduction;

/// <summary>
/// MulSigned32ByOne
/// </summary>
[Transform("IR.Optimizations.Auto.StrengthReduction")]
public sealed class MulSigned32ByOne : BaseTransform
{
	public MulSigned32ByOne() : base(IRInstruction.MulSigned32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsResolvedConstant)
			return false;

		if (context.Operand2.ConstantUnsigned64 != 1)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;

		context.SetInstruction(IRInstruction.Move32, result, t1);
	}
}

/// <summary>
/// MulSigned32ByOne_v1
/// </summary>
[Transform("IR.Optimizations.Auto.StrengthReduction")]
public sealed class MulSigned32ByOne_v1 : BaseTransform
{
	public MulSigned32ByOne_v1() : base(IRInstruction.MulSigned32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 80;

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsResolvedConstant)
			return false;

		if (context.Operand1.ConstantUnsigned64 != 1)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand2;

		context.SetInstruction(IRInstruction.Move32, result, t1);
	}
}
