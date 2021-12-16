using System.Text;
using Common;

namespace ConsoleApp2021;

public class Day16 : IDay
{
    public long Part1()
    {
        var input = PuzzleContext.Example.Select(Parse).ToArray();
        foreach (var line in input)
        {
            var remainder = string.Empty;

            var packet = Packet.Create(line, out remainder);
            var sum = packet.Sum();
            Console.WriteLine($"Sum: {sum}");


            Console.WriteLine("----");
        }

        return 0;
    }

    public long Part2()
    {
        var input = PuzzleContext.Input.Select(Parse).ToArray();

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

    public static Packet Create(string rawPacket, out string remainingBits)
    {
        Console.WriteLine(rawPacket);
        var version = Convert.ToInt32(rawPacket.Substring(0, 3), 2);
        var type = Convert.ToInt32(rawPacket.Substring(3, 3), 2);
        var rawBody = rawPacket.Substring(6);
        if (type == 4)
        {
            return LiteralPacket.Create(version, rawBody, out remainingBits);
        }
        else //if (type == 6)
        {
            return OperatorPacket.Create(version, rawBody, out remainingBits);
        }
        //else
        //{
        //    throw new Exception("Unknown packet type");
        //}
    }

    public abstract int Sum();
}

public class OperatorPacket : Packet
{
    public static OperatorPacket Create(int version, string rawBody, out string remainingBits)
    {
        Console.WriteLine($"OperatorPacket {version}");
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

        var length = Convert.ToInt32(rawBody.Substring(1, lengthBits), 2);

        List<Packet> subPackets = new();
        if (lengthTypeId == '0') // number of bits
        {
            var rawSubPackets = rawBody.Substring(1 + lengthBits, length);

            while (rawSubPackets.Length > 0)
            {
                var subPacket = Packet.Create(rawSubPackets, out rawSubPackets);
                subPackets.Add(subPacket);
            }

            remainingBits = rawBody.Substring(length + 1);
        }
        else
        {
            var packetCount = 0;
            var rawSubPackets = rawBody.Substring(1 + lengthBits);
            while (packetCount < length)
            {
                var subPacket = Packet.Create(rawSubPackets, out rawSubPackets);
                subPackets.Add(subPacket);
                packetCount++;
            }
            remainingBits = rawSubPackets;
        }
        return new OperatorPacket
        {
            Version = version,
            SubPackets = subPackets
        };
    }

    public List<Packet> SubPackets { get; set; }
    public override int Sum()
    {
        return Version + SubPackets.Sum(p => p.Sum());
    }
}

public class LiteralPacket : Packet
{
    public static LiteralPacket Create(int version, string rawBody, out string remainingBits)
    {
        Console.WriteLine($"LiteralPacket {version}");
        var startIndex = 0;
        StringBuilder binary = new StringBuilder();
        for (; ; )
        {
            var group = rawBody.Substring(startIndex, 5);
            binary.Append(group.Substring(1));

            startIndex += 5;
            if (group[0] == '0')
                break;
        }

        var value = Convert.ToInt32(binary.ToString(), 2);
        Console.WriteLine($"Value {value}");

        remainingBits = rawBody.Substring(startIndex);
        return new LiteralPacket
        {
            Version = version,
            Value = value
        };
    }

    public int Value { get; set; }
    public override int Sum()
    {
        return Version;
    }
}