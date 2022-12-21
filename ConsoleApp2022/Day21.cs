using Common;

namespace ConsoleApp2022;

public class Day21 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 152479825094094;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToDictionary(m => m.Name);

        var root = input["root"];

        var queue = new Queue<Monkey>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var monkey = queue.Dequeue();

            if (!monkey.Number.HasValue)
            {
                queue.Enqueue(monkey);
                var first = input[monkey.First];
                var second = input[monkey.Second];
                if (!first.Number.HasValue)
                {
                    if (!queue.Contains(first))
                        queue.Enqueue(first);
                }

                if (!second.Number.HasValue)
                {
                    if (!queue.Contains(second))
                    {
                        queue.Enqueue(second);
                    }
                }

                if (first.Number.HasValue && second.Number.HasValue)
                {
                    var number = monkey.Op switch
                    {
                        "+" => first.Number + second.Number,
                        "-" => first.Number - second.Number,
                        "*" => first.Number * second.Number,
                        "/" => first.Number / second.Number
                    };
                    monkey.Number = number;
                }
            }
        }

        return root.Number.Value;
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 3360561285172;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToDictionary(m => m.Name);

        var root = input["root"];
        root.Op = "-";
        var human = input["humn"];

        bool equal = false;
        human.Number = 0;

        var increment = 1000000000000L;

        while (!equal)
        {
            human.Number += increment;
            foreach (var m in input.Values)
            {
                if (!string.IsNullOrEmpty(m.Op)) m.Number = null;
            }
            
            CheckForEquality(root, input);
            equal = root.Number == 0;
            if (equal)
            {
                break;
            }

            ConsoleX.WriteLine($"{human.Number} {increment} {root.Number}");
            if (root.Number < 0)
            {
                human.Number -= increment;
                increment = increment / 10;
            }
        }
       
        return human.Number.Value;
    }

    private static void CheckForEquality(Monkey human, Dictionary<string, Monkey> input)
    {
        var queue = new Queue<Monkey>();
        queue.Enqueue(human);

        while (queue.Count > 0)
        {
            var monkey = queue.Dequeue();

            if (!monkey.Number.HasValue)
            {
                queue.Enqueue(monkey);
                var first = input[monkey.First];
                var second = input[monkey.Second];
                if (!first.Number.HasValue)
                {
                    if (!queue.Contains(first))
                        queue.Enqueue(first);
                }

                if (!second.Number.HasValue)
                {
                    if (!queue.Contains(second))
                    {
                        queue.Enqueue(second);
                    }
                }

                if (first.Number.HasValue && second.Number.HasValue)
                {
                    var number = monkey.Op switch
                    {
                        "+" => first.Number + second.Number,
                        "-" => first.Number - second.Number,
                        "*" => first.Number * second.Number,
                        "/" => first.Number / second.Number
                    };
                    monkey.Number = number;
                }
            }
        }
    }

    public Monkey Parse(string line)
    {
        return Monkey.Parse(line);
    }

}

public record Monkey(string Name)
{
    public long? Number { get; set; }

    public string First { get; set; }
    public string Op { get; set; }
    public string Second { get; set; }

    public static Monkey Parse(string line)
    {
        var (name, operation) = line.Split(": ");

        var monkey = new Monkey(name);
        if (long.TryParse(operation, out long number))
        {
            monkey.Number = number;
        }
        else
        {
            var (first, op, second) = operation.Split(" ");
            monkey.First = first;
            monkey.Op = op;
            monkey.Second = second;
        }
        
        return monkey;
    }
}