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

        PersistantBar bottomBar;
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

        private FocusSection currentlyFocusedSection;
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

            // Create Centre Content Pane, scrolling content relevant to categories.
            ContentPane contentPane = new ContentPane();
            contentPane.Name = "centreContentPane";
            mainScreen.Add(contentPane);
            scrollingContent = new Scrolling.ScrollingView();
            // Create content before adding to ScrollingView
            CreateContent();
            //contentPane.ShowContentGroup( fruitContentGroup );
            contentPane.Add(scrollingContent);
            scrollingContent.Add(fruitContentGroup);

            // Create Category Pane, scrolling categories at lower part of screen.
            scrollingCategories = new Scrolling.ScrollingView();
            mainScreen.Add(scrollingCategories);

            categoryPane = new CategoryPane();
            categoryPane.Name = "categoryPane";
            scrollingCategories.Add(categoryPane);

            // Create Persistant bottom bar, settings and checkout
            bottomBar = new PersistantBar();
            bottomBar.Name = "bottomBar";
            mainScreen.Add(bottomBar);

            CreateClusterButtons();

            window.Add(mainScreen);

            keyboardFocusManager = FocusManager.Instance;
            keyboardFocusManager.PreFocusChange += OnKeyboardPreFocusChangeSignal;
            keyboardFocusManager.FocusChanged += CategoryChanged;

            keyboardFocusManager.SetAsFocusGroup(scrollingCategories, true);
            keyboardFocusManager.SetAsFocusGroup(scrollingContent, true);
            FocusManager.Instance.SetCurrentFocusView(scrollingCategories);

            Window.Instance.KeyEvent += WindowKeyEvent;

            currentlyFocusedSection = FocusSection.CatagoryPane;;
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

        private void CreateClusterButtons()
        {
            ImageView settingsButton = new ImageView();
            settingsButton.SetImage( imagesLocation + "/fruit/mangos.jpg" );
            settingsButton.LayoutWidthSpecificationFixed = 100;
            settingsButton.LayoutHeightSpecificationFixed = 100;

            ImageView backgroundButton = new ImageView();
            backgroundButton.SetImage( imagesLocation + "/fruit/mangos.jpg" );
            backgroundButton.LayoutWidthSpecificationFixed = 100;
            backgroundButton.LayoutHeightSpecificationFixed = 100;

            ImageView infoButton = new ImageView();
            infoButton.SetImage( imagesLocation + "/fruit/mangos.jpg" );
            infoButton.LayoutWidthSpecificationFixed = 100;
            infoButton.LayoutHeightSpecificationFixed = 100;

            bottomBar.AddClusterButtons(settingsButton);
            bottomBar.AddClusterButtons(backgroundButton);
            bottomBar.AddClusterButtons(infoButton);
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
                        ScrollPane( false );

                    break;
                    case "Right" :
                        ScrollPane( true );
                    break;
                    case "Up" :
                        FocusSectionUp();
                    break;
                    case "Down" :
                        FocusSectionDown();
                    break;
                }

            }
        }

        /// Move focus to the section above current focus. Does not wrap.
        /// Uses FocusSection, upmost section is 0
        private void FocusSectionUp()
        {
            nextFocusSection = (FocusSection)Math.Max( (int)(currentlyFocusedSection-1), 0 );
            SetFocusToSection(nextFocusSection);
        }
        /// Move focus to the section below current focus. Does not wrap;
        private void FocusSectionDown()
        {
            nextFocusSection = (FocusSection)Math.Min( (int)(currentlyFocusedSection+1), Enum.GetNames(typeof(FocusSection)).Length - 1 );
            SetFocusToSection(nextFocusSection);
        }

        private void SetFocusToSection(FocusSection focusSection)
        {
            Console.WriteLine("SetFocusToSection:{0}",focusSection.ToString("G"));

            if( currentlyFocusedSection != focusSection )
            {
              // Focus out actions
              switch(currentlyFocusedSection)
              {
                case FocusSection.SettingsBar :
                {
                    bottomBar.UnFocused();
                    break;
                }
                default :
                {
                  break;
                }
              }

              currentlyFocusedSection = focusSection;

              // focus in actions
              switch(currentlyFocusedSection)
              {
                case FocusSection.SettingsBar :
                {
                    bottomBar.Focused();
                    break;
                }
                default :
                {
                    break;
                }
              }
            }
        }

        private void ScrollPane( bool reverse )
        {
            // Scroll the focused secton if it allows
            switch( currentlyFocusedSection )
            {
                case FocusSection.CatagoryPane :
                {
                    ScrollCatagories( reverse );
                    break;
                }
                case FocusSection.ContentPane :
                {
                    ScrollContent( reverse );
                    break;
                }
            }
        }

        private void ScrollCatagories( bool reverse )
        {
            scrollingCategories.Scroll( reverse );
        }

        private void ScrollContent( bool reverse )
        {
            scrollingContent.Scroll( reverse );
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
