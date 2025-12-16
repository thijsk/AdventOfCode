using System;
using System.Collections;
using System.Text;

namespace Common
{
	public static class BitArrayExtensions
	{
		public static int GetHashCodeEx(this BitArray bitArray)
		{
			ArgumentNullException.ThrowIfNull(bitArray);

			HashCode hash = new();

			foreach (bool bit in bitArray) 
			{
				hash.Add(bit);
			}

			return hash.ToHashCode();
		}

		public static bool SequenceEqual(this BitArray first, BitArray second)
		{
			if (first == null || second == null)
				return false;
			if (first.Length != second.Length)
				return false;
			for (int i = 0; i < first.Length; i++)
			{
				if (first[i] != second[i])
					return false;
			}
			return true;
		}

		public static string ToStringEx(this BitArray bitArray)
		{
			ArgumentNullException.ThrowIfNull(bitArray);
			var sb = new StringBuilder();
			for (int i = 0; i < bitArray.Length; i++)
			{
				sb.Append(bitArray[i] ? '#' : '.');
			}
			return sb.ToString();
		}
	}
}
