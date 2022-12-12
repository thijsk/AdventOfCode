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

        [TestMethod]
        public void GetNeighbors_ShouldReturnHorizontalAndVertical()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetNeighbors(1, 1);

            var expected = new[] {(0,1), (1,0), (1,2), (2,1)};

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void GetNeighbors_ShouldReturnHorizontalAndVertical_WhenInCorner()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetNeighbors(0, 0);

            var expected = new[] { (0, 1), (1, 0)};

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void GetNeighbors_ShouldReturnHorizontalAndVertical_WhenOnEdge()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetNeighbors(1, 2);

            var expected = new[] { (0, 2), (1, 1), (2,2) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }


        [TestMethod]
        public void GetNeighborsDiagnal_ShouldReturnDiagonals()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetNeighborsDiagonal(1, 1);

            var expected = new[] { (0, 0), (0, 2), (2, 0), (2, 2) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void GetNeighborsDiagnal_ShouldReturnDiagonals_WhenOnEdge()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetNeighborsDiagonal(0, 1);

            var expected = new[] { (1, 0), (1, 2) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void GetNeighborsDiagnal_ShouldReturnDiagonals_WhenInCorner()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetNeighborsDiagonal(2, 2);

            var expected = new[] { (1, 1) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void GetAllNeighbors_ShouldReturnAll()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetAllNeighbors(1, 1);

            var expected = new[] { (0, 0), (0,1), (0, 2), (1, 0), (1, 2), (2, 0), (2,1), (2, 2) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void GetAllNeighbors_ShouldReturnSome_WhenOnEdge()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetAllNeighbors(1, 2);

            var expected = new[] { (0, 1), (0, 2), (1, 1), (2, 1), (2, 2) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void GetAllNeighbors_ShouldReturnSome_WhenInCorner()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.GetAllNeighbors(0, 0);

            var expected = new[] { (0, 1), (1, 0), (1, 1) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }
    }
}