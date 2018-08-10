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
        /// Should be overridden by deriving classes to create the required Layouting example
        public abstract void Create();

        /// Should be overridden by deriving classes to remove their layouting example from stage
        public abstract void Remove();
    };

    class LayoutingExample : NUIApplication
    {
        private List<Example> layoutingExamples = new List<Example>();
        private int layoutIndex = 0;
        private PushButton nextLayout;

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

            layoutingExamples.Add(new LinearExample());
            layoutingExamples.Add(new PaddingExample());
            layoutingExamples.Add(new AbsoluteExample());
            layoutingExamples.Add(new FlexExample());
            layoutingExamples.Add(new NoLayoutExample());
            layoutingExamples.Add(new FlexContainerExample());

            layoutingExamples[0].Create();
            layoutIndex = 0;

            nextLayout = new PushButton();
            nextLayout.Name = "nextLayoutButton";
            nextLayout.ParentOrigin = ParentOrigin.TopRight;
            nextLayout.PivotPoint = PivotPoint.TopRight;
            nextLayout.PositionUsesPivotPoint = true;
            nextLayout.LabelText = "change layout";
            nextLayout.Clicked += (sender, e) =>
            {
                if (layoutingExamples.Count != 0)
                {
                    layoutingExamples[layoutIndex].Remove();
                    layoutIndex = (layoutIndex + 1) % layoutingExamples.Count;
                    layoutingExamples[layoutIndex].Create();
                }

                // Reattach 'next layout' button so it stays on top and updated.
                window.Remove(nextLayout);
                window.Add(nextLayout);
                return true;
            };

            window.Add(nextLayout);
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
            imageView.HeightResizePolicy = ResizePolicyType.Fixed;
            imageView.WidthResizePolicy = ResizePolicyType.Fixed;
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
