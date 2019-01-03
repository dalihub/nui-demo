using NUnit.Framework;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutingTests
{
    [TestFixture]
    public class LayoutLengthTest
    {
    [Test]
 	public void LayoutLength_ConstructAndCompare()
 	{
	   var result = false;
       const int TEST_VALUE = 16;
       LayoutLength maxWidth = new LayoutLength(TEST_VALUE);
       if( maxWidth.Value == TEST_VALUE )
       {
           result = true;
       }

	   Assert.IsTrue(result, "LayoutLength not retuning value set");
	}

    [Test]
 	public void LayoutLength_ConstructAndReplace()
 	{
	   var result = false;
       const int ORIGINAL_TEST_VALUE = 16;
       const int REPLACEMENT_TEST_VALUE = 16;
       LayoutLength maxWidth = new LayoutLength(ORIGINAL_TEST_VALUE);
       maxWidth = new LayoutLength(REPLACEMENT_TEST_VALUE);
       if( maxWidth.Value == REPLACEMENT_TEST_VALUE )
       {
           result = true;
       }

	   Assert.IsTrue(result, "LayoutLength not retuning replaced value");
	}


    }
}
