// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Diagnostics;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.IR;

/// <summary>
/// ConvertI32ToR4
/// </summary>
[Transform("x64.IR")]
public sealed class ConvertI32ToR4 : BaseIRTransform
{
	public ConvertI32ToR4() : base(IRInstruction.ConvertI32ToR4, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		Debug.Assert(context.Result.IsR4);

		context.ReplaceInstruction(X64.Cvtsi2ss32);
	}
}
