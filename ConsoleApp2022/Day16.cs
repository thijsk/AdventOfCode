using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IO;
using Common;

namespace ConsoleApp2022;

public class Day16 : IDay
{
    public Day16()
    {
        PuzzleContext.Answer1 = 1651;
        PuzzleContext.Answer2 = 1707;
        PuzzleContext.UseExample = false;
    }

    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToDictionary(v => v.Name);
        var start = input["AA"];
        var opened = new List<Valve>();
        var timeLeft = 30L;

        var totalValue = TotalValue(input, opened, timeLeft, start);

        return totalValue;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToDictionary(v => v.Name);
        var start = input["AA"];
        var timeLeft = 26;

        var allValves = input.Values.Where(v => v.FlowRate > 0).ToList();
        var partitions = allValves.GetAllBinaryPartitions().Where(p => p.first.Count > 2 && p.second.Count > 2).ToList();

        var totalValue = 0L;

        ConsoleX.WriteLine($"Valves: {allValves.Count()} Partitions: {partitions.Count()}");

        var counter = 0;

        foreach ( var partition in partitions )
        {
            if (++counter % 1000 == 0)
            {
                ConsoleX.WriteLine($"Counter {counter}: {totalValue}");
            }

            var valueMe = TotalValue(input, partition.first.ToList(), timeLeft, start);
            var valueElephant = TotalValue(input, partition.second.ToList(), timeLeft, start);
            var value = valueElephant + valueMe;
            
            if (value > totalValue)
            {
                totalValue = value;
                ConsoleX.WriteLine($"New best value: {value}", ConsoleColor.DarkYellow);
            }
            
        };
        

        return totalValue;
    }

    private long TotalValue(Dictionary<string, Valve> input, IList<Valve> opened, long timeLeft, Valve start)
    {
        if (timeLeft <= 0)
        {
            return 0;
        }

        var toOpen = input.Values.Where(v => v.FlowRate > 0 && !opened.Contains(v)).ToList();
        
        if (!toOpen.Any())
        {
            return 0;
        }

        var bestvalue = 0L;
        foreach (var candidate in toOpen)
        {
            var path = Dijkstra(input, start, candidate);
            var openedCopy = new List<Valve>(opened) {candidate};
            var newTimeLeft = timeLeft - (path.Count + 1);
            var value = candidate.FlowRate * newTimeLeft + TotalValue(input, openedCopy, newTimeLeft , candidate);

            if (value > bestvalue)
            {
                bestvalue = value;
            }
        }
        return bestvalue;
    }

    
    private readonly Dictionary<(Valve, Valve), List<Valve>> _dijkstraCache = new ();

    public List<Valve> Dijkstra(Dictionary<string,Valve> input, Valve start, Valve goal)
    {
        if (_dijkstraCache.ContainsKey((start, goal)))
        {
            return _dijkstraCache[(start, goal)];
        }

        PriorityQueue<Valve, long> frontier = new();

        var pathWeight = new Dictionary<Valve, long>();
        pathWeight.Add(start, 0);

        frontier.Enqueue(start, 0);
        var visited = new List<Valve>();
        var movemap = new Dictionary<Valve, Valve>();

        while (frontier.Count > 0)
        {
            //move
            var current = frontier.Dequeue();
            visited.Add(current);

            if (current.Equals(goal))
            {
                break;
            }

            // explore
            var neighbors = current.LeadsTo;
            foreach (var neighbor in neighbors.Select(n => input[n]).Where(n => !visited.Contains(n)))
            {
                var currentWeigth = pathWeight[current];
                var neighborPathWeight = currentWeigth + 1;

                if (!pathWeight.ContainsKey(neighbor) || pathWeight[neighbor] > neighborPathWeight)
                {
                    frontier.Enqueue(neighbor, neighborPathWeight);

                    pathWeight.AddOrSet(neighbor, neighborPathWeight);
                    
                    if (movemap.ContainsKey(neighbor))
                    {
                        movemap[neighbor] = current;
                    }
                    else
                    {
                        movemap.Add(neighbor, current);
                    }
                }
            }
        }

        var path = new List<Valve>();
        // Backtrack
        var prev = goal;
        if (movemap.ContainsKey(prev))
        {
            while (!prev.Equals(start))
            {
                path.Add(prev);
                prev = movemap[prev];
            }
        }

        path.Reverse();

        _dijkstraCache.Add((start,goal), path);

        return path;
    }

    public Valve Parse(string line)
    {
        var spaces = line.Split(' ');
        var name = spaces[1].Trim();
        var rate = int.Parse(spaces[4].Split('=')[1].Replace(";", ""));
        var valves = line.Replace("valve ","valves ").Split("valves")[1].Trim().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim());
        return new Valve(name, rate, valves);
    }
    
    public readonly struct Valve: IEquatable<Valve>
    {
        public Valve(string name, int flowRate, IEnumerable<string> leadsTo)
        {
            Name = name;
            FlowRate = flowRate;
            LeadsTo = new List<string>(leadsTo);
        }

        public readonly string Name;
        public readonly int FlowRate;
        public readonly List<string> LeadsTo;
        
        public override bool Equals(object? obj)
        {
            return obj is Valve other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, FlowRate, LeadsTo);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Valve other)
        {
            return Name == other.Name;
        }
    }

}