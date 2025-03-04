﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x64.Intrinsic::Get64")]
	private static void Get64(Context context, TransformContext transformContext)
	{
		context.SetInstruction(X64.MovLoad64, context.Result, context.Operand1, Operand.Constant32_0);
	}
}
