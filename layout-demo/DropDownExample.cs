using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace LayoutDemo
{
    class DropDownExample : Example
    {
        public DropDownExample() : base( "DropDownExample" )
        {}

        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] images = new string[]
            {
                resources + "/images/dropdown_bg.png",
                resources + "/images/dropdown_checkbox_on.png",
                resources + "/images/list_ic_dropdown.png",
            };
        }

        private View root;
        private Tizen.NUI.Components.DropDown dropDown = null;

        private ScrollBar scrollBar = null;
        private TextLabel text = null;

        public override void Create()
        {
            Window.Instance.BackgroundColor = Color.White;
            Size tenthOfWindowSize = new Size(Window.Instance.WindowSize.Width*.1f, Window.Instance.WindowSize.Height*.1f);

            LinearLayout linearLayout = new LinearLayout();
            linearLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
            root = new View()
            {
                Name = "example-root",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Padding = new Extents((ushort)tenthOfWindowSize.Width,(ushort)tenthOfWindowSize.Width,(ushort)(tenthOfWindowSize.Height),0),
                Layout = linearLayout,
            };

// Don't use root whilst testing DropDown style refactor, add to Window instead.
            //Window.Instance.Add(root);

            text = new TextLabel()
            {
                Text = "DropDown Clicked item string is ",
                PointSize = 18,
                HeightSpecification = 80,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HorizontalAlignment = HorizontalAlignment.Center,
                MultiLine = true,
                BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 1.0f),
                PositionY = 80,
            };
// Don't use root whilst testing DropDown style refactor, add to Window instead.
//            root.Add(text);
            Window.Instance.Add(text);

            dropDown = new DropDown()
            {
                Size2D = new Size2D(900, 108),
                Position2D = new Position2D(50, 300),
                BackgroundColor = new Color(1, 1, 1, 1),
                ListPadding = new Extents(4, 4, 4, 4),
            };
            dropDown.ItemClickEvent += DropDownItemClickEvent;
            dropDown.Style.HeaderText.Text = "TitleArea";
            dropDown.Style.HeaderText.TextColor = new Color(0, 0, 0, 1);
            dropDown.Style.HeaderText.PointSize = 28;
            dropDown.Style.HeaderText.FontFamily = "SamsungOneUI 500C";
            dropDown.Style.Button.Text.Text = "DropDown Text";
            dropDown.Style.Button.Text.TextColor = new Color(0, 0, 0, 1);
            dropDown.Style.Button.Text.PointSize = 20;
            dropDown.Style.Button.Text.FontFamily = "SamsungOneUI 500";
            dropDown.Style.Button.Icon.ResourceUrl = TestImages.images[2];
            dropDown.Style.Button.Icon.Size = new Size(48, 48);
            dropDown.Style.Button.BackgroundColor.Pressed = new Color(0,1,0,0.4f);
            dropDown.Style.Button.BackgroundColor.Normal = new Color(0,0,1,0.4f);
            dropDown.Style.Button.PositionX = 56;
            dropDown.SpaceBetweenButtonTextAndIcon = 8;
            dropDown.Style.ListBackgroundImage.ResourceUrl = TestImages.images[0];
            dropDown.Style.ListBackgroundImage.Border = new Rectangle(51, 51, 51, 51);
            dropDown.Style.ListBackgroundImage.BackgroundColor = new Color(1,1,1,1f);
            dropDown.Style.ListBackgroundImage.Size = new Size(360, 500);
            dropDown.ListMargin.Start = 20;
            dropDown.ListMargin.Top = 20;

// Don't use root whilst testing DropDown style refactor, add to Window instead.
            //root.Add(dropDown);
            Window.Instance.Add(dropDown);

            for (int i = 0; i < 8; i++)
            {
                DropDown.DropDownDataItem item = new DropDown.DropDownDataItem();
                item.Size = new Size(360, 96);
                item.BackgroundColorSelector = new Selector<Color>
                {
                    Pressed = new Color(0, 0, 0, 0.4f),
                    Other = new Color(1, 1, 1, 0),
                };
                item.Text = "Normal list " + i;
                item.PointSize = 18;
                item.FontFamily = "SamsungOne 500";
                item.TextPosition = new Position(28, 0);
                item.CheckImageSize = new Size(40, 40);
                item.CheckImageResourceUrl = TestImages.images[1];
                item.CheckImageGapToBoundary = 16;
                dropDown.AddItem(item);
            }

            dropDown.SelectedItemIndex = 2;

            dropDown.RaiseToTop();
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
                        GC.Collect();
                        break;
                    }
                    case "r":
                    {
                        dropDown.DeleteItem(2);
                        break;
                    }
                    case "s":
                    {
                        Console.WriteLine("Selected Item index:{0}",dropDown.SelectedItemIndex);
                        dropDown.SelectedItemIndex = (dropDown.SelectedItemIndex==4)?1:4;
                    }
                    break;
                }
            }
        }
        private void DropDownItemClickEvent(object sender, Tizen.NUI.Components.DropDown.ItemClickEventArgs e)
        {
            text.Text = "DropDown Clicked item string is " + e.Text + "index:" + e.Index;
        }

        public void Deactivate()
        {
            if (root != null)
            {
                if (text != null)
                {
                    root.Remove(text);
                    text.Dispose();
                    text = null;
                }

                if (dropDown != null)
                {
                    if (scrollBar != null)
                    {
                        dropDown.DetachScrollBar();
                        scrollBar.Dispose();
                        scrollBar = null;
                    }

                    root.Remove(dropDown);
                    dropDown.Dispose();
                    dropDown = null;
                }

                root.Dispose();
            }
        }
        public override void Remove()
        {
            Window window = Window.Instance;
            root.Remove(dropDown);
            window.Remove(root);
            dropDown = null;
        }
    };
}
