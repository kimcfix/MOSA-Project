// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.AddressMode;

/// <summary>
/// Sar32
/// </summary>
[Transform("x64.AddressMode")]
public sealed class Sar32 : BaseAddressModeTransform
{
	public Sar32() : base(X64.Sar32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		AddressModeConversion(context, X64.Mov32);
	}
}
