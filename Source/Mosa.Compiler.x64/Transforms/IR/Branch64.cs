// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// Branch64
/// </summary>
[Transform("x64.IR")]
public sealed class Branch64 : BaseIRTransform
{
	public Branch64() : base(IRInstruction.Branch64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		TransformContext.MoveConstantRight(context);

		var target = context.BranchTargets[0];
		var condition = context.ConditionCode;
		var operand1 = context.Operand1;
		var operand2 = context.Operand2;

		context.SetInstruction(X64.Cmp64, null, operand1, operand2);
		context.AppendInstruction(X64.Branch, condition, target);
	}
}
