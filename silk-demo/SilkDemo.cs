using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace Silk
{
    /// <summary>
    /// Demo showing layouting in an silky application scenario.
    /// </summary>
    class Demo : NUIApplication
    {
        private const string imagesLocation = "./res/images/";

        // Container for categories. Transitions between vertical and horizontal
        private View categoryStack;

        // Category titles that transition between vertical and horizontal
        private View titleStack;

        // Area of the product, corresponds to the category
        private View contentArea;

        // List of Views, one for each category.
        private List<View> categoryList;

        // List of the catergory titles
        private List<string> titleList = new List<string>{ "category A", "category B", "category C", "category D" };

        // List of a list of Views to ne used for the content
        private List<List<View>> contentStore;

        private int currentView;

        private bool listView;

        protected override void OnCreate()
        {
            base.OnCreate();
            InitializeMainScreen();
            currentView = 0;
            listView = true;
        }

        private void SetupCategoryItems()
        {
            categoryList = new List<View>();

            View itemA = new View()
            {
                Name = "ItemA",
                Background = CreateGradientVisual(new Vector4(127.0f/255.0f, 95.0f/255.0f, 215.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = 480,
                //Margin = new Extents(100,0,0,0),
            };

            View itemB = new View()
            {
                Name = "ItemB",
                Background = CreateGradientVisual(new Vector4(192.0f/255.0f, 68.0f/255.0f, 127.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = 480
            };
            View itemC = new View()
            {
                Name = "ItemC",
                Background = CreateGradientVisual(new Vector4(239.0f/255.0f, 188.0f/255.0f, 66.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = 480
            };

            View itemD = new View()
            {
                Name = "ItemD",
                Background = CreateGradientVisual(new Vector4(1.0f, 0.0f, 0.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = 480
            };

            categoryList.Add(itemA);
            categoryList.Add(itemB);
            categoryList.Add(itemC);
            categoryList.Add(itemD);
        }

        private void InitializeCategoryStack()
        {

            TransitionComponents shrinkAndGrow = new TransitionComponents();
            shrinkAndGrow.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn);
            shrinkAndGrow.Delay = 0;
            shrinkAndGrow.Duration = 480;

            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            categoryStack = new View()
            {
                Name = "CategoryStack",
                Layout = layout,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.Cyan,
            };

            categoryStack.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                                   AnimatableProperties.Position,
                                                                   0.0,
                                                                   shrinkAndGrow );

            categoryStack.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                                   AnimatableProperties.Size,
                                                                   0.0f,
                                                                   shrinkAndGrow );
            foreach( View category in categoryList)
            {
                categoryStack.Add(category);
                category.Weight = 1.0f;
            }
        }

        private void InitializeTitleStack()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            layout.CellPadding = new Size2D(60,0);

            titleStack = new View()
            {
                Layout = layout,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Name = "TitleStack",
            };

            TransitionComponents easeIn = new TransitionComponents();
            easeIn.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn);
            easeIn.Delay = 0;
            easeIn.Duration = 480;

            titleStack.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                                AnimatableProperties.Position,
                                                                0.0,
                                                                easeIn );

            foreach( string name in titleList)
            {
                TextLabel label = new TextLabel(name)
                {
                    WidthSpecification = 240,
                    HeightSpecification = 800/4,
                    PointSize = 24,
                    TextColor = Color.White,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    //Margin = new Extents (60,0,0,0),
                };
                titleStack.Add(label);
            }
        }

        private void InitializeContent()
        {
            var layout = new LinearLayout();
            contentArea = new View()
            {
                Layout = layout,
                Name = "ContentArea",
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            foreach( List<View> subList in contentStore )
            {
                LinearLayout contentItemLayout = new LinearLayout();
                contentItemLayout.LinearOrientation = LinearLayout.Orientation.Vertical;;
                View contentCategory = new View()
                {
                    Layout = contentItemLayout,
                    WidthSpecification = 480,
                    HeightSpecification = LayoutParamPolicies.WrapContent,
                };

                foreach (View item in subList)
                {
                    contentCategory.Add(item);
                }

                contentArea.Add(contentCategory);
            }

        }

        private void InitializeMainScreen()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            View mainScreen = new View()
            {
                Layout = layout,
                Name = "MainScreen",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Background = CreateGradientVisual(new Vector4(0.0f, 0.0f, 1.0f, 1.0f)).OutputVisualMap,
                //Padding = new Extents(100,100,0,0)
            };

            Window.Instance.Add(mainScreen);
            Window.Instance.KeyEvent += WindowKeyEvent;

            SetupCategoryItems();
            InitializeCategoryStack();
            InitializeTitleStack();
            InitializeContent();

            Window.Instance.Add(categoryStack);
            Window.Instance.Add(titleStack);
            Window.Instance.Add(contentArea);
        }

        private void WindowKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                Animation scrollAnimation = new Animation();
                switch( e.Key.KeyPressedName )
                {
                    case "Left" :
                    {
                        if (!listView && currentView <3)
                        {
                            scrollAnimation.AnimateTo(categoryStack, "PositionX", categoryStack.PositionX - 480,
                            0,
                            256,
                            new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn) );

                            scrollAnimation.AnimateTo(titleStack, "PositionX", titleStack.PositionX - 300,
                            0,
                            200,
                            new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn) );

                            //scrollAnimation.Play();
                            currentView++;
                        }
                    }
                    break;
                    case "Right" :
                    {
                        if (!listView && currentView > 0)
                        {
                            scrollAnimation.AnimateTo(categoryStack, "PositionX", categoryStack.PositionX + 480,
                            0,
                            256,
                            new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn) );

                            scrollAnimation.AnimateTo(titleStack, "PositionX", titleStack.PositionX + 300,
                            0,
                            200,
                            new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn) );

                            //scrollAnimation.Play();
                            currentView--;
                        }
                    }
                    break;
                    case "Up" :
                    {
                        // Get existing Linear layout and change orientation
                        LinearLayout layout = categoryStack.Layout as LinearLayout;
                        layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                        categoryStack.HeightSpecification = 300;
                        categoryStack.WidthSpecification = 480*4;

                        LinearLayout titlelayout = titleStack.Layout as LinearLayout;
                        titlelayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                        titleStack.HeightSpecification = 300;
                        titleStack.WidthSpecification = LayoutParamPolicies.WrapContent;

                        scrollAnimation.AnimateTo(titleStack, "PositionX", 120.0f,
                        0,
                        256,
                        new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn) );

                        //titleStack.Margin = new Extents(0,0,0,0);
                        listView = false;
                    }
                    break;
                    case "Down" :
                    {
                        // Get existing Linear layout and change orientation
                        LinearLayout layout = categoryStack.Layout as LinearLayout;
                        layout.LinearOrientation = LinearLayout.Orientation.Vertical;
                        categoryStack.HeightSpecification = LayoutParamPolicies.MatchParent;
                        categoryStack.WidthSpecification = LayoutParamPolicies.MatchParent;

                        LinearLayout titleLayout = titleStack.Layout as LinearLayout;
                        titleLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
                        titleStack.HeightSpecification = LayoutParamPolicies.MatchParent;
                        titleStack.WidthSpecification = LayoutParamPolicies.MatchParent;

                        scrollAnimation = new Animation();
                        scrollAnimation.AnimateTo(categoryStack, "PositionX", 0.0f,
                        0,
                        300,
                        new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear) );

                        scrollAnimation.AnimateTo(titleStack, "PositionX", 0.0f,
                        0,
                        300,
                        new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear) );

                        //titleStack.Margin = new Extents(60,0,0,0);

                        currentView = 0;
                        listView = true;
                    }
                    break;
                }
                scrollAnimation.Play();
            }
        }

        static private GradientVisual CreateGradientVisual(Vector4 targetcolor)
        {
            GradientVisual gradientVisualMap = new GradientVisual();
            PropertyArray stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(new Vector4(1.0f, 1.0f, 1.0f, 1.0f)));
            stopColor.Add(new PropertyValue(targetcolor));
            gradientVisualMap.StopColor = stopColor;
            gradientVisualMap.StartPosition = new Vector2(0.0f, 1.0f);
            gradientVisualMap.EndPosition = new Vector2(-1.0f, -0.5f);
            gradientVisualMap.PositionPolicy = VisualTransformPolicyType.Relative;
            gradientVisualMap.SizePolicy = VisualTransformPolicyType.Relative;
            return gradientVisualMap;
        }

        static void Main(string[] args)
        {
            // Do not remove this print out - helps with the TizenFX stub sync issue
            Console.WriteLine("Running Example...");
            Demo silkDemo = new Demo();
            silkDemo.Run(args);
        }
    } // Class Demo
} // namespace Silk
