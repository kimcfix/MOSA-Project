// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Diagnostics;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.Tweak;

/// <summary>
/// Setcc
/// </summary>
[Transform("x86.Tweak")]
public sealed class Setcc : BaseTransform
{
	public Setcc() : base(X86.Setcc, TransformType.Manual | TransformType.Transform)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Result.IsCPURegister)
			return false;

		return context.Result.Register == CPURegister.ESI || context.Result.Register == CPURegister.EDI;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		Debug.Assert(context.Result.IsCPURegister);
		Debug.Assert(context.Result.IsCPURegister);

		var result = context.Result;
		var instruction = context.Instruction;

		// SETcc can not use with ESI or EDI registers as source registers
		var condition = context.ConditionCode;

		var eax = Operand.CreateCPURegister32(CPURegister.EAX);

		context.SetInstruction2(X86.XChg32, eax, result, result, eax);
		context.AppendInstruction(instruction, condition, eax);
		context.AppendInstruction2(X86.XChg32, result, eax, eax, result);
	}
}
