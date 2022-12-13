using System.Text.Json.Nodes;
using Common;

namespace CommonTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Backwards_ShouldReverseString()
        {
            var input = "abcd";
            var actual = input.Backwards();
            var expected = "dcba";
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("abcd", "abcd")]
        [DataRow("dcba", "abcd")]
        [DataRow("1234", "1234")]
        [DataRow("4321", "1234")]
        [DataRow("12ab", "12ab")]
        [DataRow("ab12", "12ab")]
        [DataRow("1a2b", "12ab")]
        [DataRow("b2a1", "12ab")]
        public void Sort_ShouldOrderAscending(string input, string expected)
        {
            var actual = input.Sort();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SplitOnNewlines_ShouldHandleAllNewlines()
        {
            var input = "a\rb\nc\r\nd\te";

            var expected = new [] {"a", "b", "c", "d\te"};
            var actual = input.SplitOnNewlines();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AsJson_ShouldHandleArray()
        {
            var actual = "[1,2]".AsJson();

            Assert.IsInstanceOfType(actual, typeof(JsonArray));
            CollectionAssert.AreEqual(new[]{1,2}, actual.AsArray().Select(j => j.GetValue<int>()).ToList());
        }

        [TestMethod]
        public void AsJson_ShouldHandleInt()
        {
            var actual = "1".AsJson();

            Assert.IsInstanceOfType(actual, typeof(JsonValue));
            Assert.AreEqual(1, actual.GetValue<int>());
        }

        [TestMethod]
        public void AsJson_ShouldHandleNestedArray()
        {
            var actual = "[[1],[2]]".AsJson();
            Assert.IsInstanceOfType(actual, typeof(JsonArray));
        }

    }
}
