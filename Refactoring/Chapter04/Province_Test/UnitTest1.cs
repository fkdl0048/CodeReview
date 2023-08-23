namespace Province_Test;

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
}