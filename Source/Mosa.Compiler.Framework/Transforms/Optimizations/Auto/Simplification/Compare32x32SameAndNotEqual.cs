// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification
{
	/// <summary>
	/// Compare32x32SameAndNotEqual
	/// </summary>
	public sealed class Compare32x32SameAndNotEqual : BaseTransform
	{
		public Compare32x32SameAndNotEqual() : base(IRInstruction.Compare32x32, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			var condition = context.ConditionCode;

			if (!(context.ConditionCode == ConditionCode.NotEqual || context.ConditionCode == ConditionCode.Greater || context.ConditionCode == ConditionCode.Less || context.ConditionCode == ConditionCode.UnsignedGreater || context.ConditionCode == ConditionCode.UnsignedLess))
				return false;

			if (!AreSame(context.Operand1, context.Operand2))
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var e1 = transform.CreateConstant(To32(0));

			context.SetInstruction(IRInstruction.Move32, result, e1);
		}
	}
}
