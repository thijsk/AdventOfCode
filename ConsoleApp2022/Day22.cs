using Common;
using System;
using System.Drawing;
using System.IO.MemoryMappedFiles;

namespace ConsoleApp2022;

public class Day22 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 27436;
        PuzzleContext.UseExample = false;

        var (grid, instructions) = Parse(PuzzleContext.Input);

        var location = (row: 0, col: Array.IndexOf(grid.GetRow(0), '.'));
        var orientations = new [] {'R', 'D', 'L', 'U'};
        var orientation = 0;
        foreach (var instruction in instructions)
        {
            //var clone = (char[,]) grid.Clone();
            //grid[location.row, location.col] = 'X';
            //clone.ToConsole();
            ConsoleX.WriteLine($"Location {location}, Orientation: {orientations[orientation]}, Instruction: {instruction}");

            if (instruction is RotateInstruction rotate)
            {
                if (rotate.direction == 'R')
                {
                    orientation = (orientation + 1) % 4;
                }
                else
                {
                    orientation = (orientation - 1) % 4;
                }

                if (orientation < 0) orientation += 4;
                continue;
            }

            var move = (MoveInstruction)instruction;

            if (orientations[orientation] is 'R')
            {
                var row = new string(grid.GetRow(location.row));
                location = (row: location.row , col: Move(row, location.col, move.amount));
                continue;
            }

            if (orientations[orientation] is 'L')
            {
                var row = new string(grid.GetRow(location.row));
                location = (row: location.row, col: Move(row, location.col, -move.amount));
                continue;
            }

            if (orientations[orientation] is 'D')
            {
                var col = new string(grid.GetColumn(location.col));
                location = (row: Move(col, location.row, move.amount), col: location.col);
                continue;
            }

            if (orientations[orientation] is 'U')
            {
                var col = new string(grid.GetColumn(location.col));
                location = (row: Move(col, location.row, -move.amount), col: location.col);
                continue;
            }
        }

        return ((location.row + 1) * 1000L) + ((location.col + 1) * 4) + orientation;
    }

   

    public long Part2()
    {
        PuzzleContext.Answer2 = 15426;
        PuzzleContext.UseExample = false;

        var size = 50;
        if (PuzzleContext.UseExample)
            size = 4;

        var (grid, instructions) = Parse(PuzzleContext.Input);

        var location = (row: 0, col: Array.IndexOf(grid.GetRow(0), '.'));

        var orientation = 'R';
        foreach (var instruction in instructions)
        {
            ConsoleX.WriteLine($"Location {location}, Orientation: {orientation}, Instruction: {instruction}");

            if (instruction is RotateInstruction rotate)
            {
                orientation = DoRotate(rotate.direction, orientation);
               
                continue;
            }

            var move = (MoveInstruction)instruction;

            var amount = move.amount;
            while (amount > 0)
            {
                var (nextlocation, nextorientation) = Step(grid, location, orientation, size);
                if (IsWall(grid, nextlocation))
                {
                    amount = 0;
                    break;
                }

                location = nextlocation;
                orientation = nextorientation;
                amount--;

                //PrintGrid(grid, location, orientation, amount);
            }
        }

        ConsoleX.WriteLine($"{location}");

        return ((location.row + 1) * 1000L) + ((location.col + 1) * 4) + Array.IndexOf(Orientations, orientation);
    }

    private static void PrintGrid(char[,] grid, (int row, int col) location, char orientation, int amount)
    {
        Console.Clear();
        var clone = (char[,]) grid.Clone();
        grid[location.row, location.col] = orientation;
        clone.ToConsole();
        ConsoleX.WriteLine($"L: {location} {orientation} {amount}");
        Thread.Sleep(100);
    }

    private ((int row, int col), char orientation) Step(char[,] grid, (int row, int col) location, char orientation, int size)
    {
        (int row, int col) nextlocation = orientation switch
        {
            'R' => (location.row, location.col + 1),
            'D' => (location.row + 1, location.col),
            'L' => (location.row, location.col - 1),
            'U' => (location.row - 1, location.col),
        };

        if (IsValidLocation(grid, nextlocation))
        {
            return (nextlocation, orientation);
        }

        var face = GetFace(location, size);
        var (nextFace, nextOrientation) = Lookup(face, orientation);

        var i = 0;
        if (orientation is 'U' or 'D')
           i = location.col % size;
        else 
           i = location.row % size;

        nextlocation.row = nextFace.row * size;
        nextlocation.col = nextFace.col * size;

        switch (nextOrientation)
        {
            case ('L'):
            {
                nextlocation.col += size - 1;
                break;
            }
            case ('U'):
            {
                nextlocation.row += size - 1;
                break;
            }
        }

        switch (orientation, nextOrientation)
        {
            case ('L', 'R'):
            case ('R', 'L'):
            case ('D', 'R'):
            case ('U', 'L'):
            {
                nextlocation.row += size - 1 - i;
                break;
            }
            case ('U', 'D'):
            case ('D', 'U'):
            case ('R', 'D'):
            case ('L', 'U'):
                {
                nextlocation.col += size - 1 - i;
                break;
            }
            case ('R', 'U'):
            case ('L', 'D'):
            case ('U', 'U'):
            case ('D', 'D'):
            {
                nextlocation.col += i ;
                break;
            }
            case ('U', 'R'):
            case ('D', 'L'):
            case ('L', 'L'):
            case ('R', 'R'):
            {
                nextlocation.row += i;
                break;
            }
            default:
                throw new NotImplementedException();
        }

        return (nextlocation, nextOrientation);

    }

    private bool IsValidLocation(char[,] grid, (int row, int col) location)
    {
        if (location.row < 0 || location.row >= grid.GetLength(0))
            return false;
        if (location.col < 0 || location.col >= grid.GetLength(1))
            return false;
        return grid[location.row, location.col] != ' ';
    }

    private static readonly char[] Orientations = { 'R', 'D', 'L', 'U' };

    private Dictionary<((int col, int row), char orientation), ((int col, int row), char orientation)> _examplemap = new()
    {
        {((1, 2), 'R'), ((2, 3), 'D')},
        {((2,2), 'D'), ((1,0), 'U')},
        {((1,1), 'U'), ((0,2), 'R')}
    };

    private Dictionary<((int col, int row), char orientation), ((int col, int row), char orientation)> _inputmap = new()
    {
        {((0,1), 'U'), ((3,0), 'R')},
        {((3,0), 'L'), ((0,1), 'D')},

        {((0,2), 'U'), ((3,0), 'U')},
        {((3,0), 'D'), ((0,2), 'D')},

        {((0,2), 'R'), ((2,1), 'L')},
        {((2,1), 'R'), ((0,2), 'L')},

        {((2,0), 'L'), ((0,1), 'R')},
        {((0,1), 'L'), ((2,0), 'R')},

        {((1,1), 'L'), ((2,0), 'D')},
        {((2,0), 'U'), ((1,1), 'R')},

        {((1,1), 'R'), ((0,2), 'U')},
        {((0,2), 'D'), ((1,1), 'L')},
       
        {((2,1), 'D'), ((3,0), 'L')},
        {((3,0), 'R'), ((2,1), 'U')},
    };

    private ((int row, int col) nextface, char orientation) Lookup((int row, int col) currentface, char orientation)
    {
        var map = PuzzleContext.UseExample ? _examplemap : _inputmap;

        if (!map.ContainsKey((currentface, orientation)))
        {
            throw new NotImplementedException($"Missing map entry for {currentface} {orientation}");
        }
        return map[(currentface, orientation)];
    }

    private (int row, int col) GetFace((int row, int col) location, int size)
    {
        return (location.row / size, col: location.col/size);
    }

    private bool IsWall(char[,] grid, (int row, int col) location)
    {
        return grid[location.row, location.col] == '#';
    }
   
    private static int DoRotate(char direction, int orientation)
    {
        if (direction == 'R')
        {
            orientation = (orientation + 1) % 4;
        }
        else
        {
            orientation = (orientation - 1) % 4;
        }

        if (orientation < 0) orientation += 4;
        return orientation;
    }

    private static char DoRotate(char direction, char orientation)
    {
        var index = Array.IndexOf(Orientations, orientation);;

        if (direction == 'R')
        {
            index = (index + 1) % 4;
        }
        else
        {
            index = (index - 1) % 4;
        }

        if (index < 0) index += 4;
        return Orientations[index];
    }

    private int Move(string line, int index, int amount)
    {
        var first = GetFirst(line);
        var last = GetLast(line);

        if (amount > 0)
        {
            while (amount > 0 && index < line.Length)
            {
                var lastindex = index;
                amount--;
                index++;
                if (index > last)
                {
                    index = first;
                }
                if (line[index] == '#')
                    return lastindex;
              
            }

            return index;
        }

        while (amount < 0 && index >= 0)
        {
            var lastindex = index;
            amount++;
            index--;
            if (index < first)
            {
                index = last;
            }

            if (line[index] == '#')
                return lastindex;

        }
        return index;
    }

    

    private int GetLast(string line)
    {
        for (int i = line.Length -1; i >=0 ; i--)
        {
            if (line[i] != ' ')
                return i;
        }
        return -1;
    }

    private int GetFirst(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] != ' ')
                return i;
        }
        return -1;
    }

    private (char[,] grid, IEnumerable<IInstruction> instructions) Parse(string[] lines)
    {
        var (gridlines, instructions) = lines.SplitByEmptyLines();


        return (ParseGrid(gridlines), instructions: ParseInstructions(instructions));
    }

    private (char[,][,], IEnumerable<IInstruction> instructions) Parse2(string[] lines)
    {
        var (gridlines, instructions) = lines.SplitByEmptyLines();


        return (ParseGrid2(gridlines), instructions: ParseInstructions(instructions));
    }

    private IEnumerable<IInstruction> ParseInstructions(string[] instructions)
    {
        string move = string.Empty;
        foreach (var instruction in instructions.First())
        {
            if (instruction is 'L' or 'R')
            {
                if (!string.IsNullOrEmpty(move))
                {
                    yield return new MoveInstruction(int.Parse(move));
                    move = string.Empty;
                }

                yield return new RotateInstruction(instruction);
                continue;
            }

            move += instruction;
        }

        if (!string.IsNullOrEmpty(move))
        {
            yield return new MoveInstruction(int.Parse(move));
        }
    }

    private char[,] ParseGrid(string[] gridlines)
    {
        var maxLength = gridlines.Max(line => line.Length);
        for (var index = 0; index < gridlines.Length; index++)
        {
            var line = gridlines[index];
            gridlines[index] = line.PadRight(maxLength);
        }

        return gridlines.GetGrid(c => c);
    }

    private char[,][,] ParseGrid2(string[] gridlines)
    {
        var maxLength = gridlines.Max(line => line.Length);
        var maxHeight = gridlines.Length;

        for (var index = 0; index < gridlines.Length; index++)
        {
            var line = gridlines[index];
            gridlines[index] = line.PadRight(maxLength);
        }
        
        var cols = maxLength < maxHeight ? 3 : 4;
        var rows = maxLength < maxHeight ? 4 : 3;

        var size = maxLength / cols;

        char[,][,] result = new char[rows,cols][,];

        foreach (var col in 0..(cols - 1))
        {
            foreach (var row in 0..(rows - 1))
            {
                 var grid = new char[size, size];

                for (int gridcol = 0; gridcol < size; gridcol++)
                {
                    for (int gridrow = 0; gridrow < size; gridrow++)
                    {
                        grid[gridrow, gridcol] = gridlines[(row * size) + gridrow][(col * size) + gridcol];
                    }
                }

                result[row, col] = grid;
            }
        }



        return result;
    }



    private interface IInstruction
    {
    }

    private record struct MoveInstruction(int amount) : IInstruction;

    private record struct RotateInstruction(char direction) : IInstruction;

}
