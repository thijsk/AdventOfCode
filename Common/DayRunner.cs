using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TextCopy;

namespace Common;

public class DayRunner
{
    private readonly IDay _day;

    public DayRunner(Type dayType, [CallerFilePath] string callerFilePath = "")
    {
        _day = (IDay) Activator.CreateInstance(dayType);
        
        FillPuzzleContext();
    }

    public long RunPart1()
    {
        return RunPart(_day.Part1);
    }

    public long RunPart2()
    {
        return RunPart(_day.Part2);
    }

    public static IOrderedEnumerable<Type> GetAllIDays()
    {
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly =>
            assembly.GetTypes().Where(thetype => thetype.GetInterfaces().Contains(typeof(IDay)))).OrderBy(t => t.Name);
    }

    public override string ToString()
    {
        return _day.GetType().FullName;
    }

    private long RunPart(Func<long> part)
    {
        var title = $"{part.Target} - {part.Method.Name}";
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var answer = part();
        stopwatch.Stop();
        ConsoleX.WriteLine($"{title} : {answer}", ConsoleColor.Red);
        Console.WriteLine($"Elapsed : {stopwatch.Elapsed}");
        return answer;
    }

    private void FillPuzzleContext()
    {
        var dayName = _day.GetType().Name;
        var directory = AppContext.BaseDirectory;
        var fileName = $"{dayName}.txt";
        PuzzleContext.Input = File.ReadAllLines(Path.Combine(directory, fileName));
        var exampleFileName = $"{dayName}-example.txt";
        if (File.Exists(Path.Combine(directory, exampleFileName)))
        {
            PuzzleContext.Example = File.ReadAllLines(Path.Combine(directory, exampleFileName));
        }
    }
}