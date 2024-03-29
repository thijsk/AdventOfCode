﻿using Common;
using TextCopy;

var dayClasses = DayRunner.GetAllIDays();
var dayClass = dayClasses
    //.Where(d => d.Name == "Day05")
    .Last();

var runner = new DayRunner(dayClass);
var answer = runner.RunPart1();
if (answer != 0) ClipboardService.SetText(answer.ToString());
answer = runner.RunPart2();
if (answer != 0) ClipboardService.SetText(answer.ToString());