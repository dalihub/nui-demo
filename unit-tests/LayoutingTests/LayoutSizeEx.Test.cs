﻿using NUnit.Framework;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutingTests
{
  [TestFixture]
  public class LayoutSizeExTest
  {
    [Test]
    public void LayoutSizeEx_ConstructAndCompare()
    {
        var result = false;
        const int TEST_VALUE1 = 16;
        const int TEST_VALUE2 = 12;
        LayoutSizeEx layout = new LayoutSizeEx(TEST_VALUE1, TEST_VALUE2);
        if((layout.Width == TEST_VALUE1) & (layout.Height == TEST_VALUE2))
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutSize not returning values set");
    }

    [Test]
    public void LayoutSizeEx_IsEqualTo()
    {
        var result = false;
        const int TEST_VALUE1 = 16;
        const int TEST_VALUE2 = 12;
        LayoutSizeEx layout = new LayoutSizeEx(TEST_VALUE1, TEST_VALUE2);
        LayoutSizeEx layoutCopy = new LayoutSizeEx(TEST_VALUE1, TEST_VALUE2);
        LayoutSizeEx layoutUnique = new LayoutSizeEx(TEST_VALUE1, TEST_VALUE1);

        if((layout.IsEqualTo(layoutCopy)) & !(layout.IsEqualTo( layoutUnique)))
        {
            result = true;
        }

        Assert.IsTrue(result, "LayoutSize equal testing failed");
    }

  }
} // namespace LayoutingTests

