// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Collections.Generic;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x64.Transforms.Optimizations.Manual;

/// <summary>
/// Manual Optimizations Transformations
/// </summary>
public static class ManualTransforms
{
	public static readonly List<BaseTransform> List = new List<BaseTransform>
	{
		new Special.Deadcode(),

		new Standard.Mov32ToXor32(),
		new Standard.Mov64ToXor64(),
		new Standard.Add32ToInc32(),
		new Standard.Sub32ToDec32(),
		new Standard.Lea32ToInc32(),
		new Standard.Lea32ToDec32(),
		new Standard.Cmp32ToZero(),
		new Standard.Test32ToZero(),
		new Standard.Cmp32ToTest32(),

		//Add64ToLea64
		//Add32ToLea32
		//Sub64ToLea64
		//Sub64ToLea64
	};

	public static readonly List<BaseTransform> PostList = new List<BaseTransform>
	{
		new Special.Mov32ConstantReuse(),
	};

	public static readonly List<BaseTransform> EarlyList = new List<BaseTransform>
	{
		new Stack.Add32(),
		new Stack.Add64(),

		//new Special.Mov32ReuseConstant(), /// this can wait
		//new Special.Mov32Propagate(),
	};
}
