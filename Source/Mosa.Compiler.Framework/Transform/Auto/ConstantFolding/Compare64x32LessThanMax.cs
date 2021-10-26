// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transform.Auto.ConstantFolding
{
	/// <summary>
	/// Compare64x32LessThanMax
	/// </summary>
	public sealed class Compare64x32LessThanMax : BaseTransformation
	{
		public Compare64x32LessThanMax() : base(IRInstruction.Compare64x32)
		{
		}

		public override bool Match(Context context, TransformContext transformContext)
		{
			if (context.ConditionCode != ConditionCode.UnsignedGreaterOrEqual)
				return false;

			if (!context.Operand1.IsResolvedConstant)
				return false;

			if (context.Operand1.ConstantUnsigned64 != 0xFFFFFFFFFFFFFFFF)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transformContext)
		{
			var result = context.Result;

			var c1 = transformContext.CreateConstant(1);

			context.SetInstruction(IRInstruction.Move32, result, c1);
		}
	}
}
