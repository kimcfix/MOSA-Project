// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.FixedRegisters;

/// <summary>
/// In32
/// </summary>
[Transform("x86.FixedRegisters")]
public sealed class In32 : BaseTransform
{
	public In32() : base(X86.In32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (context.Result.IsCPURegister
			&& context.Operand1.IsCPURegister
			&& context.Result.Register == CPURegister.EAX
			&& (context.Operand1.Register == CPURegister.EDX || context.Operand1.IsConstant))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;
		var operand1 = context.Operand1;

		var eax = Operand.CreateCPURegister32(CPURegister.EAX);
		var edx = Operand.CreateCPURegister32(CPURegister.EDX);

		context.SetInstruction(X86.Mov32, edx, operand1);
		context.AppendInstruction(X86.In32, eax, edx);
		context.AppendInstruction(X86.Mov32, result, eax);
	}
}
