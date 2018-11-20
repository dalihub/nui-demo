using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace FoodShopper
{
    class PersistantBar : View
    {
        public PersistantBar()
        {
            Initialise();
        }

        private void Initialise()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            BackgroundColor = Color.Cyan;
            LayoutWidthSpecification = ChildLayoutData.MatchParent;
            LayoutHeightSpecificationFixed = 100;
        }
    }
} // namespace FoodShopper