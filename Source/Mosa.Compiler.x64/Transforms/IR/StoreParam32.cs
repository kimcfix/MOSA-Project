// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// StoreParam32
/// </summary>
[Transform("x64.IR")]
public sealed class StoreParam32 : BaseIRTransform
{
	public StoreParam32() : base(IRInstruction.StoreParam32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.SetInstruction(X64.MovStore32, null, transform.StackFrame, context.Operand1, context.Operand2);
	}
}
