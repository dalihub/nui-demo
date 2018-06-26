using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace SimpleLayout
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

    class SimpleLayout : NUIApplication
    {
        private Layer _layer;
        private Layer _layer2;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private void Initialize()
        {
            // Change the background color of Window to White
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;


            _layer = new Layer();
            window.AddLayer(_layer);

            // Create a new view
            View customLayoutView = new View();
            customLayoutView.Name = "CustomLayoutView";
            customLayoutView.ParentOrigin = ParentOrigin.Center;
            customLayoutView.PivotPoint = PivotPoint.Center;
            customLayoutView.PositionUsesPivotPoint = true;

            // Set our Custom Layout on the view
            CustomLayout layout = new CustomLayout();
            customLayoutView.Layout = layout;
            _layer.Add( customLayoutView );

            customLayoutView.SetProperty( LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-2) );  // -2 WRAP_CONTENT
            customLayoutView.SetProperty( LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(350) );
            customLayoutView.BackgroundColor = Color.Blue;

            // Add child image-views to the created view
            foreach (String image in TestImages.s_images)
            {
                customLayoutView.Add( CreateChildImageView( image, new Size2D( 100, 100 ) ) );
            }

            View testView = new View();
            CustomLayout layout2 = new CustomLayout();
            testView.Layout = layout2;

            _layer2 = new Layer();
            _layer2.Add( testView );
            testView.BackgroundColor = Color.Blue;
            testView.SetProperty( LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-2) );  // -2 WRAP_CONTENT
            testView.SetProperty( LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(80) );
            testView.Position2D = new Position2D( 100, 200 );
            window.AddLayer(_layer2);
            foreach (String image in TestImages.s_images)
            {
                testView.Add( CreateChildImageView( image, new Size2D( 50, 50 ) ) );
            }

        }

        /// <summary>
        /// Helper function to create ImageViews with given filename and size..<br />
        /// </summary>
        /// <param name="filename"> The filename of the image to use.</param>
        /// <param name="size"> The size that the image should be loaded at.</param>
        /// <returns>The created ImageView.</returns>
        ImageView CreateChildImageView( String url, Size2D size )
        {
            ImageView imageView = new ImageView();
            ImageVisual imageVisual = new ImageVisual();

            imageVisual.URL = url;
            imageVisual.DesiredHeight = size.Height;
            imageVisual.DesiredWidth = size.Width;
            imageView.Image = imageVisual.OutputVisualMap;

            imageView.Name = "ImageView";
            imageView.HeightResizePolicy = ResizePolicyType.Fixed;
            imageView.WidthResizePolicy = ResizePolicyType.Fixed;
            return imageView;
        }

        static void Main(string[] args)
        {
            Console.Write("Stating SimpleLayout");
            SimpleLayout simpleLayout = new SimpleLayout();
            simpleLayout.Run(args);
        }
    }
}
