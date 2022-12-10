using Common;

namespace ConsoleApp2022;

public class Day10 : IDay
{
    int register = 0; 
    int clock = 0;
    long signal = 0;
    int checknext = 20;
    private int[] sprite = {0, 1, 2};


    public long Part1()
    {
        PuzzleContext.Answer1 = 14860;
        return Part2();
    }

    private void Tick()
    {
        clock++;
        if (clock % checknext == 0)
        {
            signal += (clock * register);
            checknext += 40;
        }
    }

    public long Part2()
    {
        PuzzleContext.Answer2 = 14860;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse).ToArray();

        register = 1;
        clock = 0;
        signal = 0;
        checknext = 20;

        foreach ((string instruction, int? value) in input)
        {
            switch (instruction)
            {
                case "noop":
                    Draw();
                    Tick();
                   
                    break;
                case "addx":
                    Draw();
                    Tick();
                    Draw();
                    Tick();
                    
                    register += value.Value;
                    sprite = sprite = new[] { register-1, register, register+1 };
                    break;
                default:
                    throw new NotImplementedException();
            }

           
        }

        Console.WriteLine();
        Console.WriteLine();
        return signal;
    }

    private void Draw()
    {
        if (clock % 40 == 0)
        {
            Console.WriteLine();
        }
        if (sprite.Contains(clock % 40))
        {
            Console.Write('#');
        } else Console.Write('.');
        
    }

    public (string instruction, int? value) Parse(string line)
    {
        var split = line.Split(' ');

        var instruction = split[0];
        int? value = split.Length > 1 ? int.Parse(split[1]) : null;


        return (instruction, value);
    }

}