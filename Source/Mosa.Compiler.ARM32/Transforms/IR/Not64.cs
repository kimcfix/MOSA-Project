// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.ARM32.Transforms.IR;

/// <summary>
/// Not64
/// </summary>
[Transform("ARM32.IR")]
public sealed class Not64 : BaseIRTransform
{
	public Not64() : base(IRInstruction.Not64, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		transform.SplitOperand(context.Result, out var resultLow, out var resultHigh);
		transform.SplitOperand(context.Operand1, out var op1L, out var op1H);

		op1L = MoveConstantToRegisterOrImmediate(transform, context, op1L);
		op1H = MoveConstantToRegisterOrImmediate(transform, context, op1H);

		context.SetInstruction(ARM32.Mvn, resultLow, op1L);
		context.AppendInstruction(ARM32.Mvn, resultHigh, op1H);
	}
}
