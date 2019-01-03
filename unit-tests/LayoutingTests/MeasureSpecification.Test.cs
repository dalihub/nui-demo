using NUnit.Framework;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutingTests
{
  [TestFixture]
  public class MeasureSpecificationTest
  {
    [Test]
    public void MeasureSpecification_ConstructAndCompare()
    {
        var result = false;
        LayoutLengthEx TEST_VALUE1 = new LayoutLengthEx(16);
        MeasureSpecification.ModeType TEST_VALUE2 = MeasureSpecification.ModeType.Exactly;

        MeasureSpecification measureSpec = new MeasureSpecification(TEST_VALUE1, TEST_VALUE2);
        if((measureSpec.Size.Equals(TEST_VALUE1)) && (measureSpec.Mode == TEST_VALUE2))
        {
            result = true;
        }

        Assert.IsTrue(result, "MeasureSpecification not returning values set");
    }

    [Test]
    public void MeasureSpecification_Defaults()
    {
        var result = false;
        LayoutLengthEx TEST_VALUE1 = new LayoutLengthEx();

        MeasureSpecification measureSpec = new MeasureSpecification();

        if((measureSpec.Size.Equals(TEST_VALUE1)) && (measureSpec.Mode == MeasureSpecification.ModeType.Unspecified))
        {
            result = true;
        }

        Assert.IsTrue(result, "MeasureSpecification defaults failed to match:" + TEST_VALUE1.AsRoundedValue());
    }

    [Test]
    public void MeasureSpecification_OperatorCompare()
    {
        var result = false;
        LayoutLengthEx TEST_VALUE1 = new LayoutLengthEx(14);

        MeasureSpecification measureSpec = new MeasureSpecification(TEST_VALUE1,MeasureSpecification.ModeType.Unspecified );

        if((measureSpec.Size == TEST_VALUE1) && (measureSpec.Mode == MeasureSpecification.ModeType.Unspecified))
        {
            result = true;
        }

        Assert.IsTrue(result, "MeasureSpecification defaults failed to match:" + TEST_VALUE1.AsRoundedValue());
    }

    [Test]
    public void MeasureSpecification_EqualsCompare()
    {
        var result = false;
        LayoutLengthEx TEST_VALUE1 = new LayoutLengthEx(14);

        MeasureSpecification measureSpec = new MeasureSpecification(TEST_VALUE1,MeasureSpecification.ModeType.Unspecified );

        if((measureSpec.Size.Equals(TEST_VALUE1)) && (measureSpec.Mode == MeasureSpecification.ModeType.Unspecified))
        {
            result = true;
        }

        Assert.IsTrue(result, "MeasureSpecification defaults failed to match:" + TEST_VALUE1.AsRoundedValue());
    }

  }
} // namespace LayoutingTests

