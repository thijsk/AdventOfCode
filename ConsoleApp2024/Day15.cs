using Common;
using System.Collections.Specialized;
using System.Text;

namespace ConsoleApp2024;

public class Day15 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1413675;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines();
		var grid = input[0].GetGrid();
		var instructions = input[1].Select(Parse).SelectMany(c => c);

		var robot = grid.Find('@').First();


		foreach (var instruction in instructions)
		{
			grid[robot.x, robot.y] = '.';

			var direction = instruction switch
			{
				'^' => Directions.Up,
				'v' => Directions.Down,
				'<' => Directions.Left,
				'>' => Directions.Right,
				_ => throw new Exception()
			};

			robot = Move1(grid, robot, direction);
			grid[robot.x, robot.y] = '@';

		}
		grid.ToConsole();

		return grid.GetIndexes().Where(i => grid[i.x, i.y] == 'O').Sum(i => i.x * 100 + i.y);
	}

	private (int x, int y) Move1(char[,] grid, (int x, int y) from, (int x, int y) direction)
	{
		var n = grid.GetNeighborInDirection(from, direction)!.Value;

		if (grid[n.x, n.y] == '#')
		{
			return from;
		}
		if (grid[n.x, n.y] == '.')
		{
			return n;
		}
		if (grid[n.x, n.y] == 'O')
		{
			var n1 = grid.GetNeighborInDirection(n, direction)!.Value;

			while (grid[n1.x, n1.y] == 'O')
			{
				n1 = grid.GetNeighborInDirection(n1, direction)!.Value;
			}
			if (grid[n1.x, n1.y] == '.')
			{
				grid[n1.x, n1.y] = 'O';
				return n;
			}
			if (grid[n1.x, n1.y] == '#')
			{
				return from;
			}
		}

		throw new Exception();

	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 1399772;
		PuzzleContext.UseExample = false;


		var input = PuzzleContext.Input.SplitByEmptyLines();
		var grid = input[0].Select(Transform).ToArray().GetGrid();
		var instructions = input[1].Select(Parse).SelectMany(c => c);

		var robot = grid.Find('@').First();
		grid.ToConsole();

		foreach (var instruction in instructions)
		{
			ConsoleX.WriteLine($"Move {instruction}:");

			grid[robot.x, robot.y] = '.';

			var direction = instruction switch
			{
				'^' => Directions.Up,
				'v' => Directions.Down,
				'<' => Directions.Left,
				'>' => Directions.Right,
				_ => throw new ArgumentOutOfRangeException()
			};

			robot = Move2(grid, robot, direction);
			grid[robot.x, robot.y] = '@';

			grid.ToConsole();

		}

		return grid.GetIndexes().Where(i => grid[i.x, i.y] == '[').Sum(i => i.x * 100 + i.y);
	}

	private (int x, int y) Move2(char[,] grid, (int x, int y) from, (int x, int y) direction)
	{
		var na = grid.GetNeighborInDirection(from, direction)!.Value;

		if (grid[na.x, na.y] == '#')
		{
			return from;
		}
		if (grid[na.x, na.y] == '.')
		{
			return na;
		}
		if (IsBox(grid, na))
		{
			var box = GetBox(grid, na);

			if (Directions.LeftRight.Contains(direction))
			{
				List<((int x, int y) a, (int x, int y) b)> boxes = [box];
				var direction2 = direction.Multiply(2);

				var n1a = grid.GetNeighborInDirection(na, direction2)!.Value;

				while (IsBox(grid, n1a))
				{
					boxes.Add(GetBox(grid, n1a));
					n1a = grid.GetNeighborInDirection(n1a, direction2)!.Value;
				}
				if (grid[n1a.x, n1a.y] == '.')
				{
					boxes.Reverse();
					MoveLeftRight(grid, direction, boxes);
					return na;
				}
				if (grid[n1a.x, n1a.y] == '#')
				{
					return from;
				}
			}
			else
			{
				var layers = new List<((int x, int y) a, (int x, int y) b)[]>();
				((int x, int y) a, (int x, int y) b)[] layer = [box];
				layers.Add(layer);

				while (layer.Any() && CanMoveUpDown(grid, layer, direction))
				{
					layer = GetBoxesInDirection(grid, layer, direction);
					layers.Add(layer);
				}

				if (CanMoveUpDown(grid, layer, direction))
				{
					layers.Reverse();
					foreach (var l in layers)
					{
						MoveUpDown(grid, l, direction);
					}
				}
				else
				{
					return from;
				}

				return from.Add(direction);
			}
		}

		throw new Exception();
	}

	private static void MoveLeftRight(char[,] grid, (int x, int y) direction, List<((int x, int y) a, (int x, int y) b)> boxes)
	{
		foreach (var box in boxes)
		{
			var cb = grid[box.b.x, box.b.y];
			var ca = grid[box.a.x, box.a.y];
			(int x, int y) nb = box.b.Add(direction);
			(int x, int y) na = box.a.Add(direction);
			grid[nb.x, nb.y] = cb;
			grid[na.x, na.y] = ca;
		}
	}

	private void MoveUpDown(char[,] grid, ((int x, int y) a, (int x, int y) b)[] boxes, (int x, int y) direction)
	{
		foreach (var box in boxes)
		{
			var cb = grid[box.b.x, box.b.y];
			var ca = grid[box.a.x, box.a.y];

			(int x, int y) na = box.a.Add(direction);
			(int x, int y) nb = box.b.Add(direction);

			grid[na.x, na.y] = ca;
			grid[nb.x, nb.y] = cb;

			grid[box.a.x, box.a.y] = '.';
			grid[box.b.x, box.b.y] = '.';
		}
	}

	private bool CanMoveUpDown(char[,] grid, ((int x, int y) a, (int x, int y) b) box, (int x, int y) direction)
	{
		var na = grid.GetNeighborInDirection(box.a, direction)!.Value;
		var nb = grid.GetNeighborInDirection(box.b, direction)!.Value;

		if (grid[na.x, na.y] != '#' && grid[nb.x, nb.y] != '#')
		{
			return true;
		}

		return false;
	}

	private bool CanMoveUpDown(char[,] grid, ((int x, int y) a, (int x, int y) b)[] layer, (int x, int y) direction)
	{
		return layer.All(b => CanMoveUpDown(grid, b, direction));
	}

	private ((int x, int y) a, (int x, int y) b)[] GetBoxesInDirection(char[,] grid, ((int x, int y) a, (int x, int y) b)[] layer, (int x, int y) direction)
	{
		HashSet<((int x, int y) a, (int x, int y) b)> boxes = [];

		foreach (var box in layer) 
		{ 
			var na = grid.GetNeighborInDirection(box.a, direction)!.Value;
			if (IsBox(grid, na))
			{
				boxes.Add(GetBox(grid, na));
			}
			var nb = grid.GetNeighborInDirection(box.b, direction)!.Value;
			if (IsBox(grid, nb))
			{
				boxes.Add(GetBox(grid, nb));
			}
		}

		return boxes.ToArray();
	}

	private static bool IsBox(char[,] grid, (int x, int y) na)
	{
		return grid[na.x, na.y] is '[' or ']';
	}

	private static ((int x, int y) a, (int x, int y) b) GetBox(char[,] grid, (int x, int y) na)
	{
		var me = grid[na.x, na.y];
		var nb = me switch
		{
			'[' => grid.GetNeighborInDirection(na, Directions.Right)!.Value,
			']' => grid.GetNeighborInDirection(na, Directions.Left)!.Value,
			_ => throw new Exception()
		};

		if (me == '[')
		{
			return (na, nb);
		}

		return (nb, na);
	}

	private string Transform(string line)
	{
		var result = new StringBuilder();

		foreach (var c in line.ToCharArray())
		{
			if (c == '#')
			{
				result.Append("##");
			}
			else if (c == 'O')
			{
				result.Append("[]");
			}
			else if (c == '.')
			{
				result.Append("..");
			}
			else if (c == '@')
			{
				result.Append("@.");
			}
			else
			{
				throw new Exception();
			}
		}
		return result.ToString();
	}

	private char[] Parse(string line)
	{
		return line.ToCharArray();
	}

}