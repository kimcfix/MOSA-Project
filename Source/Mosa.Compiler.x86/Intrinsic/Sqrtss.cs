﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x86.Intrinsic::Sqrtss")]
	private static void Sqrtss(Context context, TransformContext transformContext)
	{
		context.SetInstruction(X86.Sqrtss, context.Result, context.Operand1);
	}
}
