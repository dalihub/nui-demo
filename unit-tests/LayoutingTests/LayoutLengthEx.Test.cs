using NUnit.Framework;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutingTests
{
  [TestFixture]
  public class LayoutLengthExTest
  {
    [Test]
    public void LayoutLengthEx_ConstructAndCompare()
    {
      var result = false;
        const int TEST_VALUE = 16;
        LayoutLengthEx maxWidth = new LayoutLengthEx(TEST_VALUE);
        if( maxWidth.AsRoundedValue() == TEST_VALUE )
        {
            result = true;
        }

      Assert.IsTrue(result, "LayoutLength not returning value set");
    }

    [Test]
    public void LayoutLengthEx_ConstructAndReplace()
    {
      var result = false;
        const int ORIGINAL_TEST_VALUE = 16;
        const int REPLACEMENT_TEST_VALUE = 16;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE);
        maxWidth = new LayoutLengthEx(REPLACEMENT_TEST_VALUE);
        if( maxWidth.AsRoundedValue() == REPLACEMENT_TEST_VALUE )
        {
            result = true;
        }

      Assert.IsTrue(result, "LayoutLength not returning replaced value");
    }

    [Test]
    public void LayoutLengthEx_ConstructInt()
    {
        var result = false;
        const int ORIGINAL_TEST_VALUE = 16;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE);
        if( maxWidth.AsRoundedValue() == ORIGINAL_TEST_VALUE )
        {
            result = true;
        }

      Assert.IsTrue(result, "LayoutLength not returning rounded value that was set");
    }

    [Test]
    public void LayoutLengthEx_ConstructFloat()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 16.4f;
        const float ROUNDED_TEST_VALUE = 16.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE);

        if( maxWidth.AsRoundedValue() == ROUNDED_TEST_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning rounded value that was set");
    }

    [Test]
    public void LayoutLengthEx_Multiply()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 6.0f;
        const float ROUNDED_RESULT_VALUE = 36.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) * new LayoutLengthEx(ORIGINAL_TEST_VALUE);

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning multipied result");
    }

    [Test]
    public void LayoutLengthEx_MultiplyByInt()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 6.0f;
        const float ROUNDED_RESULT_VALUE = 18.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) * 3;

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning multipied result");
    }

    [Test]
    public void LayoutLengthEx_Divide()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 18.0f;
        const float DENOMINATOR_VALUE = 3.0f;
        const float ROUNDED_RESULT_VALUE = 6.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) / new LayoutLengthEx(DENOMINATOR_VALUE);

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning divided result");
    }

    [Test]
    public void LayoutLengthEx_DividedByInt()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 18.0f;
        const float ROUNDED_RESULT_VALUE = 6.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) / 3;

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning divided result");
    }

    [Test]
    public void LayoutLengthEx_Subtraction()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 18.0f;
        const float SUBTRACT_VALUE = 6.0f;
        const float ROUNDED_RESULT_VALUE = 12.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) - new LayoutLengthEx(SUBTRACT_VALUE);

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning subtracted result");
    }

    [Test]
    public void LayoutLengthEx_Addition()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 3.0f;
        const float ROUNDED_RESULT_VALUE = 6.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) + new LayoutLengthEx(ORIGINAL_TEST_VALUE);

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning added result");
    }

    [Test]
    public void LayoutLengthEx_Int_Addition()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 3.0f;
        const float ROUNDED_RESULT_VALUE = 6.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) + 3;

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning added result");
    }

    [Test]
    public void LayoutLengthEx_Int_Subtraction()
    {
        var result = false;
        const float ORIGINAL_TEST_VALUE = 18.0f;
        const float ROUNDED_RESULT_VALUE = 12.0f;
        LayoutLengthEx maxWidth = new LayoutLengthEx(ORIGINAL_TEST_VALUE) - 6;

        if( maxWidth.AsRoundedValue() == ROUNDED_RESULT_VALUE )
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutLength not returning subtracted result");
    }
  }
}
