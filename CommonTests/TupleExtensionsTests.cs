using Common;

namespace CommonTests;

[TestClass]
public class TupleExtensionsTests
{

    [TestMethod]
    public void TestAdd_ShouldAddPositive()
    {
        var a = (1, 2);
        var b = (3, 4);
        var c = a.Add(b);
        Assert.AreEqual(4, c.Item1);
        Assert.AreEqual(6, c.Item2);
    }

    [TestMethod]
    public void TestAdd_ShouldAddNegative()
    {
        var a = (1, 2);
        var b = (-3, -4);
        var c = a.Add(b);
        Assert.AreEqual(-2, c.Item1);
        Assert.AreEqual(-2, c.Item2);
    }

    [TestMethod]
    public void TestAdd_ShouldAddPositiveScalar()
    {
        var a = (1, 2);
        var b = 3;
        var c = a.Add(b);
        Assert.AreEqual(4, c.Item1);
        Assert.AreEqual(5, c.Item2);
    }

    [TestMethod]
    public void TestAdd_ShouldAddNegativeScalar()
    {
        var a = (1, 2);
        var b = -3;
        var c = a.Add(b);
        Assert.AreEqual(-2, c.Item1);
        Assert.AreEqual(-1, c.Item2);
    }

    [TestMethod]
    public void TestMultipy_ShouldMultiplyPositive()
    {
        var a = (1, 2);
        var b = (3, 4);
        var c = a.Multiply(b);
        Assert.AreEqual(3, c.Item1);
        Assert.AreEqual(8, c.Item2);
    }

    [TestMethod]
    public void TestMultipy_ShouldMultiplyNegative()
    {
        var a = (1, 2);
        var b = (-3, -4);
        var c = a.Multiply(b);
        Assert.AreEqual(-3, c.Item1);
        Assert.AreEqual(-8, c.Item2);
    }

    [TestMethod]
    public void TestMultipy_ShouldMultiplyPositiveScalar()
    {
        var a = (1, 2);
        var b = 3;
        var c = a.Multiply(b);
        Assert.AreEqual(3, c.Item1);
        Assert.AreEqual(6, c.Item2);
    }

    [TestMethod]
    public void TestMultipy_ShouldMultiplyNegativeScalar()
    {
        var a = (1, 2);
        var b = -3;
        var c = a.Multiply(b);
        Assert.AreEqual(-3, c.Item1);
        Assert.AreEqual(-6, c.Item2);
    }

}