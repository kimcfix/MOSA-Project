// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.AddressMode;

/// <summary>
/// Rcr32
/// </summary>
[Transform("x86.AddressMode")]
public sealed class Rcr32 : BaseAddressModeTransform
{
	public Rcr32() : base(X86.Rcr32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		AddressModeConversion(context, X86.Mov32);
	}
}
