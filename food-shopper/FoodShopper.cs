using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace FoodShopper
{
    class FoodShopperDemo : NUIApplication
    {
        const int contentPaneHeight = 200;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private void Initialize()
        {
            TextLabel title = new TextLabel("Hello World");
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;
            window.Add(title);
            CategoryPane categoryPane = new CategoryPane();
            categoryPane.Name = "categoryPane";
            categoryPane.LayoutWidthSpecification = ChildLayoutData.MatchParent;
            categoryPane.LayoutHeightSpecificationFixed = contentPaneHeight;

            window.Add(categoryPane);
        }

        static void Main(string[] args)
        {
            FoodShopperDemo foodShopperDemo = new FoodShopperDemo();
            foodShopperDemo.Run(args);
        }
    }
}
