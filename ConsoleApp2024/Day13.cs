using Common;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Z3;

namespace ConsoleApp2024;

public class Day13 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 29436;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines().Select(Parse);

		return input.Sum(SolveWithMath);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 103729094227877;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines().Select(l => ParseWithOffset(l, 10000000000000));

		var result= input.Sum(SolveWithMath);


		return result;
	}

	private long Solve(Puzzle puzzle)
	{
		// Use brute force
		for (long apress = 1; apress <= 100; apress++)
		{
			for (long bpress = 1; bpress <= 100; bpress++)
			{
				if (puzzle.ax * apress + puzzle.bx * bpress == puzzle.px && 
					puzzle.ay * apress + puzzle.by * bpress == puzzle.py)
				{
					return 3 * apress + bpress;
				}
			}
		}
		return 0;
	}

	private long SolveWithZ3(Puzzle puzzle)
	{
		// px = ax * apress + bx * bpress
		// py = ay * apress + by * bpress

		// Just like last year, this is not a fun puzzle
		// Lets just throw it in Z3

		using var ctx = new Context();
		var s = ctx.MkOptimize(); // optimize was optional

		var apress = ctx.MkIntConst("apress");
		var bpress = ctx.MkIntConst("bpress");
		var ax = ctx.MkInt(puzzle.ax);
		var ay = ctx.MkInt(puzzle.ay);
		var bx = ctx.MkInt(puzzle.bx);
		var by = ctx.MkInt(puzzle.by);
		var px = ctx.MkInt(puzzle.px);
		var py = ctx.MkInt(puzzle.py);

		s.Add(apress > 0);
		s.Add(bpress > 0);
		s.Add(ctx.MkEq(px, ax * apress + bx * bpress));
		s.Add(ctx.MkEq(py, ay * apress + by * bpress));

		var sum = 3 * apress + bpress;

		s.MkMinimize(sum);

		if (s.Check() != Status.SATISFIABLE) return 0;

		var m = s.Model;

		var result = m.Evaluate(sum);

		return ((IntNum)result).Int64;
	}

	private long SolveWithMath(Puzzle puzzle)
	{
		// px = ax * apress + bx * bpress
		// py = ay * apress + by * bpress

		var d = puzzle.ax * puzzle.by - puzzle.ay * puzzle.bx;
		var a = puzzle.px * puzzle.by - puzzle.bx * puzzle.py;
		var b = puzzle.py * puzzle.ax - puzzle.ay * puzzle.px;

		var apress = a / d;
		var bpress = b / d;

		if (a % d == 0 && b % d == 0)
			return 3 * apress + bpress;

		return 0;
	}

	private Puzzle Parse(string[] lines) => ParseWithOffset(lines, 0);

	private Puzzle ParseWithOffset(string[] lines,long offset)
	{
		//Button A: X + 94, Y + 34
		//Button B: X + 22, Y + 67
		//Prize: X = 8400, Y = 5400
		Debug.Assert(lines.Length == 3);

		var getNumbers = new Regex(@"([\d]+)\D*([\d]+)");

		var a = getNumbers.Matches(lines[0]);
		var b = getNumbers.Matches(lines[1]);
		var p = getNumbers.Matches(lines[2]);

		return new Puzzle(
			long.Parse(a[0].Groups[1].Value),
			long.Parse(a[0].Groups[2].Value),
			long.Parse(b[0].Groups[1].Value),
			long.Parse(b[0].Groups[2].Value),
			offset + long.Parse(p[0].Groups[1].Value),
			offset + long.Parse(p[0].Groups[2].Value)
		);
	}

	private record Puzzle(long ax, long ay, long bx, long by, long px, long py);

}