// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Diagnostics;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// LoadManagedPointer
/// </summary>
[Transform("x64.IR")]
public sealed class LoadManagedPointer : BaseIRTransform
{
	public LoadManagedPointer() : base(IRInstruction.LoadManagedPointer, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		Debug.Assert(!context.Result.IsR4);
		Debug.Assert(!context.Result.IsR8);

		transform.OrderLoadStoreOperands(context);

		context.SetInstruction(X64.MovLoad32, context.Result, context.Operand1, context.Operand2);
	}
}
