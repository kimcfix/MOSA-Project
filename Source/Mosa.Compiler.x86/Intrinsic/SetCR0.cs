﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x86.Intrinsic::SetCR0")]
	private static void SetCR0(Context context, TransformContext transformContext)
	{
		Operand operand1 = context.Operand1;

		Operand eax = Operand.CreateCPURegister32(CPURegister.EAX);
		Operand cr = Operand.CreateCPURegister32(CPURegister.CR0);

		context.SetInstruction(X86.Mov32, eax, operand1);
		context.AppendInstruction(X86.MovCRStore32, null, cr, eax);
	}
}
