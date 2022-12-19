using Common;

namespace CommonTests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {

        [TestMethod]
        public void SlidingWindow()
        {
            var input = new[] {1, 2, 3, 4};

            var sut = input.SlidingWindow().ToList();

            Assert.AreEqual((0, 1, 2), sut.First());

            Assert.AreEqual((3, 4, 0), sut.Last());

            Assert.AreEqual((1, 2, 3), sut.Skip(1).First());
        }

        [TestMethod]
        public void SlidingWindow_WithNullValue()
        {
            var input = new int?[] {1, 2, 3, 4};

            var sut = input.SlidingWindow().ToList();

            Assert.AreEqual((null, 1, 2), sut.First());

            Assert.AreEqual((3, 4, null), sut.Last());

            Assert.AreEqual((1, 2, 3), sut.Skip(1).First());
        }

        [TestMethod]
        public void SlidingWindow_WithCustomValue()
        {
            var input = new object[] {1, 2, 3, 4};
            const string emptyValue = "";

            var sut = input.SlidingWindow(emptyValue).ToList();

            Assert.AreEqual((emptyValue, 1, 2), sut.First());

            Assert.AreEqual((3, 4, emptyValue), sut.Last());

            Assert.AreEqual((1, 2, 3), sut.Skip(1).First());
        }

        [TestMethod]
        public void GetPermutations()
        {
            var input = new int[] {1, 2};

            var sut = input.GetPermutations(2).Select(ints => ints.ToArray()).ToArray();

            var expected = new[] {new[] {1, 2}, new[] {2, 1}};
            Assert.AreEqual(expected.Length, sut.Length);
            foreach (var ex in expected)
            {
                Assert.IsTrue(sut.Any(ac => ac.SequenceEqual(ex)));
            }
        }

        [TestMethod]
        public void GetPermutations_WhenLargeThanSize_ShouldReturnEmpty()
        {
            var input = new int[] {1, 2};

            var sut = input.GetPermutations(3).Select(ints => ints.ToArray()).ToArray();

            Assert.AreEqual(0, sut.Length);
        }


        [TestMethod]
        public void GetPermutations_WhenSizeIsBigger()
        {
            var input = new int[] {1, 2, 3};

            var sut = input.GetPermutations(2).Select(ints => ints.ToArray()).ToArray();

            CollectionAssert.AllItemsAreUnique(sut);
            var expected = new[]
            {
                new[] {1, 2}, new[] {1, 3},
                new[] {2, 3}, new[] {2, 1},
                new[] {3, 1}, new[] {3, 2}
            };

            Assert.AreEqual(expected.Length, sut.Length);
            foreach (var ex in expected)
            {
                Assert.IsTrue(sut.Any(ac => ac.SequenceEqual(ex)));
            }
        }

        [TestMethod]
        public void GetPowerSet()
        {
            var input = new[] {1, 2, 3};

            var sut = input.GetPowerSet().Select(ints => ints.ToArray()).ToArray();

            CollectionAssert.AllItemsAreUnique(sut);
            var expected = new[]
            {
                new int[] { },
                new[] {1}, new[] {2}, new[] {3},
                new[] {1, 2}, new[] {2, 3}, new[] {1, 3},
                new[] {1, 2, 3}
            };

            Assert.AreEqual(expected.Length, sut.Length);
            foreach (var ex in expected)
            {
                Assert.IsTrue(sut.Any(ac => ac.SequenceEqual(ex)));
            }
        }

        [TestMethod]
        public void AsCircular()
        {
            var input = new[] {1, 2, 3};

            var sut = input.AsCircular();

            Assert.AreEqual(1, sut.First());
            Assert.AreEqual(2, sut.Skip(1).First());
            Assert.AreEqual(1, sut.Skip(3).First());
            Assert.AreEqual(1, sut.Skip(6).First());
            Assert.AreEqual(3, sut.Skip(8).First());
        }

        [TestMethod]
        public void AsRepeat()
        {
            var input = new[] { 1, 2, 3 };

            var sut = input.Repeat(10);
            
            Assert.AreEqual(30, sut.Count());

            Assert.AreEqual(1, sut.First());
            Assert.AreEqual(2, sut.Skip(1).First());
            Assert.AreEqual(3, sut.Skip(2).First());
            Assert.AreEqual(1, sut.Skip(3).First());
            Assert.AreEqual(2, sut.Skip(4).First());
            Assert.AreEqual(3, sut.Last());
        }

        [TestMethod]
        public void IntersectMany()
        {
            //TODO
        }


        [TestMethod]
        public void Median_Odd()
        {
            var input = new[] { 1, 3, 5 };

            var sut = input.Median();

            Assert.AreEqual(3, sut);
        }

        [TestMethod]
        public void Median_Even()
        {
            var input = new[] { 1, 3, 5, 7 };

            var sut = input.Median();

            Assert.AreEqual(4, sut);
        }

        [TestMethod]
        public void GetAllBinaryPartitions()
        {
            var input = new[] { 1, 2 };

            var sut = input.GetAllBinaryPartitions().Select(p => (first: p.first.ToArray(), second: p.second.ToArray())).ToArray();

            var expected = new[]
            {
                (first: new int[] { }, second: new[] { 1, 2 }),
                (first: new [] { 1 }, second: new[] { 2 }),
                (first: new [] { 2 }, second: new[] { 1 }),
                (first: new [] { 1, 2 }, second: new int[] { })
            };

            Assert.AreEqual(4, sut.Length);
            foreach (var ex in expected)
            {
                Assert.IsTrue(sut.Any(ac => ac.first.SequenceEqual(ex.first) && ac.second.SequenceEqual(ex.second)));
            }
        }


        [TestMethod]
        public void FilterReverseEqualPartitions()
        {
            var input = (new[] {1, 2}).GetAllBinaryPartitions();

            var sut = input.FilterReverseEqualPartitions().Select(p => (first: p.first.ToArray(), second: p.second.ToArray())).ToArray();

            var expected = new[]
            {
                (first: new int[] { }, second: new[] { 1, 2 }),
                (first: new [] { 1 }, second: new[] { 2 }),
            };

            Assert.AreEqual(2, sut.Length);
            foreach (var ex in expected)
            {
                Assert.IsTrue(sut.Any(ac => ac.first.SequenceEqual(ex.first) && ac.second.SequenceEqual(ex.second)));
            }
        }

        [TestMethod]
        public void GetHashCodeOfList()
        {
            var input1 = new[] { 1, 2 };
            var input2 = new[] { 1, 2 };

            var actual1 = input1.GetHashCodeOfList();
            var actual2 = input2.GetHashCodeOfList();

            Assert.AreEqual(actual1, actual2);
        }
    }
}
