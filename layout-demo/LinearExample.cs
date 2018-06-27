using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class LinearExample : Example
    {
        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] s_images = new string[]
            {
                resources + "/images/application-icon-101.png",
                resources + "/images/application-icon-102.png",
                resources + "/images/application-icon-103.png",
                resources + "/images/application-icon-104.png"
            };
        }

        private View view;
        private List<PushButton> buttons = new List<PushButton>();

        public override void Create()
        {
            View view = new View();
            view.Name = "LinearExample";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.SetProperty(LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(480));
            view.SetProperty(LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(800));
            view.BackgroundColor = Color.Blue;

            var layout = new LinearLayout();
            view.Layout = layout;
            view.LayoutDirection = ViewLayoutDirectionType.LTR;

            // Add child image-views to the created view
            foreach (String image in TestImages.s_images)
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(100, 100));
                view.Add(imageView);
            }

            Window window = Window.Instance;
            this.view = view;
            window.Add(view);

            PushButton directionButton = new PushButton();
            LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-reverse.png");
            LayoutingExample.SetSelectedIcon(directionButton, "./res/images/icon-reverse-selected.png");
            directionButton.ParentOrigin = new Vector3(0.33f, 1.0f, 0.5f);
            directionButton.PivotPoint = PivotPoint.BottomCenter;
            directionButton.PositionUsesPivotPoint = true;
            directionButton.MinimumSize = new Vector2(75, 75);
            directionButton.Clicked += (sender, e) =>
            {
                if (this.view.LayoutDirection == ViewLayoutDirectionType.LTR)
                {
                    this.view.LayoutDirection = ViewLayoutDirectionType.RTL;
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-play.png");
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-play-selected.png");
                }
                else
                {
                    this.view.LayoutDirection = ViewLayoutDirectionType.LTR;
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-reverse.png");
                    LayoutingExample.SetSelectedIcon(directionButton, "./res/images/icon-reverse-selected.png");
                }
                return true;
            };
            window.Add(directionButton);
            buttons.Add(directionButton);

            PushButton rotateButton = new PushButton();
            LayoutingExample.SetUnselectedIcon(rotateButton, "./res/images/icon-reset.png");
            LayoutingExample.SetSelectedIcon(rotateButton, "./res/images/icon-reset-selected.png");
            rotateButton.ParentOrigin = new Vector3(0.66f, 1.0f, 0.5f);
            rotateButton.PivotPoint = PivotPoint.BottomCenter;
            rotateButton.PositionUsesPivotPoint = true;
            rotateButton.MinimumSize = new Vector2(75, 75);
            rotateButton.Clicked += (sender, e) =>
            {
                LinearLayout linearLayout = (LinearLayout)this.view.Layout;
                if (linearLayout.LinearOrientation == LinearLayout.Orientation.Horizontal)
                {
                    linearLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
                }
                else
                {
                    linearLayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                }
                return true;
            };

            window.Add(rotateButton);
            buttons.Add(rotateButton);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(view);
            view = null;
            foreach (PushButton button in buttons)
            {
                window.Remove(button);
            }
            buttons.Clear();
        }
    };
}
