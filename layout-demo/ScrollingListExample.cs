using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class ScrollingListExample : Example
    {
        public ScrollingListExample() : base( "ScrollingListExample" )
        {}

        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] iconImage = new string[]
            {
                resources + "/images/dropdown_bg.png",
                resources + "/images/toggle-selected.jpg",
                resources + "/images/toggle-normal.jpg",
            };
        }

        private View root=null;
        private View listViewContainer=null;

        private LayoutScroller layoutScroller = null;

        private int numberOfItems = 8;

        private float SizeRatio = .8f;

        bool legacyButton = false; // Use a legacy button instead of NUI.Component Button

        public override void Create()
        {
            Window.Instance.BackgroundColor = Color.White;
            float rootWidth = Window.Instance.WindowSize.Width*SizeRatio;
            float rootHeight =  Window.Instance.WindowSize.Height*SizeRatio;
            root = new View()
            {
                Size = new Size(rootWidth,rootHeight,0.0f),
                Position = new Position((Window.Instance.WindowSize.Width - rootWidth),(Window.Instance.WindowSize.Height - rootHeight),0),
            };
            Window.Instance.Add(root);

            CreateList();
            PopulateList();

            Window.Instance.KeyEvent += OnKeyEvent;
        }


        private void OnKeyEvent( object sender, Window.KeyEventArgs eventArgs )
        {
            if( eventArgs.Key.State == Key.StateType.Down )
            {
                switch( eventArgs.Key.KeyPressedName )
                {
                    case "g":
                    {
                        Console.WriteLine("GarbageCollection started");
                        GarbageCollection();
                    }
                    break;
                }
            }
        }

        public void Deactivate()
        {
            if (root != null)
            {
                root.Dispose();
            }
        }


        public void GarbageCollection()
        {
            GC.Collect();
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(root);
        }
        private void CreateList()
        {
            listViewContainer = new View();

            LinearLayout linear = new LinearLayout();
            linear.LinearOrientation = LinearLayout.Orientation.Vertical;

            TransitionComponents slowPositioning = new TransitionComponents();
            slowPositioning.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            slowPositioning.Delay = 0;
            slowPositioning.Duration = 164;

            listViewContainer.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                                      AnimatableProperties.Position,
                                                                      0.0,
                                                                      slowPositioning );

            listViewContainer.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                                      AnimatableProperties.Size,
                                                                      0.0,
                                                                      slowPositioning );

            listViewContainer.Layout = linear;
            listViewContainer.Name = "DropDownMenuList";
            listViewContainer.WidthSpecification = LayoutParamPolicies.MatchParent;
            listViewContainer.HeightSpecification = LayoutParamPolicies.WrapContent;
            listViewContainer.Focusable = true;

            layoutScroller = new LayoutScroller()
            {
                Name = "LayoutScroller",
            };
            layoutScroller.AddLayoutToScroll(listViewContainer);

            layoutScroller.Size2D = new Size2D(360, 500);

            root.Add(layoutScroller);
        }

        private View CreateListItem(bool title, string text, bool toggle)
        {
            FlexLayout flexLayout = new FlexLayout()
            {
                Direction = FlexLayout.FlexDirection.Row,
                Justification = FlexLayout.FlexJustification.SpaceBetween,
            };

            View listItem = new View()
            {
                WidthSpecification = 360,
                HeightSpecification = 80,
                BackgroundColor = Color.White,
                Layout = flexLayout,
                Padding = new Extents(5,5,5,5),
                Name = "flexibleLayout-entry-" + text,
            };

            TextLabel textLabel = new TextLabel();
            textLabel.Text = text;
            textLabel.Name = "flexibleLayout-text-label-" + text;

            if (title)
            {
                textLabel.PointSize = 12;
                listItem.BackgroundColor = new Color(128.0f/256, 128.0f/256, 128.0f/256, 1.0f);
                listItem.HeightSpecification = 22;
            }

            listItem.Add(textLabel);

            if (toggle)
            {
                Tizen.NUI.Components.ButtonAttributes buttonAttributes = new Tizen.NUI.Components.ButtonAttributes()
                {
                    BackgroundImageAttributes = new Tizen.NUI.Components.ImageAttributes
                    {
                        ResourceURL = new Tizen.NUI.Components.StringSelector
                        {
                          Normal = TestImages.iconImage[2],
                          Selected = TestImages.iconImage[1]
                        },
                    },
                    IsSelectable = true,
                };

                Tizen.NUI.Components.Button toggleButton = new Tizen.NUI.Components.Button(buttonAttributes)
                {
                    Name = "toggleButton",
                    Size2D = new Size2D(90,30),
                };

                var imageVisualUnselected = new ImageVisual();
                imageVisualUnselected.URL = TestImages.iconImage[2];
                var imageVisualSelected = new ImageVisual();
                imageVisualSelected.URL = TestImages.iconImage[1];

                PropertyArray array = new PropertyArray();
                array.Add(new PropertyValue(TestImages.iconImage[2]));
                array.Add(new PropertyValue(TestImages.iconImage[1]));

                if(legacyButton)
                {
                    PushButton toggleButtonLegacy = new PushButton()
                    {
                        UnselectedBackgroundVisual = imageVisualUnselected.OutputVisualMap,
                        SelectedBackgroundVisual = imageVisualSelected.OutputVisualMap,
                        Togglable = true,
                        Name = "toggleButton",
                        Size2D = new Size2D(90,30),
                    };
                    Console.WriteLine("ScrollingList Example - Using Legacy Button");
                    listItem.Add(toggleButtonLegacy);
                }
                else
                {
                    listItem.Add(toggleButton);
                }
            }


            return listItem;
        }

        private void PopulateList()
        {
            listViewContainer.Add( CreateListItem(true,"Screen", false) );
            listViewContainer.Add( CreateListItem(false,"Brightness", false) );
            listViewContainer.Add( CreateListItem(false,"Motion Detection", true) );
            listViewContainer.Add( CreateListItem(false,"Clean Screen", false) );

            listViewContainer.Add( CreateListItem(true,"HomeScreen", false) );
            listViewContainer.Add( CreateListItem(false,"HomeScreen Wallpaper", false) );

            listViewContainer.Add( CreateListItem(true,"Screensaver", false) );
            listViewContainer.Add( CreateListItem(false,"Screensaver", true) );
            listViewContainer.Add( CreateListItem(false,"Screensaver themes", false) );
            listViewContainer.Add( CreateListItem(false,"Show latest info", true) );
            listViewContainer.Add( CreateListItem(false,"Screensaver After", false) );
            listViewContainer.Add( CreateListItem(false,"Screensaver Duration", false) );
        }

    };
}
