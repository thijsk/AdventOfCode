using System.Diagnostics;
using Common;

namespace ConsoleApp2023;

public class Day25 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 0;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToDictionary( k => k.key, v => v.value);

        foreach (var (from, list) in input.ToList())
        {
            foreach (var to in list)
            {
                if (!input.ContainsKey(to))
                {
                    input.Add(to, new List<string> {from});
                } 
                else
                {
                    if (!input[to].Contains(from))
                    {
                        input[to].Add(from);
                    }
                }
            }
        }

        var edges = input.Keys.SelectMany(i => input[i].Select(v =>
        {
            var (a, b) = new List<string> {i, v}.Order().ToArray();
            return (a, b);
        })).Distinct().OrderBy(i => i.Item1).ThenBy(i => i.Item2).ToList();

        // ToDotFile(edges);

        var vertices = input.Keys.OrderBy(i => i).ToList();

        while (true)
        { 
            var subsets = DoKargers(edges, vertices);
            var cuts = GetCutEdges(subsets, edges);
            if (cuts.Count > 3)
            {
                continue;
            }

            foreach (var cut in cuts)
            {
                ConsoleX.WriteLine($"Cut {cut}");
            }

            var first = subsets.First().Count;
            var second = subsets.Last().Count;

            return first * (long)second;

        }
    }

    private void ToDotFile(List<(string a,string b)> edges)
    {
        using var fs = File.OpenWrite("graph.dot");
        using var sw = new StreamWriter(fs);

        sw.WriteLine("graph G {");
        foreach (var (a,b) in edges)
        {
            sw.WriteLine($"{a} -- {b};");
        }
        sw.WriteLine("}");
        sw.Flush();
        fs.Close();
    }

    private List<(string a, string b)> GetCutEdges(List<HashSet<string>> subsets, List<(string a, string b)> edges)
    {
        var cuts = new List<(string a, string b)>();
        foreach (var edge in edges)
        {
            if (cuts.Count > 3) return cuts;
            if (subsets.First(s => s.Contains(edge.a)) != subsets.First(s => s.Contains(edge.b)))
                cuts.Add(edge);
        }
        return cuts;
    }

    private List<HashSet<string>> DoKargers(List<(string a, string b)> edges, List<string> vertices)
    {
        var subsets = vertices.Select(vertex => new HashSet<string> {vertex}).ToList();

        while (subsets.Count > 2)
        {
            var edget = edges.Random();
            
            var subset1 = subsets.First(s => s.Contains(edget.a));
            var subset2 = subsets.First(s => s.Contains(edget.b));

            if (subset1 == subset2) continue;

            subsets.Remove(subset2);
            foreach (var s in subset2)
            {
                subset1.Add(s);
            }
        }
        
        return subsets;
    }

    //private long GetGroupSize(Dictionary<string, List<string>> input, string start)
    //{
    //    var visited = new HashSet<string>();
    //    Queue<string> fronteer = new Queue<string>();
    //    fronteer.Enqueue(start);

    //    while (fronteer.TryDequeue(out var current))
    //    {
    //        if (visited.Contains(current))
    //        {
    //            continue;
    //        }

    //        visited.Add(current);

    //        foreach (var next in input[current])
    //        {
    //            fronteer.Enqueue(next);
    //        }
    //    }

    //    return visited.Count;
    //}

    public long Part2()
    {
        PuzzleContext.Answer2 = 0;
        PuzzleContext.UseExample = false;

        return 0;
    }

    private (string key, List<string> value) Parse(string line)
    {
        var (key, values) = line.Split(": ", StringSplitOptions.TrimEntries);
        var value = values.Split(" ", StringSplitOptions.TrimEntries).ToList();

        return (key, value);
    }

}