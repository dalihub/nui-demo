using NUnit.Framework;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutingTests
{
    [TestFixture]
    public class TestOne
    {
	[Test]
 	public void HasControllerBeenCreated()
 	{
	   var result = false;
       const int TEST_VALUE = 16;
       LayoutLength maxWidth = new LayoutLength(TEST_VALUE);
       if( maxWidth.Value == TEST_VALUE )
       {
           result = true;
       }

	   Assert.IsTrue(result, "1 should not be prime");
	}
    }
}
