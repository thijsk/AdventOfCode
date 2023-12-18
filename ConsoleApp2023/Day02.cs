using Common;

namespace ConsoleApp2023;

public class Day02 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 2810;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long sum = 0;
		foreach (var game in input)
		{
			var gameOk = true;

			foreach (var draw in game.Draws)
			{
				if (draw.ContainsKey("red") && draw["red"] > 12)
				{
					Console.WriteLine($"{game.GameId} is not Ok red: {draw["red"]}");
					gameOk = false;
					continue;
				}

				if (draw.ContainsKey("green") && draw["green"] > 13)
				{
					Console.WriteLine($"{game.GameId} is not Ok green: {draw["green"]}");
					gameOk = false;
					continue;
				}

				if (draw.ContainsKey("blue") && draw["blue"] > 14)
				{
					Console.WriteLine($"{game.GameId} is not Ok blue: {draw["blue"]}");
					gameOk = false;
					continue;
				}
			}
			if (gameOk)
                sum += game.GameId;
		}

		return sum;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 69110;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long sum = 0;
		foreach (var game in input)
		{
			var maxRed = game.Draws.Max(x => x.ContainsKey("red") ? x["red"] : 0);
			var maxGreen = game.Draws.Max(x => x.ContainsKey("green") ? x["green"] : 0);
			var maxBlue = game.Draws.Max(x => x.ContainsKey("blue") ? x["blue"] : 0);

			var power = maxRed * maxGreen * maxBlue;
			sum += power;
		}

		return sum;
	}

	private Game Parse(string line)
	{
		return Game.Parse(line);
	}

	private  class Game
	{
		public long GameId { get; set; }
		public List<Dictionary<string, int>> Draws { get; set; } = new();

		public static Game Parse(string line)
		{
			var result = new Game();

			result.GameId = long.Parse(line.Split(":")[0].Split(" ")[1]);
			var draws = line.Split(":")[1].Trim().Split(";");

			foreach (var draw in draws)
			{
				var drawResult = new Dictionary<string, int>();
				foreach (var reveal in draw.Split(","))
				{
					var count = reveal.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0];
					var color = reveal.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
					drawResult.Add(color, int.Parse(count));	
				}
				result.Draws.Add(drawResult);
			}

			return result;
		}
	}
}