using Common;
using System.Data;
using System.Diagnostics;

namespace ConsoleApp2024;

public class Day23 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1344;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var graph = MapToDicationary(input);

		var triangles = FindTriangles(graph).Distinct();

		var count = triangles.Count(t => t.node1.StartsWith('t') || t.node2.StartsWith('t') || t.node3.StartsWith('t'));

		return count;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 1;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var graph = MapToDicationary(input);


		var groups = FindGroups(graph);

		var largest = groups.Single(g => g.Count == groups.Max(g => g.Count));

		var result = largest.OrderBy(n => n).ToList();

		var output = $"{string.Join(",", result)}";

		if (PuzzleContext.UseExample)
		{
			Debug.Assert("co,de,ka,ta" == output);
		} else
		{
			Debug.Assert("ab,al,cq,cr,da,db,dr,fw,ly,mn,od,py,uh" == output);
		}

		Console.WriteLine(output);

		return 1;
	}

	private List<HashSet<string>> FindGroups(Dictionary<string, HashSet<string>> graph)
	{
		var groups = new List<HashSet<string>>();

		BronKerbosch(new HashSet<string>(), new HashSet<string>(graph.Keys), new HashSet<string>(), graph, groups);

		

		return groups;
	}

/*
   algorithm BronKerbosch1(R, P, X) is
   if P and X are both empty then
       report R as a maximal clique
   for each vertex v in P do
       BronKerbosch1(R ⋃ {v}, P ⋂ N(v), X ⋂ N(v))
       P := P \ {v}
       X := X ⋃ {v}
*/
	private void BronKerbosch(HashSet<string> result, HashSet<string> potential, HashSet<string> excluded, Dictionary<string, HashSet<string>> graph, List<HashSet<string>> cliques)
	{
		if (potential.Count == 0 && excluded.Count == 0)
		{
			cliques.Add(result);
			return;
		}
		var p = new HashSet<string>(potential);
		foreach (var node in p)
		{
			var neighbors = graph[node];
			var nextResult = new HashSet<string>(result.Union([node]));
			var nextPotential = new HashSet<string>(potential.Intersect(neighbors));
			var nextExcluded = new HashSet<string>(excluded.Intersect(neighbors));
			BronKerbosch(nextResult, nextPotential, nextExcluded, graph, cliques);
			potential.Remove(node);
			excluded.Add(node);
		}
	}

	private List<(string node1, string node2, string node3)> FindTriangles(Dictionary<string, HashSet<string>> graph)
	{
		var result =new List<(string node1, string node2, string node3)>();

		foreach (var node1 in graph.Keys)
		{
			foreach (var node2 in graph[node1])
			{
				foreach (var node3 in graph[node2])
				{
					if (graph[node3].Contains(node1))
					{
						var sort = new[] { node1, node2, node3 }.OrderBy(x => x).ToArray();
						result.Add((sort[0], sort[1], sort[2]));
					}
				}
			}
		}

		return result;
	}

	private Dictionary<string, HashSet<string>> MapToDicationary((string node1, string node2)[] input)
	{
		var result = new Dictionary<string, HashSet<string>>();

		foreach (var (node1, node2) in input) {

			if (!result.TryAdd(node1, new HashSet<string>() { node2 }))
			{
				result[node1].Add(node2);
			}
			if (!result.TryAdd(node2, new HashSet<string>() { node1 }))
			{
				result[node2].Add(node1);
			}
		}


		return result;
	}


	private (string node1, string node2) Parse(string line)
	{
		var nodes = line.Split('-');
		return (nodes[0], nodes[1]);
	}

}