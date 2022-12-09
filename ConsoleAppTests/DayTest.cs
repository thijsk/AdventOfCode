using System.Reflection;
using Common;

namespace ConsoleAppTests
{
    [TestClass] 
    public class DayTest
    {
        private DayRunner _sut;

        [TestInitialize]
        public void Init()
        {
            var files = Directory.EnumerateFiles(AppContext.BaseDirectory, "ConsoleApp*.dll");
            foreach (var file in files)
            {
                Assembly.LoadFrom(file);
            }
            var days = DayRunner.GetAllIDays();
            _sut = new DayRunner(days.Last());

        }

        [TestMethod]
        public void TestPart1()
        {
            var actual = _sut.RunPart1();
            var expected = PuzzleContext.Answer1;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPart2()
        {
            var actual = _sut.RunPart2();
            var expected = PuzzleContext.Answer2;
            Assert.AreEqual(expected, actual);
        }
    }
}