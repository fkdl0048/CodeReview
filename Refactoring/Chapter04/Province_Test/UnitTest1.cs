namespace Province_Test;

[TestFixture]
public class Tests
{
    private Province asia;

    [SetUp]
    public void SetUp()
    {
        asia = Chapter04.Program.SampleProvinceData();
    }

    [Test]
    public void Shortfall()
    {
        Assert.AreEqual(5, asia.Shortfall());
    }

    [Test]
    public void Profit()
    {
        Assert.AreEqual(230, asia.Profit());
    }
}