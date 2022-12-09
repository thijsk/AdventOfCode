using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Aoc
    {
        private readonly string _cookieToken;

        public Aoc(string cookieToken)
        {
            _cookieToken = cookieToken;
        }

        public async Task<string> DownloadInput(int day, int year)
        {
            using HttpClientHandler handler = new();
            handler.CookieContainer.Add(new System.Net.Cookie("session", _cookieToken, "/", "adventofcode.com"));
            HttpClient client = new(handler);

            HttpResponseMessage result = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
            var input = await result.Content.ReadAsStringAsync();

            return input;
        }
    }
}
