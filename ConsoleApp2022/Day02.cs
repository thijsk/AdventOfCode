using Common;

namespace ConsoleApp2022;

public class Day02 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();
        var totalscore = 0L;
        foreach (var game in input)
        {
            var score = GetScore(game);
            totalscore += score;
        }

        return totalscore;
    }

    private static int GetScore(char[] game)
    {
        // A for Rock, B for Paper, and C for Scissors. 
        // X for Rock, Y for Paper, and Z for Scissors.
        var score = game switch
        {
            [var other, 'X'] => 1 + (other == 'A' ? 3 : other == 'C' ? 6 : 0),
            [var other, 'Y'] => 2 + (other == 'B' ? 3 : other == 'A' ? 6 : 0),
            [var other, 'Z'] => 3 + (other == 'C' ? 3 : other == 'B' ? 6 : 0),
            _ => 0
        };
        return score;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();
        var totalscore = 0L;

        foreach (var game in input)
        {
            // A for Rock, B for Paper, and C for Scissors. 
            // X for Rock, Y for Paper, and Z for Scissors.
            // X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win.
            var move = game switch
            {
                [var other, 'X'] => other == 'A' ? 'Z' : other == 'B' ? 'X' : 'Y',
                [var other, 'Y'] => other == 'A' ? 'X' : other == 'B' ? 'Y' : 'Z',
                [var other, 'Z'] => other == 'A' ? 'Y' : other == 'B' ? 'Z' : 'X',
                _ => ' '
            };
            game[1] = move;
            totalscore += GetScore(game);
        }

        return totalscore;
    }

    public char[] Parse(string line)
    {
        return line.Split(' ').Select(s => s[0]).ToArray();
    }

}