﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Intrinsic;

/// <summary>
/// IntrinsicMethods
/// </summary>
internal static partial class IntrinsicMethods
{
	[IntrinsicMethod("Mosa.Runtime.Intrinsic::SuppressStackFrame")]
	private static void SurpressStackFrame(Context context, TransformContext transformContext)
	{
		transformContext.MethodCompiler.IsStackFrameRequired = false;
		context.Empty();
	}
}
