using System.Collections.Concurrent;
using System.Text;
using Common;

namespace ConsoleApp2022;

public class Day19 : IDay
{
    private record State(int MinutesLeft, int[] Stock, int[] Robots)
    {
        protected State(State other)
        {
            MinutesLeft = other.MinutesLeft;
            Stock = (int[])other.Stock.Clone();
            Robots = (int[])other.Robots.Clone();
        }

        public virtual bool Equals(State? other)
        {
            if (other == null)
            {
                return false;
            }
            return MinutesLeft == other.MinutesLeft && Robots.SequenceEqual(other.Robots) && Stock.SequenceEqual(other.Stock);
        }

        public override int GetHashCode()
        {
            HashCode hashCode =new HashCode();
            hashCode.Add(MinutesLeft);
            foreach (var i in 0..3)
            {
                hashCode.Add(Stock[i]);
                hashCode.Add(Robots[i]);
            }
            return hashCode.ToHashCode();
        }

        protected virtual bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"Minutes {MinutesLeft}");
            builder.Append(" | ");
            builder.Append($"Robots {string.Join(", ", Robots)}");
            builder.Append(" | ");
            builder.Append($"Stock {string.Join(", ", Stock)}");
            return true;
        }
    }

    public long Part1()
    {
        PuzzleContext.Answer1 = 1659;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var resultLock = new object();
        var result = 0L;
        Parallel.ForEach(Partitioner.Create(input),(blueprint)=>
        {
            var state = new State(24, new int[4], new int[4]);
            state.Robots[(int)Resource.ore]++;

            var best = GetBestResult(blueprint, state);

            lock (resultLock)
            {
                ConsoleX.WriteLine($"{blueprint.Id} : {best}");
                result += best * blueprint.Id;
            }
        });

        return result;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 0;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        var resultLock = new object();
        var result = 1L;
        Parallel.ForEach(Partitioner.Create(input.Take(3)), (blueprint) =>
        {
            var state = new State(32, new int[4], new int[4]);
            state.Robots[(int)Resource.ore]++;

            var best = GetBestResult(blueprint, state);

            lock (resultLock)
            {
                ConsoleX.WriteLine($"{blueprint.Id} : {best}");
                result *= best;
            }
        });

        return result;
    }

    private int GetBestResult(BluePrint blueprint, State initstate)
    {
        var queue = new Stack<State>();
        var visited = new HashSet<int>();
        int best = 0;

        queue.Push(initstate);

        while (queue.Count > 0)
        {
            var oldState = queue.Pop();
            visited.Add(oldState.GetHashCode());

            if (oldState.MinutesLeft == 0)
            {
                best = Math.Max(best, oldState.Stock[(int)Resource.geode]);
                continue;
            }
            
            var newState = oldState with { MinutesLeft = oldState.MinutesLeft - 1 };
            newState.Stock[(int)Resource.ore] += newState.Robots[(int)Resource.ore];
            newState.Stock[(int)Resource.clay] += newState.Robots[(int)Resource.clay];
            newState.Stock[(int)Resource.obsidian] += newState.Robots[(int)Resource.obsidian];
            newState.Stock[(int)Resource.geode] += newState.Robots[(int)Resource.geode];

            if (newState.Stock[(int)Resource.ore] > (newState.MinutesLeft * blueprint.MaxNeeded[(int)Resource.ore]))
            {
                newState.Stock[(int)Resource.ore] = (newState.MinutesLeft * blueprint.MaxNeeded[(int)Resource.ore]);
            }
            if (newState.Stock[(int)Resource.clay] > (newState.MinutesLeft * blueprint.MaxNeeded[(int)Resource.clay]))
            {
                newState.Stock[(int)Resource.clay] = (newState.MinutesLeft * blueprint.MaxNeeded[(int)Resource.clay]);
            }
            if (newState.Stock[(int)Resource.obsidian] > (newState.MinutesLeft * blueprint.MaxNeeded[(int)Resource.obsidian]))
            {
                newState.Stock[(int)Resource.obsidian] = (newState.MinutesLeft * blueprint.MaxNeeded[(int)Resource.obsidian]);
            }

            if (Buy(blueprint, oldState, newState, out var buyStateGeode, Resource.geode))
            {
                if (!visited.Contains(buyStateGeode.GetHashCode()))
                {
                    queue.Push(buyStateGeode);
                    continue;
                }
            }

            if (Buy(blueprint, oldState, newState, out var buyStateObsidian, Resource.obsidian))
            {
                if (!visited.Contains(buyStateObsidian.GetHashCode()))
                {
                    queue.Push(buyStateObsidian);
                }

            }

            if (Buy(blueprint, oldState, newState, out var buyStateOre, Resource.ore))
            {
                if (!visited.Contains(buyStateOre.GetHashCode()))
                    queue.Push(buyStateOre);
            }

            if (Buy(blueprint, oldState, newState, out var buyStateClay, Resource.clay))
            {
                if (!visited.Contains(buyStateClay.GetHashCode()))
                    queue.Push(buyStateClay);
            }

            if (!visited.Contains(newState.GetHashCode()))
            {
                queue.Push(newState);
            }
        }

        return best;
    }

    private bool Buy(BluePrint blueprint, State oldState, State newState, out State buyState, Resource robotResource)
    {
        buyState = newState;

        if (oldState.Robots[(int) robotResource] >= blueprint.MaxNeeded[(int)robotResource])
        {
            return false;
        }

        var costs = blueprint.Robots[(int)robotResource].Cost;
        
        buyState = newState with { };
        buyState.Robots[(int)robotResource]++;
        foreach (var (resource, count) in costs)
        {
            if (oldState.Stock[(int)resource] < count)
                return false;
            buyState.Stock[(int)resource] -= count;
        }
        return true;
    }


    private BluePrint Parse(string line)
    {
        return BluePrint.Parse(line);
    }

    private enum Resource
    {
        ore = 0,
        clay = 1,
        obsidian = 2,
        geode = 3
    }

    private record BluePrint
    {
        public int Id { get; set; }
        public List<Robot> Robots { get; init; }

        public int[] MaxNeeded { get; init; }

        public static BluePrint Parse(string line)
        {

            var split = line.Split(':');
            var id = int.Parse(split[0].Replace("Blueprint ", ""));

            var robotlines = split[1].Trim().Split("Each", StringSplitOptions.RemoveEmptyEntries);
            var robots = new List<Robot>();
            foreach (var robotline in robotlines)
            {
                robots.Add(Robot.Parse(robotline));
            }

            var maxNeeded = new int[robots.Count];
            foreach (var resource in robots.Select(r => r.Resource))
            {
                if (resource == Resource.geode)
                {
                    maxNeeded[(int) resource] = int.MaxValue;
                }
                else
                {
                    maxNeeded[(int) resource] = robots.Where(r => r.Cost.ContainsKey(resource))
                        .Select(r => r.Cost[resource]).Max();
                }
            }
            
            return new BluePrint()
            {
                Id = id,
                Robots = robots,
                MaxNeeded = maxNeeded
            };
        }
    }

    private record struct Robot
    {
        public Resource Resource { get; init; }
        public Dictionary<Resource, int> Cost { get; init; }

        public static Robot Parse(string line)
        {
            var split = line.Split("robot costs");
            var resource = Enum.Parse<Resource>(split[0].Trim());

            var costlines = split[1].Trim().Replace(".", "").Split("and");

            var cost = new Dictionary<Resource, int>();
            foreach (var costline in costlines)
            {
                var costsplit = costline.Trim().Split(" ");
                cost.Add(Enum.Parse<Resource>(costsplit[1]), int.Parse(costsplit[0]));
            }

            return new Robot()
            {
                Resource = resource,
                Cost = cost
            };
        }
    }
}