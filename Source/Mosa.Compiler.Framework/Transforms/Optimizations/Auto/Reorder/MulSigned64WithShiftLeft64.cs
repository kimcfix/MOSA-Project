// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.Reorder;

/// <summary>
/// MulSigned64WithShiftLeft64
/// </summary>
[Transform("IR.Optimizations.Auto.Reorder")]
public sealed class MulSigned64WithShiftLeft64 : BaseTransform
{
	public MulSigned64WithShiftLeft64() : base(IRInstruction.MulSigned64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IRInstruction.ShiftLeft64)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand2.Definitions[0].Operand1;
		var t3 = context.Operand2.Definitions[0].Operand2;

		var v1 = transform.VirtualRegisters.Allocate64();

		context.SetInstruction(IRInstruction.MulSigned64, v1, t1, t2);
		context.AppendInstruction(IRInstruction.ShiftLeft64, result, v1, t3);
	}
}

/// <summary>
/// MulSigned64WithShiftLeft64_v1
/// </summary>
[Transform("IR.Optimizations.Auto.Reorder")]
public sealed class MulSigned64WithShiftLeft64_v1 : BaseTransform
{
	public MulSigned64WithShiftLeft64_v1() : base(IRInstruction.MulSigned64, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.ShiftLeft64)
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;
		var t2 = context.Operand1.Definitions[0].Operand2;
		var t3 = context.Operand2;

		var v1 = transform.VirtualRegisters.Allocate64();

		context.SetInstruction(IRInstruction.MulSigned64, v1, t3, t1);
		context.AppendInstruction(IRInstruction.ShiftLeft64, result, v1, t2);
	}
}
