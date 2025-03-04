// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.ConstantFolding;

/// <summary>
/// IfThenElse64AlwaysFalse
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantFolding")]
public sealed class IfThenElse64AlwaysFalse : BaseTransform
{
	public IfThenElse64AlwaysFalse() : base(IRInstruction.IfThenElse64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override int Priority => 100;

	public override bool Match(Context context, TransformContext transform)
	{
		if (!IsResolvedConstant(context.Operand1))
			return false;

		if (!IsZero(context.Operand1))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand3;

		context.SetInstruction(IRInstruction.Move64, result, t1);
	}
}
