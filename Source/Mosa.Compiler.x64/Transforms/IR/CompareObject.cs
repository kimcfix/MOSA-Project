// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// CompareObject
/// </summary>
[Transform("x64.IR")]
public sealed class CompareObject : BaseIRTransform
{
	public CompareObject() : base(IRInstruction.CompareObject, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var condition = context.ConditionCode;
		var result = context.Result;
		var operand1 = context.Operand1;
		var operand2 = context.Operand2;

		var v1 = transform.VirtualRegisters.Allocate32();

		context.SetInstruction(X64.Cmp64, null, operand1, operand2);
		context.AppendInstruction(X64.Setcc, condition, v1);
		context.AppendInstruction(X64.Movzx8To64, result, v1);
	}
}
