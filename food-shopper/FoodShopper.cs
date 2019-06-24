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
        ContentGroup breadContentGroup;
        ContentGroup cakeContentGroup;

        ContentGroup meatContentGroup;
        ContentGroup vegetablesContentGroup;
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
            OptionsBar
        };

        private FocusSection currentlyFocusedSection;
        private FocusSection nextFocusSection;

        private int currentCategoryIndex;

        protected override void OnCreate()
        {
            base.OnCreate();
            InitializeMainScreen();
        }

        private void InitializeMainScreen()
        {
            View mainScreen = new View();
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            mainScreen.Layout = layout;
            mainScreen.WidthSpecification = LayoutParamPolicies.MatchParent;
            mainScreen.HeightSpecification = LayoutParamPolicies.MatchParent;
            mainScreen.Background = CreateGradientVisual().OutputVisualMap;

            TextLabel title = new TextLabel("Food Shopper");
            title.PointSize = 58;
            title.Margin = new Extents(10,10,5,5);
            mainScreen.Add(title);

            // Create Centre Content Pane, scrolling content relevant to categories.
            ContentPane contentPane = new ContentPane();
            contentPane.Name = "centreContentPane";
            mainScreen.Add(contentPane);
            scrollingContent = new Scrolling.ScrollingView();
            // Create content before adding to ScrollingView
            currentCategoryIndex = 2;
            CreateContent0();
            CreateContent1();
            CreateContent2();
            CreateContent3();
            CreateContent4();

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

            Window.Instance.Add(mainScreen);

            keyboardFocusManager = FocusManager.Instance;
            keyboardFocusManager.PreFocusChange += OnKeyboardPreFocusChangeSignal;
            keyboardFocusManager.FocusChanged += CategoryChanged;

            keyboardFocusManager.SetAsFocusGroup(scrollingCategories, true);
            keyboardFocusManager.SetAsFocusGroup(scrollingContent, true);

            FocusManager.Instance.SetCurrentFocusView(scrollingCategories);

            Window.Instance.KeyEvent += WindowKeyEvent;

            currentlyFocusedSection = FocusSection.TitleBar;
            nextFocusSection = FocusSection.CatagoryPane;
            SetFocusToSection(nextFocusSection);
        }

        private void CreateContent0()
        {
            // Create fruit content item, this could be done on demand to save memory.

            fruitContentGroup = new ContentGroup();
            uint contentId = 1;
            fruitContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/fruit/blueberries.jpg" ) );
            fruitContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/fruit/mangos.jpg" ) );
            fruitContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/fruit/oranges.jpg" ) );
        }

        private void CreateContent1()
        {
            // Create bread content item, this could be done on demand to save memory.

            breadContentGroup = new ContentGroup();
            uint contentId = 1;
            breadContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/bread/baguette.jpeg" ) );
            breadContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/bread/sesameSeededBun.jpeg" ) );
            breadContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/bread/slicedLoaf.jpeg" ) );
            breadContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/bread/spanishBread.jpeg" ) );
        }

        private void CreateContent2()
        {
            // Create cake content item, this could be done on demand to save memory.

            cakeContentGroup = new ContentGroup();
            uint contentId = 1;
            cakeContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/cake/cookies.jpeg" ) );
            cakeContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/cake/muffin.jpeg" ) );
            cakeContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/cake/swissRoll.jpeg" ) );
        }

        private void CreateContent3()
        {
            // Create cake content item, this could be done on demand to save memory.

            meatContentGroup = new ContentGroup();
            uint contentId = 1;
            meatContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/meat/steak.jpeg" ) );
        }
        private void CreateContent4()
        {
            // Create cake content item, this could be done on demand to save memory.

            vegetablesContentGroup = new ContentGroup();
            uint contentId = 1;
            vegetablesContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/vegetables/garlic.jpeg" ) );
            vegetablesContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/vegetables/onions.jpeg" ) );
            vegetablesContentGroup.AddContentItem( new ContentGroup.ContentItemInfo( contentId++, imagesLocation + "/vegetables/peppers.jpeg" ) );
        }
        private void CreateClusterButtons()
        {
            ImageView settingsButton = new ImageView();
            settingsButton.SetImage( imagesLocation + "/clusterOptions/exit-icon.png" );
            settingsButton.WidthSpecification = 100;
            settingsButton.HeightSpecification = 100;

            ImageView backgroundButton = new ImageView();
            backgroundButton.SetImage( imagesLocation + "/clusterOptions/search-icon.png" );
            backgroundButton.WidthSpecification = 100;
            backgroundButton.HeightSpecification = 100;

            ImageView infoButton = new ImageView();
            infoButton.SetImage( imagesLocation + "/clusterOptions/settings-icon.png" );
            infoButton.WidthSpecification = 100;
            infoButton.HeightSpecification = 100;

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
                        HorizontalOperation( false );

                    break;
                    case "Right" :
                        HorizontalOperation( true );
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
                case FocusSection.OptionsBar :
                {
                    bottomBar.UnFocused();
                    break;
                }
                case FocusSection.CatagoryPane :
                {
                    Animation scaleAnimation = new Animation();
                    scaleAnimation.SetDuration(.2f);
                    scaleAnimation.AnimateTo(categoryPane, "Scale", new Size(1.0f, 1.0f, 1.0f) );
                    scaleAnimation.Play();
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
                case FocusSection.OptionsBar :
                {
                    bottomBar.Focused();
                    break;
                }
                case FocusSection.CatagoryPane :
                {
                    Animation scaleAnimation = new Animation();
                    scaleAnimation.SetDuration(.2f);
                    scaleAnimation.AnimateTo(categoryPane, "Scale", new Size(1.1f, 1.1f, 1.0f));
                    scaleAnimation.Play();
                  break;
                }
                default :
                {
                    break;
                }
              }
            }
        }

        private void HorizontalOperation( bool reverse )
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
                case FocusSection.OptionsBar :
                {
                    SelectOption( reverse );
                    break;
                }
            }
        }

        private void ScrollCatagories( bool reverse )
        {
            int newCatagory = scrollingCategories.Scroll( reverse );
            if ( newCatagory != currentCategoryIndex )
            {
                currentCategoryIndex = newCatagory;
                Console.WriteLine("Scrolling to category[{0}]", currentCategoryIndex);

                View category = categoryPane.GetCategory(currentCategoryIndex);

                switch( category.Name )
                {
                  case "Category_fruit.jpg" :
                  {
                      scrollingContent.Add(fruitContentGroup);
                      break;
                  }
                  case "Category_bread.jpg" :
                  {
                      scrollingContent.Add(breadContentGroup);
                      break;
                  }
                  case "Category_cake.jpg" :
                  {
                      scrollingContent.Add(cakeContentGroup);
                      break;
                  }
                  case "Category_meat.jpg" :
                  {
                      scrollingContent.Add(meatContentGroup);
                      break;
                  }
                  case "Category_vegetables.jpg" :
                  {
                      scrollingContent.Add(vegetablesContentGroup);
                      break;
                  }
                  default :
                  {
                      break;
                  }
                }
            }
        }

        private void ScrollContent( bool reverse )
        {
            scrollingContent.Scroll( reverse );
        }

        private void SelectOption( bool reverse )
        {
          // Cycle through options, focuses each one.

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

        static private GradientVisual CreateGradientVisual()
        {
            GradientVisual gradientVisualMap = new GradientVisual();
            PropertyArray stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(new Vector4(1.0f, 1.0f, 1.0f, 1.0f)));
            stopColor.Add(new PropertyValue(new Vector4(0.0f, 1.0f, 1.0f, 1.0f)));
            gradientVisualMap.StopColor = stopColor;
            gradientVisualMap.StartPosition = new Vector2(-1.0f, 0.0f);
            gradientVisualMap.EndPosition = new Vector2(1.0f, -0.5f);
            gradientVisualMap.PositionPolicy = VisualTransformPolicyType.Relative;
            gradientVisualMap.SizePolicy = VisualTransformPolicyType.Relative;
            return gradientVisualMap;
        }

        static void Main(string[] args)
        {
            // Do not remove this print out - helps with the TizenFX stub sync issue
            Console.WriteLine("Running Example...");
            FoodShopperDemo foodShopperDemo = new FoodShopperDemo();
            foodShopperDemo.Run(args);
        }
    } // Class FoodShopperDemo
} // namespace FoodShopper
