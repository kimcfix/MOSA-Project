// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.ConstantFolding;

/// <summary>
/// SubCarryIn32NoCarry
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantFolding")]
public sealed class SubCarryIn32NoCarry : BaseTransform
{
	public SubCarryIn32NoCarry() : base(IRInstruction.SubCarryIn32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand3.IsResolvedConstant)
			return false;

		if (context.Operand3.ConstantUnsigned64 != 0)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand2;

		context.SetInstruction(IRInstruction.Sub32, result, t1, t2);
	}
}
