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

        private FocusManager keyboardFocusManager;

        private View categoryStack;

        private List<View> catergoryList;

        protected override void OnCreate()
        {
            base.OnCreate();
            InitializeMainScreen();
        }

        private void SetupCategoryItems()
        {
            catergoryList = new List<View>();

            View itemA = new View()
            {
                Name = "ItemA",
                Background = CreateGradientVisual(new Vector4(127.0f/255.0f, 95.0f/255.0f, 215.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Margin = new Extents(100,0,0,0)
            };

            View itemB = new View()
            {
                Name = "ItemB",
                Background = CreateGradientVisual(new Vector4(192.0f/255.0f, 68.0f/255.0f, 127.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = LayoutParamPolicies.MatchParent
            };
            View itemC = new View()
            {
                Name = "ItemC",
                Background = CreateGradientVisual(new Vector4(239.0f/255.0f, 188.0f/255.0f, 66.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = LayoutParamPolicies.MatchParent
            };

            View itemD = new View()
            {
                Name = "ItemD",
                Background = CreateGradientVisual(new Vector4(1.0f, 0.0f, 0.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = LayoutParamPolicies.MatchParent
            };

            catergoryList.Add(itemA);
            catergoryList.Add(itemB);
            catergoryList.Add(itemC);
            catergoryList.Add(itemD);
        }

        private void InitialzeCategoryStack()
        {
            categoryStack = new View();
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            categoryStack.Layout = layout;
            categoryStack.WidthSpecification = LayoutParamPolicies.MatchParent;
            categoryStack.HeightSpecification = LayoutParamPolicies.MatchParent;

            foreach( View category in catergoryList)
            {
                categoryStack.Add(category);
                category.Weight = 1.0f;
            }
        }

        private void InitializeMainScreen()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            View mainScreen = new View()
            {
                Layout = layout,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Background = CreateGradientVisual(new Vector4(0.0f, 0.0f, 1.0f, 1.0f)).OutputVisualMap,
                //Padding = new Extents(100,100,0,0)
            };

            Window.Instance.Add(mainScreen);
            Window.Instance.KeyEvent += WindowKeyEvent;

            SetupCategoryItems();
            InitialzeCategoryStack();

            Window.Instance.Add(categoryStack);
        }

        private void WindowKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                switch( e.Key.KeyPressedName )
                {
                    case "Left":
                    break;
                    case "Right" :
                    break;
                    case "Up" :
                    {
                        var layout = new LinearLayout();
                        layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                        categoryStack.Layout = layout;
                        categoryStack.HeightSpecification = 400;
                        categoryStack.WidthSpecification = LayoutParamPolicies.WrapContent;
                    }
                    break;
                    case "Down" :
                    {
                        var layout = new LinearLayout();
                        layout.LinearOrientation = LinearLayout.Orientation.Vertical;
                        categoryStack.Layout = layout;
                        categoryStack.HeightSpecification = LayoutParamPolicies.MatchParent;
                        categoryStack.WidthSpecification = LayoutParamPolicies.MatchParent;
                    }
                    break;
                }
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
