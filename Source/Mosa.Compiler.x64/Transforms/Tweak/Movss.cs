// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Diagnostics;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.Tweak;

/// <summary>
/// Movss
/// </summary>
[Transform("x64.Tweak")]
public sealed class Movss : BaseTransform
{
	public Movss() : base(X64.Movss, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		return context.Operand1.IsCPURegister && context.Result.Register == context.Operand1.Register;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		Debug.Assert(context.Result.IsCPURegister);
		Debug.Assert(context.Operand1.IsCPURegister);

		context.Empty();
	}
}
