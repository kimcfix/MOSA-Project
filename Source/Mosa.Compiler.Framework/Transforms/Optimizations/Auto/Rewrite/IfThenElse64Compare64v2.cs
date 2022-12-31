// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Rewrite
{
	/// <summary>
	/// IfThenElse64Compare64v2
	/// </summary>
	public sealed class IfThenElse64Compare64v2 : BaseTransform
	{
		public IfThenElse64Compare64v2() : base(IRInstruction.IfThenElse64, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.Compare64x64)
				return false;

			if (context.Operand1.Definitions[0].ConditionCode != ConditionCode.Equal)
				return false;

			if (!context.Operand1.Definitions[0].Operand1.IsResolvedConstant)
				return false;

			if (context.Operand1.Definitions[0].Operand1.ConstantUnsigned64 != 0)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1.Definitions[0].Operand2;
			var t2 = context.Operand2;
			var t3 = context.Operand3;

			context.SetInstruction(IRInstruction.IfThenElse64, result, t1, t3, t2);
		}
	}

	/// <summary>
	/// IfThenElse64Compare64v2_v1
	/// </summary>
	public sealed class IfThenElse64Compare64v2_v1 : BaseTransform
	{
		public IfThenElse64Compare64v2_v1() : base(IRInstruction.IfThenElse64, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.Compare64x64)
				return false;

			if (context.Operand1.Definitions[0].ConditionCode != ConditionCode.Equal)
				return false;

			if (!context.Operand1.Definitions[0].Operand2.IsResolvedConstant)
				return false;

			if (context.Operand1.Definitions[0].Operand2.ConstantUnsigned64 != 0)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1.Definitions[0].Operand1;
			var t2 = context.Operand2;
			var t3 = context.Operand3;

			context.SetInstruction(IRInstruction.IfThenElse64, result, t1, t3, t2);
		}
	}
}
