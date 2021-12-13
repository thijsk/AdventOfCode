using Common;

namespace ConsoleApp2021;

public class Day12 : IDay
{
    public long Part1()
    {
        var input = Parse(PuzzleContext.Input);

        var start = input["start"];

        var visited = new List<string>();
        var localPath = new List<string>();
        var paths = FindPaths1(input, visited, "start", localPath);

        return paths.Count;
    }

    private List<List<string>> FindPaths1(Dictionary<string, List<string>> map, List<string> visited, string node, List<string> localPath)
    {
        if (char.IsLower(node[0]))
        {
            visited.Add(node);
        }

        localPath.Add(node);

        var result = new List<List<string>>();
        if (node == "end")
        {
            result.Add(localPath);
            return result;
        }

        foreach (var next in map[node].Where(n => !visited.Contains(n)))
        {
            result.AddRange(FindPaths1(map, visited.ToList(), next, localPath.ToList()));
        }
        return result;
    }

    public long Part2()
    {
        var input = Parse(PuzzleContext.Input);

        var start = input["start"];

        var visited = new Dictionary<string, int>();
        foreach (var node in input.Keys)
        {
            visited.Add(node, 0);
        }
        var localPath = new List<string>();
        var paths = FindPaths2(input, visited, "start", localPath);

        return paths.Count;
    }
    private List<List<string>> FindPaths2(Dictionary<string, List<string>> map, Dictionary<string, int> visited, string node, List<string> localPath)
    {
        if (char.IsLower(node[0]))
        {
            visited[node]++;
        }

        localPath.Add(node);

        var result = new List<List<string>>();
        if (node == "end")
        {
            result.Add(localPath);
            var str = string.Join(',', localPath);
            Console.WriteLine(str);
            return result;
        }

        foreach (var next in map[node].Where(n => (visited[n] == 0 || !visited.Values.Any(v => v == 2)) && n != "start" ))
        {
            var nextvisited = new Dictionary<string, int>(visited);
            result.AddRange(FindPaths2(map, nextvisited, next, localPath.ToList()));
        }
        return result;
    }

    public Dictionary<string, List<string>> Parse(string[] lines)
    {
        var map = new Dictionary<string, List<string>>();
        foreach (var line in lines)
        {
            var split = line.Split('-');
            var from = split[0];
            var to = split[1];

            if (!map.ContainsKey(from))
            {
                map.Add(from, new List<string>());
            }

            if (!map.ContainsKey(to))
            {
                map.Add(to, new List<string>());
            }

            map[from].Add(to);
            map[to].Add(from);
        }

        return map;
    }



}