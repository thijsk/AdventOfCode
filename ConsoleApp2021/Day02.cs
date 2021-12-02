using Common;

namespace ConsoleApp2021;

public class Day02 : IDay
{
    public long Part1()
    {
        var input = File.ReadAllLines("Day02.txt").Select(Parse).ToArray();

        var horizontal = 0;
        var depth = 0;

        foreach (var (instruction, value) in input)
        {
            if (instruction == "forward")
            {
                horizontal += value;
            }
            else if (instruction == "down")
            {
                depth+=value;
            }
            else if (instruction == "up")
            {
                depth-=value;
            }
        }

        return horizontal*depth;
    }

    public long Part2()
    {
        var input = File.ReadAllLines("Day02.txt").Select(Parse).ToArray();

        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        foreach (var (instruction, value) in input)
        {
            if (instruction == "forward")
            {
                horizontal += value;
                depth += (aim * value);
            }
            else if (instruction == "down")
            {
                aim += value;
            }
            else if (instruction == "up")
            {
                aim -= value;
            }
        }

        return horizontal * depth;
    }

    public (string,  int) Parse(string line)
    {
        var split = line.Split(' ');
        return (split[0], int.Parse(split[1]));
    }

}
