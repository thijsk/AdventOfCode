using System.Diagnostics;
using Common;

namespace ConsoleApp2022;

public class Day20 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 19559;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        List<Box> original = new();
        List<Box> sorted = new();

        foreach (var item in input)
        {
            var box = new Box(item);

            original.Add(box);
            sorted.Add(box);
        }

        var maxIndex = (original.Count);

        foreach (var box in original)
        {
            var oldindex = sorted.IndexOf(box);

            var newindex = (oldindex + box.Value) % (maxIndex -1);
            if (newindex < 0)
                newindex += (maxIndex - 1);

            sorted.RemoveAt(oldindex);
            sorted.Insert((int)newindex, box);

            //ConsoleX.WriteLine($"{box} moves from {oldindex} to {newindex}");
            //ConsoleX.WriteLine($"{string.Join(",", sorted)}");
        }

        var zeroBox = original.First(b => b.Value == 0);
        var indexOfZero = sorted.IndexOf(zeroBox);

        var indexOne = (indexOfZero + 1000) % maxIndex;
        if (indexOne < 0)
            indexOne += maxIndex;
        var indexTwo = (indexOfZero + 2000) % maxIndex;
        if (indexTwo < 0)
            indexTwo += maxIndex;
        var indexThree = (indexOfZero + 3000) % maxIndex;
        if (indexThree < 0)
                indexThree += maxIndex;

        var one = sorted[indexOne].Value;
        var two = sorted[indexTwo].Value;
        var three = sorted[indexThree].Value;

        ConsoleX.WriteLine($"{one} {two} {three}");

        //var result = 0;
        var result =  one + two + three + 0L;
        return result;
    }
    
    public long Part2()
    {
        PuzzleContext.Answer1 = 0;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        List<Box> original = new();
        List<Box> sorted = new();

        foreach (var item in input)
        {
            var box = new Box(item * 811589153L);

            original.Add(box);
            sorted.Add(box);
        }
        ConsoleX.WriteLine(string.Join(", ", sorted));

        var maxIndex = (original.Count);

        foreach (var round in 1..10)
        {
            foreach (var box in original)
            {
                var oldindex = sorted.IndexOf(box);

                var newindex = (oldindex + box.Value) % (maxIndex - 1);
                if (newindex < 0)
                    newindex += (maxIndex - 1);

                sorted.RemoveAt(oldindex);
                sorted.Insert((int) newindex, box);
            }
            
            ConsoleX.WriteLine($"After {round} round of mixing:");
            ConsoleX.WriteLine(string.Join(", ", sorted));
        }

        var zeroBox = original.First(b => b.Value == 0);
        var indexOfZero = sorted.IndexOf(zeroBox);

        var indexOne = (indexOfZero + 1000) % maxIndex;
        if (indexOne < 0)
            indexOne += maxIndex;
        var indexTwo = (indexOfZero + 2000) % maxIndex;
        if (indexTwo < 0)
            indexTwo += maxIndex;
        var indexThree = (indexOfZero + 3000) % maxIndex;
        if (indexThree < 0)
            indexThree += maxIndex;

        var one = sorted[indexOne].Value;
        var two = sorted[indexTwo].Value;
        var three = sorted[indexThree].Value;

        ConsoleX.WriteLine($"{one} {two} {three}");

        //var result = 0;
        var result = one + two + three + 0L;
        return result;
    }

    public int Parse(string line)
    {
        return int.Parse(line);
    }

    private class Box
    {
        public Box(long value)
        {
            Value = value;
        }

        public long Value { get; }

        public override string ToString()
        {
            return Value.ToString();

        }
    }

}
