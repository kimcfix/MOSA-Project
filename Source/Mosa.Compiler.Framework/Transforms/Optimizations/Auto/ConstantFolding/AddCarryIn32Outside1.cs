// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.ConstantFolding;

/// <summary>
/// AddCarryIn32Outside1
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantFolding")]
public sealed class AddCarryIn32Outside1 : BaseTransform
{
	public AddCarryIn32Outside1() : base(IRInstruction.AddCarryIn32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!IsResolvedConstant(context.Operand1))
			return false;

		if (!IsResolvedConstant(context.Operand3))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand2;
		var t3 = context.Operand3;

		var e1 = Operand.CreateConstant(Add32(To32(t1), BoolTo32(To32(t3))));

		context.SetInstruction(IRInstruction.Add32, result, t2, e1);
	}
}
