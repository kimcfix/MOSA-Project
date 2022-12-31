// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification
{
	/// <summary>
	/// Compare64x32SwapToZero
	/// </summary>
	public sealed class Compare64x32SwapToZero : BaseTransform
	{
		public Compare64x32SwapToZero() : base(IRInstruction.Compare64x32, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (context.ConditionCode != ConditionCode.NotEqual)
				return false;

			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (!context.Operand2.IsResolvedConstant)
				return false;

			if (context.Operand2.ConstantUnsigned64 != 1)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.And32)
				return false;

			if (!context.Operand1.Definitions[0].Operand2.IsResolvedConstant)
				return false;

			if (context.Operand1.Definitions[0].Operand2.ConstantUnsigned64 != 1)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1.Definitions[0].Operand1;

			var v1 = transform.AllocateVirtualRegister(transform.I4);

			var c1 = transform.CreateConstant(0);
			var c2 = transform.CreateConstant(1);

			context.SetInstruction(IRInstruction.And32, v1, t1, c2);
			context.AppendInstruction(IRInstruction.Compare64x32, ConditionCode.Equal, result, v1, c1);
		}
	}

	/// <summary>
	/// Compare64x32SwapToZero_v1
	/// </summary>
	public sealed class Compare64x32SwapToZero_v1 : BaseTransform
	{
		public Compare64x32SwapToZero_v1() : base(IRInstruction.Compare64x32, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (context.ConditionCode != ConditionCode.NotEqual)
				return false;

			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (!context.Operand2.IsResolvedConstant)
				return false;

			if (context.Operand2.ConstantUnsigned64 != 1)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.And32)
				return false;

			if (!context.Operand1.Definitions[0].Operand1.IsResolvedConstant)
				return false;

			if (context.Operand1.Definitions[0].Operand1.ConstantUnsigned64 != 1)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1.Definitions[0].Operand2;

			var v1 = transform.AllocateVirtualRegister(transform.I4);

			var c1 = transform.CreateConstant(0);
			var c2 = transform.CreateConstant(1);

			context.SetInstruction(IRInstruction.And32, v1, t1, c2);
			context.AppendInstruction(IRInstruction.Compare64x32, ConditionCode.Equal, result, v1, c1);
		}
	}
}
