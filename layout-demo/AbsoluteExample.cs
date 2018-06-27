using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class AbsoluteExample : Example
    {
        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] s_images = new string[]
            {
                resources + "/images/gallery-small-23.jpg",
            };
        }

        private View view;
        private bool fullSize = false;

        public override void Create()
        {
            // Absolute Layout is created automatically in Window.
            View view = new View();
            view.Name = "AbsoluteExample";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.SetProperty(LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-2));
            view.SetProperty(LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(-2));
            view.BackgroundColor = Color.White;

            var layout = new AbsoluteLayout();
            view.Layout = layout;

            // Add child image-views to the created view
            ImageView imageView = LayoutingExample.CreateChildImageView("./res/images/gallery-small-23.jpg", new Size2D(100, 100), new Position2D (0, 0));
            view.Add(imageView);
            imageView = LayoutingExample.CreateChildImageView("./res/images/gallery-small-23.jpg", new Size2D(100, 100), new Position2D (100, 0));
            view.Add(imageView);
            imageView = LayoutingExample.CreateChildImageView("./res/images/gallery-small-23.jpg", new Size2D(100, 100), new Position2D (0, 100));
            view.Add(imageView);
            imageView = LayoutingExample.CreateChildImageView("./res/images/gallery-small-23.jpg", new Size2D(200, 200), new Position2D (100, 100));
            view.Add(imageView);

            Window window = Window.Instance;
            window.BackgroundColor = Color.Blue;
            this.view = view;

            PushButton sizeButton = new PushButton();
            sizeButton.ParentOrigin = ParentOrigin.BottomCenter;
            sizeButton.PivotPoint = PivotPoint.BottomCenter;
            sizeButton.PositionUsesPivotPoint = true;
            sizeButton.LabelText = "change size";
            sizeButton.Clicked += (sender, e) =>
            {
                if (!fullSize)
                {
                    this.view.SetProperty(LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(480));
                    this.view.SetProperty(LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(800));
                }
                else
                {
                    this.view.SetProperty(LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-2));
                    this.view.SetProperty(LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(-2));
                }
                fullSize = !fullSize;
                return true;
            };

            window.Add(view);
            window.Add(sizeButton);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(this.view);
            this.view = null;
        }
    };
}
