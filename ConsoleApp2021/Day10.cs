using System.Collections;
using System.Text.Json.Serialization.Metadata;
using Common;

namespace ConsoleApp2021;

public class Day10 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input;

        var stack = new Stack<char>();
        var score = 0;
        foreach (var line in input)
        {
            foreach (var chr in line)
            {
                if (IsOpen(chr))
                {
                    stack.Push(chr);
                }
                else if (IsClose(chr))
                {
                    if (Matches(stack.Peek(), chr))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        Console.WriteLine($"Expected {MatchOpen(stack.Peek())}, but found {chr} instead.");
                        score += ScoreOf(chr);
                        break;
                    }
                }
            }
        }

        return score;
    }

    private char MatchOpen(char chr)
    {
        return close[open.ToList().IndexOf(chr)];
    }

    private int ScoreOf(char chr)
    {
        return chr switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => 0
        };
    }

    private int ScoreOf2(char chr)
    {
        return chr switch
        {
            ')' => 1,
            ']' => 2,
            '}' => 3,
            '>' => 4,
            _ => 0
        };
    }

    private bool Matches(char peek, char chr)
    {
        return open.IndexOf(peek) == close.IndexOf(chr);
    }

    readonly List<char> close = new() { ')', ']', '}', '>' };
    private bool IsClose(char chr)
    {
        return close.Contains(chr);
    }

    readonly List<char> open = new() { '(', '[', '{', '<' };
    private bool IsOpen(char chr)
    {
        return open.Contains(chr);
    }

    public long Part2()
    {
        var input = PuzzleContext.Input;

        var scores = new List<long>();
        foreach (var line in input)
        {
            var stack = new Stack<char>();
            long linescore = 0;
            bool invalid = false;
            foreach (var chr in line)
            {
                if (IsOpen(chr))
                {
                    stack.Push(chr);
                }
                else if (IsClose(chr))
                {
                    if (Matches(stack.Peek(), chr))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        invalid = true;
                        break;
                    }
                }
            }

            if (invalid) continue;
            foreach (var chr in stack)
            {
                linescore *= 5;
                linescore += ScoreOf2(MatchOpen(chr));
            }

            // Console.WriteLine(linescore);
            scores.Add(linescore);
        }

        var orderedscores = scores.OrderBy(s => s);
        var mid = orderedscores.Count() / 2;

        return orderedscores.ToArray()[mid];
    }
}