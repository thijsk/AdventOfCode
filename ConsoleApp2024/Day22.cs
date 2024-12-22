using Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp2024;

public class Day22 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 19822877190;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var loops = 2000L;

		Debug.Assert(37 == Mix(42, 15));
		Debug.Assert(16113920 == Prune(100000000));


		return	input.Sum(seed => GenerateSecret(seed, loops));
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var loops = 2000L;

		//input = [123];
		//loops = 10;

		Debug.Assert(37 == Mix(42, 15));
		Debug.Assert(16113920 == Prune(100000000));

		long sum = 0;

		List<long[]> prices = new();
		List<long[]> changes = new();

		foreach ( var seed in input)
		{
			var pricei = new long[loops];
			var changei = new long[loops];

			var secret = seed;
			var lastprice = seed % 10;
			for (int loop = 0; loop < loops; loop++)
			{
				secret = GenerateSecret(secret, 1);
				var price = secret % 10;
				var change = price - lastprice;
				pricei[loop] = price;
				changei[loop] = change;
				lastprice = price;
			}
			prices.Add(pricei);
			changes.Add(changei);
		}

		var sequences = changes.SelectMany(GrabSequences).Distinct(new LongArrayComparer()).ToArray();

		var maxBananas = 0L;
		ConsoleX.WriteLine(sequences.Length);
		long counter = 0;

		return sequences.AsParallel().Max(sequence =>
		{
			var bananas = 0L;
			for (int i = 0; i < input.Length; i++)
			{
				var banana = GetBananas(prices[i], changes[i], sequence);
				bananas += banana;
			}
			return bananas;
		});
	}

	private long GetBananas(long[] prices, long[] changes, long[] sequence)
	{
		var index = FindSequence(changes, sequence);
		if (index.HasValue)
		{
			return prices[index.Value];
		}
		return 0;
	}

	public static int? FindSequence(long[] changes, long[] sequence)
	{
		if (sequence.Length > changes.Length)
		{
			return null; // shorter sequence cannot exist in longer sequence
		}

		for (int i = 0; i <= changes.Length - sequence.Length; i++)
		{
			bool match = true;
			for (int j = 0; j < sequence.Length; j++)
			{
				if (changes[i + j] != sequence[j])
				{
					match = false;
					break;
				}
			}

			if (match)
			{
				return i + sequence.Length - 1; // return position of last item
			}
		}

		return null; // shorter sequence not found
	}

	public class LongArrayComparer : IEqualityComparer<long[]>
	{
		public bool Equals(long[] x, long[] y)
		{
			if (x == null && y == null) return true;
			if (x == null || y == null) return false;
			if (x.Length != y.Length) return false;

			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != y[i]) return false;
			}

			return true;
		}

		public int GetHashCode(long[] obj)
		{
			unchecked
			{
				int hash = 17;
				foreach (var value in obj)
				{
					hash = hash * 23 + value.GetHashCode();
				}
				return hash;
			}
		}
	}

		private IEnumerable<long[]> GrabSequences(long[] allsequences)
	{
		for (int i = 0; i <= allsequences.Length - 4; i++)
		{
			long[] sequence = new long[4];
			Array.Copy(allsequences, i, sequence, 0, 4);
			yield return sequence;
		}
	}

	private long GenerateSecret(long seed, long loops)
	{
		var result = seed;

		for (int loop = 0; loop < loops; loop++)
		{
			result = Prune(Mix(result, result * 64));
			result = Prune(Mix(result, result / 32));
			result = Prune(Mix(result, result * 2048));
			//ConsoleX.WriteLine(result);
		}
		return result;
	}

	private long Prune(long mix)
	{
		return mix % 16777216;
	}

	private long Mix(long p0, long p1)
	{
		return p0 ^ p1;
	}



	private long Parse(string line)
	{
		return long.Parse(line);
	}

}