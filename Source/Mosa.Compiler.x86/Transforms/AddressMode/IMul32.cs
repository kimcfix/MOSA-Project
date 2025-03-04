// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.AddressMode;

/// <summary>
/// IMul32
/// </summary>
[Transform("x86.AddressMode")]
public sealed class IMul32 : BaseAddressModeTransform
{
	public IMul32() : base(X86.IMul32, TransformType.Manual | TransformType.Transform)
	{
	}

	public override void Transform(Context context, TransformContext transform)
	{
		AddressModeConversionCummulative(context, X86.Mov32);
	}
}
