// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Collections.Generic;
using Mosa.Compiler.Framework;

namespace Mosa.Compiler.x86.Transforms.IR;

/// <summary>
/// IR Transformation List
/// </summary>
public static class IRTransforms
{
	public static readonly List<BaseTransform> List = new List<BaseTransform>
	{
		new Add32(),
		new AddCarryIn32(),
		new AddCarryOut32(),
		new AddManagedPointer(),
		new AddOverflowOut32(),
		new AddR4(),
		new AddR8(),
		new AddressOf(),
		new ArithShiftRight32(),
		new BitCopyR4To32(),
		new BitCopy32ToR4(),
		new CallDirect(),
		new Branch32(),
		new BranchObject(),
		new BranchManagedPointer(),
		new CompareObject(),
		new CompareManagedPointer(),
		new CompareR4(),
		new CompareR8(),
		new Compare32x32(),
		new ConvertR4ToR8(),
		new ConvertR4ToI32(),
		new ConvertR4ToU32(),
		new ConvertR8ToR4(),
		new ConvertR8ToI32(),
		new ConvertR8ToU32(),
		new ConvertI32ToR4(),
		new ConvertI32ToR8(),
		new ConvertU32ToR4(),
		new ConvertU32ToR8(),
		new DivR4(),
		new DivR8(),
		new DivSigned32(),
		new DivUnsigned32(),
		new IfThenElse32(),
		new Jmp(),
		new LoadR4(),
		new LoadR8(),
		new LoadObject(),
		new LoadManagedPointer(),
		new Load32(),
		new LoadParamObject(),
		new LoadParamManagedPointer(),
		new LoadParam32(),
		new LoadParamR4(),
		new LoadParamR8(),
		new LoadParamSignExtend16x32(),
		new LoadParamSignExtend8x32(),
		new LoadParamZeroExtend16x32(),
		new LoadParamZeroExtend8x32(),
		new LoadSignExtend16x32(),
		new LoadSignExtend8x32(),
		new LoadZeroExtend16x32(),
		new LoadZeroExtend8x32(),
		new And32(),
		new Not32(),
		new Or32(),
		new Xor32(),
		new MoveR4(),
		new MoveR8(),
		new Move32(),
		new MoveObject(),
		new MoveManagedPointer(),
		new MulCarryOut32(),
		new MulOverflowOut32(),
		new MulR4(),
		new MulR8(),
		new MulSigned32(),
		new MulUnsigned32(),
		new Nop(),
		new RemSigned32(),
		new RemUnsigned32(),
		new ShiftLeft32(),
		new ShiftRight32(),
		new SignExtend16x32(),
		new SignExtend8x32(),
		new StoreR4(),
		new StoreR8(),
		new Store16(),
		new Store32(),
		new StoreObject(),
		new StoreManagedPointer(),
		new Store8(),
		new StoreParamR4(),
		new StoreParamR8(),
		new StoreParam16(),
		new StoreParam32(),
		new StoreParam8(),
		new StoreParamObject(),
		new StoreParamManagedPointer(),
		new Sub32(),
		new SubManagedPointer(),
		new SubCarryIn32(),
		new SubCarryOut32(),
		new SubOverflowOut32(),
		new SubR4(),
		new SubR8(),
		new ZeroExtend16x32(),
		new ZeroExtend8x32(),
		new Add64(),
		new AddCarryOut64(),
		new AddOverflowOut64(),
		new BitCopyR8To64(),
		new BitCopy64ToR8(),
		new ArithShiftRight64(),
		new ArithShiftRight64Less32(),
		new ArithShiftRight64Less64(),
		new Call(),
		new Compare32x64(),
		new Compare64x32(),
		new Compare64x64(),
		new Branch64(),
		new ConvertI64ToR4(),
		new ConvertI64ToR8(),
		new IfThenElse64(),
		new Load64(),
		new LoadParam64(),
		new LoadParamSignExtend16x64(),
		new LoadParamSignExtend32x64(),
		new LoadParamSignExtend8x64(),
		new LoadParamZeroExtend16x64(),
		new LoadParamZeroExtend32x64(),
		new LoadParamZeroExtend8x64(),
		new And64(),
		new Not64(),
		new Or64(),
		new Xor64(),
		new Move64(),
		new MulSigned64(),
		new MulUnsigned64(),
		new ShiftLeft64(),
		new ShiftRight64(),
		new SignExtend16x64(),
		new SignExtend32x64(),
		new SignExtend8x64(),
		new GetHigh32(),
		new GetLow32(),
		new Store64(),
		new StoreParam64(),
		new Sub64(),
		new SubCarryOut64(),
		new SubOverflowOut64(),
		new To64(),
		new Truncate64x32(),
		new ZeroExtend16x64(),
		new ZeroExtend32x64(),
		new ZeroExtend8x64(),
	};
}
