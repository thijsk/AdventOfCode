using Common;
using System.Collections.Generic;

namespace ConsoleApp2023;

public class Day20 : IDay
{
    const bool High = true;
    private const bool Low = false;

    public long Part1()
    {
        PuzzleContext.Answer1 = 743090292;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToDictionary(x => x.Name, x => x);

        foreach (var m in input)
        {
            var (name, module) = m;
            module.SetInputs(input.Values.Where(m => m.Outputs.Contains(name)).Select(m => m.Name));
        }

        List<bool> pulses = new();

        foreach (var _ in 1..1000)
        {
            pulses.AddRange(PushButton(input));
        }

        var high = pulses.Count(x => x == High);
        var low = pulses.Count(x => x == Low);
        ConsoleX.WriteLine($"Low: {low} High {high}");

        return high * (long)low;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 241528184647003;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToDictionary(x => x.Name, x => x);

        input.Add("rx", Parse("rx -> "));

        foreach (var m in input)
        {
            var (name, module) = m;
            module.SetInputs(input.Values.Where(m => m.Outputs.Contains(name)).Select(m => m.Name));
        }

        var inputOfRx = input["rx"].Inputs.First();
        var inputsOfRx = input[inputOfRx].Inputs.ToDictionary(x => x, _ => 0L);
        
        long push = 0;
        while(true)
        {
            push++;
            PushButton(input, d =>
            {
                var (i, o, s) = d;
                if (o == inputOfRx && s == High)
                {
                    inputsOfRx[i] = push;
                }
            });
            if (inputsOfRx.Values.All(x => x > 0))
            {
                foreach (var i in inputsOfRx)
                {
                    ConsoleX.WriteLine($"{i.Key} {i.Value}");
                }
                return Math2.LeastCommonMultiple(inputsOfRx.Values.ToArray());
            }
        }
    }

    private static IEnumerable<bool> PushButton(Dictionary<string, Module> input, Action<(string, string, bool)> onOutput = null)
    {
        List<bool> pulses = new();
        Queue<(string input, string output, bool pulse)> work = new();

        work.Enqueue(("button", "broadcaster", Low));

        while (work.TryDequeue(out var w))
        {
            var (i, o, p) = w;
            pulses.Add(p);
            //ConsoleX.WriteLine($"{i} -{(p ? "high" : "low")}-> {o}");

            if (!input.ContainsKey(o))
            {
                //ConsoleX.WriteLine($"No module for {o}");
                continue;
            }
            var module = input[o];
            
            foreach (var output in module.Pulse(i, p))
            {
                onOutput?.Invoke(output);
                work.Enqueue(output);
            }
        }

       // var high = pulses.Count(x => x == High);
       // var low = pulses.Count(x => x == Low);
       // ConsoleX.WriteLine($"Low: {low} High {high}");

        return pulses;
    }

    private Module Parse(string line)
    {
        return Module.Parse(line);
    }

    private class Module
    {
        public string Name;
        public char Type;
        public string[] Outputs = Array.Empty<string>();
        public string[] Inputs = Array.Empty<string>();

        private bool _state = Low;
        private Dictionary<string, bool> _inputStates = new();

        public static Module Parse(string line)
        {
            var (nameandtype, destinations) = line.Split("->", StringSplitOptions.TrimEntries);

            var name = nameandtype.Trim('%', '&');
            var type = nameandtype[0];
            var outputs = destinations.Split(",", StringSplitOptions.TrimEntries);

            return new Module()
            {
                Name = name,
                Type = type,
                Outputs = outputs
            };
        }

        public IEnumerable<(string input, string output, bool pulse)> Pulse(string sender, bool pulse)
        {
            return Type switch
            {
                '%' => //FlipFlop
                    FlipFlip(pulse),
                '&' => // Conjunction
                    Conjunction(sender, pulse),
                _ => BroadCast(pulse)
            };
        }



        private IEnumerable<(string input, string output, bool pulse)> Conjunction(string sender, bool pulse)
        {
            _inputStates.AddOrSet(sender, pulse);
            
            if (Inputs.All(_inputStates.ContainsKey) && _inputStates.Values.All(x => x == High))
            {
                return BroadCast(Low);
            }

            return BroadCast(High);
        }

        private IEnumerable<(string input, string output, bool pulse)> FlipFlip(bool pulse)
        {
            if (pulse == High)
            {
                return Enumerable.Empty<(string input, string output, bool pulse)>();
            }

            _state = !_state;
            return BroadCast(_state);
        }

        private IEnumerable<(string input, string output, bool pulse)> BroadCast(bool pulse)
        {
            foreach (var output in Outputs)
                yield return (Name, output, pulse);
        }

        public void SetInputs(IEnumerable<string> inputs)
        {
            Inputs = inputs.ToArray();
        }
    }

}