// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Rewrite
{
	/// <summary>
	/// Compare32x64GreaterThanZero
	/// </summary>
	public sealed class Compare32x64GreaterThanZero : BaseTransform
	{
		public Compare32x64GreaterThanZero() : base(IRInstruction.Compare32x64, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (context.ConditionCode != ConditionCode.UnsignedGreater)
				return false;

			if (!IsZero(context.Operand2))
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1;
			var t2 = context.Operand2;

			context.SetInstruction(IRInstruction.Compare32x64, ConditionCode.NotEqual, result, t1, t2);
		}
	}

	/// <summary>
	/// Compare32x64GreaterThanZero_v1
	/// </summary>
	public sealed class Compare32x64GreaterThanZero_v1 : BaseTransform
	{
		public Compare32x64GreaterThanZero_v1() : base(IRInstruction.Compare32x64, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (context.ConditionCode != ConditionCode.UnsignedLess)
				return false;

			if (!IsZero(context.Operand1))
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1;
			var t2 = context.Operand2;

			context.SetInstruction(IRInstruction.Compare32x64, ConditionCode.NotEqual, result, t2, t1);
		}
	}
}
