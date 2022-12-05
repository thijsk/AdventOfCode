using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using Common;

namespace ConsoleApp2022;

public class Day05 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.SplitByEmptyLines();
        var stacks = GetStacks(input[0]);
        var moves = GetMoves(input[1]);

        foreach (var move in moves)
        {
            foreach (var count in 1..move.count)
            {
                var item = stacks[move.from-1].Pop();
                stacks[move.to-1].Push(item);
            }
        }

        var result = new StringBuilder();
        foreach (var stack in stacks)
        {
            result.Append(stack.Peek());
        }

        Console.WriteLine(result.ToString());

        return 0;
    }

    private List<(int count, int from, int to)> GetMoves(string[] lines)
    {
        
        // move 5 from 3 to 6
        List<(int count,int from,int to)> result = new();
        foreach (var line in lines)
        {
            var splits = line.Split(' ');
            var count = int.Parse(splits[1]);
            var from = int.Parse(splits[3]);
            var to = int.Parse(splits[5]);
            result.Add((count,from,to));
        }

        return result;
    }

    private List<Stack<char>> GetStacks(string[] lines)
    {
        /*
[B] [N] [J] [S] [Z] [W] [F] [W] [R]
 1   2   3   4   5   6   7   8   9 
         */

        var stacks = new List<List<char>>();
        foreach (var i in 1..9)
        {
            stacks.Add(new List<char>());   
        }
        foreach (var line in lines)
        {
            var stack = 0;
            for (var pos = 1; pos < line.Length; pos += 4)
            {
                var letter = line[pos];;
                if (char.IsAsciiLetter(letter))
                stacks[stack].Add(letter);
                stack++;
            }
        }

        var result = new List<Stack<char>>();
        foreach (var list in stacks)
        {
            list.Reverse();
            var stack = new Stack<char>(list);
            result.Add(stack);
        }

        

        return result;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.SplitByEmptyLines();
        var stacks = GetStacks(input[0]);
        var moves = GetMoves(input[1]);

        foreach (var move in moves)
        {
            var items = new List<char>();

            foreach (var count in 1..move.count)
            {
                items.Add(stacks[move.from - 1].Pop());
            }

            items.Reverse();

            foreach (var item in items)
            {
                stacks[move.to - 1].Push(item);
            }
        }

        var result = new StringBuilder();
        foreach (var stack in stacks)
        {
            result.Append(stack.Peek());
        }

        Console.WriteLine(result.ToString());

        return 0;
    }
    

}