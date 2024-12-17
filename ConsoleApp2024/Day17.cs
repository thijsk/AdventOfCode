using Common;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using TextCopy;

namespace ConsoleApp2024;

public class Day17 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines();
		var (a, b, c) = ParseRegisters(input[0]);
		var instructions = ParseInstruction(input[1]);

		var outputs = RunProgram(instructions, a, b, c);

		var result = string.Join(",", outputs);
		Console.WriteLine(result);

		if (result == "4,3,7,1,5,3,0,5,4")
			return 1;
		return 0;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 190384615275535;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines();
		var (a, b, c) = ParseRegisters(input[0]);
		var instructions = ParseInstruction(input[1]);
		var expected = string.Join(",", instructions);

		ConsoleX.WriteLine(expected);

		a = 35184372088832;

		while (true)
		{
			ConsoleX.Write($"Trying a: {a} ");

			var outputs = RunProgram(instructions, a, b, c);

			var actual = string.Join(",", outputs);
			ConsoleX.WriteLine($"{actual}");

			if (outputs.SequenceEqual(instructions))
			{
				ConsoleX.WriteLine($"Found a: {a}");
				return a;
			}

			if (instructions.Length > outputs.Count)
			{
				ConsoleX.WriteLine("*");
				a *= 2;
				continue;
			}

			if (instructions.Length == outputs.Count)
			{
				ConsoleX.Write("^");
				for (int o = outputs.Count - 1; o >= 0; o--)
				{
					if (instructions[o] != outputs[o])
					{
						ConsoleX.WriteLine($" Mismatch at {o}");
						// Thanks to https://www.reddit.com/r/adventofcode/comments/1hg38ah/comment/m2gkd6m/
						a += Math2.Pow(8, o);
						break;
					}
				}
				continue;
			}

			if (instructions.Length < outputs.Count)
			{
				ConsoleX.WriteLine("/");
				a /= 2;
				continue;
			}



			//a++;
		}

		return 0;
	}

	private static List<int> RunProgram(int[] instructions, long a, long b, long c)
	{
		var pointer = 0;

		List<int> outputs = new List<int>();

		int Read() { 
			var value = instructions[pointer];
			pointer++;
			return value;
		}

		long Combo(int read)
		{
			return read switch
			{
				0 => 0,
				1 => 1,
				2 => 2,
				3 => 3,
				4 => a,
				5 => b,
				6 => c,
				_ => throw new Exception()
			};
		}

		void Step()
		{
			switch (Read())
			{
				case 0: //adv
					var anumerator = a;
					var adenominator = (int)Math.Pow(2, Combo(Read()));
					a = anumerator / adenominator;
					break;
				case 1: //bxl
					b = b ^ Read();
					break;
				case 2: //bst
					b = Combo(Read()) & 7;
					break;
				case 3: //jnz
					if (a == 0)
						pointer++;
					else
						pointer = Read();
					break;
				case 4: //bxc
					b = b ^ c;
					Read();
					break;
				case 5: //out
					int output = (int)Combo(Read()) & 7;
					outputs.Add(output);
					break;
				case 6: //bdv
					var bnumerator = a;
					var bdenominator = (int)Math.Pow(2, Combo(Read()));
					b = bnumerator / bdenominator;
					break;
				case 7: // cdv
					var cnumerator = a;
					var cdenominator = (int)Math.Pow(2, Combo(Read()));
					c = cnumerator / cdenominator;
					break;
				default:
					throw new Exception();
			}
		}

		bool running = true;
		while (running)
		{
			Step();

			if (pointer >= instructions.Length)
				running = false;
		}

		//Console.WriteLine($"Registers: a: {a}, b: {b}, c: {c}");
		return outputs;
	}

	private int[] ParseInstruction(string[] lines)
	{
		Debug.Assert(lines.Length == 1);
		var instructions = lines[0].Split(": ")[^1].Split(',').Select(int.Parse);
		return instructions.ToArray();
	}

	private (long a, long b, long c) ParseRegisters(string[] strings)
	{
		Debug.Assert(strings.Length == 3);

		var a = int.Parse(strings[0].Split(": ")[^1]);
		var b = int.Parse(strings[1].Split(": ")[^1]);
		var c = int.Parse(strings[2].Split(": ")[^1]);

		return (a, b, c);
	}
}