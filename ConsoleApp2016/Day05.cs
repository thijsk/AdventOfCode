using Common;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp2016;

public class Day05 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.First();

		var index = 0;

		var password = "";

		MD5 md5 = MD5.Create();

		while (password.Length < 8)
		{
			var hash = md5.ComputeHash(Encoding.ASCII.GetBytes($"{input}{index}"));
			var hashStr = string.Concat(hash.Select(b => b.ToString("x2")));
			if (hashStr.StartsWith("00000"))
			{
				password += hashStr[5];
			}

			index++;
		}

		Console.WriteLine(password);
		return 0;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.First();
		var index = 0;

		var password = new char[8];

		MD5 md5 = MD5.Create();

		while (password.Any(c => c == '\0'))
		{
			var hash = md5.ComputeHash(Encoding.ASCII.GetBytes($"{input}{index}"));
			var hashStr = string.Concat(hash.Select(b => b.ToString("x2")));
			if (hashStr.StartsWith("00000"))
			{
				var pos = hashStr[5] - '0';
				if (pos < 8 && password[pos] == '\0')
				{
					password[pos] = hashStr[6];
				}
			}

			index++;
		}

		Console.WriteLine(password);
		return 0;
	}
}