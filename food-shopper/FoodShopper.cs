using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace FoodShopper
{
    class FoodShopperDemo : NUIApplication
    {
        private const string imagesLocation = "./res/images/";
        const int contentPaneHeight = 200;

        ContentGroup fruitContentGroup;
        CategoryPane categoryPane;
        private FocusManager keyboardFocusManager;

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
            categoryPane = new CategoryPane();
            categoryPane.Name = "categoryPane";
            mainScreen.Add(categoryPane);


            // Create Persistant bottom bar, settings and checkout
            PersistantBar bottomBar = new PersistantBar();
            bottomBar.Name = "bottomBar";
            mainScreen.Add(bottomBar);

            window.Add(mainScreen);

            CreateContent();
            contentPane.ShowContentGroup( fruitContentGroup );

            keyboardFocusManager = FocusManager.Instance;
            keyboardFocusManager.PreFocusChange += OnKeyboardPreFocusChangeSignal;
            keyboardFocusManager.FocusChanged += CategoryChanged;

            keyboardFocusManager.SetAsFocusGroup(categoryPane, true);
            FocusManager.Instance.SetCurrentFocusView( categoryPane.GetCategory() );

        }

        private void CreateContent()
        {
            // Create a test content item, this could be done on demand to save memory.

            fruitContentGroup = new ContentGroup();
            uint contentId = 1;
            ContentGroup.ContentItemInfo fruitItem1 = new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/fruit/blueberries.jpg" );
            fruitContentGroup.AddContentItem( fruitItem1 );

            ContentGroup.ContentItemInfo fruitItem2 = new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/fruit/mangos.jpg" );
            fruitContentGroup.AddContentItem( fruitItem2 );

            ContentGroup.ContentItemInfo fruitItem3 = new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/fruit/oranges.jpg" );
            fruitContentGroup.AddContentItem( fruitItem3 );
        }

        private void CategoryChanged(object source, FocusManager.FocusChangedEventArgs e )
        {
            Console.WriteLine("CategoryChanged");
        }

        private View OnKeyboardPreFocusChangeSignal(object source, FocusManager.PreFocusChangeEventArgs e)
        {
            View nextView = null;
            switch( e.Direction )
            {
              case View.FocusDirection.Up:
                  Console.WriteLine("FocusPreChanged Up");
              break;
              case View.FocusDirection.Down:
                  Console.WriteLine("FocusPreChanged Down");
              break;
              case View.FocusDirection.Left:
                  Console.WriteLine("FocusPreChanged Left ");
              break;
              case View.FocusDirection.Right:
                  Console.WriteLine("FocusPreChanged Right");
              break;
              default:
                  Console.WriteLine("FocusPreChanged Unknown");
              break;
            }

            return nextView;
        }


        static void Main(string[] args)
        {
            FoodShopperDemo foodShopperDemo = new FoodShopperDemo();
            foodShopperDemo.Run(args);
        }
    } // Class FoodShopperDemo
} // namespace FoodShopper
