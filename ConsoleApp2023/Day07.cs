using System.Text;
using Common;

namespace ConsoleApp2023;

public class Day07 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 248836197;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var ordered = input.OrderBy(input => input.type).ThenBy(input => input.hand);
		var values = ordered.Select((v, i) => (inp: v, value: (i+1) * v.bid, index: i));
		
		return values.Sum(v => v.value);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 251195607;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse2).ToArray();

		var ordered = input.OrderBy(input => input.type).ThenBy(input => input.hand);
		var values = ordered.Select((v, i) => (inp: v, value: (i + 1) * v.bid, index: i));

		return values.Sum(v => v.value);
	}

	public (string hand, long bid, int type) Parse(string line)
	{
		var hand = string.Join("", line.Split(" ").First().Trim().Select(CardToChar));
		var bid = long.Parse(line.Split(" ").Last().Trim());
		var type = FindType(hand);
		return (hand, bid, type);
	}
	public (string hand, long bid, int type) Parse2(string line)
	{
		var hand = string.Join("", line.Split(" ").First().Trim().Select(CardToChar2));
		var bid = long.Parse(line.Split(" ").Last().Trim());
		var type = FindType2(hand);
		return (hand, bid, type);
	}

	private int FindType2(string hand)
	{
		var hands = SolveJokers(hand);

		var types = hands.Select(FindType).ToList();
		return types.Max();
	}

	private IEnumerable<string> SolveJokers(string hand)
	{
		// if hand contains a joker, replace it with all possible cards
		var jokers = hand.Count(c => c == 'A');
		if (jokers == 0)
		{
			yield return hand;
		}
		else
		{
			var cards = "BCDEFGHIJKLM".ToCharArray();
			foreach (var card in cards)
			{
				var index = hand.IndexOf('A');
				var newHandBuilder = new StringBuilder(hand);
				newHandBuilder[index] = card;
				foreach (var solvedHand in SolveJokers(newHandBuilder.ToString()))
				{
					yield return solvedHand;
				}
			}
		}
	}

	private int FindType(string hand)
	{
		var groups = hand.GroupBy(c => c).OrderBy(g => g.Count()).ToList();
		if (groups.Count == 1)
		{
			// Five of a kind
			return 7;
		} else if (groups.Count == 2)
		{
			if (groups.Last().Count() == 4)
			{
				// Four of a kind
				return 6;
			}
			else
			{
				// Full house
				return 5;
			}
		}
		else if (groups.Count == 3)
		{
			if (groups.Last().Count() == 3)
			{
				// Three of a kind
				return 4;
			}
			else
			{
				// Two pair
				return 3;
			}
		}
		else if (groups.Count == 4)
		{
			// One pair
			return 2;
		}
		else if (groups.Count == 5)
		{
			// High card
			return 1;
		}
		else
		{
			throw new ArgumentOutOfRangeException(nameof(hand), hand, null);
		}
	}

	private char CardToChar(char card)
	{
		return card switch
		{
			'2' => 'A',
			'3' => 'B',
			'4' => 'C',
			'5' => 'D',
			'6' => 'E',
			'7' => 'F',
			'8' => 'G',
			'9' => 'H',
			'T' => 'I',
			'J' => 'J',
			'Q' => 'K',
			'K' => 'L',
			'A' => 'M',
			_ => throw new ArgumentOutOfRangeException(nameof(card), card, null)
		};
	}

	private char CardToChar2(char card)
	{
		return card switch
		{
			'J' => 'A',
			'2' => 'B',
			'3' => 'C',
			'4' => 'D',
			'5' => 'E',
			'6' => 'F',
			'7' => 'G',
			'8' => 'H',
			'9' => 'I',
			'T' => 'J',
			'Q' => 'K',
			'K' => 'L',
			'A' => 'M',
			_ => throw new ArgumentOutOfRangeException(nameof(card), card, null)
		};
	}


}