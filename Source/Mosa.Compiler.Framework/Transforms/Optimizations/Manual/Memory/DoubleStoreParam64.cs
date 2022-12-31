﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Manual.Memory
{
	public sealed class DoubleStoreParam64 : BaseTransform
	{
		public DoubleStoreParam64() : base(IRInstruction.StoreParam64, TransformType.Manual | TransformType.Optimization)
		{
		}

		public override bool Match(Context context, TransformContext transform)
		{
			var next = GetNextNodeUntil(context, IRInstruction.StoreParam64, transform.Window);

			if (next == null)
				return false;

			if (next.Operand1 != context.Operand1)
				return false;

			return true;
		}

		public override void Transform(Context context, TransformContext transform)
		{
			context.SetNop();
		}
	}
}
