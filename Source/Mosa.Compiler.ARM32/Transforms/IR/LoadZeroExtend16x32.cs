// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Diagnostics;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.ARM32.Transforms.IR;

/// <summary>
/// LoadZeroExtend16x32
/// </summary>
[Transform("ARM32.IR")]
public sealed class LoadZeroExtend16x32 : BaseIRTransform
{
	public LoadZeroExtend16x32() : base(IRInstruction.LoadZeroExtend16x32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		Debug.Assert(!context.Result.IsR4);
		Debug.Assert(!context.Result.IsR8);

		transform.OrderLoadStoreOperands(context);

		TransformLoad(transform, context, ARM32.Ldr16, context.Result, transform.StackFrame, context.Operand1);
	}
}
