﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x64.Intrinsic::GetCR0")]
	private static void GetCR0(Context context, TransformContext transformContext)
	{
		context.SetInstruction(X64.MovCRLoad64, context.Result, Operand.CreateCPURegister64(CPURegister.CR0));
	}
}
