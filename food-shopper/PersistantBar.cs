using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace FoodShopper
{
    class PersistantBar : View
    {
        View settingsCluster;
        GridLayout settingsClusterLayout;

        const int BAR_HEIGHT = 100;

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
            settingsCluster.Layout = settingsHorizontalLayout;
            settingsCluster.WidthSpecification = LayoutParamPolicies.MatchParent;

        }

        public void UnFocused()
        {
            settingsCluster.Layout = settingsClusterLayout;
            // Not possible to Match to parents height to make cluster square hence use fixed value;
            settingsCluster.WidthSpecification = BAR_HEIGHT;
            settingsCluster.HeightSpecification = LayoutParamPolicies.MatchParent;
        }

        private void Initialize()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            BackgroundColor = Color.Cyan;
            WidthSpecification = LayoutParamPolicies.MatchParent;
            HeightSpecification = BAR_HEIGHT;
            Name = "persistantBar";
            CreateCluster();
        }

        private void CreateCluster()
        {
            settingsCluster = new View();
            settingsCluster.Name = "settingsCluster";
            settingsClusterLayout = new GridLayout();
            settingsClusterLayout.Columns = 2;
            //settingsClusterLayout.LayoutAnimate = true;
            UnFocused();
            Add(settingsCluster);
        }


    }
} // namespace FoodShopper
