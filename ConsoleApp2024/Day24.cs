using Common;
using System.Diagnostics;

namespace ConsoleApp2024;

public class Day24 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 45213383376616;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines();

		var inputgates = input[1].Select(ParseGate).ToList();
		var inputwires = input[0].Select(ParseWire).ToDictionary(x => x.Key, x => x.Value);

		var wires = CalculateStates(inputwires, inputgates);

		var zwires = wires.Where(x => x.Key.StartsWith('z')).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var xwires = wires.Where(x => x.Key.StartsWith('x')).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var ywires = wires.Where(x => x.Key.StartsWith('y')).OrderBy(x => x.Key).Select(x => x.Value).ToList();

		var result = WiresToLong(zwires);

		return result;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 44663761780968;
		PuzzleContext.UseExample = false;
		var input = PuzzleContext.Input.SplitByEmptyLines();

		var inputgates = input[1].Select(ParseGate).ToList();
		var inputwires = input[0].Select(ParseWire).ToDictionary(x => x.Key, x => x.Value);

		List<(string, string)> swaps = [];
		
		// https://www.101computing.net/binary-additions-using-logic-gates/
		
		var carrywire = string.Empty;
		for (int bit = 0; bit < 45; bit++)
		{
			string xwire = $"x{bit:D2}";
			string ywire = $"y{bit:D2}";
			string outputwire = $"z{bit:D2}";

			if (bit == 0)
			{
				carrywire = inputgates.Single(g => FindGate(g, "x00", "y00") && g.operand == "AND").outputwire;
				continue;
			}

			ConsoleX.WriteLine($"Bit {bit:D2}: {xwire} {ywire} {carrywire}", ConsoleColor.Yellow);

			var zgate = inputgates.FirstOrDefault(g => g.outputwire == outputwire);

			var sum1gate = inputgates.FirstOrDefault(g => FindGate(g, xwire, ywire) && g.operand == "XOR");
			var carry1gate = inputgates.FirstOrDefault(g => FindGate(g, xwire, ywire) && g.operand == "AND");
			var sum2gate = inputgates.FirstOrDefault(g => FindGate(g, carrywire, sum1gate.outputwire) && g.operand == "XOR");
			var carry2gate = inputgates.FirstOrDefault(g => FindGate(g, carrywire, sum1gate.outputwire) && g.operand == "AND");
			var carry12gate = inputgates.FirstOrDefault(g => FindGate(g, carry1gate.outputwire, carry2gate.outputwire) && g.operand == "OR");

			ConsoleX.WriteLine($"zgate:       {zgate.wire1} {zgate.operand} {zgate.wire2} -> {zgate.outputwire}");
			ConsoleX.WriteLine($"sum1gate:    {sum1gate.wire1} {sum1gate.operand} {sum1gate.wire2} -> {sum1gate.outputwire}");
			ConsoleX.WriteLine($"carry1gate:  {carry1gate.wire1} {carry1gate.operand} {carry1gate.wire2} -> {carry1gate.outputwire}");
			ConsoleX.WriteLine($"sum2gate:    {sum2gate.wire1} {sum2gate.operand} {sum2gate.wire2} -> {sum2gate.outputwire}");
			ConsoleX.WriteLine($"carry2gate:  {carry2gate.wire1} {carry2gate.operand} {carry2gate.wire2} -> {carry2gate.outputwire}");
			ConsoleX.WriteLine($"carry12gate: {carry12gate.wire1} {carry12gate.operand}  {carry12gate.wire2} -> {carry12gate.outputwire}");

			if (zgate.wire1 == carrywire && zgate.wire2 != sum1gate.outputwire)
			{
				ConsoleX.WriteLine($"Swapping {zgate.wire2} && {sum1gate.outputwire}", ConsoleColor.Red);
				Swap(inputgates, zgate.wire2, sum1gate.outputwire);
				swaps.Add((zgate.wire2, sum1gate.outputwire));
				bit--;
				continue;
			}

			if (zgate.wire2 == carrywire && zgate.wire1 != sum1gate.outputwire)
			{
				ConsoleX.WriteLine($"Swapping {zgate.wire1} && {sum1gate.outputwire}", ConsoleColor.Red);
				Swap(inputgates, zgate.wire1, sum1gate.outputwire);
				swaps.Add((zgate.wire1, sum1gate.outputwire));
				bit--;
				continue;
			}

			if (sum2gate.outputwire != outputwire)
			{
				ConsoleX.WriteLine($"Swapping {sum2gate.outputwire} && {outputwire}", ConsoleColor.Red);
				Swap(inputgates, sum2gate.outputwire, outputwire);
				swaps.Add((sum2gate.outputwire, outputwire));
				bit--;
				continue;
			}

			carrywire = carry12gate.outputwire;
		}

		var wires = CalculateStates(inputwires, inputgates);

		var xwires = wires.Where(x => x.Key.StartsWith('x')).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var ywires = wires.Where(x => x.Key.StartsWith('y')).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var zwires = wires.Where(x => x.Key.StartsWith('z')).OrderBy(x => x.Key).Select(x => x.Value).ToList();

		var xvalue = WiresToLong(xwires);
		var yvalue = WiresToLong(ywires);
		var zvalue = WiresToLong(zwires);
		var expected = (xvalue + yvalue);
		Debug.Assert(zvalue != 45213383376616);
		Debug.Assert(zvalue == expected);

		var swapped = swaps.SelectMany(swaps => new List<string>() { swaps.Item1, swaps.Item2 }).OrderBy(x => x).ToList();
		var answer = string.Join(',', swapped);

		Debug.Assert(answer == "cnk,mps,msq,qwf,vhm,z14,z27,z39");
		Console.WriteLine(answer);

		return zvalue;
	}

	private static void ApplySwaps(List<(string, string)> swaps, List<(string operand, string wire1, string wire2, string outputwire)> inputgates)
	{
		foreach (var swap in swaps)
		{
			var (wire1, wire2) = swap;
			Swap(inputgates, wire1, wire2);
		}
	}

	private static void Swap(List<(string operand, string wire1, string wire2, string outputwire)> inputgates, string wire1, string wire2)
	{
		var gate1 = inputgates.Single(gate => gate.outputwire == wire1);
		var gate2 = inputgates.Single(gate => gate.outputwire == wire2);

		ConsoleX.WriteLine($"Swapped {gate1.wire1} {gate1.operand} {gate1.wire2} -> {gate1.outputwire}");
		ConsoleX.WriteLine($"With    {gate2.wire1} {gate2.operand} {gate2.wire2} -> {gate2.outputwire}");

		inputgates.Remove(gate1);
		gate1.outputwire = wire2;
		inputgates.Add(gate1);

		inputgates.Remove(gate2);
		gate2.outputwire = wire1;
		inputgates.Add(gate2);
	}

	private static bool FindGate((string operand, string wire1, string wire2, string outputwire) gate, string xwire, string ywire)
	{
		return (gate.wire1 == xwire && gate.wire2 == ywire) || (gate.wire1 == ywire && gate.wire2 == xwire);
	}

	private static long WiresToLong(List<int> wires)
	{
		long result = 0;
		var bitcount = 0;

		foreach (var wirevalue in wires)
		{
			var shifted = (long)wirevalue << bitcount;
			result |= shifted ;
			bitcount++;
		}

		return result;
	}

	private static Dictionary<string, int> CalculateStates(Dictionary<string, int> inputwires, List<(string operand, string wire1, string wire2, string outputwire)> inputgates)
	{
		var wires = inputwires.ToDictionary(wire => wire.Key, wire => wire.Value);
		var todo = new Queue<(string operand, string wire1, string wire2, string outputwire)>(inputgates);

		while (todo.Count > 0)
		{
			var (operand, wire1, wire2, outputwire) = todo.Dequeue();

			Debug.Assert(!wires.ContainsKey(outputwire));

			if (wires.TryGetValue(wire1, out var wire1value) && wires.TryGetValue(wire2, out var wire2value))
			{
				var outputvalue = operand switch
				{
					"AND" => wire1value & wire2value,
					"OR" => wire1value | wire2value,
					"XOR" => wire1value ^ wire2value,
					_ => throw new Exception("Unknown operand")
				};

				wires.Add(outputwire, outputvalue);
			}
			else
			{
				todo.Enqueue((operand, wire1, wire2, outputwire));
			}
		}

		return wires;
	}



	private (string operand, string wire1, string wire2, string outputwire) ParseGate(string line)
	{
		var result = line.Split(" ");
		return (result[1], result[0], result[2], result[4]);
	}

	private KeyValuePair<string, int> ParseWire(string line)
	{
		var result = line.Split(": ");
		return new KeyValuePair<string, int>(result[0], int.Parse(result[1]));
	}
}