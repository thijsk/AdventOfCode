using Common;
using System;
using System.IO.MemoryMappedFiles;

namespace ConsoleApp2022;

public class Day22 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 27436;
        PuzzleContext.UseExample = false;


        //return PuzzleContext.Answer1;
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

        var (map, instructions) = Parse2(PuzzleContext.Input);

        var (location, currentgrid) = GetStartLocation(map);


        ConsoleX.WriteLine($"{currentgrid} {location}");

        var orientation = 0;
        foreach (var instruction in instructions)
        {
         
        }

        return ((location.row + 1) * 1000L) + ((location.col + 1) * 4) + orientation;
    }



    private bool IsGrid(char[,][,] map, (int row, int col) grid)
    {
        if (grid.row < 0 || grid.row > map.GetLength(0))
            return false;
        if (grid.col < 0 || grid.col > map.GetLength(0))
            return false;
        return true;
    }

    private static ((int row, int col) location, (int row, int col) currentgrid) GetStartLocation(char[,][,] grid)
    {
        var location = (row: 0, col: 0);
        var currentgrid = (row: 0, col: 0);

        while (true)
        {
            var startgrid = grid[currentgrid.row, currentgrid.col];
            if (startgrid[0, 0] == ' ') // empty grid
            {
                currentgrid.col++;
                continue;
            }

            location = (row: 0, col: Array.IndexOf(startgrid.GetRow(0), '.'));
            break;
        }

        return (location, currentgrid);
    }

    private static int DoRotate(RotateInstruction rotate, int orientation)
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
