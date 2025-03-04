﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Compiler.x86.Intrinsic::Invlpg")]
	private static void Invlpg(Context context, TransformContext transformContext)
	{
		//Debug.Assert(context.Operand1.IsConstant);
		context.SetInstruction(X86.Invlpg, null, context.Operand1);
	}
}
