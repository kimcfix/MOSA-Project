// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.Tweak;

/// <summary>
/// Lzcnt32
/// </summary>
[Transform("x86.Tweak")]
public sealed class Lzcnt32 : BaseTransform
{
	public Lzcnt32() : base(X86.Lzcnt32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsConstant)
			return false;

		if (context.Operand1.IsCPURegister)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		transform.MoveOperand1ToVirtualRegister(context, X86.Mov32);
	}
}
