// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.IR;

/// <summary>
/// ShiftLeft64
/// </summary>
[Transform("x86.IR")]
public sealed class ShiftLeft64 : BaseIRTransform
{
	public ShiftLeft64() : base(IRInstruction.ShiftLeft64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		transform.SplitOperand(context.Result, out var resultLow, out var resultHigh);
		transform.SplitOperand(context.Operand1, out var op1L, out var op1H);

		var count = context.Operand2;

		var v1 = transform.VirtualRegisters.Allocate32();
		var v2 = transform.VirtualRegisters.Allocate32();

		context.SetInstruction(X86.Shld32, resultHigh, op1H, op1L, count);
		context.AppendInstruction(X86.Shl32, v1, op1L, count);

		if (count.IsConstant)
		{
			// FUTURE: Optimization - Test32 and conditional moves are not necessary if the count is a resolved constant

			context.AppendInstruction(X86.Mov32, v2, count);
			context.AppendInstruction(X86.Test32, null, v2, Operand.Constant32_32);
			context.AppendInstruction(X86.CMov32, ConditionCode.NotEqual, resultHigh, resultHigh, v1);
			context.AppendInstruction(X86.Mov32, resultLow, Operand.Constant32_0);
			context.AppendInstruction(X86.CMov32, ConditionCode.Equal, resultLow, resultLow, v1);
		}
		else
		{
			context.AppendInstruction(X86.Test32, null, count, Operand.Constant32_32);
			context.AppendInstruction(X86.CMov32, ConditionCode.NotEqual, resultHigh, resultHigh, v1);
			context.AppendInstruction(X86.Mov32, resultLow, Operand.Constant32_0);
			context.AppendInstruction(X86.CMov32, ConditionCode.Equal, resultLow, resultLow, v1);
		}
	}
}
