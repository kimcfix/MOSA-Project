﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System.Collections.Generic;
using Mosa.Compiler.Common.Configuration;

namespace Mosa.Utility.Configuration;

public static class CommandLineArguments
{
	public static List<Argument> Map { get; } = GetMap();

	private static List<Argument> GetMap()
	{
		var map = new List<Argument>
		{
			new Argument { Name = null, Setting = "Compiler.SourceFiles", Value = null, IsList = true},

			new Argument { Name = "-threading", Setting = "Compiler.Multithreading", Value = "true"},
			new Argument { Name = "-threading-off", Setting = "Compiler.Multithreading", Value = "false"},
			new Argument { Name = "-threads", Setting = "Compiler.Multithreading.MaxThreads" },
			new Argument { Name = "-settings", Setting = "Settings", Value = null, IsList = true},

			new Argument { Name = "-o", Setting = "Compiler.OutputFile"},

			new Argument { Name = "-base", Setting = "Compiler.BaseAddress"},
			new Argument { Name = "-scanner", Setting = "Compiler.MethodScanner", Value = "true"},
			new Argument { Name = "-no-code", Setting = "Compiler.Binary", Value = "false"},
			new Argument { Name = "-path", Setting = "SearchPaths", IsList = true},

			new Argument { Name = "-inline", Setting = "Optimizations.Inline", Value = "true"},
			new Argument { Name = "-inline-off", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-ssa", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-ssa-off", Setting = "Optimizations.SSA", Value = "false"},
			new Argument { Name = "-sccp", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-sccp-off", Setting = "Optimizations.SCCP", Value = "false"},
			new Argument { Name = "-basic-optimizations", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-basic-optimizations-off", Setting = "Optimizations.Basic", Value = "false"},
			new Argument { Name = "-inline-explicit", Setting = "Optimizations.Inline.Explicit", Value = "true"},
			new Argument { Name = "-inline-explicit-off", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-long-expansion", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-long-expansion-off", Setting = "Optimizations.LongExpansion", Value = "false"},
			new Argument { Name = "-two-pass", Setting = "Optimizations.TwoPass", Value = "true"},
			new Argument { Name = "-two-pass-off", Setting = "Optimizations.TwoPass", Value = "true"},
			new Argument { Name = "-value-numbering", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-value-numbering-off", Setting = "Optimizations.ValueNumbering", Value = "false"},
			new Argument { Name = "-loop-invariant-code-motion", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "true"},
			new Argument { Name = "-loop-invariant-code-motion-off", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-platform-optimizations", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-platform-optimizations-off", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-bit-tracker", Setting = "Optimizations.BitTracker", Value = "true"},
			new Argument { Name = "-bit-tracker-off", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-bittracker", Setting = "Optimizations.BitTracker", Value = "true"},
			new Argument { Name = "-bittracker-off", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-devirtualization", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-devirtualization-off", Setting = "Optimizations.Devirtualization", Value = "false"},
			new Argument { Name = "-inline-level", Setting = "Optimizations.Inline.Maximum"},
			new Argument { Name = "-basic-optimization-window", Setting = "Optimizations.Basic.Window"},

			new Argument { Name = "-output-nasm", Setting = "CompilerDebug.NasmFile", Value = "%DEFAULT%"},
			new Argument { Name = "-output-asm", Setting = "CompilerDebug.AsmFile", Value = "%DEFAULT%"},
			new Argument { Name = "-output-map", Setting = "CompilerDebug.MapFile", Value = "%DEFAULT%"},
			new Argument { Name = "-output-time", Setting = "CompilerDebug.CompilerTimeFile", Value = "%DEFAULT%"},
			new Argument { Name = "-output-debug", Setting = "CompilerDebug.DebugFile", Value = "%DEFAULT%"},
			new Argument { Name = "-output-inlined", Setting = "CompilerDebug.InlinedFile", Value = "%DEFAULT%"},
			new Argument { Name = "-output-hash", Setting = "CompilerDebug.PreLinkHashFile", Value = "%DEFAULT%"},
			new Argument { Name = "-output-hash", Setting = "CompilerDebug.PostLinkHashFile", Value = "%DEFAULT%"},

			new Argument { Name = "-output-debug-file", Setting = "CompilerDebug.DebugFile"},

			new Argument { Name = "-check", Setting = "CompilerDebug.FullCheckMode", Value = "true"},

			new Argument { Name = "-platform", Setting = "Compiler.Platform"},
			new Argument { Name = "-x86", Setting = "Compiler.Platform", Value = "x86"},
			new Argument { Name = "-x64", Setting = "Compiler.Platform", Value = "x64"},
			new Argument { Name = "-armv8a32", Setting = "Compiler.Platform", Value = "armv8a32"},

			new Argument { Name = "-interrupt-method", Setting = "X86.InterruptMethodName"},

			new Argument { Name = "-inline-exclude", Setting = "Optimizations.Inline.Exclude", IsList = true},

			// Linker:
			new Argument { Name = "-emit-all-symbols", Setting = "Linker.Symbols", Value = "true"},
			new Argument { Name = "-emit-all-symbols-off", Setting = "Linker.Symbols", Value = "false"},
			new Argument { Name = "-emit-relocations", Setting = "Linker.StaticRelocations", Value = "true"},
			new Argument { Name = "-emit-relocations-off", Setting = "Linker.StaticRelocations", Value = "false"},
			new Argument { Name = "-emit-static-relocations", Setting = "Linker.StaticRelocations", Value = "true"},
			new Argument { Name = "-emit-dwarf", Setting = "Linker.Dwarf", Value = "true"},
			new Argument { Name = "-emit-dwarf-off", Setting = "Linker.Dwarf", Value = "false"},
			new Argument { Name = "-dwarf", Setting = "Linker.Dwarf", Value = "true"},

			// Explorer:
			new Argument { Name = "-filter", Setting = "Explorer.Filter", Value = null},
			new Argument { Name = "-explorer-debug", Setting = "Explorer.DebugDiagnostic", Value = "true"},
			new Argument { Name = "-autostart", Setting = "Explorer.Start", Value = "true"},

			// Launcher:
			new Argument { Name = "-autoexit", Setting = "Launcher.Exit", Value = "true"},
			new Argument { Name = "-autoexit-off", Setting = "Launcher.Exit", Value = "false"},
			new Argument { Name = "-autostart", Setting = "Launcher.Start", Value = "true"},
			new Argument { Name = "-autostart-off", Setting = "Launcher.Start", Value = "false"},
			new Argument { Name = "-autolaunch", Setting = "Launcher.Launch", Value="true"},
			new Argument { Name = "-autolaunch-off", Setting = "Launcher.Launch", Value="false"},

			new Argument { Name = "-destination", Setting = "Image.Folder"},
			new Argument { Name = "-dest", Setting = "Image.Folder"},

			new Argument { Name = "-launch", Setting = "Launcher.Launch", Value="true"},
			new Argument { Name = "-launch-off", Setting = "Launcher.Launch", Value="false"},

			// Launcher - Emulator:
			new Argument { Name = "-emulator", Setting = "Emulator"},
			new Argument { Name = "-qemu", Setting = "Emulator", Value="qemu"},
			new Argument { Name = "-vmware", Setting = "Emulator", Value="vmware"},
			new Argument { Name = "-bochs", Setting = "Emulator", Value="bochs"},
			new Argument { Name = "-virtualbox", Setting = "Emulator", Value="virtualbox"},
			new Argument { Name = "-display", Setting = "Emulator.Display", Value = "on"},
			new Argument { Name = "-display-off", Setting = "Emulator.Display", Value = "off"},
			new Argument { Name = "-memory", Setting = "Emulator.Memory"},
			new Argument { Name = "-cores", Setting = "Emulator.Cores"},
			new Argument { Name = "-gdb", Setting = "Emulator.GDB", Value="true"},

			// Launcher - Image:
			new Argument { Name = "-vhd", Setting = "Image.Format", Value="vhd"},
			new Argument { Name = "-img", Setting = "Image.Format", Value="img"},
			new Argument { Name = "-vdi", Setting = "Image.Format", Value="vdi"},
			new Argument { Name = "-vmdk", Setting = "Image.Format", Value="vmdk"},
			new Argument { Name = "-image", Setting = "Image.ImageFile"},

			new Argument { Name = "-blocks", Setting = "Image.DiskBlocks"},
			new Argument { Name = "-volume-label", Setting = "Image.VolumeLabel"},
			new Argument { Name = "-mbr", Setting = "Image.MasterBootRecordFile"},
			new Argument { Name = "-boot", Setting = "Image.BootBlockFile"},

			new Argument { Name = "-m", Setting = "Image.MasterBootRecordFile"},
			new Argument { Name = "-b", Setting = "Image.BootBlockFile"},

			// Launcher - Boot:
			new Argument { Name = "-multiboot-v1", Setting = "Multiboot.Version", Value = "v1"},
			new Argument { Name = "-multiboot-v2", Setting = "Multiboot.Version", Value = "v2"},
			new Argument { Name = "-multiboot-none", Setting = "Multiboot.Version", Value = ""},
			new Argument { Name = "-multiboot", Setting = "Multiboot.Version", Value = null},

			// Launcher - Serial:
			new Argument { Name = "-serial-connection", Setting = "Emulator.Serial"},
			new Argument { Name = "-serial-pipe", Setting = "Emulator.Serial", Value = "pipe"},
			new Argument { Name = "-serial-tcpclient", Setting = "Emulator.Serial", Value = "tcpclient"},
			new Argument { Name = "-serial-tcpserver", Setting = "Emulator.Serial", Value = "tcpserver"},
			new Argument { Name = "-serial-connection-port", Setting = "Emulator.Serial.Port"},
			new Argument { Name = "-serial-connection-host", Setting = "Emulator.Serial.Host"},

			new Argument { Name = "-video", Setting = "Multiboot.Video", Value = "true"},
			new Argument { Name = "-video-width", Setting = "Multiboot.Video.Width"},
			new Argument { Name = "-video-height", Setting = "Multiboot.Video.Height"},
			new Argument { Name = "-video-depth", Setting = "Multiboot.Video.Depth"},

			new Argument { Name = "-vmware-svga", Setting = "Emulator.SVGA", Value="vmware"},
			new Argument { Name = "-virtio-vga", Setting = "Emulator.SVGA", Value="virtio"},

			new Argument { Name = "-gdb-port", Setting = "GDB.Port"},
			new Argument { Name = "-gdb-host", Setting = "GDB.Host"},

			new Argument { Name = "-osname", Setting = "OS.Name"},

			new Argument { Name = "-launch-gdb", Setting = "Launcher.GDB", Value="true"},
			new Argument { Name = "-launch-debugger", Setting = "Launcher.Debugger", Value="true"},

			// Base directory is the output directory
			new Argument { Name = "-include", Setting = "Image.FileSystem.RootInclude" },

			new Argument { Name = "-bios", Setting = "Image.Firmware", Value="bios"},
			new Argument { Name = "-uefi", Setting = "Image.Firmware", Value="uefi"},
			new Argument { Name = "-firmware", Setting = "Image.Firmware"},

			// Advance:
			new Argument { Name = "-plug-korlib", Setting = "Launcher.PlugKorlib", Value = "true"},
			new Argument { Name = "-test", Setting = "Launcher.Test", Value = "true"},

			// Debugger:
			new Argument { Name = "-breakpoints", Setting = "Debugger.BreakpointFile"},
			new Argument { Name = "-watch", Setting = "Debugger.WatchFile"},

			// Unit Test:
			new Argument { Name = "-filter", Setting = "UnitTest.Filter", Value = null},
			new Argument { Name = "-unittest-maxerrors", Setting = "UnitTest.MaxErrors"},

			// Optimization Levels:
			new Argument { Name = "-o0", Setting = "Optimizations.Basic", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.SSA", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.ValueNumbering", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.SCCP", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.Devirtualization", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.LongExpansion", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o0", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-o0", Setting = "Optimizations.Basic.Window", Value = "1"},

			new Argument { Name = "-o1", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o1", Setting = "Optimizations.SSA", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.ValueNumbering", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.SCCP", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o1", Setting = "Optimizations.LongExpansion", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o1", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-o1", Setting = "Optimizations.Basic.Window", Value = "1"},

			new Argument { Name = "-o2", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o2", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o2", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o2", Setting = "Optimizations.SCCP", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o2", Setting = "Optimizations.LongExpansion", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o2", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-o2", Setting = "Optimizations.Basic.Window", Value = "1"},

			new Argument { Name = "-o3", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o3", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o3", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o3", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-o3", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o3", Setting = "Optimizations.LongExpansion", Value = "false"},
			new Argument { Name = "-o3", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-o3", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-o3", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-o3", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-o3", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o3", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o3", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-o3", Setting = "Optimizations.Basic.Window", Value = "5"},

			new Argument { Name = "-o4", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o4", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o4", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o4", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-o4", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o4", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-o4", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-o4", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-o4", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-o4", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-o4", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o4", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o4", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-o4", Setting = "Optimizations.Basic.Window", Value = "5"},

			new Argument { Name = "-o5", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o5", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o5", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o5", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-o5", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o5", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-o5", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-o5", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-o5", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-o5", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-o5", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o5", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o5", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-o5", Setting = "Optimizations.Basic.Window", Value = "5"},

			new Argument { Name = "-o6", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.Inline", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.Inline.Explicit", Value = "true"},
			new Argument { Name = "-o6", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-o6", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o6", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o6", Setting = "Optimizations.Inline.Maximum", Value = "5"},
			new Argument { Name = "-o6", Setting = "Optimizations.Basic.Window", Value = "5"},

			new Argument { Name = "-o7", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.Inline", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.Inline.Explicit", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "true"},
			new Argument { Name = "-o7", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-o7", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o7", Setting = "Optimizations.Inline.Maximum", Value = "10"},
			new Argument { Name = "-o7", Setting = "Optimizations.Basic.Window", Value = "5"},

			new Argument { Name = "-o8", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.Inline", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.Inline.Explicit", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.BitTracker", Value = "true"},
			new Argument { Name = "-o8", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-o8", Setting = "Optimizations.Inline.Maximum", Value = "10"},
			new Argument { Name = "-o8", Setting = "Optimizations.Basic.Window", Value = "5"},

			new Argument { Name = "-o9", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.Inline", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.Inline.Explicit", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.BitTracker", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.TwoPass", Value = "true"},
			new Argument { Name = "-o9", Setting = "Optimizations.Inline.Maximum", Value = "12"},
			new Argument { Name = "-o9", Setting = "Optimizations.Basic.Window", Value = "10"},

			new Argument { Name = "-oNone", Setting = "Optimizations.Basic", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.SSA", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.ValueNumbering", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.SCCP", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.Devirtualization", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.LongExpansion", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-oNone", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-oNone", Setting = "Optimizations.Basic.Window", Value = "1"},

			new Argument { Name = "-oMax", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.Inline", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.Inline.Explicit", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.BitTracker", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.TwoPass", Value = "true"},
			new Argument { Name = "-oMax", Setting = "Optimizations.Inline.Maximum", Value = "12"},
			new Argument { Name = "-oMax", Setting = "Optimizations.Basic.Window", Value = "20"},

			new Argument { Name = "-oSize", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.SCCP", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.LongExpansion", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.Platform", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.Inline", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.Inline.Explicit", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.BitTracker", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.TwoPass", Value = "true"},
			new Argument { Name = "-oSize", Setting = "Optimizations.Inline.Maximum", Value = "3"},
			new Argument { Name = "-oSize", Setting = "Optimizations.Basic.Window", Value = "10"},

			new Argument { Name = "-oFast", Setting = "Optimizations.Basic", Value = "true"},
			new Argument { Name = "-oFast", Setting = "Optimizations.SSA", Value = "true"},
			new Argument { Name = "-oFast", Setting = "Optimizations.ValueNumbering", Value = "true"},
			new Argument { Name = "-oFast", Setting = "Optimizations.SCCP", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.Devirtualization", Value = "true"},
			new Argument { Name = "-oFast", Setting = "Optimizations.LongExpansion", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.Platform", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.Inline", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.Inline.Explicit", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.LoopInvariantCodeMotion", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.BitTracker", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.TwoPass", Value = "false"},
			new Argument { Name = "-oFast", Setting = "Optimizations.Inline.Maximum", Value = "0"},
			new Argument { Name = "-oFast", Setting = "Optimizations.Basic.Window", Value = "1"},
		};

		return map;
	}
}
