using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace LayoutDemo
{
    class FlexExample : Example
    {
        public FlexExample() : base( "FlexLayout" )
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
                resources + "/images/application-icon-104.png",
                resources + "/images/application-icon-105.png",
                resources + "/images/application-icon-106.png",
                resources + "/images/application-icon-103.png",
                resources + "/images/application-icon-104.png",
                resources + "/images/application-icon-105.png",
                resources + "/images/application-icon-106.png"
            };

        }

        private View view;
        private List<Button> buttons = new List<Button>();

        public override void Create()
        {
            Window window = Window.Instance;

            view = new View();
            view.Name = "FlexExample";
            view.ParentOrigin = ParentOrigin.TopLeft;
            view.PivotPoint = PivotPoint.TopLeft;
            view.PositionUsesPivotPoint = true;
            view.WidthSpecification = LayoutParamPolicies.MatchParent;
            int viewHeight = (int)(window.Size.Height * .90);

            view.HeightSpecification = viewHeight;

            view.PositionY = window.Size.Height - viewHeight;

            view.Padding = new Extents(190,150,100,100);
            var layout = new FlexLayout();

            layout.WrapType = FlexLayout.FlexWrapType.NoWrap;
            layout.ItemsAlignment = FlexLayout.AlignmentType.Center;
            view.Layout = layout;
            view.LayoutDirection = ViewLayoutDirectionType.LTR;

            // Add child image-views to the created view

            foreach (String image in TestImages.s_images)
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(100, 100));
                view.Add(imageView);
            }

            window.Add(view);

            Button directionButton = new Button();
            LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-reverse.png");
            LayoutingExample.SetSelectedIcon(directionButton, "./res/images/icon-reverse-selected.png");
            directionButton.ParentOrigin = new Vector3(0.2f, 1.0f, 0.5f);
            directionButton.PivotPoint = PivotPoint.BottomCenter;
            directionButton.PositionUsesPivotPoint = true;
            directionButton.MinimumSize = new Vector2(75, 75);
            directionButton.Clicked += (sender, e) =>
            {
                if (this.view.LayoutDirection == ViewLayoutDirectionType.LTR)
                {
                    this.view.LayoutDirection = ViewLayoutDirectionType.RTL;
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-play.png");
                    LayoutingExample.SetSelectedIcon(directionButton, "./res/images/icon-play-selected.png");
                }
                else
                {
                    this.view.LayoutDirection = ViewLayoutDirectionType.LTR;
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-reverse.png");
                    LayoutingExample.SetSelectedIcon(directionButton, "./res/images/icon-reverse-selected.png");

                }
                return ;

            };

            window.Add(directionButton);
            buttons.Add(directionButton);


            Button wrapButton = new Button();
            LayoutingExample.SetUnselectedIcon(wrapButton, "./res/images/icon-w.png");
            LayoutingExample.SetSelectedIcon(wrapButton, "./res/images/icon-w-selected.png");
            wrapButton.ParentOrigin = new Vector3(0.4f, 1.0f, 0.5f);
            wrapButton.PivotPoint = PivotPoint.BottomCenter;
            wrapButton.PositionUsesPivotPoint = true;
            wrapButton.MinimumSize = new Vector2(75, 75);
            wrapButton.Clicked += (sender, e) =>
            {
                FlexLayout flexLayout = (FlexLayout)this.view.Layout;
                if (flexLayout.WrapType == FlexLayout.FlexWrapType.Wrap)
                {
                    view.Padding = new Extents(0, 0, 0, 0);
                    flexLayout.WrapType = FlexLayout.FlexWrapType.NoWrap;
                    flexLayout.Alignment = FlexLayout.AlignmentType.Center;
                    flexLayout.ItemsAlignment = FlexLayout.AlignmentType.Center;
                }
                else
                {
                    view.Padding = new Extents(25, 25, 75, 75);
                    flexLayout.WrapType = FlexLayout.FlexWrapType.Wrap;
                    flexLayout.Alignment = FlexLayout.AlignmentType.FlexStart;
                    flexLayout.ItemsAlignment = FlexLayout.AlignmentType.FlexStart;
                }
                //return true;
            };

            window.Add(wrapButton);
            buttons.Add(wrapButton);

            Button justifyButton = new Button();

            LayoutingExample.SetUnselectedIcon(justifyButton, "./res/images/icon-item-view-layout-grid.png");
            LayoutingExample.SetSelectedIcon(justifyButton, "./res/images/icon-item-view-layout-grid-selected.png");
            justifyButton.ParentOrigin = new Vector3(0.6f, 1.0f, 0.5f);
            justifyButton.PivotPoint = PivotPoint.BottomCenter;
            justifyButton.PositionUsesPivotPoint = true;
            justifyButton.MinimumSize = new Vector2(75, 75);
            justifyButton.Clicked += (sender, e) =>

            {
                FlexLayout flexLayout = (FlexLayout)this.view.Layout;

                if (flexLayout.Justification == FlexLayout.FlexJustification.FlexStart)
                {
                    flexLayout.Justification = FlexLayout.FlexJustification.Center;
                }
                else
                {
                    flexLayout.Justification = FlexLayout.FlexJustification.FlexStart;
                }

                //return true;

            };
            window.Add(justifyButton);
            buttons.Add(justifyButton);


            Button rotateButton = new Button();
            LayoutingExample.SetUnselectedIcon(rotateButton, "./res/images/icon-reset.png");
            LayoutingExample.SetSelectedIcon(rotateButton, "./res/images/icon-reset-selected.png");
            rotateButton.ParentOrigin = new Vector3(0.8f, 1.0f, 0.5f);
            rotateButton.PivotPoint = PivotPoint.BottomCenter;
            rotateButton.PositionUsesPivotPoint = true;
            rotateButton.MinimumSize = new Vector2(75, 75);

            rotateButton.Clicked += (sender, e) =>
            {
                FlexLayout flexLayout = (FlexLayout)this.view.Layout;

                if (flexLayout.Direction == FlexLayout.FlexDirection.Row)
                {
                    flexLayout.Direction = FlexLayout.FlexDirection.Column;
                }
                else
                {
                    flexLayout.Direction = FlexLayout.FlexDirection.Row;
                }
                //return true;
            };
            window.Add(rotateButton);
            buttons.Add(rotateButton);
        }


        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(view);
            view = null;
            foreach (Button button in buttons)
            {
                window.Remove(button);
            }
            buttons.Clear();
        }

    };

}