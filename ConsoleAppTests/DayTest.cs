using System.Reflection;
using Common;

namespace ConsoleAppTests
{
    [TestClass] 
    public class DayTest
    {
        public static IEnumerable<object[]> DaysForTest
        {
            get
            {
                var files = Directory.EnumerateFiles(AppContext.BaseDirectory, "ConsoleApp*.dll");
                foreach (var file in files)
                {
                    Assembly.LoadFrom(file);
                }
                var days = DayRunner.GetAllIDays();
                return days.SelectMany(GetWrapper);
            }
        }

        private static IEnumerable<object[]> GetWrapper(Type arg)
        {
            yield return new[] { new DayWrapper(arg, 1) };
            yield return new[] { new DayWrapper(arg, 2) };
        }

        public class DayWrapper
        {
            public DayWrapper(Type dayType, int part)
            {
                this.dayType = dayType;
                this.part = part;
            }

            private Type dayType { get; }
            private int part { get; }

            public (long expected, long actual) Invoke()
            {
                var day = new DayRunner(dayType);

                if (part == 1)
                { 
                    var actual = day.RunPart1();
                    var expected = PuzzleContext.Answer1;
                    return (expected, actual);
                }

                if (part == 2)
                {
                    var actual = day.RunPart2();
                    var expected = PuzzleContext.Answer2;
                    return (expected, actual);
                }
                throw new Exception("Invalid part");
            }

            public override string ToString()
            {
                return $"{dayType.Name} Part {part}";
            }
        }

        public static string GetDynamicDataDisplayName(MethodInfo methodInfo, object[] data)
        {
            return ((DayWrapper)data[0]).ToString();
        }

        [TestMethod]
        [DynamicData(nameof(DaysForTest), DynamicDataDisplayName = nameof(GetDynamicDataDisplayName))]
        public void TestDayPart(DayWrapper sut)
        {
            var (expected, actual) = sut.Invoke();
            Assert.AreEqual(expected, actual);
        }
    }
}