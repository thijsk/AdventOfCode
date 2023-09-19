namespace ConsoleApp2016;
/*
--- Day 3: Squares With Three Sides ---
Now that you can think clearly, you move deeper into the labyrinth of hallways and office furniture that makes up this part of Easter Bunny HQ. This must be a graphic design department; the walls are covered in specifications for triangles.

Or are they?

The design document gives the side lengths of each triangle it describes, but... 5 10 25? Some of these aren't triangles. You can't help but mark the impossible ones.

In a valid triangle, the sum of any two sides must be larger than the remaining side. For example, the "triangle" given above is impossible, because 5 + 10 is not larger than 25.

In your puzzle input, how many of the listed triangles are possible?
 */
public class Day03 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 862;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		int count = 0;
		foreach (var triangle in input)
		{
			int x = triangle[0];
			int y = triangle[1];
			int z = triangle[2];

			if (x + y > z && x + z > y && y + z > x) count++;
		}	
		
		return count;
	}


	/*
	 Now that you've helpfully marked up their design documents, it occurs to you that triangles are specified in groups of three vertically. Each set of three numbers in a column specifies a triangle. Rows are unrelated.

For example, given the following specification, numbers with the same hundreds digit would be part of the same triangle:

101 301 501
102 302 502
103 303 503
201 401 601
202 402 602
203 403 603
In your puzzle input, and instead reading by columns, how many of the listed triangles are possible?
	 */
	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		List<int[]> transposed = new();
		for (int i = 0; i < input.Length; i += 3)
		{
			var one = input[i];
			var two = input[i + 1]; 
			var three = input[i + 2];

			transposed.Add(new[] { one[0], two[0], three[0] });
			transposed.Add(new[] { one[1], two[1], three[1] });
			transposed.Add(new[] { one[2], two[2], three[2] });
		}

		int count = 0;
		foreach (var triangle in transposed)
		{
			int x = triangle[0];
			int y = triangle[1];
			int z = triangle[2];

			if (x + y > z && x + z > y && y + z > x) count++;
		}

		return count;
	}

	public int[] Parse(string line)
	{
		return line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i.Trim())).ToArray();
	}

}