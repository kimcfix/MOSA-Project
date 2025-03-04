﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace Mosa.Compiler.Framework.Transforms.LowerTo32;

public sealed class ArithShiftRight64By32 : BaseLower32Transform
{
	public ArithShiftRight64By32() : base(IRInstruction.ArithShiftRight64, TransformType.Manual | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		return transform.IsLowerTo32 && context.Operand2.IsResolvedConstant && context.Operand2.ConstantUnsigned32 == 32;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;
		var operand1 = context.Operand1;

		var v1 = transform.VirtualRegisters.Allocate32();
		var v2 = transform.VirtualRegisters.Allocate32();

		context.SetInstruction(IRInstruction.GetHigh32, v1, operand1);
		context.AppendInstruction(IRInstruction.ArithShiftRight32, v2, v1, Operand.Constant32_31);
		context.AppendInstruction(IRInstruction.To64, result, v1, v2);
	}
}
