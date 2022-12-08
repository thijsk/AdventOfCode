using System.Diagnostics;
using System.Reflection;
using Common;
using TextCopy;

var dayClasses = Assembly.GetExecutingAssembly().GetTypes()
    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IDay))).OrderBy(t => int.Parse(t.Name[3..]));

foreach (var dayClass in dayClasses
             .Where(d => d.Name == "Day00")
             .TakeLast(1))
{
    var name = dayClass.Name;
    var day = (IDay)Activator.CreateInstance(dayClass)!;
    
    Console.WriteLine(name);
    BuildContext(name);

    RunDay(day.Part1);
    RunDay(day.Part2);
}

void RunDay(Func<long> part)
{
    var title = part.Target?.ToString();
    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var answer = part().ToString();
    stopwatch.Stop();
    ConsoleX.WriteLine($"{title}   : {answer}", ConsoleColor.Red);
    Console.WriteLine($"Elapsed : {stopwatch.Elapsed}");
    if (answer != "0") ClipboardService.SetText(answer);
}

void BuildContext(string name)
{
    var directory = Aoc.GetSourceDirectory();
    var fileName = $"{name}.txt";
    PuzzleContext.Input = File.ReadAllLines(Path.Combine(directory, fileName));
    var exampleFileName = $"{name}-example.txt";
    if (File.Exists(Path.Combine(directory, exampleFileName)))
    {
        PuzzleContext.Example = File.ReadAllLines(Path.Combine(directory, exampleFileName));
    }
}