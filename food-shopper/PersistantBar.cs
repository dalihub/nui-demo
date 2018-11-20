using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace FoodShopper
{
    class PersistantBar : View
    {
        View settingsCluster;
        GridLayout settingsClusterLayout;

        public PersistantBar()
        {
            Initialize();
        }

        public void AddClusterButtons(View button)
        {
          settingsCluster.Add(button);
        }

        public void Focused()
        {
            LinearLayout settingsHorizontalLayout = new LinearLayout();
            settingsHorizontalLayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            settingsHorizontalLayout.LayoutAnimate = true;
            settingsCluster.Layout = settingsHorizontalLayout;
            settingsCluster.LayoutWidthSpecification = ChildLayoutData.WrapContent;
        }

        public void UnFocused()
        {
            settingsCluster.Layout = settingsClusterLayout;
            settingsCluster.LayoutWidthSpecificationFixed = 200;
            settingsCluster.LayoutHeightSpecification = ChildLayoutData.MatchParent;
        }

        private void Initialize()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            BackgroundColor = Color.Cyan;
            LayoutWidthSpecification = ChildLayoutData.MatchParent;
            LayoutHeightSpecificationFixed = 100;
            CreateCluster();
        }

        private void CreateCluster()
        {
            settingsCluster = new View();
            settingsClusterLayout = new GridLayout();
            settingsClusterLayout.SetColumns( 2 );
            settingsClusterLayout.LayoutAnimate = true;
            UnFocused();
            Add(settingsCluster);
        }


    }
} // namespace FoodShopper