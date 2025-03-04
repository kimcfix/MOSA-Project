// Copyright (c) MOSA Project. Licensed under the New BSD License.

namespace Mosa.DeviceSystem;

/// <summary>
///
/// </summary>
public interface IPartitionDevice
{
	/// <summary>
	/// Gets the starting block number on the disk
	/// </summary>
	uint StartBlock { get; }

	/// <summary>
	/// Gets the block count.
	/// </summary>
	/// <value>The block count.</value>
	uint BlockCount { get; }

	/// <summary>
	/// Gets the size of the block.
	/// </summary>
	/// <value>The size of the block.</value>
	uint BlockSize { get; }

	/// <summary>
	/// Gets a value indicating whether this instance can write.
	/// </summary>
	/// <value><c>true</c> if this instance can write; otherwise, <c>false</c>.</value>
	bool CanWrite { get; }

	/// <summary>
	/// Reads the block.
	/// </summary>
	/// <param name="block">The block.</param>
	/// <param name="count">The count.</param>
	/// <returns></returns>
	byte[] ReadBlock(uint block, uint count);

	/// <summary>
	/// Reads the block.
	/// </summary>
	/// <param name="block">The block.</param>
	/// <param name="count">The count.</param>
	/// <param name="data">The data.</param>
	/// <returns></returns>
	bool ReadBlock(uint block, uint count, byte[] data);

	/// <summary>
	/// Writes the block.
	/// </summary>
	/// <param name="block">The block.</param>
	/// <param name="count">The count.</param>
	/// <param name="data">The data.</param>
	/// <returns></returns>
	bool WriteBlock(uint block, uint count, byte[] data);
}
