﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x86.Intrinsic::CpuIdEBX")]
	private static void CpuIdEBX(Context context, TransformContext transformContext)
	{
		var result = context.Result;
		var operand1 = context.Operand1;
		var operand2 = context.Operand2;

		var eax = Operand.CreateCPURegister32(CPURegister.EAX);
		var ebx = Operand.CreateCPURegister32(CPURegister.EBX);
		var ecx = Operand.CreateCPURegister32(CPURegister.ECX);
		var edx = Operand.CreateCPURegister32(CPURegister.EDX);

		context.SetInstruction(X86.Mov32, eax, operand1);
		context.AppendInstruction(X86.Mov32, ecx, operand2); context.AppendInstruction(X86.Mov32, ecx, Operand.Constant32_0);
		context.AppendInstruction(X86.CpuId, eax, eax, ecx);
		context.AppendInstruction(IRInstruction.Gen, eax, ebx, ecx, edx);
		context.AppendInstruction(X86.Mov32, result, ebx);
	}
}
