using System.Reflection;
using Common;
using Xunit;
using Xunit.Internal;

namespace ConsoleAppTests
{
	public class DayTest
	{
		[Theory(DisableDiscoveryEnumeration =false)]
		[ClassData(typeof(GetDaysForTest))]
		public void TestDayPart2(Type day, int part)
		{
			var runner = new DayPartTestRunner(day, part);
			var (expected, actual) = runner.Invoke();
			Assert.Equal(expected, actual);
		}

		class GetDaysForTest : TheoryData<Type, int>
        {
            public GetDaysForTest()
            {
				var files = Directory.EnumerateFiles(AppContext.BaseDirectory, "ConsoleApp*.dll").ToArray();
				files.ForEach(file => Assembly.LoadFrom(file));
				
				var days = DayRunner.GetAllIDays();
				foreach (var day in days)
				{
					var part1 = new TheoryDataRow<Type, int>(day, 1)
					{
						TestDisplayName = $"{day.Name} Part 1 "
					};
					Add(part1);
					var part2 = new TheoryDataRow<Type, int>(day, 2)
					{
						TestDisplayName = $"{day.Name} Part 2 "
					};
					Add(part2);
				}
            }
		}

		public class DayPartTestRunner(Type dayType, int part)
        {
	        private Type DayType { get; } = dayType;
	        private int Part { get; } = part;

	        public (long expected, long actual) Invoke()
	        {
		        var day = new DayRunner(DayType);

		        if (Part == 1)
		        {
			        var actual = day.RunPart1();
			        var expected = PuzzleContext.Answer1;
			        return (expected, actual);
		        }

		        if (Part == 2)
		        {
			        var actual = day.RunPart2();
			        var expected = PuzzleContext.Answer2;
			        return (expected, actual);
		        }
		        throw new Exception("Invalid part");
	        }

	        public override string ToString()
	        {
		        return $"{DayType.Name} Part {Part}";
	        }
        }
	}
}