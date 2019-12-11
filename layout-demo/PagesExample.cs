using System;
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace LayoutDemo
{
    class PagesExample : Example
    {
        public PagesExample() : base( "PagesExample" )
        {}

        static class TestImages
        {
            public const string listIconsDir = "/images/applicationIcons";
        }

        private readonly string[] applicationNames = new string[]
        {
            "Dash", "Music", "Calendar", "Clock", "Deals", "Engineer Mode", "FridgeManager","Gallery",
            "FoodHub", "HomeHub", "Internet", "Planner", "Memo", "AM Brief", "Demo", "Charm",
            "Circle", "Care","Settings", "Shopping List", "Recipes", "SmartView", "SmartThings",
            "Streaming", "Timer", "Todo", "Trivia", "Radio"
        };

        private readonly Color[] colors = { Color.White, Color.Green, Color.Magenta, Color.Cyan };

        private string[] applicationIconsArray;

        private View root = null;
        private View listViewContainer = null;

        private int numberOfPages = 4;

        private Tizen.NUI.Components.LayoutScroller layoutScroller = null;

        public override void Create()
        {
            Window.Instance.BackgroundColor = Color.White;
            float rootWidth = Window.Instance.WindowSize.Width;
            float rootHeight = Window.Instance.WindowSize.Height;
            root = new View()
            {
                Size = new Size(rootWidth, rootHeight, 0.0f),
            };
            Window.Instance.Add(root);

            CreatePages();
            PopulateList();
        }

        public void Deactivate()
        {
            if (root != null)
            {
                root.Dispose();
            }
        }

        private void GetFiles(string sourceDirectory, ref string[] targetArray)
        {
            targetArray = Directory.GetFiles(sourceDirectory);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(root);
        }

        private void CreatePages()
        {

            LinearLayout linear = new LinearLayout();

            linear.LinearOrientation = LinearLayout.Orientation.Horizontal;

            listViewContainer = new View()
            {
                Layout = linear,
                Name = "DropDownMenuList",
                WidthSpecification = Window.Instance.WindowSize.Width * numberOfPages,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(40, 40, 0, 0),
            };

            TransitionComponents slowPositioning = new TransitionComponents();
            slowPositioning.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            slowPositioning.Delay = 0;
            slowPositioning.Duration = 164;

            listViewContainer.LayoutTransition = new LayoutTransition(TransitionCondition.LayoutChanged,
                                                                      AnimatableProperties.Position,
                                                                      0.0,
                                                                      slowPositioning);

            listViewContainer.LayoutTransition = new LayoutTransition(TransitionCondition.LayoutChanged,
                                                                      AnimatableProperties.Size,
                                                                      0.0,
                                                                      slowPositioning);

            layoutScroller = new Tizen.NUI.Components.LayoutScroller()
            {
                Name = "LayoutScroller",
                FlickAnimationSpeed = 0.8f,
                FlickDistanceMultiplierRange = new Vector2(0.2f, 0.5f),
                ScrollingAxis = Tizen.NUI.Components.LayoutScroller.Axis.Horizontal,
                SnapToPage = true,
            };
            layoutScroller.Add(listViewContainer);

            layoutScroller.WidthSpecification = LayoutParamPolicies.MatchParent;
            layoutScroller.HeightSpecification = LayoutParamPolicies.MatchParent;

            root.Add(layoutScroller);
        }

        private View CreatePage(int imageIndex, string text)
        {
            var rand = new System.Random();

            LinearLayout linearLayout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
            };

            View listItem = new View()
            {
                WidthSpecification = Window.Instance.WindowSize.Width,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Layout = linearLayout,
                Padding = new Extents(15, 15, 40, 40),
                Name = "flexibleLayout-entry-" + text,
                BackgroundColor = colors[(rand.Next(0, 3))],
            };

            TextLabel textLabel = new TextLabel()
            {
                Text = text,
                Name = "flexibleLayout-text-label-" + text,
                Margin = new Extents(10, 0, 0, 0),
                TextColor = new Color(0.6f, 0.6f, 0.6f, 1),
                PointSize = 38,
                FontFamily = "SamsungOneUI 500C",
            };
            listItem.Add(textLabel);

            if (imageIndex >= 0)
            {
                ImageView icon = new ImageView(applicationIconsArray[imageIndex])
                {
                    WidthSpecification = 200,
                    HeightSpecification = 200,
                };
                listItem.Add(icon);
            }
            return listItem;
        }

        private void PopulateList()
        {
            GetFiles("./res"+TestImages.listIconsDir, ref applicationIconsArray);
            int numberOfIcons = applicationIconsArray.Length;
            var rand = new System.Random();

            for (int index=0; index < numberOfPages; index++)
            {
                listViewContainer.Add(CreatePage(rand.Next(0, numberOfIcons), applicationNames[index]));
            };
            layoutScroller.NumberOfPages = numberOfPages;

        }
    };
}
