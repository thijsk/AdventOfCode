using Common;

namespace ConsoleApp2023;

public class Day04 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long sum = 0;
		foreach (var draw in input)
		{
			var count = draw.winning.Intersect(draw.my).Count();
			if (count == 0) continue;
			var value = (int)Math.Pow(2, count-1);
			sum += value;

            Console.WriteLine($"Card {draw.card} Matches {count} Value {value}");
        }

		return sum;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		Dictionary<long, int> copies = new();
		foreach (var draw in input)
		{
			copies.Add(draw.card, 1);
		}

		long sum = 0;
		foreach (var draw in input)
		{
			var count = draw.winning.Intersect(draw.my).Count();
			if (count == 0) continue;
			var value = (int)Math.Pow(2, count - 1);
			var cardcopies = copies[draw.card];

			foreach (var copy in Enumerable.Range(1, cardcopies))
			{
				foreach (var cardnumber in Enumerable.Range((int) (draw.card + 1), count))
				{
					if (copies.ContainsKey(cardnumber))
					{
						copies[cardnumber]++;
						//Console.WriteLine($"You win a copy of card {cardnumber}");
					}
				}
			}

			Console.WriteLine($"Card {draw.card} Matches {count} Value {value} Copies {cardcopies}");
		}

		return copies.Values.Sum();
	}

	public (long card, int[] winning, int[] my)Parse(string line)
	{
		var (card, numbers) = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
		var (winningnumbers, mynumbers) = numbers.Split('|', StringSplitOptions.RemoveEmptyEntries);
		var winning = winningnumbers.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
		var my = mynumbers.Split<int>(' ');
		return (long.Parse(card.Split(' ').Last()), winning, my);
	}

}