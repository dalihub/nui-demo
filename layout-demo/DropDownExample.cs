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
            public static readonly string[] iconImage = new string[]
            {
                resources + "/images/dropdown_bg.png",
                resources + "/images/dropdown_checkbox_on.png",
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

            Window.Instance.Add(root);

            text = new TextLabel()
            {
                Text = "DropDown Clicked item string is ",
                PointSize = 14,
                HeightSpecification = 80,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HorizontalAlignment = HorizontalAlignment.Center,
                MultiLine = true,
                BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 1.0f),
            };
            root.Add(text);

            DropDownAttributes dropDownAttributes = new DropDownAttributes();

            dropDown = new Tizen.NUI.Components.DropDown(dropDownAttributes)
            {
                ListSize = new Size(360, 500),
                Name = "DropDownButtonControl",
                HeaderText = "Selectable item",
                HeaderTextColor = new Color(0, 0, 0, 1),
                HeaderTextPointSize = 28,
                HeaderTextFontFamily = "SamsungOneUI 500C",
                ButtonText = "DropDown Text",
                ButtonTextColor = new Color(0, 0, 0, 1),
                ButtonTextPointSize = 20,
                ButtonTextFontFamily = "SamsungOneUI 500",
                ButtonIconSize = new Size(48, 48),
                LeftSpace = 56,
                SpaceBetweenButtonTextAndIcon = 8,
                ListBackgroundImageURL = TestImages.iconImage[0],
                ListBackgroundImageBorder = new Rectangle(51, 51, 51, 51),
                ListMargin = new Extents(20,0,20,0),
                BackgroundColor = new Color(1, 1, 1, 1),
                ListPadding = new Extents(4, 4, 4, 4),
            };

            dropDown.WidthSpecification = 900; // LayoutParamPolicies.MatchParent
            dropDown.HeightSpecification = 108;

            dropDown.ItemClickEvent += DropDownItemClickEvent;
            root.Add(dropDown);

            for (int i = 0; i < 8; i++)
            {
                DropDown.DropDownItemData item = new DropDown.DropDownItemData();
                item.Size = new Size(360, 96);
                item.BackgroundColorSelector = new ColorSelector
                {
                    Pressed = new Color(0, 0, 0, 0.4f),
                    Other = new Color(1, 1, 1, 0),
                };
                item.Text = "Normal list " + i;
                item.PointSize = 18;
                item.FontFamily = "SamsungOne 500";
                item.TextPosition = new Position(28, 0);
                item.CheckImageSize = new Size(40, 40);
                item.CheckImageResourceUrl = TestImages.iconImage[1];
                item.CheckImageRightSpace = 16;
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

        private void ButtonClickEvent(object sender, Tizen.NUI.Components.Button.ClickEventArgs e)
        {
            Tizen.NUI.Components.Button btn = sender as Tizen.NUI.Components.Button;
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
