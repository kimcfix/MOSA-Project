// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.Optimizations.Manual.Standard;

[Transform("x86.Optimizations.Manual.Standard")]
public sealed class Sub32ToDec32 : BaseTransform
{
	public Sub32ToDec32() : base(X86.Sub32, TransformType.Manual | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsResolvedConstant)
			return false;

		if (context.Operand2.ConstantUnsigned64 != 1)
			return false;

		if (context.Operand1 != context.Result)
			return false;

		if (context.Operand1.Register == CPURegister.ESP)
			return false;

		if (!(AreStatusFlagsUsed(context.Node.Next, false, true, false, false, false) == TriState.No))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		context.SetInstruction(X86.Dec32, result, result);
	}
}
