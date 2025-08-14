public class SampleTests
{
    private int value;

    [Setup]
    public void Init()
    {
        value = 5;
    }

    [Test]
    public void TestAddition()
    {
        if (value + 5 != 10)
            throw new Exception("Addition failed");
    }

    [Test]
    public void TestFailure()
    {
        throw new Exception("Intentional failure");
    }

    [Teardown]
    public void Cleanup()
    {
        value = 0;
    }
}