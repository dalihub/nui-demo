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

        private View popupShadow;
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

            popupShadow = new View()
            {
                Name = "popupShadow",
                Size2D = new Size2D(476,500),
                BackgroundColor = Color.Red,
                Position2D = new Position2D(0, 40),
            };

	    View view1 = new View()
	    {
		PositionUsesPivotPoint = true,
		PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
		BackgroundColor = Color.Black,
	    };

            View popupBG = new View()
            {
                Name = "popupBG",
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                BackgroundColor = Color.Yellow,
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
            popupBG.Add(textLabel);


            View popupBody = new View()
            {
                 Name = "popupBody",
                 LayoutWidthSpecification = ChildLayoutData.WrapContent,
                 LayoutHeightSpecification = ChildLayoutData.WrapContent
            };
            popupBG.Add(popupBody);

            View contentAreaOne = new View()
            {
                Name = "contentAreaOne",
                Margin = new Extents(10,10,10,10),
                Layout = createVbox(),
                LayoutWidthSpecification = ChildLayoutData.WrapContent,
                LayoutHeightSpecification = ChildLayoutData.WrapContent,
                BackgroundColor = Color.Red,
            };
            popupBody.Add(contentAreaOne);

            View contentAreaTwo = new View()
            {
                Name = "contentAreaTwo",
                Margin = new Extents(10,10,10,10),
                Layout = createVbox(),
                LayoutWidthSpecification = ChildLayoutData.WrapContent,
                LayoutHeightSpecification = ChildLayoutData.WrapContent,
                BackgroundColor = Color.Cyan,
            };
            popupBody.Add(contentAreaTwo);

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
                contentAreaTwo.Add(children2[i]);
            };

            popupBody.Layout = createHbox();
            popupBG.Layout = createVbox();

	    view1.Add(popupBG);
            popupShadow.Add( view1 );
            Window.Instance.GetDefaultLayer().Add(popupShadow);
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
            window.GetDefaultLayer().Remove(popupShadow);
            helpButton = null;
            popupShadow = null;
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
