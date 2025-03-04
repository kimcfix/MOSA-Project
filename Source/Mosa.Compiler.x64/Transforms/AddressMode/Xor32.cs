// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.AddressMode;

/// <summary>
/// Xor32
/// </summary>
[Transform("x64.AddressMode")]
public sealed class Xor32 : BaseAddressModeTransform
{
	public Xor32() : base(X64.Xor32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		AddressModeConversionCummulative(context, X64.Mov32);
	}
}
