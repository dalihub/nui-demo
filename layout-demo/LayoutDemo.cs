using System;
using System.Collections.Generic;
using Tizen.System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    /// <summary>
    /// Abstract base class for layouting examples.
    /// </summary>
    abstract class Example
    {
	// Constructor
        protected Example(string name)
        {
            exampleName = name;
        }
        /// Should be overridden by deriving classes to create the required Layouting example
        public abstract void Create();

        /// Should be overridden by deriving classes to remove their layouting example from stage
        public abstract void Remove();

	// Get the title of the derived example
        public string GetLabel()
        {
            return exampleName;
        }

        protected string exampleName = "Title";
    };

    class LayoutingExample : NUIApplication
    {
        private List<Example> layoutingExamples = new List<Example>();
        private int layoutIndex = 0;
        private PushButton nextLayout;
        private TextLabel exampleTitle;

        static private View toolbar;

        public static ref View GetToolbar()
        {
            return ref toolbar;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            InitializeToolbar();
            Initialize();
        }

	// Create a tool bar for title and buttons
        private void InitializeToolbar()
        {
            // Window window = Window.Instance;
            // toolbar = new View();
            // var layout = new LinearLayout();
            // toolbar.Layout = layout;
            // toolbar.SetProperty(LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-1));
            // toolbar.SetProperty(LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(-2));
            // window.Add(toolbar);
        }

        private void Initialize()
        {
            // Change the background color of Window to White
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

            layoutingExamples.Add(new LinearExampleEx());
            layoutingExamples.Add(new LinearExample());
            layoutingExamples.Add(new PaddingExample());
            layoutingExamples.Add(new AbsoluteExample());
            layoutingExamples.Add(new FlexExample());
            layoutingExamples.Add(new GridExample());
            layoutingExamples.Add(new NestedLayoutExample());
            layoutingExamples.Add(new NoLayoutExample());
            layoutingExamples.Add(new ChildAddedToViewExample());

            layoutIndex = 0;
            layoutingExamples[layoutIndex].Create();

            string currentExampleLabel = layoutingExamples[layoutIndex].GetLabel();

            nextLayout = new PushButton();
            nextLayout.LabelText = "change layout";
            nextLayout.Clicked += (sender, e) =>
            {
                if (layoutingExamples.Count != 0)
                {
                    layoutingExamples[layoutIndex].Remove();
                    layoutIndex = (layoutIndex + 1) % layoutingExamples.Count;
                    layoutingExamples[layoutIndex].Create();
                    currentExampleLabel = layoutingExamples[layoutIndex].GetLabel();
                    exampleTitle.Text = currentExampleLabel;
                }
                return true;
            };

            exampleTitle = new TextLabel();
            exampleTitle.Text = currentExampleLabel;
            exampleTitle.Margin = new Extents( 10, 10, 0, 0);
            //toolbar.Add(nextLayout);
            //toolbar.Add(exampleTitle);
        }

        /// <summary>
        /// Helper function to create ImageViews with given filename and size..<br />
        /// </summary>
        /// <param name="filename"> The filename of the image to use.</param>
        /// <param name="size"> The size that the image should be loaded at.</param>
        /// <returns>The created ImageView.</returns>
        public static ImageView CreateChildImageView(String url, Size2D size)
        {
            ImageView imageView = new ImageView();
            ImageVisual imageVisual = new ImageVisual();

            imageVisual.URL = url;
            imageVisual.DesiredHeight = size.Height;
            imageVisual.DesiredWidth = size.Width;
            imageView.Image = imageVisual.OutputVisualMap;

            imageView.Name = "ImageView";
            imageView.HeightResizePolicy = ResizePolicyType.FillToParent;
            imageView.WidthResizePolicy = ResizePolicyType.FillToParent;
            return imageView;
        }

        public static ImageView CreateChildImageView(String url, Size2D size, Position2D position)
        {
            ImageView imageView = CreateChildImageView(url, size);
            imageView.Position2D = position;
            return imageView;
        }

        public static void SetSelectedIcon(Button button, string url)
        {
            var imageVisual = new ImageVisual();
            imageVisual.URL = url;
            button.SelectedVisual = imageVisual.OutputVisualMap;
        }

        public static void SetUnselectedIcon(Button button, string url)
        {
            var imageVisual = new ImageVisual();
            imageVisual.URL = url;
            button.UnselectedVisual = imageVisual.OutputVisualMap;
        }

        static void Main(string[] args)
        {
            // Do not remove this print out - helps with the TizenFX stub sync issue
            Console.WriteLine("Run layout demo...");
            LayoutingExample example = new LayoutingExample();
            example.Run(args);
        }
    }
}
