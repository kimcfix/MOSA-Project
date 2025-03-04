// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Diagnostics;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.IR;

/// <summary>
/// LoadParamR8
/// </summary>
[Transform("x86.IR")]
public sealed class LoadParamR8 : BaseIRTransform
{
	public LoadParamR8() : base(IRInstruction.LoadParamR8, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		Debug.Assert(context.Result.IsR8);

		context.SetInstruction(X86.MovsdLoad, context.Result, transform.StackFrame, context.Operand1);
	}
}
