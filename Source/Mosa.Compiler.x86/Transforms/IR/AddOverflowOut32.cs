// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.IR;

/// <summary>
/// AddOverflowOut32
/// </summary>
[Transform("x86.IR")]
public sealed class AddOverflowOut32 : BaseIRTransform
{
	public AddOverflowOut32() : base(IRInstruction.AddOverflowOut32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;
		var result2 = context.Result2;
		var operand1 = context.Operand1;
		var operand2 = context.Operand2;

		var v1 = transform.VirtualRegisters.Allocate32();

		context.SetInstruction(X86.Add32, result, operand1, operand2);
		context.AppendInstruction(X86.Setcc, ConditionCode.Overflow, v1);
		context.AppendInstruction(X86.Movzx8To32, result2, v1);
	}
}
