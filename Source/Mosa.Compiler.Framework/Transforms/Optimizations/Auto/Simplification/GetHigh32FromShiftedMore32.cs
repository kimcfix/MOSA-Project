// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification
{
	/// <summary>
	/// GetHigh32FromShiftedMore32
	/// </summary>
	public sealed class GetHigh32FromShiftedMore32 : BaseTransform
	{
		public GetHigh32FromShiftedMore32() : base(IRInstruction.GetHigh32, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.ShiftRight64)
				return false;

			if (!IsGreaterOrEqual(And32(To32(context.Operand1.Definitions[0].Operand2), 63), 32))
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var c1 = transform.CreateConstant(0);

			context.SetInstruction(IRInstruction.Move32, result, c1);
		}
	}
}
