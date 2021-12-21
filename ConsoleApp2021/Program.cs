using System.Diagnostics;
using System.Reflection;
using Common;
using TextCopy;

var dayClasses = Assembly.GetExecutingAssembly().GetTypes()
    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IDay))).OrderBy(t => int.Parse(t.Name[3..]));

foreach (var dayClass in dayClasses/*.Where(d => d.Name == "Day19")*/.TakeLast(1))
{
    Console.WriteLine(dayClass.Name);
    var day = (IDay)Activator.CreateInstance(dayClass)!;

    PuzzleContext.Input = File.ReadAllLines(dayClass.Name + ".txt");
    if (File.Exists(dayClass.Name + "-example.txt"))
    {
        PuzzleContext.Example = File.ReadAllLines(dayClass.Name + "-example.txt");
    }

    var stopwatch = new Stopwatch();
    stopwatch.Start();
    var answer1 = day.Part1().ToString();
    stopwatch.Stop();
    ConsoleX.WriteLine("Part1   : " + answer1, ConsoleColor.Red);
    Console.WriteLine($"Elapsed : {stopwatch.Elapsed}");
    if (answer1 != "0") ClipboardService.SetText(answer1);

    stopwatch.Restart();
    var answer2 = day.Part2().ToString();
    stopwatch.Stop();
    ConsoleX.WriteLine("Part2   : " + answer2, ConsoleColor.Red);
    Console.WriteLine($"Elapsed : {stopwatch.Elapsed}");
    if (answer2 != "0") ClipboardService.SetText(answer2);
}

//Console.ReadLine();