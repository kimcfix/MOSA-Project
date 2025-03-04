﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.Manual.Standard;

[Transform("x86.Optimizations.Manual.Standard")]
public sealed class Mov32ToXor32 : BaseTransform
{
	public Mov32ToXor32() : base(X86.Mov32, TransformType.Manual | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsConstantZero)
			return false;

		if (AreStatusFlagUsed(context))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		context.SetInstruction(X86.Xor32, context.Result, context.Result, context.Result);
	}
}
