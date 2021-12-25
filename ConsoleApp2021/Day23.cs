using Common;

namespace ConsoleApp2021
{
    public class Day23 : IDay
    {
        public long Part1()
        {
            var input = Parse(PuzzleContext.Input);

            var cost = Solve(input);

            return 0;
        }

        private long Solve(Burrow input)
        {
            // Done?
            var done = input.Amphipods.All(pod => pod.IsInOwnRoom());

            var paths = FindPaths(input, 0);

            return 0;
        }

        private IEnumerable<(Burrow burrow, long cost)> FindPaths(Burrow input, int cost)
        {
            // From hall to room?
            foreach (var pod in input.Amphipods.Where(pod => !IsDone(pod, input) &&pod.IsInHall()))
            {
                Console.WriteLine("pod in hall");
            }

            // From room to hall?
            foreach (var pod in input.Amphipods.Where(pod => !IsDone(pod, input) && pod.IsInRoom()))
            {
                Console.WriteLine("pod in room");
            }

            return new List<(Burrow, long)>();
        }

        private bool IsDone(Amphipod pod, Burrow input)
        {
            return pod.IsInOwnRoom() && NobodyElseBelowMe(pod, input);
        }

        private bool NobodyElseBelowMe(Amphipod pod, Burrow input)
        {
            return input.Amphipods.Any(other =>
                other.Room == pod.Room && other.Roomdepth > pod.Roomdepth && other.Letter != pod.Letter);
        }

        public long Part2()
        {
            var input = Parse(PuzzleContext.Input);

            return 0;
        }

        public Burrow Parse(string[] lines)
        {
            int depth = 0;

            var pods = new List<Amphipod>();

            foreach (var line in lines[2..^1])
            {
                foreach (var room in (1..4).AsEnumerable())
                {
                    var letter = line[1 + (room * 2)];
                    var pod = new Amphipod()
                    {
                        Letter = letter,
                        Room = room,
                        Roomdepth = 0
                    };
                    pods.Add(pod);
                }
                depth++;
            }

            var burrow = new Burrow();
            burrow.Amphipods = pods.ToArray();
            return burrow;
        }


        public struct Burrow
        {
            public Amphipod[] Amphipods;
        }

    }

    public struct Amphipod
    {
        public char Letter;
        public int? Room;
        public int? Roomdepth;
        public int? Hall;

        public int HomeRoom => Letter - 64;

        public bool IsInHall()
        {
            return Hall != null;
        }

        public bool IsInRoom()
        {
            return Room != null;
        }

        public bool IsInOwnRoom()
        {
            return HomeRoom == Room;
        }
    }
}
