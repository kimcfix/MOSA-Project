﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms
{
	public abstract class BaseX64Transform : BaseTransform
	{
		public BaseX64Transform(BaseInstruction instruction, TransformType type, bool log = false)
			: base(instruction, type, log)
		{ }

		#region Helpers

		public static Operand MoveConstantToFloatRegister(TransformContext transform, Context context, Operand operand)
		{
			if (!operand.IsConstant)
				return operand;

			var label = transform.CreateFloatingPointLabel(operand);

			var v1 = operand.IsR4 ? transform.VirtualRegisters.AllocateR4() : transform.VirtualRegisters.AllocateR8();

			var instruction = operand.IsR4 ? X64.MovssLoad : X64.MovsdLoad;

			context.InsertBefore().SetInstruction(instruction, v1, label, Operand.Constant32_0);

			return v1;
		}

		#endregion Helpers
	}
}
