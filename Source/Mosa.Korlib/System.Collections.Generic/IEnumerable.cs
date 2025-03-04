﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace System.Collections.Generic;

/// <summary>
/// Exposes the enumerator, which supports a simple iteration over a collection of a specified type.
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
public interface IEnumerable<out T> : IEnumerable
{
	/// <summary>
	/// Gets the enumerator.
	/// </summary>
	/// <returns></returns>
	new IEnumerator<T> GetEnumerator();
}
