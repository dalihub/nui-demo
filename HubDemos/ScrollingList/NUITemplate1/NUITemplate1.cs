using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUITemplate1
{
    class Program : NUIApplication
    {
        static class TestImages
        {
            public const string listIconsDir = "/images/applicationIcons";

            /// Child image filenames
            public static readonly string[] iconImage = new string[]
            {
                "/images/dropdown_bg.png",
                "/images/toggle-selected.jpg",
                "/images/toggle-normal.jpg",
            };

        }

        private readonly string[] applicationNames = new string[]
        {
            "Dash", "Music", "Calendar", "Clock", "Deals", "Engineer Mode", "FridgeManager","Gallery",
            "FoodHub", "HomeHub", "Internet", "Planner", "Memo", "AM Brief", "Demo", "Charm",
            "Circle", "Care","Settings", "Shopping List", "Recipes", "SmartView", "SmartThings",
            "Streaming", "Timer", "Todo", "Trivia", "Radio"
        };

        private string[] applicationIconsArray;

        private View root = null;
        private View listViewContainer = null;

        private LayoutScroller layoutScroller = null;

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

            CreateList();
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
            targetArray = Directory.GetFiles(sourceDirectory);
        }

        private void CreateList()
        {

            LinearLayout linear = new LinearLayout();
            linear.LinearOrientation = LinearLayout.Orientation.Vertical;

            listViewContainer = new View()
            {
                Layout = linear,
                Name = "DropDownMenuList",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Focusable = true,
                Padding = new Extents(40,40,0,0),
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

            layoutScroller = new LayoutScroller()
            {
                Name = "LayoutScroller",
                FlickAnimationDurationModifier = 0.8f,
                FlickDistanceMultiplierRange = new Vector2(0.3f,0.6f),
            };
            layoutScroller.AddLayoutToScroll(listViewContainer);

            layoutScroller.WidthSpecification = LayoutParamPolicies.MatchParent;
            layoutScroller.HeightSpecification = LayoutParamPolicies.MatchParent;

            root.Add(layoutScroller);
        }

        private View CreateListItem(int imageIndex, string text, bool toggle)
        {
            LinearLayout linearLayout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Horizontal,
            };

            View listItem = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Layout = linearLayout,
                Padding = new Extents(5, 5, 40, 40),
                Name = "flexibleLayout-entry-" + text,
            };

            TextLabel textLabel = new TextLabel()
            {
                Text = text,
                Name = "flexibleLayout-text-label-" + text,
                Margin = new Extents(90, 0, 0, 0),
                TextColor = new Color(0.6f,0.6f,0.6f,1),
                PointSize = 38,
                FontFamily = "SamsungOneUI 500C",
        };

            if (imageIndex>=0)
            {
                ImageView icon = new ImageView(applicationIconsArray[imageIndex]);
                listItem.Add(icon);
            }

            listItem.Add(textLabel);

            if (toggle)
            {
                Tizen.NUI.Components.ButtonStyle buttonStyle = new Tizen.NUI.Components.ButtonStyle
                {
                    Icon = new ImageViewStyle
                    {
                        ResourceUrl = new Tizen.NUI.Components.StringSelector
                        {
                            Normal = this.DirectoryInfo.Resource + TestImages.iconImage[2],
                            Selected = this.DirectoryInfo.Resource + TestImages.iconImage[1]
                        },
                    },
                    IsSelectable = true,
                };

                Tizen.NUI.Components.Button toggleButton = new Tizen.NUI.Components.Button(buttonStyle)
                {
                    Name = "toggleButton",
                    Size2D = new Size2D(90, 30),
                };

                listItem.Add(toggleButton);
            }


            return listItem;
        }

        private void PopulateList()
        {
            GetFiles(this.DirectoryInfo.Resource+TestImages.listIconsDir, ref applicationIconsArray);
            int numberOfIcons = applicationIconsArray.Length;
            var rand = new System.Random();

            foreach (string name in applicationNames)
            {

                listViewContainer.Add(CreateListItem(rand.Next(0,numberOfIcons), name, false));
                listViewContainer.Add(new View() { WidthSpecification = LayoutParamPolicies.MatchParent,
                                                   HeightSpecification = 2,
                                                   BackgroundColor = new Color(0.8f,0.8f,0.8f,1),
                                                 });
            };

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
