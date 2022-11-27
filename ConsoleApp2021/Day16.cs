using System.Text;
using Common;

namespace ConsoleApp2021;

public class Day16 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();
        foreach (var line in input)
        {
            var remainder = 0;

            var packet = Packet.Create(line, out remainder);
            long sum = packet.Sum();
            ConsoleX.WriteLine($"Sum: {sum}");

            ConsoleX.WriteLine("----");
            return sum;
        }

        return 0;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();
        foreach (var line in input)
        {
            var remainder = 0;

            var packet = Packet.Create(line, out remainder);
            long result = packet.Operate();
            ConsoleX.WriteLine($"Result: {result}");

            ConsoleX.WriteLine("----");
            return result;
        }

        return 0;
    }

    public string Parse(string line)
    {
        return String.Join(string.Empty, line.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
    }

}

public abstract class Packet
{
    public int Version { get; set; }

    public static Packet Create(string rawPacket, out int usedBits)
    {
       // Console.WriteLine(rawPacket);
        var version = Convert.ToInt32(rawPacket.Substring(0, 3), 2);
        var type = Convert.ToInt32(rawPacket.Substring(3, 3), 2);
        var rawBody = rawPacket.Substring(6);
        Packet packet;
        if (type == 4)
        {
            packet = LiteralPacket.Create(version, rawBody, out usedBits);
        }
        else //if (type == 6)
        {
            packet = OperatorPacket.Create(type, version, rawBody, out usedBits);


        }
        usedBits += 6;
        return packet;
        //else
        //{
        //    throw new Exception("Unknown packet type");
        //}
    }

    public abstract long Sum();
    public abstract long Operate();
}

public class OperatorPacket : Packet
{
    public static OperatorPacket Create(int type, int version, string rawBody, out int usedBits)
    {
        ConsoleX.WriteLine($"OperatorPacket {version}");
        var lengthTypeId = rawBody[0];
        var lengthBits = 0;
        if (lengthTypeId == '0') // number of bits
        {
            lengthBits = 15;
        }
        else // number pf packets
        {
            lengthBits = 11;
        }

        var totalUsedBits = 1 + lengthBits;

        var length = Convert.ToInt64(rawBody.Substring(1, lengthBits), 2);

        List<Packet> subPackets = new();
        if (lengthTypeId == '0') // number of bits
        {
            while (length > 0)
            {
                var rawSubPacket = rawBody.Substring(totalUsedBits);
                var subPacket = Packet.Create(rawSubPacket, out var packetBits);
                subPackets.Add(subPacket);
                totalUsedBits += packetBits;
                length -= packetBits;
            }
        }
        else
        {
            var packetCount = 0;
            while (packetCount < length)
            {
                var rawSubPacket = rawBody.Substring(totalUsedBits);
                var subPacket = Packet.Create(rawSubPacket, out var packetBits);
                subPackets.Add(subPacket);
                totalUsedBits += packetBits;
                packetCount++;
            }
        }

        usedBits = totalUsedBits;
        return new OperatorPacket
        {
            Type = type,
            Version = version,
            SubPackets = subPackets
        };
    }

    public int Type { get; set; }

    public List<Packet> SubPackets { get; set; }
    public override long Sum()
    {
        return Version + SubPackets.Sum(p => p.Sum());
    }

    public override long Operate()
    {
        return Type switch
        {
            0 => SubPackets.Sum(p => p.Operate()),
            1 => SubPackets.Aggregate(1L, (acc, packet) => acc * packet.Operate() ),
            2 => SubPackets.Min(p => p.Operate()),
            3 => SubPackets.Max(p => p.Operate()),
            5 => SubPackets.First().Operate() > SubPackets.Last().Operate() ? 1 : 0,
            6 => SubPackets.First().Operate() < SubPackets.Last().Operate() ? 1 : 0,
            7 => SubPackets.First().Operate() == SubPackets.Last().Operate() ? 1 : 0,
            _ => throw new InvalidOperationException()
        };
    }
}

public class LiteralPacket : Packet
{
    public static LiteralPacket Create(int version, string rawBody, out int usedBits)
    {
        ConsoleX.WriteLine($"LiteralPacket {version}");
        usedBits = 0;
        StringBuilder binary = new StringBuilder();
        for (; ; )
        {
            var group = rawBody.Substring(usedBits, 5);
            binary.Append(group.Substring(1));

            usedBits += 5;
            if (group[0] == '0')
                break;
        }

        var value = Convert.ToInt64(binary.ToString(), 2);
        ConsoleX.WriteLine($"Value {value}");

        return new LiteralPacket
        {
            Version = version,
            Value = value
        };
    }

    public long Value { get; set; }
    public override long Sum()
    {
        return Version;
    }

    public override long Operate()
    {
        return Value;
    }
}