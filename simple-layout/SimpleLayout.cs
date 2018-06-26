using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

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

            // Create first new view
            View view = new View();
            view.Name = "CustomLayoutView";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            // Set our Custom Layout on the first view
            var layout = new CustomLayout();
            view.Layout = layout;
            view.SetProperty( LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-2) );
            view.SetProperty( LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(350) );
            view.BackgroundColor = Color.Blue;
            window.Add( view);

            // Create second View
            View littleView = new View();
            littleView.Name = "LittleView";
            // Set our Custom Layout on the little view
            var layout2 = new CustomLayout();
            littleView.Layout = layout2;
            littleView.SetProperty( LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-2) );
            littleView.SetProperty( LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(-2) );
            littleView.BackgroundColor = Color.Red;
            littleView.Position2D = new Position2D(50,50);
            // Add second View to a Layer
            Layer layer2 = new Layer();
            window.AddLayer( layer2 );
            layer2.Add( littleView );
        
            // Create single single ImageView in it's own Layer
            Layer layer1 = new Layer();
            window.AddLayer( layer1 );
            layer1.Add( CreateChildImageView( TestImages.s_images[1], new Size2D( 100, 100 ) ) );

            // Initially single ImageView is not on top.
            layer2.Raise();

            // Add an ImageView directly to window
            window.Add( CreateChildImageView( TestImages.s_images[2], new Size2D( 200, 200 ) ) );

            // Add child image-views to the created view
            foreach (String image in TestImages.s_images)
            {
                view.Add( CreateChildImageView( image, new Size2D( 100, 100 ) ) );
                littleView.Add( CreateChildImageView( image, new Size2D(50,50 ) ) ); 
            }

            // Example info
            TextLabel label = new TextLabel("Blue icon in a layer");
            label.ParentOrigin = ParentOrigin.TopCenter;
            label.Position2D = new Position2D(-50,0);
            window.Add( label );

            // Add button to Raise or Lower the single ImageView
            PushButton button = new PushButton();
            button.LabelText = "Raise Layer";
            button.ParentOrigin = ParentOrigin.BottomCenter;
            button.PivotPoint = PivotPoint.BottomCenter;
            button.PositionUsesPivotPoint = true;
            window.Add( button );
    
            button.Clicked += (obj, e) =>
            {
                PushButton sender = obj as PushButton;

                if (sender.LabelText == "Raise Layer")
                {
                layer1.RaiseToTop();
                sender.LabelText = "Lower Layer";
                }
                else
                {
                sender.LabelText = "Raise Layer";
                layer1.LowerToBottom();
                }
                return true;
            };
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
            Console.Write("Stating SimpleLayout\n");
            SimpleLayout simpleLayout = new SimpleLayout();
            simpleLayout.Run(args);
        }
    }
}
