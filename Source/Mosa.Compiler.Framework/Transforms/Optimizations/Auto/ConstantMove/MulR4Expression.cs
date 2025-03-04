// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

namespace Mosa.Compiler.Framework.Transforms.Optimizations.Auto.ConstantMove;

/// <summary>
/// MulR4Expression
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantMove")]
public sealed class MulR4Expression : BaseTransform
{
	public MulR4Expression() : base(IRInstruction.MulR4, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IRInstruction.MulR4)
			return false;

		if (IsResolvedConstant(context.Operand1))
			return false;

		if (!IsResolvedConstant(context.Operand2.Definitions[0].Operand2))
			return false;

		if (IsResolvedConstant(context.Operand2.Definitions[0].Operand1))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand2.Definitions[0].Operand1;
		var t3 = context.Operand2.Definitions[0].Operand2;

		var v1 = transform.VirtualRegisters.AllocateR4();

		context.SetInstruction(IRInstruction.MulR4, v1, t1, t2);
		context.AppendInstruction(IRInstruction.MulR4, result, v1, t3);
	}
}

/// <summary>
/// MulR4Expression_v1
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantMove")]
public sealed class MulR4Expression_v1 : BaseTransform
{
	public MulR4Expression_v1() : base(IRInstruction.MulR4, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.MulR4)
			return false;

		if (IsResolvedConstant(context.Operand2))
			return false;

		if (!IsResolvedConstant(context.Operand1.Definitions[0].Operand2))
			return false;

		if (IsResolvedConstant(context.Operand1.Definitions[0].Operand1))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;
		var t2 = context.Operand1.Definitions[0].Operand2;
		var t3 = context.Operand2;

		var v1 = transform.VirtualRegisters.AllocateR4();

		context.SetInstruction(IRInstruction.MulR4, v1, t3, t1);
		context.AppendInstruction(IRInstruction.MulR4, result, v1, t2);
	}
}

/// <summary>
/// MulR4Expression_v2
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantMove")]
public sealed class MulR4Expression_v2 : BaseTransform
{
	public MulR4Expression_v2() : base(IRInstruction.MulR4, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand2.IsVirtualRegister)
			return false;

		if (!context.Operand2.IsDefinedOnce)
			return false;

		if (context.Operand2.Definitions[0].Instruction != IRInstruction.MulR4)
			return false;

		if (IsResolvedConstant(context.Operand1))
			return false;

		if (!IsResolvedConstant(context.Operand2.Definitions[0].Operand1))
			return false;

		if (IsResolvedConstant(context.Operand2.Definitions[0].Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1;
		var t2 = context.Operand2.Definitions[0].Operand1;
		var t3 = context.Operand2.Definitions[0].Operand2;

		var v1 = transform.VirtualRegisters.AllocateR4();

		context.SetInstruction(IRInstruction.MulR4, v1, t1, t3);
		context.AppendInstruction(IRInstruction.MulR4, result, v1, t2);
	}
}

/// <summary>
/// MulR4Expression_v3
/// </summary>
[Transform("IR.Optimizations.Auto.ConstantMove")]
public sealed class MulR4Expression_v3 : BaseTransform
{
	public MulR4Expression_v3() : base(IRInstruction.MulR4, TransformType.Auto | TransformType.Optimization)
	{
	}

	public override bool Match(Context context, TransformContext transform)
	{
		if (!context.Operand1.IsVirtualRegister)
			return false;

		if (!context.Operand1.IsDefinedOnce)
			return false;

		if (context.Operand1.Definitions[0].Instruction != IRInstruction.MulR4)
			return false;

		if (IsResolvedConstant(context.Operand2))
			return false;

		if (!IsResolvedConstant(context.Operand1.Definitions[0].Operand1))
			return false;

		if (IsResolvedConstant(context.Operand1.Definitions[0].Operand2))
			return false;

		return true;
	}

	public override void Transform(Context context, TransformContext transform)
	{
		var result = context.Result;

		var t1 = context.Operand1.Definitions[0].Operand1;
		var t2 = context.Operand1.Definitions[0].Operand2;
		var t3 = context.Operand2;

		var v1 = transform.VirtualRegisters.AllocateR4();

		context.SetInstruction(IRInstruction.MulR4, v1, t3, t2);
		context.AppendInstruction(IRInstruction.MulR4, result, v1, t1);
	}
}
