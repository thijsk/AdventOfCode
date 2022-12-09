using Common;

namespace ConsoleApp2021
{
    public class Day25 : IDay
    {
        public long Part1()
        {
            var input = Parse(PuzzleContext.Input);



            bool hasmoved = true;
            long moves = 0;
            while (hasmoved)
            {
                var hasMovedEast = Move(input, '>', out var outputEast);
                var hasMovedSouth = Move(outputEast, 'v', out var outputSouth);

                input = outputSouth;
                hasmoved = hasMovedEast || hasMovedSouth;
                moves++;

            }
            PrintGrid(input);
            return moves;
        }

        private bool Move(char[,] input, char move, out char[,] output)
        {
            var result = false;

            var rows = input.GetLength(0);
            var columns = input.GetLength(1);

            output = (char[,])input.Clone();


            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var sc = input[r, c];

                    if (sc == '>' && sc == move)
                    {
                        var next = (c + 1) % columns;
                        if (input[r, next] == '.')
                        {
                            output[r, c] = '.';
                            output[r, next] = '>';
                            result = true;
                            continue;
                        }
                    }
                    if (sc == 'v' && sc == move)
                    {
                        var next = (r + 1) % rows;
                        if (input[next, c] == '.')
                        {
                            output[r,c] = '.';
                            output[next,c] = 'v';
                            result = true;
                            continue;
                        }
                    }
                }
            }

            return result;
        }

        public long Part2()
        {
            var input = Parse(PuzzleContext.Input);

            return 0;
        }
        public void PrintGrid(char[,] grid)
        {
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    char sc = grid[r, c];
                    Console.Write(sc);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public char[,] Parse(string[] lines)
        {
            var width = lines.First().Length;
            var height = lines.Count();

            var grid = new char[height, width];

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    grid[h, w] = lines[h][w];
                }
            }
            return grid;
        }


    }
}