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
        public void GetRowCount_ShouldReturnXLength()
        {
            var _sut = new[,]
            {
                { 1, 2, 3},
                { 4, 5, 6}
            };
            var actual = _sut.GetRowCount();

            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void GetColumnCount_ShouldReturnYLength()
        {
            var _sut = new[,]
            {
                { 1, 2, 3},
                { 4, 5, 6}
            };
            var actual = _sut.GetColumnCount();

            Assert.AreEqual(3, actual);
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
            var actual2 = _sut.GetNeighbors((1,1));
            CollectionAssert.AreEqual(actual, actual2);
            
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
            var actual2 = _sut.GetNeighbors((0, 0));
            CollectionAssert.AreEqual(actual, actual2);

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
            var actual2 = _sut.GetNeighbors((1, 2));
            CollectionAssert.AreEqual(actual, actual2);

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
            var actual2 = _sut.GetNeighborsDiagonal((1, 1));
            CollectionAssert.AreEqual(actual, actual2);

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
            var actual2 = _sut.GetNeighborsDiagonal((0, 1));
            CollectionAssert.AreEqual(actual, actual2);

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
            var actual2 = _sut.GetNeighborsDiagonal((2, 2));
            CollectionAssert.AreEqual(actual, actual2);

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
            var actual2 = _sut.GetAllNeighbors((1, 1));
            CollectionAssert.AreEqual(actual, actual2);

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
            var actual2 = _sut.GetAllNeighbors((1, 2));
            CollectionAssert.AreEqual(actual, actual2);

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
            var actual2 = _sut.GetAllNeighbors((0, 0));
            CollectionAssert.AreEqual(actual, actual2);

            var expected = new[] { (0, 1), (1, 0), (1, 1) };

            CollectionAssert.AreEqual(expected.Order().ToList(), actual.Order().ToList());
        }

        [TestMethod]
        public void RotateLeft_ShouldRotateBy90Degrees_3x3()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.RotateLeft();

            var expected =  new[,] {
                { 3, 6, 9},
                { 2, 5, 8},
                { 1, 4, 7}
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RotateLeft_ShouldRotateBy90Degrees_2x2()
        {
            var _sut = new[,] {
                { 1, 2},
                { 3, 4}
            };
            var actual = _sut.RotateLeft();

            var expected = new[,] {
                { 2, 4},
                { 1, 3}
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RotateLeft_ShouldRotateBy90Degrees_1x3()
        {
            var _sut = new[,] {
                { 1, 2, 3}
            };
            var actual = _sut.RotateLeft();

            var expected = new[,] {
                { 3,},
                { 2 },
                { 1 }
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RotateRight_ShouldRotateBy90Degrees_3x3()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.RotateRight();

            var expected = new[,] {
                { 7, 4, 1},
                { 8, 5, 2},
                { 9, 6, 3}
            };

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void RotateRight_ShouldRotateBy90Degrees_2x2()
        {
            var _sut = new[,] {
                { 1, 2},
                { 3, 4}
            };
            var actual = _sut.RotateRight();

            var expected = new[,] {
                { 3, 1},
                { 4, 2}
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RotateRight_ShouldRotateBy90Degrees_1x3()
        {
            var _sut = new[,] {
                { 1, 2, 3}
            };
            var actual = _sut.RotateRight();

            var expected = new[,] {
                { 1,},
                { 2 },
                { 3 }
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipHorizontal_ShouldFlipAlongTheHorizontalAxis_3x3()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.FlipHorizontal();

            var expected = new[,] {
                { 7, 8, 9},
                { 4, 5, 6},
                { 1, 2, 3}
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipHorizontal_ShouldFlipAlongTheHorizontalAxis_2x2()
        {
            var _sut = new[,] {
                { 1, 2 },
                { 3, 4 }
            };
            var actual = _sut.FlipHorizontal();

            var expected = new[,] {
                { 3, 4 },
                { 1, 2 }
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipHorizontal_ShouldFlipAlongTheHorizontalAxis_1x3()
        {
            var _sut = new[,] {
                { 1 },
                { 2 },
                { 3 }
            };
            var actual = _sut.FlipHorizontal();

            var expected = new[,] {
                { 3 },
                { 2 },
                { 1 }
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipHorizontal_ShouldFlipAlongTheHorizontalAxis_3x1()
        {
            var _sut = new[,] {
                { 1, 2, 3 }
            };
            var actual = _sut.FlipHorizontal();

            var expected = new[,] {
                { 1, 2, 3 }
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipVertical_ShouldFlipAlongTheVerticalAxis_3x3()
        {
            var _sut = new[,] {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            };
            var actual = _sut.FlipVertical();

            var expected = new[,] {
                { 3, 2, 1},
                { 6, 5, 4},
                { 9, 8, 7}
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipVertical_ShouldFlipAlongTheVerticalAxis_2x2()
        {
            var _sut = new[,] {
                { 1, 2 },
                { 3, 4 }
            };
            var actual = _sut.FlipVertical();

            var expected = new[,] {
                { 2, 1 },
                { 4, 3 }
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipVertical_ShouldFlipAlongTheVerticalAxis_1x3()
        {
            var _sut = new[,] {
                { 1, 2, 3 }
            };
            var actual = _sut.FlipVertical();

            var expected = new[,] {
                { 3, 2, 1}
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlipVertical_ShouldFlipAlongTheVerticalAxis_3x1()
        {
            var _sut = new[,] {
                { 1 },
                { 2 },
                { 3 }
            };
            var actual = _sut.FlipVertical();

            var expected = new[,] {
                { 1 },
                { 2 },
                { 3 }
            };

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}