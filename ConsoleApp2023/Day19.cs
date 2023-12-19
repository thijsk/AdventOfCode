using Common;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

namespace ConsoleApp2023;

public class Day19 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 446935;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        List<Part> A = new();
        List<Part> R = new();

        foreach (var part in input.parts)
        {
           ApplyRulesToPart(part, input.flows, A, R);
        }

        return A.Sum(p => p.x + p.m + p.a + p.s);
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 141882534122898;
        PuzzleContext.UseExample = false;

        var input = Parse(PuzzleContext.Input);

        var workflows = input.flows;

        var sum = ProcessRecursive(workflows, "in", 1..4000, 1..4000, 1..4000, 1..4000);

        return sum;
    }

    private long ProcessRecursive(Dictionary<string, Workflow> workflows, string wf,
        Range x, Range m, Range a, Range s)
    {
        if (x.Size() < 1 || m.Size() < 1 || a.Size() < 1 || s.Size() < 1)
        {
            return 0;
        }

        if (wf == "A")
        {
            long xSize = x.Size();
            long mSize = m.Size();
            long aSize = a.Size();
            long sSize = s.Size();
            var product = xSize * mSize * aSize * sSize;
            Debug.Assert(product > 0);
            return product;
        }

        if (wf == "R")
        {
            return 0;
        }
        
        long sum = 0;
        var workflow = workflows[wf];

        ConsoleX.WriteLine($"WF {wf} : {x} {m} {a} {s}");

        foreach (var rule in workflow.Rules)
        {
            ConsoleX.WriteLine($"Rule {rule.Expression} : {rule.Destination}");

            if (rule.Expression == null)
            {
                sum += ProcessRecursive(workflows, rule.Destination, x, m, a, s);
                return sum;
            }

            var (variable, op, value) = (rule.Expression.Variable, rule.Expression.Operator, (int)rule.Expression.Value);

            if (variable == 'x')
            {
                var x1 = ReduceRange(op, value, x);
                sum += ProcessRecursive(workflows, rule.Destination, x1, m, a, s);
                x = ReduceRangeInverse(op, value, x);
            }
            else if (variable == 'm')
            {
                var m1 = ReduceRange(op, value, m);
                sum += ProcessRecursive(workflows, rule.Destination, x, m1, a, s);
                m = ReduceRangeInverse(op, value, m);
            }
            else if (variable == 'a')
            {
                var a1 = ReduceRange(op, value, a);
                sum += ProcessRecursive(workflows, rule.Destination, x, m, a1, s);
                a = ReduceRangeInverse(op, value, a);
            }
            else if (variable == 's')
            {
                var s1 = ReduceRange(op, value, s);
                sum += ProcessRecursive(workflows, rule.Destination, x, m, a, s1);
                s = ReduceRangeInverse(op, value, s);
            }
        }

        return sum;

    }

    private Range ReduceRangeInverse(char op, int value, Range r)
    {
        switch (op)
        {
            // becomes >=
            case '<':
            {
               return ReduceRange('>', value-1, r);
            }
            // becomes <=
            case '>':
            {
               return ReduceRange('<', value+1, r);
            }
            default:
                throw new Exception("Unknown operator");
        }
    }

    private static Range ReduceRange(char op, int value, Range r)
    {
        switch (op)
        {
            case '<':
            {
                int end = Math.Min((value - 1), r.End.Value);
                return r.Start..end;
            }
            case '>':
            {
                int start = Math.Max((value + 1), r.Start.Value);
                return start..r.End;
            }
            default:
                throw new Exception("Unknown operator");
        }
    }

    private void ApplyRulesToPart(Part part, Dictionary<string, Workflow> workflows, List<Part> accepted, List<Part> rejected)
    {
        var workflow = workflows["in"];

        while (true)
        {
            foreach (var rule in workflow.Rules)
            {
                if (rule.Expression == null || rule.Expression.Evaluate(part))
                {
                    if (rule.Destination == "A")
                    {
                        accepted.Add(part);
                        return;
                    }

                    if (rule.Destination == "R")
                    {
                        rejected.Add(part);
                        return;
                    }

                    workflow = workflows[rule.Destination];
                    break;
                }
            }
        }

    }

    private (Dictionary<string, Workflow> flows, List<Part> parts) Parse(string[] lines)
    {
        var (ruleLines, partLines) = lines.SplitByEmptyLines()[..2];

        Dictionary<string, Workflow> flows = new();
        foreach (var line in ruleLines)
        {
            var workflow = Workflow.Parse(line);
            flows.Add(workflow.Name, workflow);
        }

        List<Part> parts = new();
        foreach (var line in partLines)
        {
            var part = Part.Parse(line);
            parts.Add(part);
        }

        return (flows, parts);
    }

    class Workflow
    {
        public string Name { get; set; }
        public List<Rule> Rules { get; set; } = new();

        public static Workflow Parse(string line)
        {
            Workflow workflow = new();

            var (name, rules) = line.Split("{");
            workflow.Name = name.Trim();

            Array.ForEach(rules.Trim('}').Split(","), x =>
            {
                var (expression, destination) = x.Split(":");
                workflow.Rules.Add(destination != null
                    ? new Rule {Expression = Expression.Parse(expression), Destination = destination}
                    : new Rule {Expression = null, Destination = expression});
            });
            return workflow;
        }
    }

    class Rule
    {
        public Expression? Expression { get; set; }
        public string Destination { get; set; }
    }

    class Expression
    {
        public char Variable { get; set; }
        public char Operator { get; set; }
        public long Value { get; set; }

        public bool Evaluate(Part part)
        {
            var value = Variable switch
            {
                'x' => part.x,
                'm' => part.m,
                'a' => part.a,
                's' => part.s,
                _ => throw new Exception("Unknown variable")
            };

            return Operator switch
            {
                '<' => value < Value,
                '>' => value > Value,
                _ => throw new Exception("Unknown operator")
            };
        }

        public static Expression Parse(string line)
        {
            Expression expression = new();

            expression.Variable = line[0];
            expression.Operator = line[1];
            expression.Value = long.Parse(line[2..]);
            return expression;
        }

        public override string ToString()
        {
            return $"{Variable}{Operator}{Value}";
        }
    }

    struct Part
    {
        public long x;
        public long m;
        public long a;
        public long s;

        public static Part Parse(string line)
        {
            Part part = new();
            Array.ForEach(line.Trim('{', '}').Split(","), x =>
            {
                var (key, value) = x.Split("=");
                switch (key)
                {
                    case "x":
                        part.x = long.Parse(value);
                        break;
                    case "m":
                        part.m = long.Parse(value);
                        break;
                    case "a":
                        part.a = long.Parse(value);
                        break;
                    case "s":
                        part.s = long.Parse(value);
                        break;
                }
            });
            return part;
        }
    }

}

