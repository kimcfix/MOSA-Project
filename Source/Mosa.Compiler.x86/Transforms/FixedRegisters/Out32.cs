// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.FixedRegisters;

/// <summary>
/// Out32
/// </summary>
[Transform("x86.FixedRegisters")]
public sealed class Out32 : BaseTransform
{
	public Out32() : base(X86.Out32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (context.Operand1.IsCPURegister
			&& context.Operand2.IsCPURegister
			&& (context.Operand1.Register == CPURegister.EDX || context.Operand1.IsConstant)
			&& context.Operand2.Register == CPURegister.EAX)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var operand1 = context.Operand1;
		var operand2 = context.Operand2;

		var eax = Operand.CreateCPURegister32(CPURegister.EAX);
		var edx = Operand.CreateCPURegister32(CPURegister.EDX);

		context.SetInstruction(X86.Mov32, edx, operand1);
		context.AppendInstruction(X86.Mov32, eax, operand2);
		context.AppendInstruction(X86.Out32, null, edx, eax);
	}
}
