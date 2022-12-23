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


        return PuzzleContext.Answer1;
        
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
        PuzzleContext.Answer2 = 0;
        PuzzleContext.UseExample = true;

        var orientations = new[] { 'R', 'D', 'L', 'U' };

        var size = 50;
        if (PuzzleContext.UseExample)
            size = 4;

        var (grid, instructions) = Parse(PuzzleContext.Input);

        var location = (row: 0, col: Array.IndexOf(grid.GetRow(0), '.'));

        var orientation = 0;
        foreach (var instruction in instructions)
        {
            ConsoleX.WriteLine($"Location {location}, Orientation: {orientations[orientation]}, Instruction: {instruction}");

            if (instruction is RotateInstruction rotate)
            {
                orientation = DoRotate(rotate.direction, orientation);
                continue;
            }

            var move = (MoveInstruction)instruction;

            var amount = move.amount;
            while (amount > 0)
            {
                var nextlocation = Step(grid, location, orientation, size);
                if (IsWall(grid, nextlocation))
                {
                    amount = 0;
                    break;
                }

                location = nextlocation;
                amount--;
            }
        }

        return ((location.row + 1) * 1000L) + ((location.col + 1) * 4) + orientation;
    }

    private (int row, int col) Step(char[,] grid, (int row, int col) location, int orientation, int size)
    {
        (int row, int col) nextlocation = orientation switch
        {
            0 => (location.row, location.col + 1), //R
            1 => (location.row + 1, location.col), //D
            2 => (location.row, location.col - 1), //L
            3 => (location.row - 1, location.col), //U
        };

        if (orientation is 0 or 2)
        {
            var row = grid.GetRow(location.row);
            if (nextlocation.row > row.Length || nextlocation.row < 0 || grid[nextlocation.row, nextlocation.col] == ' ')
            {
                var index = GetIndex(location, size);
                // find next 
            }
            else
                return nextlocation;
        }

        if (orientation is 1 or 3)
        {
            var col = grid.GetColumn(location.col);
            if (nextlocation.col > col.Length || nextlocation.col < 0 || grid[nextlocation.row, nextlocation.col] == ' ')
            {
                nextlocation = StepToNextFace(grid, location, orientation, size);
                // find next 
            }
            else
                return nextlocation;
        }

        throw new NotImplementedException();
    }

    private (int row, int col) StepToNextFace(char[,] grid, (int row, int col) location, int orientation, int size)
    {
        var currentface = GetIndex(location, size);

        var (nextface, nextorientation) = Lookup(currentface, orientation);

        switch (orientation, nextorientation)
        {
            case (0, 1):
            {
                
                break;
            }
        }

        throw new NotImplementedException();
    }

    private Dictionary<((int col, int row), int orientation), ((int col, int row), int orientation)> _map = new()
    {
        {((1, 2), 0), ((2, 3), 1)}
    };

    private ((int row, int col) nextface, int orientation) Lookup((int row, int col) currentface, int orientation)
    {
        return _map[(currentface, orientation)];
    }

    private (int row, int col) GetIndex((int row, int col) location, int size)
    {
        return (location.row / size, col: location.col/size);
    }

    private bool IsWall(char[,] grid, (int row, int col) location)
    {
        return grid[location.row, location.col] == '#';
    }


    private bool IsGrid(char[,][,] map, (int row, int col) grid)
    {
        if (grid.row < 0 || grid.row > map.GetLength(0))
            return false;
        if (grid.col < 0 || grid.col > map.GetLength(0))
            return false;
        return true;
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
