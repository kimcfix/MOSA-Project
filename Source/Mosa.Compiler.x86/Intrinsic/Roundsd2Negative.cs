﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x86.Intrinsic::Roundsd2Negative")]
	private static void Roundsd2Negative(Context context, TransformContext transformContext)
	{
		context.SetInstruction(X86.Roundsd, context.Result, context.Operand1, Operand.Constant32_1);
	}
}
