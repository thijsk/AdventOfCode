using Common;
using Microsoft.Z3;
using System.Collections;
using System.Diagnostics;

namespace ConsoleApp2025;

public class Day10 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 520;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var totalPresses = 0L;

		foreach (var (goal, buttons, joltage) in input)
		{
			var stateCache = new HashSet<int>();

			totalPresses += CountPresses(goal, buttons, stateCache).Count();
		}
		return totalPresses;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 20626;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var totalPresses = 0L;

		foreach (var (goal, buttons, joltage) in input)
		{
			var patterns = GetPatterns(buttons, joltage.Length);
			count2memory.Clear();
			var presses = CountPressess2(joltage, buttons, patterns);

		//	Console.WriteLine($"joltage: {string.Join(',', joltage)} presses: {presses}");
			totalPresses += presses;
		}

		return totalPresses;
	}

	
		

	private Dictionary<int, List<List<int>>> GetPatterns(int[][] buttons, int length)
	{
		var result = new Dictionary<int, List<List<int>>>();
		var buttonIndexes = Enumerable.Range(0, buttons.Length);
		for (int p = 0; p <= buttons.Length; p++)
		{
			var combos = buttonIndexes.Combinations(p);
			foreach (var combo in combos)
			{
				var pattern = new BitArray(length);
				foreach (var b in combo)
				{
					foreach (var toggle in buttons[b])
					{
						pattern[toggle] = !pattern[toggle];
					}
				}

				var key = pattern.GetHashCodeEx();

				if (!result.ContainsKey(key))		
				{
					result[key] = new List<List<int>>();
				}

				result[key].Add(combo.ToList());
			}
		}

		return result;
	}

	private List<int> CountPresses(BitArray goal, int[][] buttons, HashSet<int> stateCache)
	{
		var emptyState = new BitArray(goal.Length);

		var frontier = new Queue<(BitArray state, List<int> presses, long depth)>();

		for (int b = 0; b < buttons.Length; b++)
		{
			var firstState = ApplyButton1(emptyState, buttons[b]);
			stateCache.Add(firstState.GetHashCodeEx());
			frontier.Enqueue((firstState, [b], 1L));
		}

		while (frontier.Count > 0)
		{
			var (state, presses, depth) = frontier.Dequeue();

			if (goal.SequenceEqual(state))
			{
				Debug.Assert(presses.Count == depth, "Press count and depth do not match");
				return presses;
			}

			depth++;

			for (int b = 0; b < buttons.Length; b++)
			{
				if (b == presses.Last())
					continue;
				var nextState = ApplyButton1(state, buttons[b]);
				if (stateCache.Add(nextState.GetHashCodeEx()))
				{
					var nextpresses = new List<int>(presses) { b };
					frontier.Enqueue((nextState, nextpresses, depth));
				}
			}
		}

		throw new Exception("No solution found");
	}

	private BitArray ApplyButton1(BitArray state, int[] buttonToggles)
	{
		var stateArr = (BitArray) state.Clone();

		foreach (var toggle in buttonToggles) 
		{
			stateArr[toggle] = !stateArr[toggle];
		}

		return stateArr;
	}


	Dictionary<int, long> count2memory = new ();

	private long CountPressess2(int[] joltage, int[][] buttons, Dictionary<int, List<List<int>>> patterns)
	{
		var key = joltage.GetHashCodeOfList();

		long result;
		if (count2memory.TryGetValue(key, out result))
			return result;
		result = long.MaxValue;

		if (joltage.All(j => j == 0))
		{
			result = 0L;
			count2memory.Add(key, result);
			return result;
		}

		var odds = FindOdds(joltage);

		if (patterns.TryGetValue(odds.GetHashCodeEx(), out var options))
		{
			foreach (var option in options)
			{
				var joltageAfter = ApplyButton2(joltage, buttons, option);
				if (joltageAfter.Any(j => j < 0))
					continue;

				var halfJoltage = joltageAfter.Select(j => j / 2).ToArray();

				var halfCount = CountPressess2(halfJoltage, buttons, patterns);

				if (halfCount == long.MaxValue)
					continue;

				var totalPresses = option.Count + (2L * halfCount);

				result = Math.Min(result, totalPresses);
			}
		}

		count2memory.Add(key, result);
		return result;
	}

	private static BitArray FindOdds(int[] joltage)
	{
		var odds = new BitArray(joltage.Length);
		
		for (int i = 0; i < joltage.Length; i++)
		{
			if (joltage[i] % 2 == 1)
			{
				odds[i] = true;
			}
			else
			{
				odds[i] = false;
			}
		}
		return odds;
	}

	private static long SolveWithZ3(int[][] buttons, int[] joltage)
	{
		var context = new Context();
		var optimize = context.MkOptimize();

		var buttonPresses = new IntExpr[buttons.Length];

		for (int b = 0; b < buttons.Length; b++)
		{
			buttonPresses[b] = context.MkIntConst($"button_{b}_presses");
			optimize.Add(context.MkGe(buttonPresses[b], context.MkInt(0)));
		}

		for (var j = 0; j < joltage.Length; j++)
		{
			var terms = new List<ArithExpr>();
			for (var b = 0; b < buttons.Length; b++)
			{
				if (buttons[b].Contains(j))
				{
					terms.Add(buttonPresses[b]);
				}
			}


			var sumExpr = context.MkAdd(terms.ToArray());
			var targetExpr = context.MkInt(joltage[j]);
			optimize.Add(context.MkEq(sumExpr, targetExpr));
		}


		optimize.MkMinimize(context.MkAdd(buttonPresses));

		var status = optimize.Check();

		if (status != Status.SATISFIABLE)
		{
			throw new Exception("No solution found");
		}
		long totalPresses = 0L;
		for (int b = 0; b < buttons.Length; b++)
		{
			var value = optimize.Model.Evaluate(buttonPresses[b]);
			var intValue = ((IntNum)value).Int;
			totalPresses += intValue;
		}

		return totalPresses;
	}

	private int[] ApplyButton2(int[] state, int[][] buttons, List<int> presses)
	{
		var newstate = (int[]) state.Clone();
		foreach (var press in presses)
		{
			foreach (var toggle in buttons[press])
			{
				newstate[toggle]--;
			}
		}

		return newstate;
	}

	private (BitArray lights, int[][] buttons, int[] joltage) Parse(string line)
	{
		var split = line.Split(' ');
		var lights = new BitArray(split[0].Trim('[').Trim(']').Select(c => c == '#' ? true : false).ToArray());
		var buttons = split[1..^1]
			.Select(s => s.Trim('(').Trim(')').Split(',').Select(int.Parse).ToArray())
			.ToArray();
		var joltage = split[^1].Trim('{').Trim('}').Split(',').Select(int.Parse).ToArray();

		return (lights, buttons, joltage);
	}

}