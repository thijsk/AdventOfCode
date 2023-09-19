namespace ConsoleApp2016;

public class Day08 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 115;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var grid = new bool[6, 50];

		foreach (var instruction in input)
		{
			instruction.Apply(ref grid);
			grid.ToConsole(b => b ? '#' : '.');
			ConsoleX.WriteLine();
		}

		// count all the "true" values in the grid;
		var count = 0;
		for (var x = 0; x < grid.GetLength(0); x++)
		{
			for (var y = 0; y < grid.GetLength(1); y++)
			{
				if (grid[x, y])
				{
					count++;
				}
			}
		}
		return count;
	}
	
	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		return 0;
	}

	private IInterface Parse(string line)
	{
		if (line.StartsWith("rect"))
		{
			return Rect.Parse(line);
		}
		else if (line.StartsWith("rotate"))
		{
			return Rotate.Parse(line);
		}
		throw new Exception("Unknown instruction");
	}
}

public interface IInterface
{
	void Apply(ref bool[,] grid);
}

public class Rect : IInterface
{
	public int Width { get; set; }
	public int Height { get; set; }

	public static Rect Parse(string line)
	{
		var xy = line.Split(' ')[1].Split('x');
		return new Rect()
		{
			Width = int.Parse(xy[0]),
			Height = int.Parse(xy[1])
		};
	}

	public void Apply(ref bool[,] grid)
	{
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				grid[y, x] = true;
			}
		}	
	}
}

public abstract class Rotate : IInterface
{
	public int Index { get; set; }

	public int By { get; set; }

	public abstract void Apply(ref bool[,] grid);

	public static Rotate Parse(string line)
	{
		var indexString = line.Split(' ')[2].Split('=')[1];
		var amountString = line.Split(' ')[4];
		var dirString = line.Split(' ')[1];

		Rotate? rotate = dirString switch
		{
			"row" => new RotateRow(),
			"column" => new RotateCol(),
			_ => throw new InvalidOperationException()
		};

		rotate.Index = int.Parse(indexString);
		rotate.By = int.Parse(amountString);

		return rotate;
	}
}

public class RotateRow : Rotate
{
	public override void Apply(ref bool[,] grid)
	{
		var x = this.Index;

		var newValues = new bool[grid.GetLength(1)];

		for (var i = 0; i < grid.GetLength(1); i++)
		{
			var newIndex = (i + By) % grid.GetLength(1);

			newValues[newIndex] = grid[x, i];
		}

		for (var i = 0; i < grid.GetLength(1); i++)
		{
			grid[x, i] = newValues[i];
		}
	}
}

public class RotateCol : Rotate
{
	public override void Apply(ref bool[,] grid)
	{
		var y = this.Index;
		
		var newValues = new bool[grid.GetLength(0)];

		for (var i = 0; i < grid.GetLength(0); i++)
		{
			var newIndex = (i + By) % grid.GetLength(0);

			newValues[newIndex] = grid[i, y];
		}

		for (var i = 0; i < grid.GetLength(0); i++)
		{
			grid[i, y] = newValues[i];
		}
		
	}
}
