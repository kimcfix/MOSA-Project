// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.AddressMode;

/// <summary>
/// Mulss
/// </summary>
[Transform("x64.AddressMode")]
public sealed class Mulss : BaseAddressModeTransform
{
	public Mulss() : base(X64.Mulss, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		AddressModeConversionCummulative(context, X64.Movss);
	}
}
