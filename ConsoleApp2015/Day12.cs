using System.IO;
using System.Linq;
using Common;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2015
{
    class Day12 : IDay
    {
        public long Part1()
        {
            var str = File.ReadAllText("day12.txt");
            var j = JToken.Parse(str);
            
            var result = Sum(j);

            return result;
        }

        private int Sum(JToken jToken)
        {
            int sum = 0;
            if (jToken.Type == JTokenType.Integer)
                sum = jToken.Value<int>();
            foreach (var child in jToken.Children())
            {
                sum += Sum(child);
            }
            return sum;
        }
        public long Part2()
        {
            var str = File.ReadAllText("day12.txt");
            var j = JToken.Parse(str);
            
            var result = Sum2(j);

            return result;
        }

        private int Sum2(JToken jToken)
        {
            int sum = 0;
            if (jToken.Type == JTokenType.Integer)
                sum = jToken.Value<int>();

            if (jToken.Type == JTokenType.Object)
            {
                var jObject = (JObject) jToken;
                if (jObject.Properties().Any(p => p.Value.ToString() == "red"))
                    return 0;
            }
            foreach (var child in jToken.Children())
            {
                sum += Sum2(child);
            }
            return sum;
        }
    }
}
