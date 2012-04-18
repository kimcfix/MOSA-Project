﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Compiler.Framework;
using Mosa.Compiler.Framework.Operands;
using Mosa.Compiler.Framework.Platform;

namespace Mosa.Platform.x86.Instructions.Registers
{
	/// <summary>
	/// 
	/// </summary>
	public class DivUsage : IInstructionRegisterUsage
	{

		/// <summary>
		/// Static Instance 
		/// </summary>
		public static DivUsage Instance = new DivUsage();

		#region Data Members

		/// <summary>
		/// Defines the register set of the target architecture.
		/// </summary>
		protected static readonly Register[] GeneralPurpose32BitRegisters = new Register[]
		{
			////////////////////////////////////////////////////////
			// 32-bit general purpose registers
			////////////////////////////////////////////////////////
			GeneralPurposeRegister.EAX,
			GeneralPurposeRegister.ECX,
			GeneralPurposeRegister.EDX,
			GeneralPurposeRegister.EBX,
			GeneralPurposeRegister.ESI,
			GeneralPurposeRegister.EDI,
		};

		protected static readonly Register[] MMXloatingPoint128BitRegisters = new Register[]
		{
			////////////////////////////////////////////////////////
			// MMX floating point registers
			////////////////////////////////////////////////////////			
			MMXRegister.MM0,
			MMXRegister.MM1,
			MMXRegister.MM2,
			MMXRegister.MM3,
			MMXRegister.MM4,
			MMXRegister.MM5,
			MMXRegister.MM6,
			MMXRegister.MM7,
		};

		protected static readonly Register[] SSEFloatingPoint128BitRegisters = new Register[]
		{
			////////////////////////////////////////////////////////
			// SSE 128-bit floating point registers
			////////////////////////////////////////////////////////
			SSE2Register.XMM0,
			SSE2Register.XMM1,
			SSE2Register.XMM2,
			SSE2Register.XMM3,
			SSE2Register.XMM4,
			SSE2Register.XMM5,
			SSE2Register.XMM6,
			SSE2Register.XMM7
		};

		protected static readonly Register[] SegmentationRegisters = new Register[]
		{
			////////////////////////////////////////////////////////
			// Segmentation Registers
			////////////////////////////////////////////////////////
			SegmentRegister.CS,
			SegmentRegister.DS,
			SegmentRegister.ES,
			SegmentRegister.FS,
			SegmentRegister.GS,
			SegmentRegister.SS
		};

		#endregion //  Data Members

		/// <summary>
		/// Defines the EDX register.
		/// </summary>
		private static readonly Register[] EDXRegister = new Register[]
		{
			GeneralPurposeRegister.EDX,
		};

		/// <summary>
		/// Defines the EAX register.
		/// </summary>
		private static readonly Register[] EAXRegister = new Register[]
		{
			GeneralPurposeRegister.EAX,
		};

		/// <summary>
		/// Defines the DIV registers.
		/// </summary>
		private static readonly Register[] DIVRegisters = new Register[]
		{
			GeneralPurposeRegister.EDX,
			GeneralPurposeRegister.EAX,
		};

		#region IOpcodeRegisterUsage

		/// <summary>
		/// Gets the available result registers.
		/// </summary>
		/// <returns>Returns list of available (primary) registers for result </returns>
		public Register[] GetAvailableResultRegisters()
		{
			return EAXRegister;
		}

		/// <summary>
		/// Gets the available result registers.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>Returns list of available (primary) registers for result, given opcode address mode</returns>
		public Register[] GetAvailableResultRegisters(OperandAddressMode addressMode)
		{
			return EAXRegister;
		}

		/// <summary>
		/// Gets the available operand1 registers.
		/// </summary>
		/// <returns>
		/// Returns list of available (primary) registers for operand 1
		/// </returns>
		public Register[] GetAvailableOperand1Registers()
		{
			return EAXRegister;
		}

		/// <summary>
		/// Gets the available operand1 registers.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		/// Returns list of available (primary) registers for operand 1, given opcode address mode
		/// </returns>
		public Register[] GetAvailableOperand1Registers(OperandAddressMode addressMode)
		{
			return EAXRegister;
		}

		/// <summary>
		/// Gets the modified registers.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <returns>
		/// Returns modified register(s), given the result register. It may *not* return the result register.
		/// </returns>
		public Register[] GetModifiedRegisters(Register register)
		{
			return DIVRegisters;
		}

		/// <summary>
		/// Determines whether the specified results and operands are valid.
		/// </summary>
		/// <param name="result">The results.</param>
		/// <param name="resultType">Type of the result.</param>
		/// <param name="operand1">The operand1.</param>
		/// <param name="operand1AddressMode">The operand1 address mode.</param>
		/// <param name="operand2">The operand2.</param>
		/// <param name="operand2AddressMode">The operand2 address mode.</param>
		/// <returns>
		///   <c>true</c> if the specified results1 is valid; otherwise, <c>false</c>.
		/// </returns>
		public bool IsValid(Register result, OperandAddressMode resultAddressMode, Register operand1, OperandAddressMode operand1AddressMode, Register operand2, OperandAddressMode operand2AddressMode)
		{
			return !GeneralPurposeUsage.IsMemoryToMemory(resultAddressMode, operand1AddressMode);
		}

		/// <summary>
		/// Gets the unspecified source registers. An operand is unspecified when it is not included in the context 
		/// operand list (for whatever reason). This method assists with opcode/register analysis that need to know
		/// this detail.
		/// </summary>
		/// <param name="register">The register.</param>
		/// <returns>
		/// Returns unspecified operand register(s), given result register.
		/// </returns>
		public Register[] GetUnspecifiedSourceRegisters(Register register)
		{
			return DIVRegisters;
		}

		#endregion
	}
}
