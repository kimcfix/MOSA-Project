// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.ARM32.Transforms.IR;

/// <summary>
/// BranchManagedPointer
/// </summary>
[Transform("ARM32.IR")]
public sealed class BranchManagedPointer : BaseIRTransform
{
	public BranchManagedPointer() : base(IRInstruction.BranchManagedPointer, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		TransformContext.MoveConstantRight(context);

		var target = context.BranchTargets[0];
		var condition = context.ConditionCode;
		var operand1 = context.Operand1;
		var operand2 = context.Operand2;

		context.SetInstruction(ARM32.Cmp, null, operand1, operand2);
		context.AppendInstruction(ARM32.B, condition, target);
	}
}
