using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace MyPages
{
    class Program : NUIApplication
    {
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

        private Scrollable scrollable = null;

        private int numberOfPages = 4;

        protected override void OnCreate()
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

            Window.Instance.KeyEvent += OnKeyEvent;
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
            targetArray = System.IO.Directory.GetFiles(sourceDirectory);
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

            scrollable = new Scrollable()
            {
                Name = "Scrollable",
                // FlickAnimationSpeed = 0.8f,
                // FlickDistanceMultiplierRange = new Vector2(0.2f, 0.5f),
                ScrollingDirection = Tizen.NUI.Components.Scrollable.Direction.Horizontal,
                SnapToPage = true,
            };
            scrollable.Add(listViewContainer);

            scrollable.WidthSpecification = LayoutParamPolicies.MatchParent;
            scrollable.HeightSpecification = LayoutParamPolicies.MatchParent;

            root.Add(scrollable);
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
            GetFiles(this.DirectoryInfo.Resource + TestImages.listIconsDir, ref applicationIconsArray);
            int numberOfIcons = applicationIconsArray.Length;
            var rand = new System.Random();

            for (int index=0; index < numberOfPages; index++)
            {
                listViewContainer.Add(CreatePage(rand.Next(0, numberOfIcons), applicationNames[index]));
            };
            scrollable.NumberOfPages = numberOfPages;

        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
