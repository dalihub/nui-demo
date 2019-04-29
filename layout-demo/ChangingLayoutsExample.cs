using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    /// <summary>
    /// Example showing the changing back and forth from a Grid and Linear layout.
    /// </summary>
    class ChangingLayoutsExample : Example
    {
        public ChangingLayoutsExample() : base( "Changing Layouts" )
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

        private bool gridLayout = true;

        public override void Create()
        {
            Window window = LayoutingExample.GetWindow();
            view = new View();
            view.Name = "ChangingLayout";
            view.WidthSpecification = LayoutParamPolicies.WrapContent;
            view.HeightSpecification = LayoutParamPolicies.WrapContent;

            // Position layout within Window.
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;

            // Start with a GridLayout
            SetGridLayout(view);

            // Add child image-views to the created view
            foreach (String image in TestImages.s_images)
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(100, 100));
                view.Add(imageView);
                view.MinimumSize = new Size2D(200,200);
            }

            LayoutingExample.GetWindow().Add(view);

            // Setup button to switch layouts.
            PushButton changeLayoutButton = new PushButton();
            changeLayoutButton.Name = "changeLayout-button";
            LayoutingExample.SetUnselectedIcon(changeLayoutButton, "./res/images/iconLinear.png");
            LayoutingExample.SetSelectedIcon(changeLayoutButton, "./res/images/iconLinearSelected.png");
            changeLayoutButton.ParentOrigin = new Vector3(0.33f, 1.0f, 0.5f);
            changeLayoutButton.PivotPoint = PivotPoint.BottomCenter;
            changeLayoutButton.PositionUsesPivotPoint = true;
            changeLayoutButton.MinimumSize = new Vector2(75, 75);
            changeLayoutButton.Clicked += (sender, e) =>
            {
                if (gridLayout)
                {
                    SetLinearLayout(view);
                    LayoutingExample.SetUnselectedIcon(changeLayoutButton, "./res/images/iconGrid.png");
                    LayoutingExample.SetSelectedIcon(changeLayoutButton, "./res/images/iconGridSelected.png");
                }
                else
                {
                    SetGridLayout(view);
                    LayoutingExample.SetUnselectedIcon(changeLayoutButton, "./res/images/iconLinear.png");
                    LayoutingExample.SetSelectedIcon(changeLayoutButton, "./res/images/iconLinearSelected.png");
                }

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

        private void SetGridLayout(View targetView)
        {
            var layout = new GridLayout();
            layout.Columns = 2;
            targetView.Layout = layout;
            gridLayout = true;
        }

        private void SetLinearLayout(View targetView)
        {
            var newLayout = new LinearLayout();
            targetView.Layout = newLayout;
            gridLayout = false;
        }
    };
}
