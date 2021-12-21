using Common;

namespace ConsoleApp2021;

public class Day21 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var dice = new DeterministicDice();

        var p1 = input[0];
        var p2 = input[1];

        var s1 = 0L;
        var s2 = 0L;
        while (true)
        {
            var roll1 = dice.Roll() + dice.Roll() + dice.Roll();
            p1 += roll1;
            p1 = (p1 - 1) % 10 + 1;
            s1 += p1;
            //Console.WriteLine($"Player 1 rolls {roll1} and moves to space {p1} for a total score of {s1}");
            if (s1 >= 1000)
            {
                return s2 * dice.Rolls;
            }

            var roll2 = dice.Roll() + dice.Roll() + dice.Roll();
            p2 += roll2;
            p2 = (p2 - 1) % 10 + 1;
            s2 += p2;
            //Console.WriteLine($"Player 2 rolls {roll2} and moves to space {p2} for a total score of {s2}");
            if (s2 >= 1000)
            {
                return s1 * dice.Rolls;
            }
        }

        return 0;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();
        var p1 = (int)input[0];
        var p2 = (int)input[1];

        _playCache = new Dictionary<((int p1, long s1) pl1, (int p2, long s2) pl2), (long w1, long w2)>();
        var result = Play((p1, 0), (p2, 0));


        return Math.Max(result.w1, result.w2);
    }

    private Dictionary<((int p1, long s1) pl1, (int p2, long s2) pl2), (long w1, long w2)> _playCache;

    private readonly (int value, int universes)[] _quantumDieRolls = {
        (3, 1), (4, 3),(5, 6),(6, 7),(7, 6),(8, 3),(9, 1)
    };

    private (long w1, long w2) Play((int position, long score) pl1, (int position, long score) pl2)
    {
        if (pl2.score >= 21)
        {
            return (0, 1);
        }

        if (_playCache.TryGetValue((pl1, pl2), out var value))
        {
            return value;
        }

        var result= _quantumDieRolls.Aggregate((0L, 0L), ((long w1, long w2) total, (int value, int universes) roll) =>
        {
            var newposition = (pl1.position + roll.value - 1) % 10 + 1;
            var newscore = pl1.score + newposition;
            var wins = Play(pl2,(newposition, newscore)); // in & out reversed
            total.w1 += wins.w2 * roll.universes;
            total.w2 += wins.w1 * roll.universes;
            return total;
        });
        _playCache.Add((pl1, pl2), result);
        return result;
    }

    public long Parse(string line)
    {
        return long.Parse(line[^1].ToString());
    }

}

public class DeterministicDice
{
    private int _currentValue = 0;
    private int _rolls = 0;

    public int Roll()
    {
        _rolls++;
        _currentValue++;
        if (_currentValue == 101)
            _currentValue = 1;

        return _currentValue;
    }

    public int Rolls => _rolls;
}