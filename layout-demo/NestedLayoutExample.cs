using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class NestedLayoutExample : Example
    {
        public NestedLayoutExample() : base( "Nested Layouts" )
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
            Window window = LayoutingExample.GetWindow();
            contentBackgroundShadow = new View()
            {
                Name = "contentBackgroundShadow",
                Size2D = new Size2D(window.Size.Width,500),
                BackgroundColor = new Color(75f,0f,130f, 0.8f),
                Position2D = new Position2D(0, 40),
            };

            // Create gradient visual that can be set as a background
            GradientVisual gradientVisualMap1 = new GradientVisual();
            PropertyArray stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(new Vector4(0.35f, 0.0f, 0.65f, 0.9f)));
            stopColor.Add(new PropertyValue(new Vector4(1.0f, 0.99f, 0.89f, 0.9f)));
            gradientVisualMap1.StopColor = stopColor;
            gradientVisualMap1.StartPosition = new Vector2(0.0f, -0.5f);
            gradientVisualMap1.EndPosition = new Vector2(-0.5f, 0.5f);
            gradientVisualMap1.PositionPolicy = VisualTransformPolicyType.Relative;
            gradientVisualMap1.SizePolicy = VisualTransformPolicyType.Relative;

            int shadowOffset = 4;
            View backgroundContainer = new View()
            {
                Name = "backgroundContainer",
                PositionUsesPivotPoint = true,
                Size2D = new Size2D((window.Size.Width -(shadowOffset*2)) , (500 -(shadowOffset*2))),
                Position2D = new Position2D(shadowOffset,shadowOffset),
                PivotPoint = PivotPoint.TopLeft,
                ParentOrigin = ParentOrigin.TopLeft,
                Background = gradientVisualMap1.OutputVisualMap,
            };

            View contentBackground = new View()
            {
                Name = "contentBackground",
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                BackgroundColor = Color.Yellow,
                Layout = createVbox(),
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
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
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };
            contentBackground.Add(contentContainer);

            View contentAreaOne = new View()
            {
                Name = "contentAreaOne",
                Layout = createVbox(),
                Margin = new Extents(10,10,10,10),
                Padding = new Extents(15,10,15,10),
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                BackgroundColor = Color.Red,
            };

            contentContainer.Add(contentAreaOne);

            View contentAreaTwo = new View()
            {
                Name = "contentAreaTwo",
                Layout = createVbox(),
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                BackgroundColor = Color.Cyan,
            };
            contentContainer.Add(contentAreaTwo);

            ImageView[] children = new ImageView[3];
            for( int i = 0; i < 3; i++ )
            {
                children[i] = new ImageView("./res/images/gallery-small-23.jpg");
                children[i].PivotPoint = PivotPoint.Center;
                children[i].WidthSpecification = 70;
                children[i].HeightSpecification = 70;
                children[i].BackgroundColor = new Color( i * 0.25f, i * 0.25f, 1.0f, 1.0f );
                children[i].Name = "imageView1stSet_" + i;
                contentAreaOne.Add(children[i]);
            };

            ImageView[] children2 = new ImageView[5];
            for( int i = 0; i < 5; i++ )
            {
                children2[i] = new ImageView("./res/images/application-icon-102.png");
                children2[i].PivotPoint = PivotPoint.Center;
                children2[i].WidthSpecification = 200;
                children2[i].HeightSpecification = 70;
                children2[i].BackgroundColor = new Color( i * 0.25f, i * 0.25f, 1.0f, 1.0f );
                children2[i].Name = "imageView2ndSet_" + i;
                contentAreaTwo.Add(children2[i]);
            };

            backgroundContainer.Add( contentBackground );
            contentBackgroundShadow.Add( backgroundContainer );

            window.Add(contentBackgroundShadow);
            CreateHelpButton();
            LayoutingExample.GetToolbar().Add( helpButton );
        }

        public override void Remove()
        {
            Window window = LayoutingExample.GetWindow();
            if(helpImageView)
            {
                window.Remove(helpImageView);
                helpImageView = null;
            }
            helpShowing = false;
            LayoutingExample.GetToolbar().Remove(helpButton);
            window.Remove(contentBackgroundShadow);
            helpButton = null;
            contentBackgroundShadow = null;
        }

        // Shows a thumbnail of the expected output
        private void CreateHelpButton()
        {
            helpButton = new PushButton();
            helpButton.LabelText = "Help";
            helpButton.Name = "help-button";
            helpButton.Clicked += (sender, e) =>
            {
                Window window = LayoutingExample.GetWindow();
                if ( ! helpShowing )
                {
                    helpImageView = LayoutingExample.CreateChildImageView("./res/images/nested-layers-help.png", new Size2D(200, 200));
                    helpImageView.Position2D = new Position2D( 0, helpButton.Size2D.Height );
                    helpShowing = true;
                    window.Add( helpImageView );
                }
                else
                {
                    window.Remove(  helpImageView );
                    helpShowing = false;
                }
                return true;
            };
        }

    };
}
