﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x86.Intrinsic::In8")]
	private static void In8(Context context, TransformContext transformContext)
	{
		var v1 = transformContext.VirtualRegisters.Allocate32();

		var result = context.Result;

		context.SetInstruction(X86.In8, v1, context.Operand1);
		context.AppendInstruction(X86.Movzx8To32, result, v1);

		//context.SetInstruction(X86.In8, result, context.Operand1);
	}
}
