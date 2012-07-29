﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using System.Collections.Generic;
using Mosa.Compiler.Framework;
using Mosa.Compiler.Metadata.Signatures;
using Mosa.Compiler.TypeSystem;
using Mosa.Compiler.Framework.IR;

namespace Mosa.Platform.x86.Intrinsic
{
	/// <summary>
	///
	/// </summary>
	public class GetEBP : IIntrinsicPlatformMethod
	{

		#region Methods

		/// <summary>
		/// Replaces the intrinsic call site
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="typeSystem">The type system.</param>
		void IIntrinsicPlatformMethod.ReplaceIntrinsicCall(Context context, BaseMethodCompiler methodCompiler)
		{
			context.SetInstruction(IRInstruction.Move, context.Result, Operand.CreateCPURegister(BuiltInSigType.UInt32, GeneralPurposeRegister.EBP));
		}

		#endregion // Methods

	}
}
