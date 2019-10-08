using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class AbsoluteExample : Example
    {
        public AbsoluteExample() : base( "Absolute Layout" )
        {}

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
        private PushButton sizeButton;

        public override void Create()
        {
            Window window = Window.Instance;

            // Absolute Layout is created automatically in Window.
            View view = new View();
            view.Name = "demo-absoluteLayout";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.WidthSpecification =  LayoutParamPolicies.WrapContent;
            view.HeightSpecification =  LayoutParamPolicies.WrapContent;
            view.BackgroundColor = Color.Blue;

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

            this.view = view;

            sizeButton = new PushButton();
            sizeButton.Name = "absolute-size-change-button";
            sizeButton.ParentOrigin = ParentOrigin.BottomCenter;
            sizeButton.PivotPoint = PivotPoint.BottomCenter;
            sizeButton.PositionUsesPivotPoint = true;
            sizeButton.LabelText = "change size";
            sizeButton.Clicked += (sender, e) =>
            {
                Tizen.Log.Error("Seoyeon", "Seoyeon@@@@@@@@ 1. view width : "+view.Size2D.Width+", Height : "+view.Size2D.Height+"\n");
                Tizen.Log.Error("Seoyeon", "Seoyeon@@@@@@@@ 2. View width : "+this.view.Size2D.Width+", Height : "+this.view.Size2D.Height+"\n");
                if (!fullSize)
                {
                    this.view.WidthSpecification = LayoutParamPolicies.MatchParent;
                    this.view.HeightSpecification = LayoutParamPolicies.MatchParent;
                }
                else
                {
                    this.view.WidthSpecification = LayoutParamPolicies.WrapContent;
                    this.view.HeightSpecification = LayoutParamPolicies.WrapContent;
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
            window.Remove(view);
            view = null;
            window.Remove(sizeButton);
            sizeButton = null;
        }
    };
}
