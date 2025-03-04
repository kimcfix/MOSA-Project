﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x64.Intrinsic::FrameCallRetU8")]
	private static void FrameCallRetU8(Context context, TransformContext transformContext)
	{
		var result = context.Result;
		var methodAddress = context.Operand1;

		var eax = Operand.CreateCPURegister64(CPURegister.RAX);
		var edx = Operand.CreateCPURegister64(CPURegister.RDX);

		transformContext.SplitOperand(result, out Operand op0L, out Operand op0H);

		context.SetInstruction(X64.Call, null, methodAddress);
		context.AppendInstruction(IRInstruction.Gen, eax);
		context.AppendInstruction(IRInstruction.Gen, edx);
		context.AppendInstruction(X64.Mov64, op0L, eax);
		context.AppendInstruction(X64.Mov64, op0H, edx);
	}
}
