// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Simplification
{
	/// <summary>
	/// Or32SignExtend16x32SignExtend16x32
	/// </summary>
	public sealed class Or32SignExtend16x32SignExtend16x32 : BaseTransform
	{
		public Or32SignExtend16x32SignExtend16x32() : base(IRInstruction.Or32, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (!context.Operand2.IsVirtualRegister)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.SignExtend16x32)
				return false;

			if (context.Operand2.Definitions.Count != 1)
				return false;

			if (context.Operand2.Definitions[0].Instruction != IRInstruction.SignExtend16x32)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1.Definitions[0].Operand1;
			var t2 = context.Operand2.Definitions[0].Operand1;

			var v1 = transform.AllocateVirtualRegister(transform.I4);

			context.SetInstruction(IRInstruction.Or32, v1, t1, t2);
			context.AppendInstruction(IRInstruction.SignExtend16x32, result, v1);
		}
	}

	/// <summary>
	/// Or32SignExtend16x32SignExtend16x32_v1
	/// </summary>
	public sealed class Or32SignExtend16x32SignExtend16x32_v1 : BaseTransform
	{
		public Or32SignExtend16x32SignExtend16x32_v1() : base(IRInstruction.Or32, TransformType.Auto | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			if (!context.Operand1.IsVirtualRegister)
				return false;

			if (!context.Operand2.IsVirtualRegister)
				return false;

			if (context.Operand1.Definitions.Count != 1)
				return false;

			if (context.Operand1.Definitions[0].Instruction != IRInstruction.SignExtend16x32)
				return false;

			if (context.Operand2.Definitions.Count != 1)
				return false;

			if (context.Operand2.Definitions[0].Instruction != IRInstruction.SignExtend16x32)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			var result = context.Result;

			var t1 = context.Operand1.Definitions[0].Operand1;
			var t2 = context.Operand2.Definitions[0].Operand1;

			var v1 = transform.AllocateVirtualRegister(transform.I4);

			context.SetInstruction(IRInstruction.Or32, v1, t2, t1);
			context.AppendInstruction(IRInstruction.SignExtend16x32, result, v1);
		}
	}
}
