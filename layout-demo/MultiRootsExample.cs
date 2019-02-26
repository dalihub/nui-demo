using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    /// <summary>
    /// Tests layouting a tree with multiple roots and Views not parented to any Layout
    /// </summary>
    class MultiRootsExample : Example
    {
        public MultiRootsExample() : base( "Multi Roots" )
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

        /*
         *    View02 and View04 set as LinearLayouts
         *
         *    View01 and View02 both added to the window, will overlap unless explicitly positioned.
         *
         *            window
         *         |              |
         *     View01            View02 (LL)
         *     |    |             |      |
         *  View03 View04(LL)     View05 LegacyContainer
         *     |        |  |           |             |
         * View07       | View 08    ImageView05    View06
         *   |          |  |                |          |
         * ImageView04  | ImageView03   ImageView02  ImageView01
         *           ImageView06
         */

        private View View01; // Green
        private View View02; // Blue
        private View View03; // Yellow
        private View View04; // Cyan
        private View View05; // Yellow
        private View View07; // Blue
        private View View08; // Magenta

        private ImageView ImageView01;
        private ImageView ImageView02;
        private ImageView ImageView03;
        private ImageView ImageView04;
        private ImageView ImageView05;
        private ImageView ImageView06;

        private ImageView helpImageView;
        PushButton helpButton;
        bool helpShowing = false;
        private List<PushButton> buttons = new List<PushButton>();

        public override void Create()
        {
            Window window = Window.Instance;

            View01 = new View()
            {
              BackgroundColor = Color.Green,
              Position2D = new Position2D(0, window.Size.Height/8),
              Size2D = new Size2D( window.Size.Width / 2, (window.Size.Height/8) * 7),
              Name = "View01"
            };
            window.Add(View01);

            View02 = new View()
            {
                Position2D = new Position2D(window.Size.Width / 2, window.Size.Height/8),
                WidthSpecification = (window.Size.Width / 2),
                HeightSpecification = (window.Size.Height/8) * 7,
                BackgroundColor = Color.Blue,
                Layout = new LinearLayout(),
                Name = "View02LinearView",
            };
            window.Add(View02);

            View03 = new View()
            {
                BackgroundColor = Color.Yellow,
                Name = "View03",
                Size2D = new Size2D(20,(window.Size.Height/8) * 7)
            };
            View01.Add(View03);

            LinearLayout verticalLayout = new LinearLayout();
            verticalLayout.LinearOrientation = LinearLayout.Orientation.Vertical;

            View04 = new View()
            {
                BackgroundColor = Color.Cyan,
                PositionX = View03.Size2D.Width,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Layout = verticalLayout,
                Name = "View04LinearView",
            };
            View01.Add(View04);

            View07 = new View()
            {
                BackgroundColor = Color.Blue,
                Size2D = new Size2D(200,200),
                Name = "View07"
            };
            View03.Add(View07);

            ImageView06 = LayoutingExample.CreateChildImageView("res/images/application-icon-101.png", new Size2D(100, 100));
            ImageView06.Name = "imageView-06";
            View04.Add(ImageView06);

            ImageView04 = LayoutingExample.CreateChildImageView("res/images/application-icon-101.png", new Size2D(200, 200));
            ImageView04.Name = "imageView-04";
            View07.Add(ImageView04);

            View08 = new View()
            {
                BackgroundColor = Color.Magenta,
                Size2D = new Size2D( 280,280),
                Name = "View08"
            };
            View04.Add(View08);

            ImageView03 = LayoutingExample.CreateChildImageView("res/images/application-icon-101.png", new Size2D(100, 100));
            ImageView03.Name = "imageView-03";
            View08.Add(ImageView03);

            View05 = new View()
            {
                BackgroundColor = Color.Yellow,
                Name = "View05",
                Size2D = new Size2D(10,100)
            };
            View02.Add(View05);

            FlexContainer legacyContainer = new FlexContainer();
            legacyContainer.FlexDirection = FlexContainer.FlexDirectionType.Column;
            legacyContainer.Size2D = new Size2D(View02.Size2D.Width / 2, 280);
            legacyContainer.Name = "legacyFlexContainer";
            View02.Add(legacyContainer);

            ImageView02 = LayoutingExample.CreateChildImageView("res/images/application-icon-103.png", new Size2D(100, 100));
            ImageView02.Name = "imageView-02";
            legacyContainer.Add(ImageView02);
            ImageView01 = LayoutingExample.CreateChildImageView("res/images/application-icon-104.png", new Size2D(100, 100));
            ImageView01.Name = "imageView-01";
            legacyContainer.Add(ImageView01);

            ImageView05 = LayoutingExample.CreateChildImageView("res/images/application-icon-102.png", new Size2D(100, 100));
            ImageView05.Name = "imageView-05";
            View05.Add(ImageView05);

            CreateHelpButton();
            LayoutingExample.GetToolbar().Add( helpButton );
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            if(helpImageView )
            {
                window.Remove(helpImageView);
                helpImageView = null;
            }
            helpShowing = false;
            LayoutingExample.GetToolbar().Remove(helpButton);
            window.Remove(View01);
            window.Remove(View02);
            helpButton = null;
        }

        // Shows a thumbnail of the expected output
        private void CreateHelpButton()
        {
            helpButton = new PushButton();
            helpButton.LabelText = "Help";
            helpButton.Clicked += (sender, e) =>
            {
                if (! helpShowing)
                {
                    Window window = Window.Instance;
                    helpImageView = LayoutingExample.CreateChildImageView("./res/images/child-added-to-view-example.png", new Size2D(200, 200));
                    helpImageView.Position2D = new Position2D(0, helpButton.Size2D.Height);
                    helpShowing = true;
                    window.Add( helpImageView );
                }
                else
                {
                    Window window = Window.Instance;
                    window.Remove(helpImageView);
                    helpShowing = false;
                }
                return true;
            };
        }

    };
}
