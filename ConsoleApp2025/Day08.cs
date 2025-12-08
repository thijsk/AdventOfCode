using Common;

namespace ConsoleApp2025;

public class Day08 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 98696;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		foreach (var box in input.Select((b, i) => (b, i)))
		{
			box.b.CircuitId = box.i + 1;
		}

		var n = FindClosestNeighbors(input);

		var connectionCount = 0;

		foreach (var pair in n)
		{
			
			var boxA = pair[0];
			var boxB = pair[1];

			var circuitIdToKeep = Math.Min(boxA.CircuitId, boxB.CircuitId);
			var circuitIdToReplace = Math.Max(boxA.CircuitId, boxB.CircuitId);

			input.Where(b => b.CircuitId == circuitIdToReplace).ToList().ForEach(b => b.CircuitId = circuitIdToKeep);

			//Console.WriteLine($"Connecting {boxA} and {boxB}");

			connectionCount++;
			if (connectionCount == (PuzzleContext.UseExample ? 10 : 1000))
			{
				break;
			}
		}

		var circuits = input.Where(c => c.CircuitId != 0).GroupBy(j => j.CircuitId).ToList().OrderByDescending(c => c.Count());
		var counts = circuits.Take(3).Select(g => g.Count());
		var result = counts.Aggregate(1L, (g1, g2) => g1 * g2);

		return result;
	}

	private List<JunctionBox[]> FindClosestNeighbors(JunctionBox[] input)
	{
		var pairs = input.Combinations(2).ToList();
		return pairs.OrderBy(p => p[0].Position.DistanceTo(p[1].Position)).ToList();
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 2245203960;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		foreach (var box in input.Select((b, i) => (b, i)))
		{
			box.b.CircuitId = box.i + 1;
		}

		var n = FindClosestNeighbors(input);

		foreach (var pair in n)
		{

			var boxA = pair[0];
			var boxB = pair[1];

			var circuitIdToKeep = Math.Min(boxA.CircuitId, boxB.CircuitId);
			var circuitIdToReplace = Math.Max(boxA.CircuitId, boxB.CircuitId);

			input.Where(b => b.CircuitId == circuitIdToReplace).ToList().ForEach(b => b.CircuitId = circuitIdToKeep);

			//Console.WriteLine($"Connecting {boxA} and {boxB}");

			if (input.All(b => b.CircuitId == circuitIdToKeep)) 
			{
				return boxA.Position.x * boxB.Position.x;
			}
		}


		return 0L;
	}

	private JunctionBox Parse(string line)
	{
		return new JunctionBox
		{
			Position = new Point3<long>(
				long.Parse(line.Split(',')[0]),
				long.Parse(line.Split(',')[1]),
				long.Parse(line.Split(',')[2])),
		};
	}


	private class JunctionBox
	{
		public Point3<long> Position { get; set; }

		public int CircuitId { get; set; } = 0;

		public override string ToString()
		{
			return $"JunctionBox({Position}, CircuitId={CircuitId})";
		}
	}
}