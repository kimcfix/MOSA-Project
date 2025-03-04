﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using System;

namespace Mosa.Compiler.Framework;

/// <summary>
/// Used for defining targets when using intrinsic replacements
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class TransformAttribute : Attribute
{
	public string Section { get; }

	public TransformAttribute(string section)
	{
		Section = section;
	}
}
