using Common;

namespace ConsoleApp2024;

public class Day09 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 6344673854800;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input.First());

		var file = true;
		var fileIndex = 0;

		var fileSystem = new List<long>();

		foreach (var value in input)
		{
			if (file)
			{
				for (int i = 0; i < value; i++)
				{
					fileSystem.Add(fileIndex);
				}

				fileIndex++;
				file = false;
			}
			else
			{
				for (int i = 0; i < value; i++)
				{
					fileSystem.Add(-1);	
				}
				file = true;
			}
		}


		var firstFreeIndex = fileSystem.FindIndex(f => f == -1);
		var lastFileIndex = fileSystem.FindLastIndex(f => f != -1);
		while (firstFreeIndex < lastFileIndex)
		{
			fileSystem[firstFreeIndex] = fileSystem[lastFileIndex];
			fileSystem[lastFileIndex] = -1;

			firstFreeIndex = fileSystem.FindIndex(firstFreeIndex, f => f == -1);
			lastFileIndex = fileSystem.FindLastIndex(lastFileIndex,f => f != -1);
		}

		return fileSystem.Where(v => v != -1).Select((v, i) => v * i).Sum();
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 6360363199987;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input.First());

		var file = true;
		var fileIndex = 0;

		var fileSystem = new List<(long id, long size)>();

		foreach (var value in input)
		{
			if (file)
			{
				fileSystem.Add((fileIndex, value));

				fileIndex++;
				file = false;
			}
			else
			{
				if (value > 0)
				{
					fileSystem.Add((-1, value));
				}
				file = true;
			}
		}

		var fileId = fileIndex - 1;
		while (fileId >= 0)
		{
			var index = fileSystem.FindIndex(f => f.id == fileId);
			var fileSize = fileSystem[index].size;

			var firstFreeIndex = fileSystem.FindIndex(f => f.id == -1 && f.size >= fileSize);
			
			if (firstFreeIndex != -1 && firstFreeIndex < index) {
				var gapSize = fileSystem[firstFreeIndex].size;
				fileSystem[index] = (-1, fileSize);
				fileSystem[firstFreeIndex] = (fileId, fileSize);
				if (gapSize > fileSize)
				{
					fileSystem.Insert(firstFreeIndex + 1, (-1, gapSize - fileSize));
				}
			
			}

			fileId--;
		}

		//Print(fileSystem);

		long checksum = 0;
		long i = 0;
		foreach (var block in fileSystem)
		{
			if (block.id == -1)
			{
				i += block.size;
				continue;
			}

			for (int j = 0; j < block.size; j++)
			{
				checksum += block.id * i;
				i++;
			}
		}			
			
		return checksum;
	}

	void Print(List<(long id, long size)> fileSystem)
	{
		foreach (var block in fileSystem)
		{
			for (int j = 0; j < block.size; j++)
			{
				if (block.id == -1)
				{
					Console.Write(".");
				}
				else
				{
					Console.Write(block.id);
				}
			}
		}
	}

	private long[] Parse(string line)
	{
		return line.Select(c => long.Parse($"{c}")).ToArray();
	}

}