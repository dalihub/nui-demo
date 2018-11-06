using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class NestedLayoutExample : Example
    {
        public NestedLayoutExample() : base( "Nested" )
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

        private View contentBackgroundShadow;
        private ImageView helpImageView;
        PushButton helpButton;
        bool helpShowing = false;
        private List<PushButton> buttons = new List<PushButton>();

        public LinearLayout createVbox()
        {
            LinearLayout vbox = new LinearLayout();
            vbox.LinearOrientation = LinearLayout.Orientation.Vertical;
            return vbox;
        }

        public LinearLayout createHbox()
        {
            LinearLayout hbox = new LinearLayout();
            hbox.LinearOrientation = LinearLayout.Orientation.Horizontal;
            return hbox;
        }

        public override void Create()
        {
            Window window = Window.Instance;
            window.BackgroundColor = Color.Green;

            contentBackgroundShadow = new View()
            {
                Name = "contentBackgroundShadow",
                Size2D = new Size2D(476,500),
                BackgroundColor = Color.Red,
                Position2D = new Position2D(0, 40),
            };

            View backgroundContainer = new View()
            {
                Name = "backgroundContainer",
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                BackgroundColor = Color.Red,
            };

            View contentBackground = new View()
            {
                Name = "contentBackground",
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                BackgroundColor = Color.Yellow,
                Layout = createVbox(),
                LayoutWidthSpecification = ChildLayoutData.WrapContent,
                LayoutHeightSpecification = ChildLayoutData.WrapContent,
            };

            TextLabel textLabel = new TextLabel()
            {
                BackgroundColor = Color.White,
                Position2D = new Position2D(0, -10),
                Name = "TextLabel",
                Text = "Popup title",
                PointSize = 34
            };
            contentBackground.Add(textLabel);


            View contentContainer = new View()
            {
                 Name = "contentContainer",
                 Layout = createHbox(),
                 LayoutWidthSpecification = ChildLayoutData.WrapContent,
                 LayoutHeightSpecification = ChildLayoutData.WrapContent
            };
            contentBackground.Add(contentContainer);

            View contentAreaOne = new View()
            {
                Name = "contentAreaOne",
                Margin = new Extents(10,10,10,10),
                Layout = createVbox(),
                LayoutWidthSpecification = ChildLayoutData.WrapContent,
                LayoutHeightSpecification = ChildLayoutData.WrapContent,
                BackgroundColor = Color.Red,
            };
            contentContainer.Add(contentAreaOne);

            View contentAreaTwo = new View()
            {
                Name = "contentAreaTwo",
                Margin = new Extents(10,10,10,10),
                Layout = createVbox(),
                LayoutWidthSpecification = ChildLayoutData.WrapContent,
                LayoutHeightSpecification = ChildLayoutData.WrapContent,
                BackgroundColor = Color.Cyan,
            };
            contentContainer.Add(contentAreaTwo);

            ImageView[] children = new ImageView[3];
            for( int i = 0; i < 3; i++ )
            {
                children[i] = new ImageView("./res/images/gallery-small-23.jpg");
                children[i].PivotPoint = PivotPoint.Center;
                children[i].LayoutWidthSpecificationFixed = 70;
                children[i].LayoutHeightSpecificationFixed = 70;
                children[i].BackgroundColor = new Color( i * 0.25f, i * 0.25f, 1.0f, 1.0f );
                children[i].Name = "imageView";
                contentAreaOne.Add(children[i]);
            };

            ImageView[] children2 = new ImageView[5];
            for( int i = 0; i < 5; i++ )
            {
                children2[i] = new ImageView("./res/images/application-icon-102.png");
                children2[i].PivotPoint = PivotPoint.Center;
                children2[i].LayoutWidthSpecificationFixed = 200;
                children2[i].LayoutHeightSpecificationFixed = 70;
                children2[i].BackgroundColor = new Color( i * 0.25f, i * 0.25f, 1.0f, 1.0f );
                children2[i].Name = "imageView2";
                contentAreaTwo.Add(children2[i]);
            };


            backgroundContainer.Add( contentBackground );
            contentBackgroundShadow.Add( backgroundContainer );


            Window.Instance.GetDefaultLayer().Add(contentBackgroundShadow);
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
            window.BackgroundColor = Color.White;
            window.GetDefaultLayer().Remove(contentBackgroundShadow);
            helpButton = null;
            contentBackgroundShadow = null;
        }

	    // Shows a thumbnail of the expected output
        private void CreateHelpButton()
        {
            helpButton = new PushButton();
            helpButton.LabelText = "Help";
            helpButton.Clicked += (sender, e) =>
            {
                if ( ! helpShowing )
                {
                    Window window = Window.Instance;
                    helpImageView = LayoutingExample.CreateChildImageView("./res/images/nested-layers-help.png", new Size2D(200, 200));
                    helpImageView.Position2D = new Position2D( 0, helpButton.Size2D.Height );
                    helpShowing = true;
                    window.Add( helpImageView );
                }
                else
                {
                    Window window = Window.Instance;
                    window.Remove(  helpImageView );
                    helpShowing = false;
                }
                return true;
            };
        }

    };
}
