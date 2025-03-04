// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification;

/// <summary>
/// Compare64x64SameAndNotEqual
/// </summary>
[Transform("IR.Optimizations.Auto.Simplification")]
public sealed class Compare64x64SameAndNotEqual : BaseTransform
{
	public Compare64x64SameAndNotEqual() : base(IRInstruction.Compare64x64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		var condition = context.ConditionCode;

		if (!(context.ConditionCode is ConditionCode.NotEqual or ConditionCode.Greater or ConditionCode.Less or ConditionCode.UnsignedGreater or ConditionCode.UnsignedLess))
			return false;

		if (!AreSame(context.Operand1, context.Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var e1 = Operand.CreateConstant(To64(0));

		context.SetInstruction(IRInstruction.Move64, result, e1);
	}
}
