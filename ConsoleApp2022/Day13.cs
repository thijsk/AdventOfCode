using Common;

namespace ConsoleApp2022;

public class Day13 : IDay, IComparer<Day13.DataList>
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 6656;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.SplitByEmptyLines();

        var pairCounter = 1;
        List<int> rightOrderIndexes = new List<int>();
        foreach (var pair in input)
        {
            ConsoleX.WriteLine($"== Pair {pairCounter}");
            var left = Parse(pair[0]);
            var right = Parse(pair[1]);

            var depth = 0;

            var result =Compare(depth, left, right);

            if (result.HasValue && result.Value)
            {
                rightOrderIndexes.Add(pairCounter);
            }

            pairCounter++;

        }

        return rightOrderIndexes.Sum();
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 19716;
        PuzzleContext.UseExample = false;

        var packets = PuzzleContext.Input.Where(l => !string.IsNullOrEmpty(l)).Select(Parse).ToList();
        var divider1 = Parse("[[2]]");
        var divider2 = Parse("[[6]]");
        packets.Add(divider1);
        packets.Add(divider2);

        packets.Sort(this);
        foreach (var packet in packets)
        {
            ConsoleX.WriteLine(packet);
        }

        var index1 = packets.IndexOf(divider1) + 1;
        var index2 = packets.IndexOf(divider2) +1;

        return index1 * index2;
    }

    private static bool? Compare(int depth, IDataPacket left, IDataPacket right)
    {
        ConsoleX.WriteLine($"{" ".Repeat(depth)}- Compare {left} vs {right}");

        if (left is DataInteger && right is DataInteger)
        {
            var leftValue = ((DataInteger)left).Value;
            var rightValue = ((DataInteger)right).Value;
            if (leftValue < rightValue)
            {
                ConsoleX.WriteLine($"{" ".Repeat(depth)}  - Left side is smaller, so inputs are in the right order");
                return true;
            }

            if (leftValue > rightValue)
            {
                ConsoleX.WriteLine($"{" ".Repeat(depth)}  - Right side is smaller, so inputs are not in the right order");
                return false;
            }

            return null;
        }

        if (left is DataInteger)
        {
            var retry = new DataList {left};
            return Compare(depth, retry, right);
        }

        if (right is DataInteger)
        {
            var retry = new DataList { right };
            return Compare(depth, left, retry);
        }

        depth += 2;

        var leftList = (DataList) left;
        var rightList = (DataList) right;

        for (int i = 0; i < Math.Max(leftList.Count, rightList.Count); i++)
        {
            if (i >= leftList.Count)
            {
                ConsoleX.WriteLine($"{" ".Repeat(depth)}- Left ran out of items, so inputs are in the right order");
                return true;
            }

            if (i >= rightList.Count)
            {
                ConsoleX.WriteLine($"{" ".Repeat(depth)}- Right ran out of items, so inputs are not in the right order");
                return false;
            }
            
            var result  = Compare(depth, leftList[i], rightList[i]);
            if (result.HasValue)
                return result;
        }

        return null;
    }

 

    public interface IDataPacket
    {
        public string ToString();
    }

    public class DataList : List<IDataPacket>, IDataPacket
    {
        public override string ToString()
        {
            return $"[{string.Join(',', this)}]";
        }
    }

    public class DataInteger : IDataPacket
    {
        public override string ToString()
        {
            return Value.ToString();
        }

        public DataInteger(int value)
        {
            Value = value;
        }
        public int Value { get; init; }
    }

    public DataList Parse(string line)
    {
        var stack = new Stack<DataList>();
        DataList current = new DataList();
        string item = "";

        for (int index = 0; index < line.Length; index++) 
        {
            switch (line[index])
            {
                case '[':
                {
                    stack.Push(current);
                    current = new DataList();
                    break;
                }
                case ']':
                {
                    if (!string.IsNullOrEmpty(item)) current.Add(new DataInteger(int.Parse(item)));
                    item = "";
                    var last = current;
                    current = stack.Pop();
                    current.Add(last);
                    break;
                }
                case ',':
                {
                    if (!string.IsNullOrEmpty(item)) current.Add(new DataInteger(int.Parse(item)));
                    item = "";
                    break;
                }
                default:
                {
                    item += line[index];
                    break;
                }
            }
        }

        return current[0] as DataList;
    }

    public int Compare(DataList? x, DataList? y)
    {
        var result = Compare(0, x, y);
        if (result == true) return -1;
        if (result == false) return 1;
        return 0;
    }
}