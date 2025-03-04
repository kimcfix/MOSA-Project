// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.FixedRegisters;

/// <summary>
/// Rcr32
/// </summary>
[Transform("x86.FixedRegisters")]
public sealed class Rcr32 : BaseTransform
{
	public Rcr32() : base(X86.Rcr32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (context.Operand2.IsConstant)
			return false;

		if (context.Operand2.Register == CPURegister.ECX)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var operand1 = context.Operand1;
		var operand2 = context.Operand2;
		var result = context.Result;

		var ecx = Operand.CreateCPURegister32(CPURegister.ECX);

		context.SetInstruction(X86.Mov32, ecx, operand2);
		context.AppendInstruction(X86.Rcr32, result, operand1, ecx);
	}
}
