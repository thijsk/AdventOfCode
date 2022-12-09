using Common;

namespace CommonTests
{
    [TestClass]
    public class ArrayExtensionTests
    {
        [DataTestMethod]
        [DataRow("abcd",1, 2, "bc")]
        [DataRow("abcd", 0, 0, "a")]
        [DataRow("abcd", 3, 3, "d")]
        [DataRow("abcd", 2, 1, "cb")]
        [DataRow("abcd", 0, 7, "abcd")]
        [DataRow("abcd", 7, 0, "dcba")]
        public void GetSubRange_ShouldReturnCorrectRange(string input, int start, int end, string expected)
        {
            var actual = input.ToArray().SubRange(start, end);

            CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
        }
    }
}