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

		var zwires = wires.Where(x => x.Key.StartsWith("z")).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var xwires = wires.Where(x => x.Key.StartsWith("x")).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var ywires = wires.Where(x => x.Key.StartsWith("y")).OrderBy(x => x.Key).Select(x => x.Value).ToList();

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

		List<(string,string)> swaps = [("qwf", "cnk"), ("z14", "vhm"), ("z27", "mps"), ("z39","msq")];

		ApplySwaps(swaps, inputgates);

		var susgates = inputgates.Where(x => x.outputwire.StartsWith("z")).Where(x => x.operand != "XOR" || x.wire1.StartsWith('x') || x.wire1.StartsWith('y') || x.wire2.StartsWith('x') || x.wire2.StartsWith('y')).Where(x => x.outputwire != "z00").ToList();
		foreach (var zg in susgates)
		{
			ConsoleX.WriteLine($"{zg.wire1} {zg.operand} {zg.wire2} -> {zg.outputwire}");
		}

		// https://www.101computing.net/binary-additions-using-logic-gates/
		var carrywire = inputgates.Single(g => FindGate(g, "x00", "y00") && g.operand == "AND").outputwire;

		foreach (int bit in Enumerable.Range(1, 44))
		{
			string xwire = $"x{bit:D2}";
			string ywire = $"y{bit:D2}";
			string outputwire = $"z{bit:D2}";

			ConsoleX.WriteLine($"Bit {bit:D2} {xwire} {ywire} {carrywire}", ConsoleColor.Yellow);

			// There should an XOR gat for the x and y wires
			var sum1gate = inputgates.First(g => FindGate(g, xwire, ywire) && g.operand == "XOR");
			var sum1 = sum1gate.outputwire;
			
			var sum2gate = inputgates.FirstOrDefault(g => FindGate(g, carrywire, sum1) && g.operand == "XOR");
			var carry2gate = inputgates.FirstOrDefault(g => FindGate(g, carrywire, sum1) && g.operand == "AND");

			if ((sum2gate == default || sum2gate.outputwire != outputwire) && carry2gate == default)
			{
				// if both are wrong, the carrywire is probably wrong
				ConsoleX.WriteLine($"No sum2gate and carry2gate for {bit}: {carrywire} is probably wrong");
			}

			if (sum2gate == default)
			{
				ConsoleX.WriteLine($"No sum2gate for {bit}: {carrywire} XOR {sum1} -> {outputwire} ; {sum1} is probably wrong");

			} else if (sum2gate.outputwire != outputwire)
			{ 
				// see if I can find the gate that would have been the sum2gate
				var expectedsum2GateOnOutput = inputgates.Single(g => (g.outputwire == outputwire));
				ConsoleX.WriteLine($"Probable sum2gate for {bit}: {expectedsum2GateOnOutput.wire1} {expectedsum2GateOnOutput.operand} {expectedsum2GateOnOutput.wire2} -> {expectedsum2GateOnOutput.outputwire}");
				var expectedsum2GateOnInputs = inputgates.Single(g => FindGate(g, carrywire, sum1) && g.operand == "XOR");
				ConsoleX.WriteLine($"Probable sum2gate for {bit}: {expectedsum2GateOnInputs.wire1} {expectedsum2GateOnInputs.operand} {expectedsum2GateOnInputs.wire2} -> {expectedsum2GateOnInputs.outputwire}");

				if (expectedsum2GateOnOutput.wire1 != carrywire && expectedsum2GateOnOutput.wire2 == carrywire)
				{
					ConsoleX.WriteLine($"Try swapping outputs {sum1} and {expectedsum2GateOnOutput.wire1}");
				}
				if (expectedsum2GateOnOutput.wire2 != carrywire && expectedsum2GateOnOutput.wire2 == carrywire)
				{
					ConsoleX.WriteLine($"Try swapping outputs {sum1} and {expectedsum2GateOnOutput.wire2}");
				}
				if (expectedsum2GateOnOutput.wire1 != carrywire && expectedsum2GateOnOutput.wire2 != carrywire)
				{
					ConsoleX.WriteLine($"output to {carrywire} is probably wrong");
					ConsoleX.WriteLine($"Try swapping outputs {expectedsum2GateOnInputs.wire1} or {expectedsum2GateOnInputs.wire2} with {expectedsum2GateOnInputs.outputwire}");
				}
			}

			if (carry2gate == default)
			{
				ConsoleX.WriteLine($"No carry2gate for {bit}: {carrywire} AND {sum1}");
			}

			var carry1gate = inputgates.First(g => FindGate(g, xwire, ywire) && g.operand == "AND");
			
			var carry1 = carry1gate.outputwire;
			var carry2 = carry2gate.outputwire;

			var carrygate = inputgates.FirstOrDefault(g => FindGate(g, carry1, carry2) && g.operand == "OR");
			
			if (carrygate == default)
			{
				ConsoleX.WriteLine($"No carrygate for {bit}: {carry1} OR {carry2} -> {outputwire}");
				break;
			}

			carrywire = carrygate.outputwire;
			ConsoleX.WriteLine($"Carrywire {carrywire}");
		}

		var wires = CalculateStates(inputwires, inputgates);

		var xwires = wires.Where(x => x.Key.StartsWith("x")).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var ywires = wires.Where(x => x.Key.StartsWith("y")).OrderBy(x => x.Key).Select(x => x.Value).ToList();
		var zwires = wires.Where(x => x.Key.StartsWith("z")).OrderBy(x => x.Key).Select(x => x.Value).ToList();

		var zs = wires.Where(x => x.Key.StartsWith("z")).OrderBy(x => x.Key).Select(x => x.Key).ToList();

		var xvalue = WiresToLong(xwires);
		var yvalue = WiresToLong(ywires);
		var zvalue = WiresToLong(zwires);
		var expected = (xvalue + yvalue);
		Debug.Assert(zvalue != 45213383376616);
		Debug.Assert(zvalue == expected);

		var swapped = swaps.SelectMany(swaps => new List<string>() { swaps.Item1, swaps.Item2 }).OrderBy(x => x).ToList();

		Console.WriteLine(string.Join(',', swapped));

		return zvalue;
	}

	private void ApplySwaps(List<(string, string)> swaps, List<(string operand, string wire1, string wire2, string outputwire)> inputgates)
	{
		foreach (var swap in swaps)
		{
			var (wire1, wire2) = swap;
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

		while (todo.Any())
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