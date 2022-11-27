using System.Collections;
using System.Collections.Concurrent;
using Common;

namespace ConsoleApp2021;

public class Day06 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.SelectMany(Parse).ToList();

        var day = 0;
        //Print(day, input);
        while (day < 80)
        {
            var newFish = new List<Fish>();

            foreach (var fish in input)
            {
                fish.Age--;
                if (fish.Age < 0)
                {
                    fish.Age = 6;
                    newFish.Add(new Fish(8));
                }
            }


            input.AddRange(newFish);
            day++;

            //Print(day, input);
        }

        return input.Count;
    }

    private void Print(int day, List<Fish> input)
    {
        Console.WriteLine($"Day {day}: {string.Join(',', input)}");
    }

    public long Part2()
    {
        var sea = new List<long>(9);
        sea.AddRange(Enumerable.Repeat((long)0, 9));

        var input = PuzzleContext.Input.SelectMany(Parse).GroupBy(fish => fish.Age);

        foreach (var group in input)
        {
            sea[group.Key] = group.Count();
        }

        var day = 0;
        var last = 0;
        while (day < 256)
        {
            var newFish  = sea[0];

            sea.RemoveAt(0);
            sea[6] += newFish;
            sea.Add(newFish);
            day++;
           // Console.WriteLine($"Day {day} : {sea.Sum()} {string.Join(',', sea)}");
        }

        return sea.Sum();
    }

    public IEnumerable<Fish> Parse(string line)
    {
        return line.Split(',').Select(v => new Fish(int.Parse(v)));
    }

}

public class Fish
{
    public Fish(int age)
    {
        Age = age;
    }

    public int Age { get; set; }

    public override string ToString()
    {
        return Age.ToString();
    }
}