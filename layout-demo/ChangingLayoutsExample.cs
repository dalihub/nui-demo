using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class ChangingLayoutsExample : Example
    {
        public ChangingLayoutsExample() : base( "ChangingLayouts" )
        {}

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
            view = new View();
            view.Name = "ChangingLayout";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.WidthSpecification = 480;
            view.HeightSpecification = 800;

            //var layout = new LinearLayoutEx();
            var layout = new GridLayout();
            layout.Columns = 2;
            view.LayoutEx = layout;
            //view.LayoutDirection = ViewLayoutDirectionType.LTR;

            // Add child image-views to the created view
            foreach (String image in TestImages.s_images)
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(100, 100));
                view.Add(imageView);
                view.MinimumSize = new Size2D(200,200);
            }

            LayoutingExample.GetWindow().Add(view);

            PushButton changeLayoutButton = new PushButton();
            changeLayoutButton.Name = "changeLayout-button";
            LayoutingExample.SetUnselectedIcon(changeLayoutButton, "./res/images/icon-reverse.png");
            LayoutingExample.SetSelectedIcon(changeLayoutButton, "./res/images/icon-reverse-selected.png");
            changeLayoutButton.ParentOrigin = new Vector3(0.33f, 1.0f, 0.5f);
            changeLayoutButton.PivotPoint = PivotPoint.BottomCenter;
            changeLayoutButton.PositionUsesPivotPoint = true;
            changeLayoutButton.MinimumSize = new Vector2(75, 75);
            changeLayoutButton.Clicked += (sender, e) =>
            {
                //(view.LayoutEx as GridLayout ).Columns = 3;
                var newLayout = new LinearLayoutEx();
                // var newLayout = new GridLayout();
                // newLayout.Columns = 2;
                view.LayoutEx = newLayout;
                // LayoutingExample.SetUnselectedIcon(changeLayoutButton, "./res/images/icon-play.png");
                // LayoutingExample.SetUnselectedIcon(changeLayoutButton, "./res/images/icon-play-selected.png");
                return true;
            };
            LayoutingExample.GetWindow().Add(changeLayoutButton);
            buttons.Add(changeLayoutButton);
        }

        public override void Remove()
        {
            LayoutingExample.GetWindow().Remove(view);

            view = null;
            foreach (PushButton button in buttons)
            {
                LayoutingExample.GetWindow().Remove(button);
            }
            buttons.Clear();
        }
    };
}
