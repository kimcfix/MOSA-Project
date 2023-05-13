// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification;

/// <summary>
/// CompareManagedPointerSameAndNotEqual
/// </summary>
[Transform("IR.Optimizations.Auto.Simplification")]
public sealed class CompareManagedPointerSameAndNotEqual : BaseTransform
{
	public CompareManagedPointerSameAndNotEqual() : base(IRInstruction.CompareManagedPointer, TransformType.Auto | TransformType.Optimization)
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

		var e1 = Operand.CreateConstant(To32(0));

		context.SetInstruction(IRInstruction.Move32, result, e1);
	}
}
