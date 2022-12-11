using Common;

namespace ConsoleApp2022;

public class Day11 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 90882;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        foreach (var round in 1..20)
        {
            foreach (var monkey in input.Values)
            {
                monkey.Turn(input);
            }

            foreach (var monkey in input.Values)
            {
                ConsoleX.WriteLine($"Monkey {monkey.Number}: {string.Join(',', monkey.Items)}");
            }

        }

        foreach (var monkey in input.Values)
        {
            ConsoleX.WriteLine($"Monkey {monkey.Number} inspects items {monkey.InspectedItems} times.");
        }

        return input.Values.Select(monkey => (long)monkey.InspectedItems).OrderByDescending(v => v).Take(2)
            .Aggregate((one, two) => one * two);
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 30893109657;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        var commonMultiple = input.Values.Select(m => m.Test).Aggregate((one, two) => one * two) ;

        foreach (var round in 1..10000)
        {
            foreach (var monkey in input.Values)
            {
                monkey.Turn(input, commonMultiple);
            }

            if (round % 1000 == 0 || round == 1 || round == 20)
            {
                Console.WriteLine($"== After round {round} ==");
                foreach (var monkey in input.Values)
                {
                    Console.WriteLine($"Monkey {monkey.Number} inspects items {monkey.InspectedItems} times.");
                }
            }
        }

       

        return input.Values.Select(monkey => (long)monkey.InspectedItems).OrderByDescending(v => v).Take(2)
            .Aggregate((one, two) => one * two);
    }

    public Dictionary<int, Monkey> Parse(string[] lines)
    {
        var result = new Dictionary<int, Monkey>();
        foreach (var block in lines.SplitByEmptyLines())
        {
            var monkey = new Monkey()
            {
                Number = int.Parse(block[0][7].ToString()),
                Items = block[1].Trim().Split(':')[1].Trim().Split(',').Select(i => long.Parse(i.Trim())).ToList(),
                Operation = block[2].Trim().Split(':')[1].Trim().Split('=')[1].Trim(),
                Test = int.Parse(block[3].Trim().Split(' ')[3].Trim()),
                IfTrue = int.Parse(block[4].Trim().Split(' ')[5].Trim()),
                IfFalse = int.Parse(block[5].Trim().Split(' ')[5].Trim())
            };
            result.Add(monkey.Number, monkey);
        }

        return result;
    }

    public class Monkey
    {
        private readonly ExpressionEvaluator _evaluator;
        private readonly string _operation;
        private readonly Func<object, decimal> _expression;

        public Monkey()
        {
            _evaluator = new ExpressionEvaluator();
        }

        public int Number { get; init; }
        public List<long> Items { get; init; }

        public string Operation
        {
            get => _operation;
            init
            {
                _operation = value;
                _expression = _evaluator.Compile(Operation);
            }
        }

        public int Test { get; init; }

        public int IfTrue { get; init; }
        public int IfFalse { get; init; }

        public int InspectedItems { get; private set; }

        public void Turn(Dictionary<int, Monkey> monkeys, int commonMultiple = 0)
        {
           
            ConsoleX.WriteLine($"Monkey {Number}");
            foreach (var item in Items)
            {
                InspectedItems++;
                ConsoleX.WriteLine($"  Monkey inspects an item with a worry level of {item}.");
                var @new = _expression(new { old = item });
                var worry = (long)@new; 
                ConsoleX.WriteLine($"    Worry level is {Operation} from {item} to {worry}");
                if (commonMultiple == 0)
                {
                    worry /= 3;
                    ConsoleX.WriteLine($"    Monkey gets bored with item, Worry level is divided by 3 to {worry}");
                }
                else
                {
                    if (worry > commonMultiple)
                    {
                        worry %= commonMultiple;
                    }
                }


                var test = worry % Test == 0;
                if (test)
                {
                    ConsoleX.WriteLine($"    Current worry level is divisible by {Test}");
                    ConsoleX.WriteLine($"    Item with worry leel {worry} is thrown to monkey {IfTrue}");
                    monkeys[IfTrue].Items.Add(worry);
                }
                else
                {
                    ConsoleX.WriteLine($"    Current worry level is not divisible by {Test}");
                    ConsoleX.WriteLine($"    Item with worry level {worry} is thrown to monkey {IfFalse}");

                    monkeys[IfFalse].Items.Add(worry);
                }
            }

            Items.Clear();
        }
    }
}