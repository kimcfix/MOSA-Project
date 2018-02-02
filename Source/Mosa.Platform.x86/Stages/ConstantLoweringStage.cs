// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Stages
{
	/// <summary>
	/// ConstantLoweringStage
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.BaseTransformationStage" />
	public sealed class ConstantLoweringStage : BaseTransformationStage
	{
		protected override void PopulateVisitationDictionary()
		{
			AddVisitation(X86.Add32, Add32);
			AddVisitation(X86.Adc32, Adc32);
			AddVisitation(X86.And32, And32);
			AddVisitation(X86.Btr32, Btr32);
			AddVisitation(X86.Bts32, Bts32);
			AddVisitation(X86.Sub32, Sub32);
			AddVisitation(X86.Test32, Test32);
			AddVisitation(X86.Xor32, Xor32);
			AddVisitation(X86.Or32, Or32);
			AddVisitation(X86.Sbb32, Sbb32);
			AddVisitation(X86.Shr32, Shr32);
			AddVisitation(X86.Shl32, Shl32);
			AddVisitation(X86.Sar32, Sar32);
			AddVisitation(X86.Rcr32, Rcr32);
			AddVisitation(X86.Out8, Out8);
			AddVisitation(X86.Out16, Out16);
			AddVisitation(X86.Out32, Out32);
		}

		#region Visitation Methods

		public void Add32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.AddConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Adc32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.AdcConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void And32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.AndConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Btr32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.BtrConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Bts32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.BtsConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Sub32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.SubConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Test32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.TestConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Xor32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.XorConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Or32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.OrConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Sbb32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.SbbConst32, context.Result, context.Operand1, context.Operand2);
			}
		}

		public void Shr32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				if (context.Operand2.IsConstantOne)
				{
					context.SetInstruction(X86.ShrConstOne32, context.Result, context.Operand1, context.Operand2);
				}
				else
				{
					context.SetInstruction(X86.ShrConst32, context.Result, context.Operand1, context.Operand2);
				}
			}
		}

		public void Shl32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				if (context.Operand2.IsConstantOne)
				{
					context.SetInstruction(X86.ShlConstOne32, context.Result, context.Operand1, context.Operand2);
				}
				else
				{
					context.SetInstruction(X86.ShlConst32, context.Result, context.Operand1, context.Operand2);
				}
			}
		}

		public void Sar32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				if (context.Operand2.IsConstantOne)
				{
					context.SetInstruction(X86.SarConstOne32, context.Result, context.Operand1, context.Operand2);
				}
				else
				{
					context.SetInstruction(X86.SarConst32, context.Result, context.Operand1, context.Operand2);
				}
			}
		}

		public void Rcr32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				if (context.Operand2.IsConstantOne)
				{
					context.SetInstruction(X86.RcrConstOne32, context.Result, context.Operand1, context.Operand2);
				}
				else
				{
					context.SetInstruction(X86.RcrConst32, context.Result, context.Operand1, context.Operand2);
				}
			}
		}

		public void Out8(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.OutConst8, null, context.Operand1, context.Operand2);
			}
		}

		public void Out16(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.OutConst16, null, context.Operand1, context.Operand2);
			}
		}

		public void Out32(Context context)
		{
			if (context.Operand2.IsConstant)
			{
				context.SetInstruction(X86.OutConst32, null, context.Operand1, context.Operand2);
			}
		}

		#endregion Visitation Methods
	}
}
