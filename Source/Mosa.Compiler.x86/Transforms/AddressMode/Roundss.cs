// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.AddressMode;

/// <summary>
/// Roundss
/// </summary>
[Transform("x86.AddressMode")]
public sealed class Roundss : BaseAddressModeTransform
{
	public Roundss() : base(X86.Roundss, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		AddressModeConversion(context, X86.Movss);
	}
}
