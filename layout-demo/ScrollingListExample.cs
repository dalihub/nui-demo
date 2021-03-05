using System;
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace LayoutDemo
{
    class ScrollingListExample : Example
    {
        public ScrollingListExample() : base( "ScrollingListExample" )
        {}

        static class TestImages
        {
            public const string listIconsDir = "/images/applicationIcons";

            /// Child image filenames
            public static readonly string[] iconImage = new string[]
            {
                "./res" + "/images/dropdown_bg.png",
                "./res" + "/images/toggle-selected.jpg",
                "./res" + "/images/toggle-normal.jpg",
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

        private Tizen.NUI.Components.ScrollableBase scrollable = null;

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

            CreateList();
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

        private void OnScrollEnded(object sender, Tizen.NUI.Components.ScrollEventArgs e)
        {
            Console.WriteLine("OnScrollEnded");
        }

        private void OnScrollStarted(object sender, Tizen.NUI.Components.ScrollEventArgs e)
        {
            Console.WriteLine("OnScrollStart");
        }

        private void CreateList()
        {

            LinearLayout linear = new LinearLayout();
            linear.LinearOrientation = LinearLayout.Orientation.Vertical;

            listViewContainer = new View()
            {
                Layout = linear,
                Name = "ListContainer",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Focusable = true,
                Padding = new Extents(40,40,0,0),
            };

            scrollable = new Tizen.NUI.Components.ScrollableBase()
            {
                Name = "LayoutScroller",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            scrollable.Add(listViewContainer);
            scrollable.ScrollAnimationEnded += OnScrollEnded;
            scrollable.ScrollDragStarted += OnScrollStarted;

            root.Add(scrollable);
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
                Name = "scrollingItem-entry-" + text,
            };

            TextLabel textLabel = new TextLabel()
            {
                Text = text,
                Name = "scrollingItem-entry-text-label-" + text,
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
                            Normal = "res/" + TestImages.iconImage[2],
                            Selected = "res/" + TestImages.iconImage[1]
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
            GetFiles("res/"+TestImages.listIconsDir, ref applicationIconsArray);
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
    };
}
