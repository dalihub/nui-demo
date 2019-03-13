using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class PaddingExample : Example
    {
        public PaddingExample() : base( "PaddingExample" )
        {}

        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] s_images = new string[]
            {
                resources + "/images/gallery-small-23.jpg",
                resources + "/images/gallery-small-23.jpg",
                resources + "/images/gallery-small-23.jpg",
                resources + "/images/gallery-small-23.jpg",
            };
        }
        private View view;

        public override void Create()
        {
            View view = new View();
            view.Name = "PaddingExample";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.WidthSpecification = 480;
            view.WidthSpecification = 800;

            var layout = new LinearLayoutEx();
            view.LayoutEx = layout;
            view.LayoutDirection = ViewLayoutDirectionType.LTR;

            // Add child image-views to the created view
            foreach (String image in TestImages.s_images)
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(100, 100));
                imageView.Margin = new Extents(10, 10, 0, 0);
                imageView.TouchEvent += (sender, e) =>
                {
                    if (sender is ImageView && e.Touch.GetState(0) == PointStateType.Down)
                    {
                        ImageView iv = (ImageView)sender;
                        if (iv.Padding.EqualTo(new Extents(0, 0, 0, 0)))
                        {
                            iv.Padding = new Extents(10, 10, 10, 10);
                        }
                        else
                        {
                            iv.Padding = new Extents(0, 0, 0, 0);
                        }
                    }
                    return true;
                };

                view.Add(imageView);
            }

            View window = LayoutingExample.GetWindowView();
            this.view = view;
            window.Add(view);
        }

        public override void Remove()
        {
            View window = LayoutingExample.GetWindowView();
            window.Remove(this.view);
            this.view = null;
        }
    };
}
