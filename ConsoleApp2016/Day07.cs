using Common;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp2016;

public class Day07 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var supportsTls = 0;
		foreach (var ipv7 in input)
		{
			var validSequences = ipv7.sequences.Any(HasValidAbba);
			var validHypernetSequences = ipv7.hypernetseqences.Any(HasValidAbba);

			var valid = validSequences && !validHypernetSequences;
			if (valid)
			{
				supportsTls++;
			}
		}

		return supportsTls;
	}

	private bool HasValidAbba(string seq)
	{
		for (int i = 0; i < seq.Length - 3; i++)
		{
			if (seq[i] == seq[i + 3] && seq[i + 1] == seq[i + 2] && seq[i] != seq[i + 1])
			{
				return true;
			}
		}

		return false;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var supportsSls = 0;
		foreach (var ipv7 in input)
		{
			var abaSequences = new List<string>();
			abaSequences = ipv7.sequences.SelectMany(GetAbaSequences).ToList();
			var reverseAbaSequences = abaSequences.Select(InvertAba);
			if (ipv7.hypernetseqences.Any(h => reverseAbaSequences.Any(h.Contains)))
			{
				supportsSls++;
			}
		}

		return supportsSls;
	}

	private string InvertAba(string arg)
	{ 
		return $"{arg[1]}{arg[0]}{arg[1]}";
	}

	private IList<string> GetAbaSequences(string arg)
	{
		var abaSequences = new List<string>();
		for (int i = 0; i < arg.Length - 2; i++)
		{
			if (arg[i] == arg[i + 2] && arg[i] != arg[i + 1])
			{
				abaSequences.Add(arg.Substring(i, 3));
			}
		}

		return abaSequences;
	}

	public (string line, string[] sequences, string[] hypernetseqences) Parse(string line)
	{
		// break up the line into sequences and hypernet sequences, hypernet sequences are enclosed in square brackets
		var sequences = new List<string>();
		var hypernetseqences = new List<string>();

		var currentSequence = new StringBuilder();
		var currentHypernetSequence = new StringBuilder();

		var inHypernet = false;

		foreach (var character in line)
		{
			if (character == '[')
			{
				Debug.Assert(!inHypernet);
				inHypernet = true;
				sequences.Add(currentSequence.ToString());
				currentSequence.Clear();
			}
			else if (character == ']')
			{
				inHypernet = false;
				hypernetseqences.Add(currentHypernetSequence.ToString());
				currentHypernetSequence.Clear();
			}
			else
			{
				if (inHypernet)
				{
					currentHypernetSequence.Append(character);
				}
				else
				{
					currentSequence.Append(character);
				}
			}
		}

		if (currentSequence.Length > 0)
		{
			sequences.Add(currentSequence.ToString());
		}

		if (currentHypernetSequence.Length > 0)
		{
			hypernetseqences.Add(currentHypernetSequence.ToString());
		}
		return (line, sequences.ToArray(), hypernetseqences.ToArray());

	}

}