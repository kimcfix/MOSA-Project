﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace Mosa.Utility.SourceCodeGenerator;

public class BuildX64 : BuildBaseTemplate
{
	public BuildX64(string jsonFile, string destinationPath, string destinationFile)
		: base(jsonFile, destinationPath, destinationFile)
	{
	}

	protected override void Body()
	{
		Lines.AppendLine("using Mosa.Compiler.Framework;");
		Lines.AppendLine("using Mosa.Compiler.x64.Instructions;");
		Lines.AppendLine();
		Lines.AppendLine("namespace Mosa.Compiler.x64;");
		Lines.AppendLine();
		Lines.AppendLine("/// <summary>");
		Lines.AppendLine("/// X64 Instructions");
		Lines.AppendLine("/// </summary>");
		Lines.AppendLine("public static class X64");
		Lines.AppendLine("{");

		foreach (var entry in Entries.Instructions)
		{
			Lines.AppendLine("\tpublic static readonly BaseInstruction " + entry.Name + " = new " + entry.Name + "();");
		}

		Lines.AppendLine("}");
	}
}
