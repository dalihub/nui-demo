using NUnit.Framework;

namespace LayoutingTests
{
    [TestFixture]
    public class TestOne
    {
	[Test]
 	public void HasControllerBeenCreated()
 	{
	   var result = true;
	   Assert.IsFalse(result, "1 should not be prime");
	}
    }
}
