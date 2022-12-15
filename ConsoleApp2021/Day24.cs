using System.Diagnostics;
using System.Reflection;
using System.Text;
using Common;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ConsoleApp2021
{
    public class Day24 : IDay
    {
        public long Part1()
        {
            var alus = Parse(PuzzleContext.Input);

            var alloptions = FindOptions(alus);

            var findz = 0;
            var alui = 0;
            long ans = 0;
            foreach (var option in alloptions.ToArray().Reverse())
            {
                var alu = alus[alui];
                var w = option.Where(o => o.z == findz).Max(o => o.w);

                var result = alu.Calc(0, 0, findz, w);
                findz = result.z;

                ans *= 10;
                ans += w;
                alui++;
            }

            return ans;
        }

        private static List<List<(int w, int z)>> FindOptions(IAlu[] alus)
        {
            var allowedz = new HashSet<int>();
            allowedz.Add(0);
            var alloptions = new List<List<(int w, int z)>>();
            foreach (var alu in alus.Reverse())
            {
                //Console.WriteLine(alu.GetType().Name);

                var nextz = new HashSet<int>();
                var options = new List<(int w, int z)>();
                for (var w = 1; w < 10; w++)
                {
                    foreach (var z in Enumerable.Range(0, 400000))
                    {
                        var result = alu.Calc(0, 0, z, w);

                        if (allowedz.Contains(result.z))
                        {
                            //Console.Write($"[w{w} z{z}]");
                            options.Add((w, z));
                            nextz.Add(z);
                        }
                    }
                }
                allowedz = nextz;
                alloptions.Add(options);
            }

            return alloptions;
        }

        public long Part2()
        {
            var alus = Parse(PuzzleContext.Input);

            var alloptions = FindOptions(alus);

            var findz = 0;
            var alui = 0;
            long ans = 0;
            foreach (var option in alloptions.ToArray().Reverse())
            {
                var alu = alus[alui];
                var w = option.Where(o => o.z == findz).Min(o => o.w);

                var result = alu.Calc(0, 0, findz, w);
                findz = result.z;

                ans *= 10;
                ans += w;
                alui++;
            }


            return ans;
        }

        public IAlu[] Parse(string[] lines)
        {
            List<IAlu> alus = new();
            AluBuilder current = null;
            var count = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith("inp"))
                {
                    if (current != null)
                    {
                        alus.Add(current?.Build());
                    }

                    current = new AluBuilder($"alu{count++}");
                }
                else
                {
                    var split = line.Split(' ');
                    var code = split[0] switch
                    {
                        "add" => $"{split[1]}+={split[2]};",
                        "mul" => $"{split[1]}*={split[2]};",
                        "div" => $"{split[1]}/={split[2]};",
                        "mod" => $"{split[1]}%={split[2]};",
                        "eql" => $"{split[1]}=({split[1]}=={split[2]}?1:0);",
                        _ => ""
                    };

                    Debug.Assert(!string.IsNullOrEmpty(code));

                    current?.Add(code);
                }
            }

            alus.Add(current.Build());

            return alus.ToArray();
        }
    }

    public class AluBuilder
    {
        private StringBuilder _source;
        private string _name;

        public AluBuilder(string name)
        {
            _name = name;
            _source = new StringBuilder();
            _source.Append("public class " + _name + " : ConsoleApp2021.IAlu {");
            _source.Append("public (int x, int y, int z) Calc(int x, int y, int z, int w) {");
        }

        public IAlu Build()
        {
            _source.Append("return (x, y, z);}");
            _source.Append("} return typeof(" + _name + ");");
            var script = CSharpScript.Create(_source.ToString(), ScriptOptions.Default.WithReferences(Assembly.GetExecutingAssembly()));
            script.Compile();
            var scriptType = (Type)script.RunAsync().Result.ReturnValue;
            return (IAlu)Activator.CreateInstance(scriptType)! ?? throw new InvalidOperationException();
        }

        public void Add(string code)
        {
            _source.Append(code);
        }
    }

    public interface IAlu
    {
        public (int x, int y, int z) Calc(int x, int y, int z, int w);
    }

}
