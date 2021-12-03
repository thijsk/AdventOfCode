using System.Reflection;
using Common;
using TextCopy;

var dayClasses = Assembly.GetExecutingAssembly().GetTypes()
    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IDay))).OrderBy(t => int.Parse(t.Name[3..]));

foreach (var dayClass in dayClasses.TakeLast(1))
{
    Console.WriteLine(dayClass.Name);
    var day = (IDay)Activator.CreateInstance(dayClass)!;

    PuzzleContext.Input = File.ReadAllLines(dayClass.Name + ".txt");
    if (File.Exists(dayClass.Name + "-example.txt"))
    {
        PuzzleContext.Example = File.ReadAllLines(dayClass.Name + "-example.txt");
    }

    var answer1 = day.Part1().ToString();
    var answer2 = day.Part2().ToString();

    Console.WriteLine("Part1 : " + answer1);
    if (answer1 != "0") ClipboardService.SetText(answer1);
    Console.WriteLine("Part2 : " + answer2);
    if (answer2 != "0") ClipboardService.SetText(answer2);
}

//Console.ReadLine();