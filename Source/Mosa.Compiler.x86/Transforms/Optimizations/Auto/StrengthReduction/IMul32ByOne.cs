// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.Optimizations.Auto.StrengthReduction;

/// <summary>
/// IMul32ByOne
/// </summary>
[Transform("x86.Optimizations.Auto.StrengthReduction")]
public sealed class IMul32ByOne : BaseTransform
{
	public IMul32ByOne() : base(X86.IMul32, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsResolvedConstant)
			return false;

		if (context.Operand2.ConstantUnsigned64 != 1)
			return false;

		if (AreStatusFlagUsed(context))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var c1 = Operand.CreateConstant(1);

		context.SetInstruction(X86.Mov32, result, c1);
	}
}
