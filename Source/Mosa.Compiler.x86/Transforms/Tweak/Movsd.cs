// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.Tweak;

/// <summary>
/// Movsd
/// </summary>
[Transform("x86.Tweak")]
public sealed class Movsd : BaseTransform
{
	public Movsd() : base(X86.Movsd, TransformType.Manual | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Result.IsCPURegister)
			return false;

		if (!context.Operand1.IsCPURegister)
			return false;

		return context.Result.Register == context.Operand1.Register;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.Empty();
	}
}
