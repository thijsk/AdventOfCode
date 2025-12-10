using Common;
using Microsoft.Z3;

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
			var stateCache = new HashSet<string>();
			var initialState = new string('.', goal.Length);

			var frontier = new Queue<(string state, int button, long depth)>();


			for (int b = 0; b < buttons.Length; b++)
			{
				var firstState = ApplyButton1(initialState, buttons[b]);
				stateCache.Add(firstState);
				frontier.Enqueue((firstState, b, 1L));
			}


			while (frontier.Count > 0)
			{
				var (state, button, depth) = frontier.Dequeue();

				if (state == goal)
				{
					totalPresses += depth;
					break;
				}

				depth++;

				for (int b = 0; b < buttons.Length; b++)
				{
					if (b == button)
						continue;
					var nextState = ApplyButton1(state, buttons[b]);
					if (stateCache.Add(nextState))
					{
						frontier.Enqueue((nextState, b, depth));
					}
				}

			}


		}


		return totalPresses;
	}

	private string ApplyButton1(string state, int[] buttonToggles)
	{
		var stateArr = state.ToCharArray();

		foreach (var toggle in buttonToggles) 
		{
			stateArr[toggle] = stateArr[toggle] == '.' ? '#' : '.';
		}

		return new string(stateArr);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 20626;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var totalPresses = 0L;

		//foreach (var (goal, buttons, joltage) in input)
		//{
		//	var stateCache = new HashSet<int>();
		//	var initialState = new int[joltage.Length];

		//	var frontier = new Queue<(int[] state, int button, long depth)>();


		//	for (int b = 0; b < buttons.Length; b++)
		//	{
		//		var firstState = ApplyButton2(initialState, buttons[b]);
		//		stateCache.Add(firstState.GetHashCode<int>());
		//		frontier.Enqueue((firstState, b, 1L));
		//	}


		//	while (frontier.Count > 0)
		//	{
		//		var (state, button, depth) = frontier.Dequeue();

		//		if (state.SequenceEqual(joltage))
		//		{
		//			totalPresses += depth;
		//			break;
		//		}

		//		depth++;

		//		for (int b = 0; b < buttons.Length; b++)
		//		{
		//			var nextState = ApplyButton2(state, buttons[b]);
		//			if (Overshoot(nextState, joltage))
		//				continue;
		//			if (stateCache.Contains(nextState.GetHashCode<int>()))
		//				continue;
		//			stateCache.Add(nextState.GetHashCode<int>());
		//			frontier.Enqueue((nextState, b, depth));
		//		}

		//	}

		//	Console.WriteLine($"Completed one circuit, total presses so far: {totalPresses}");


		//}

		foreach (var (goal, buttons, joltage) in input)
		{
			var context = new Context();
			var optimize = context.MkOptimize();

			var buttonPresses = new IntExpr[buttons.Length];

			for (int b = 0; b < buttons.Length; b++)
			{
				buttonPresses[b] = context.MkIntConst($"button_{b}_presses");
				optimize.Add(context.MkGe(buttonPresses[b], context.MkInt(0)));
			}

			for (var j =0 ; j < joltage.Length; j++)
			{
				var terms = new List<ArithExpr>();
				for (var b =0; b < buttons.Length; b++)
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

			for (int b = 0; b < buttons.Length; b++)
			{
				var value = optimize.Model.Evaluate(buttonPresses[b]);
				var intValue = ((IntNum)value).Int;
				totalPresses += intValue;
			}


		}

		// Puzzles where Z3 is "needed" are not fun.

		return totalPresses;
	}

	private bool Overshoot(int[] nextState, int[] joltage)
	{
		for (int i = 0; i < nextState.Length; i++)
		{
			if (nextState[i] > joltage[i])
				return true;
		}
		return false;	
	}

	private int[] ApplyButton2(int[] state, int[] buttonToggles)
	{
		var newstate = state.Clone() as int[];
		foreach (var toggle in buttonToggles)
		{
			newstate[toggle]++;
		}

		return newstate;
	}

	private (string lights, int[][] buttons, int[] joltage) Parse(string line)
	{
		var split = line.Split(' ');
		var lights = split[0].Trim('[').Trim(']');
		var buttons = split[1..^1]
			.Select(s => s.Trim('(').Trim(')').Split(',').Select(int.Parse).ToArray())
			.ToArray();
		var joltage = split[^1].Trim('{').Trim('}').Split(',').Select(int.Parse).ToArray();

		return (lights, buttons, joltage);
	}

}