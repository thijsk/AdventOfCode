using Common;
using System.Diagnostics;

namespace ConsoleApp2024;

public class Day05 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 5964;
		PuzzleContext.UseExample = false;

		var inputs = PuzzleContext.Input.SplitByEmptyLines();
		var rules = inputs[0].Select(ParseRule).ToArray();
		var updates = inputs[1].Select(ParseUpdates).ToArray();

		long sum = 0;
		long wrong = 0;

		foreach (var update in updates)
		{
			if (UpdateIsCorrect(update, rules))
			{
				var middle = update[update.Length / 2];
				ConsoleX.WriteLine(middle, ConsoleColor.Cyan);
				sum += middle;
			}
			else
			{
				wrong++;
			}
		}

		ConsoleX.WriteLine("Wrong: " + wrong, ConsoleColor.Red);

		return sum;
	}

	private bool UpdateIsCorrect(int[] update, (int first, int second)[] rules)
	{
		ConsoleX.WriteLine(string.Join(',', update), ConsoleColor.Yellow);

		for (var i = 0; i < update.Length; i++)
		{
			var page = update[i];
			var appliedRules = rules.Where(r => r.first == page).ToArray();

			if (appliedRules.Length == 0)
			{
				continue;
			}

			foreach (var rule in appliedRules)
			{
				if (!update.Contains(rule.second))
				{
					continue;
				}
				var indexOfSecond = Array.IndexOf(update, rule.second);
				if (indexOfSecond < i)
				{
					ConsoleX.WriteLine("Invalid", ConsoleColor.Red);
					return false;
				}
			}
		}
		ConsoleX.WriteLine("Valid", ConsoleColor.Green);
		return true;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 4719;
		PuzzleContext.UseExample = false;

		var inputs = PuzzleContext.Input.SplitByEmptyLines();
		var rules = inputs[0].Select(ParseRule).ToArray();
		var updates = inputs[1].Select(ParseUpdates).ToArray();

		long sum = 0;

		foreach (var update in updates)
		{
			if (!UpdateIsCorrect(update, rules))
			{
				var reOrderUpdate = ReOrder(update, rules);
				Debug.Assert(UpdateIsCorrect(reOrderUpdate, rules));
				var middle = reOrderUpdate[reOrderUpdate.Length / 2];
				ConsoleX.WriteLine(middle, ConsoleColor.Cyan);
				sum += middle;
			}
		}

		return sum;
	}

	private int[] ReOrder(int[] update, (int first, int second)[] rules)
	{
		//return update.GetPermutations().Select(p => p.ToArray()).AsParallel().FirstOrDefault(option => UpdateIsCorrect(option, rules)); // LOL

		var newUpdate = new List<int>();
		newUpdate.AddRange(update);

		foreach (var page in update)
		{
			var appliedRules = rules.Where(r => r.first == page).ToArray();

			if (appliedRules.Length == 0)
			{
				newUpdate.Remove(page);
				newUpdate.Add(page);
				continue;
			}

			var minIndex = int.MaxValue;

			foreach (var rule in appliedRules)
			{
				if (!update.Contains(rule.second))
				{
					continue;
				}
				var indexOfSecond = newUpdate.IndexOf(rule.second);
				minIndex = Math.Min(minIndex, indexOfSecond);
			}

			if (minIndex < newUpdate.IndexOf(page))
			{
				newUpdate.Remove(page);
				newUpdate.Insert(minIndex, page);
			}
		}

		return newUpdate.ToArray();
	}

	private (int first, int second) ParseRule(string line)
	{
		(int first, int second) = line.Split('|').Select(int.Parse).ToArray();
		return (first, second);
	}

	private int[] ParseUpdates(string line)
	{
		return line.Split(',').Select(int.Parse).ToArray();
	}

}