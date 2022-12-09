using Common;

var dayClasses = DayRunner.GetAllIDays();
var dayClass = dayClasses
    //.Where(d => d.Name == "Day00")
    .Last();

var runner = new DayRunner(dayClass);
runner.RunPart1();
runner.RunPart2();