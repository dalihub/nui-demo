using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class PaddingExample : Example
    {
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

            Window window = Window.Instance;
            this.view = view;
            window.Add(view);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(this.view);
            this.view = null;
        }
    };
}
