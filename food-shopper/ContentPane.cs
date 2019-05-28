using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace FoodShopper
{
    class ContentPane : View
    {
        public ContentPane()
        {
            Initialize();
        }

        private void Initialize()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            BackgroundColor = Color.Blue;
            WidthSpecification = LayoutParamPolicies.MatchParent;
            LayoutHeightSpecificationFixed = 600;
        }

        public void ShowContentGroup( ContentGroup contentGroup )
        {
            Add(contentGroup);
        }

    }; // class ContentPane

} // namespace FoodShopper

