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
            InitializeMainScreen();
        }

        private void InitializeMainScreen()
        {
            TextLabel title = new TextLabel("Food Shopper");
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;
            window.Add(title);

            View mainScreen = new View();
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            mainScreen.Layout = layout;
            mainScreen.LayoutWidthSpecification = ChildLayoutData.MatchParent;
            mainScreen.LayoutHeightSpecification = ChildLayoutData.MatchParent;;

            // Create Centre Content Pane, scrolling content relevant to catergories.
            ContentPane contentPane = new ContentPane();
            contentPane.Name = "centreContentPane";
            mainScreen.Add(contentPane);

            // Create Category Pane, scrolling categories at lower part of screen.
            CategoryPane categoryPane = new CategoryPane();
            categoryPane.Name = "categoryPane";
            mainScreen.Add(categoryPane);


            // Create Persistant bottom bar, settings and checkout
            PersistantBar bottomBar = new PersistantBar();
            bottomBar.Name = "bottomBar";
            mainScreen.Add(bottomBar);

            window.Add(mainScreen);
        }

        static void Main(string[] args)
        {
            FoodShopperDemo foodShopperDemo = new FoodShopperDemo();
            foodShopperDemo.Run(args);
        }
    } // Class FoodShopperDemo
} // namespace FoodShopper
