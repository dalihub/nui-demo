using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace FoodShopper
{
    /// <summary>
    /// Food shopping demo application
    /// Uses Layouts to split the screen vertical into quadarants.
    /// Top is a slim title/info bar
    /// Next down is the large main content which shows a specific food item
    /// that can be selected for purchase.
    /// Below that content pane is a smaller scrolling horizontal layout with the different food categories,
    /// Changing the category will change the large main content so matches the category.
    /// The bottom bar has settings and the checkout button. When this bar is focused the settings
    /// button grows to make selecting one of the 3 options easier.
    /// </summary>
    class FoodShopperDemo : NUIApplication
    {
        private const string imagesLocation = "./res/images/";

        ContentGroup fruitContentGroup;
        CategoryPane categoryPane;

        Scrolling.ScrollingView scrollingCategories;
        Scrolling.ScrollingView scrollingContent;

        private FocusManager keyboardFocusManager;

        private enum FocusSection
        {
            TitleBar,
            ContentPane,
            CatagoryPane,
            SettingsBar
        };

        private FocusSection focusedSection;
        private FocusSection nextFocusSection;

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

            scrollingContent = new Scrolling.ScrollingView();
            mainScreen.Add(scrollingContent);

            // Create Centre Content Pane, scrolling content relevant to catergories.
            ContentPane contentPane = new ContentPane();
            contentPane.Name = "centreContentPane";
            scrollingContent.Add(contentPane);

            scrollingCategories = new Scrolling.ScrollingView();
            mainScreen.Add(scrollingCategories);

            // Create Category Pane, scrolling categories at lower part of screen.
            categoryPane = new CategoryPane();
            categoryPane.Name = "categoryPane";
            scrollingCategories.Add(categoryPane);

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

            keyboardFocusManager.SetAsFocusGroup(scrollingCategories, true);
            //FocusManager.Instance.SetCurrentFocusView( categoryPane.GetCategory() );
            FocusManager.Instance.SetCurrentFocusView( scrollingCategories);

            Window.Instance.KeyEvent += WindowKeyEvent;

            focusedSection = FocusSection.CatagoryPane;;
            nextFocusSection = FocusSection.CatagoryPane;
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


        private void WindowKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                switch( e.Key.KeyPressedName )
                {
                    case "Left":
                        Console.WriteLine("Scrolling Left" );
                        ScrollPane( false );

                    break;
                    case "Right" :
                        Console.WriteLine("Scrolling Right" );
                        ScrollPane( true );
                    break;
                    case "Up" :
                        Console.WriteLine("Move Up" );
                        FocusSectionUp();
                    break;
                    case "Down" :
                        Console.WriteLine("Move Down" );
                        FocusSectionDown();
                    break;
                }

            }
        }

        /// Move focus to the section above current focus. Does not wrap.
        /// Uses FocusSection, upmost section is 0
        private void FocusSectionUp()
        {
            nextFocusSection = (FocusSection)Math.Max( (int)--focusedSection, 0 );
        }
        /// Move focus to the section below current focus. Does not wrap;
        private void FocusSectionDown()
        {
            nextFocusSection = (FocusSection)Math.Max( (int)++focusedSection, 0 );
        }

        private void ScrollPane( bool reverse )
        {
            // Scroll the focused secton if it allows
            switch( focusedSection )
            {
                case FocusSection.CatagoryPane :
                {
                    ScrollCatagories( reverse );
                    break;
                }
                case FocusSection.ContentPane :
                {
                    break;
                }

            }
        }

        private void ScrollCatagories( bool reverse )
        {
            scrollingCategories.Scroll( reverse );
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
