// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.IR;

/// <summary>
/// SignExtend8x64
/// </summary>
[Transform("x86.IR")]
public sealed class SignExtend8x64 : BaseIRTransform
{
	public SignExtend8x64() : base(IRInstruction.SignExtend8x64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		transform.SplitOperand(context.Result, out var resultLow, out var resultHigh);

		var v1 = transform.VirtualRegisters.Allocate32();

		context.SetInstruction(X86.Movsx8To32, resultLow, context.Operand1);
		context.AppendInstruction(X86.Cdq32, resultHigh, resultLow);
	}
}
