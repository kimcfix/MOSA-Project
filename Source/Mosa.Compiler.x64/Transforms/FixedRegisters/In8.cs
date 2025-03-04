// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.FixedRegisters;

/// <summary>
/// In8
/// </summary>
[Transform("x64.FixedRegisters")]
public sealed class In8 : BaseTransform
{
	public In8() : base(X64.In8, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		return !(context.Result.IsCPURegister
				 && context.Operand1.IsCPURegister
				 && context.Result.Register == CPURegister.RAX
				 && (context.Operand1.Register == CPURegister.RDX || context.Operand1.IsConstant));
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;
		var operand1 = context.Operand1;

		var rax = Operand.CreateCPURegister64(CPURegister.RAX);
		var rdx = Operand.CreateCPURegister(operand1, CPURegister.RDX);

		context.SetInstruction(X64.Mov64, rdx, operand1);
		context.AppendInstruction(X64.In8, rax, rdx);
		context.AppendInstruction(X64.Mov64, result, rax);
	}
}
