using System.Globalization;
using Common;

namespace ConsoleApp2021;

public class Day08 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse);

        return input.Sum(i => i.output.Count(d =>
        {
            d.GuessValue();
            return d.Value != -1;
        }));
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse);

        var sum = 0;
        foreach (var line in input)
        {
            line.input.ForEach(d =>
            {
                d.GuessValue();
            });

            var one = line.input.First(d => d.Value == 1);
            var four = line.input.First(d => d.Value == 4);
            var seven = line.input.First(d => d.Value == 7);
            var eight = line.input.First(d => d.Value == 8);

            var twothreefive = line.input.Where(d => d.Letters.Length == 5).ToList();
            var zerosixnine = line.input.Where(d => d.Letters.Length == 6).ToList();

            var three = twothreefive.First(d => d.Letters.Contains(one.Letters[0]) && d.Letters.Contains(one.Letters[1]));
            three.Value = 3;

            var twofive = twothreefive.Where(d => d.Letters != three.Letters);

            var threehorizontal = three.Letters.Where(l => !one.Letters.Contains(l)).ToArray();
            var zero = zerosixnine.First(d => !threehorizontal.All(l => d.Letters.Contains(l)));
            zero.Value = 0;

            var sixnine = zerosixnine.Where(d => d.Letters != zero.Letters);

            var nine = sixnine.First(d => four.Letters.All(l => d.Letters.Contains(l)));
            nine.Value = 9;

            var six = sixnine.First(d => d.Letters != nine.Letters);
            six.Value = 6;

            var five = twofive.First(d => six.Letters.Count(l => d.Letters.Contains(l)) == 5);
            five.Value = 5;

            var two = twofive.First(d => d.Letters != five.Letters);
            two.Value = 2;


            foreach (var digit in line.output)
            {
                digit.Value = line.input.First(d => d.Letters.Sort() == digit.Letters.Sort()).Value;
            }

            var output = (line.output[0].Value * 1000) +
                         (line.output[1].Value * 100) +
                         (line.output[2].Value * 10) +
                         (line.output[3].Value);
            sum += output;
        }

        return sum;
    }

    public (List<Digit> input, List<Digit> output) Parse(string line)
    {
        var split = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
        var input = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(letters => new Digit(letters)).ToList();
        var output = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(letters => new Digit(letters)).ToList();
        return (input, output);
    }

}

public class Digit
{
    public int Value = -1;
    public readonly string Letters;

    public Digit(string letters)
    {
        Letters = letters;
    }

    public void GuessValue()
    {
        switch (Letters.Length)
        {
            case 2: Value = 1;
                break;
            case 3: Value = 7;
                break;
            case 4: Value = 4;
                break;
            case 7: Value = 8;
                break;
        }
    }
}