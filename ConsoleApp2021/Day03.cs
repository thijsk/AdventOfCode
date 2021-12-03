using Common;

namespace ConsoleApp2021;

public class Day03 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.ToArray();

        var len = input.First().Length;

        var minresult = new char[len];
        var maxresult = new char[len];

        for (int pos = 0; pos < len; pos++)
        {
            var group = input.Select(line => line[pos]).GroupBy(c => c).OrderBy(g => g.Count());

            var minimum = group.First().Key;
            var maximum = group.Last().Key;

            minresult[pos] = minimum;
            maxresult[pos] = maximum;
        }

        Console.WriteLine(minresult);
        Console.WriteLine(maxresult);


        var gamma = Convert.ToInt64(new string(maxresult), 2);
        var epsilon = Convert.ToInt64(new string(minresult), 2);


        return gamma * epsilon;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.ToArray();

        var len = input.First().Length;

        var oxy = input.ToList();
        var co2 = input.ToList();

        for (int pos = 0; pos < len; pos++)
        {
            if (oxy.Count > 1)
            {
                var group = oxy.Select(line => line[pos]).GroupBy(c => c).OrderByDescending(g => g.Count())
                    .ThenByDescending(g => g.Key);
                oxy = oxy.Where(o => o[pos] == group.First().Key).ToList();
            }

            if (co2.Count > 1)
            {
                var group = co2.Select(line => line[pos]).GroupBy(c => c).OrderBy(g => g.Count())
                    .ThenBy(g => g.Key);
                co2 = co2.Where(o => o[pos] == group.First().Key).ToList();
            }

        }

        Console.WriteLine(oxy.First());
        Console.WriteLine(co2.First());

        var oxyrating = Convert.ToInt64(oxy.First(), 2);
        var co2rating = Convert.ToInt64(co2.First(), 2);


        return oxyrating * co2rating;
    }

}