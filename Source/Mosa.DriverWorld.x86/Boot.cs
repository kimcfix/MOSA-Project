﻿/*
 * (c) 2012 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 */

using Mosa.DeviceSystem;
using Mosa.Kernel.x86;
using Mosa.Kernel.x86.Smbios;
using Mosa.Platform.Internal.x86;

namespace Mosa.DriverWorld.x86
{
	/// <summary>
	///
	/// </summary>
	public static class Boot
	{
		public static ConsoleSession console;

		public static ConsoleSession Console
		{
			get { return console; }
		}

		/// <summary>
		/// Main
		/// </summary>
		public static void Main()
		{
			Mosa.Kernel.x86.Kernel.Setup();

			console = ConsoleManager.Controller.Boot;

			Console.Clear();

			IDT.SetInterruptHandler(ProcessInterrupt);

			Console.Color = Colors.White;
			Console.BackgroundColor = Colors.Green;

			Console.Write(@"                   MOSA OS Version 1.4 - Compiler Version 1.4");
			FillLine();
			Console.Color = Colors.White;
			Console.BackgroundColor = Colors.Black;

			Console.WriteLine("> Initializing hardware abstraction layer...");
			Setup.Initialize();

			Console.WriteLine("> Adding hardware devices...");
			Setup.Start();

			Console.WriteLine("> System ready");
			Console.WriteLine();
			Console.Goto(24, 0);
			Console.Color = Colors.White;
			Console.BackgroundColor = Colors.Green;
			Console.Write("          Copyright (C) 2008-2015 [Managed Operating System Alliance]");
			FillLine();

			Process();
		}

		public static void Process()
		{
			int lastSecond = -1;

			Console.BackgroundColor = Colors.Black;
			Console.Goto(21, 0);
			Console.Color = Colors.White;
			Console.Write("> ");

			Mosa.DeviceDriver.ScanCodeMap.US KBDMAP = new DeviceDriver.ScanCodeMap.US();

			while (true)
			{
				byte scancode = Setup.Keyboard.GetScanCode();

				if (scancode != 0)
				{
					//	Debug.Trace("Main.Main Key Scan Code: " + scancode.ToString());

					KeyEvent keyevent = KBDMAP.ConvertScanCode(scancode);

					//	Debug.Trace("Main.Main Key Character: " + keyevent.Character.ToString());

					if (keyevent.KeyPress == KeyEvent.Press.Make)
					{
						if (keyevent.Character != 0)
						{
							Console.Write(keyevent.Character);
						}

						if (keyevent.KeyType == KeyType.F1)
							ConsoleManager.Controller.Active = ConsoleManager.Controller.Boot;
						else if (keyevent.KeyType == KeyType.F2)
							ConsoleManager.Controller.Active = ConsoleManager.Controller.Debug;
					}
					//	Debug.Trace("Main.Main Key Character: " + ((uint)keyevent.Character).ToString());
				}

				if (Setup.CMOS.Second != lastSecond)
				{
					//DebugClient.SendAlive();
					lastSecond = Setup.CMOS.Second;
					//Debug.Trace("Main.Main Ping Alive");
				}

				//DebugClient.Process();
				Native.Hlt();
			}
		}

		public static void FillLine()
		{
			for (uint c = 80 - Console.Column; c != 0; c--)
				Console.Write(' ');
		}

		public static void PrintDone()
		{
			InBrackets("Done", Colors.White, Colors.LightGreen);
			Console.WriteLine();
		}

		public static void BulletPoint()
		{
			Console.Color = Colors.Yellow;
			Console.Write("  * ");
			Console.Color = Colors.White;
		}

		public static void InBrackets(string message, byte outerColor, byte innerColor)
		{
			Console.Color = outerColor;
			Console.Write("[");
			Console.Color = innerColor;
			Console.Write(message);
			Console.Color = outerColor;
			Console.Write("]");
		}

		public static void ProcessInterrupt(uint interrupt, uint errorCode)
		{
			if (interrupt == 0x20)
			{
				// Timer Interrupt! Switch Tasks!
			}
			else if (interrupt >= 0x20 && interrupt < 0x30)
			{
				Mosa.DeviceSystem.HAL.ProcessInterrupt((byte)(interrupt - 0x20));

				//Debug.Trace("Returned from HAL.ProcessInterrupt");
			}
		}
	}
}
