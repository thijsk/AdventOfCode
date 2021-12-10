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
        switch (chr)
        {
            case ')': return 3;
            case ']': return 57;
            case '}': return 1197;
            case '>': return 25137;
        }

        return 0;

    }

    private int ScoreOf2(char chr)
    {
        switch (chr)
        {
            case ')': return 1;
            case ']': return 2;
            case '}': return 3;
            case '>': return 4;
        }

        return 0;

    }

    private bool Matches(char peek, char chr)
    {
        return open.ToList().IndexOf(peek) == close.ToList().IndexOf(chr);
    }

    readonly char[] close = new[] { ')', ']', '}', '>' };
    private bool IsClose(char chr)
    {
        return close.Contains(chr);
    }

    readonly char[] open = new[] { '(', '[', '{', '<' };
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

            Console.WriteLine(linescore);
            scores.Add(linescore);
        }

        var orderedscores = scores.OrderBy(s => s);
        var mid = orderedscores.Count() / 2;

        return orderedscores.ToArray()[mid];
    }
}