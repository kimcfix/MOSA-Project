// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.FixedRegisters;

/// <summary>
/// Cdq32
/// </summary>
[Transform("x64.FixedRegisters")]
public sealed class Cdq32 : BaseTransform
{
	public Cdq32() : base(X64.Cdq32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (context.Result.IsCPURegister
			&& context.Operand1.IsCPURegister
			&& context.Result.Register == CPURegister.RDX
			&& context.Operand1.Register == CPURegister.RAX)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var operand1 = context.Operand1;
		var result = context.Result;

		var rax = Operand.CreateCPURegister32(CPURegister.RAX);
		var rdx = Operand.CreateCPURegister32(CPURegister.RDX);

		context.SetInstruction(X64.Mov64, rax, operand1);
		context.AppendInstruction(X64.Cdq32, rdx, rax);
		context.AppendInstruction(X64.Mov64, result, rdx);
	}
}
