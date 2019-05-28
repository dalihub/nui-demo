using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class ChangingSizeExample : Example
    {
        public ChangingSizeExample() : base( "Changing Size" )
        {}

        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] s_images = new string[]
            {
                resources + "/images/application-icon-101.png",
                resources + "/images/application-icon-104.png"
            };
        }

        private View contentBackgroundShadow;
        private List<RadioButton> childButtons = new List<RadioButton>();
        private List<RadioButton> parentButtons = new List<RadioButton>();
        private ImageView[] children;
        private View contentContainer;
        private View buttonBar;

        public LinearLayout createHbox()
        {
            LinearLayout hbox = new LinearLayout();
            hbox.LinearOrientation = LinearLayout.Orientation.Horizontal;
            return hbox;
        }

        public LinearLayout createVbox()
        {
            LinearLayout vbox = new LinearLayout();
            vbox.LinearOrientation = LinearLayout.Orientation.Vertical;
            return vbox;
        }

        public void createParentButtons()
 {
            RadioButton button60 = new RadioButton("60");
            parentButtons.Add(button60);
            button60.Clicked += (sender, e) =>
            {
                contentContainer.WidthSpecification = 60;
                contentContainer.HeightSpecification = 60;
                return true;
            };


            RadioButton parentButton100 = new RadioButton("100");
            parentButtons.Add(parentButton100);
            parentButton100.Clicked += (sender, e) =>
            {
                contentContainer.Size2D = new Size2D(100,100);
                return true;
            };

            RadioButton parentButtonWrap = new RadioButton("Wrap");
            parentButtons.Add(parentButtonWrap);
            parentButtonWrap.Clicked += (sender, e) =>
            {
                contentContainer.WidthSpecification = LayoutParamPolicies.WrapContent;
                contentContainer.HeightSpecification = LayoutParamPolicies.WrapContent;
                return true;
            };
        }

        public void createChildButtons()
        {
            RadioButton button70 = new RadioButton("70");
            childButtons.Add(button70);
            button70.Clicked += (sender, e) =>
            {
                children[0].Size2D = new Size2D(70,70);
                return true;
            };


            RadioButton button90 = new RadioButton("90");
            childButtons.Add(button90);
            button90.Clicked += (sender, e) =>
            {
                children[0].WidthSpecification = 90;
                children[0].HeightSpecification = 90;
                return true;
            };

            RadioButton buttonWrap = new RadioButton("Wrap");
            childButtons.Add(buttonWrap);
            buttonWrap.Clicked += (sender, e) =>
            {
                children[0].WidthSpecification = LayoutParamPolicies.WrapContent;
                children[0].HeightSpecification = LayoutParamPolicies.WrapContent;
                return true;
            };

            RadioButton buttonMatch = new RadioButton("Match");
            childButtons.Add(buttonMatch);
            buttonMatch.Clicked += (sender, e) =>
            {
                children[0].WidthSpecification = LayoutParamPolicies.MatchParent;
                children[0].HeightSpecification = LayoutParamPolicies.MatchParent;
                return true;
            };
        }

        public void createButtonBar()
        {
            buttonBar = new View();
            buttonBar.WidthSpecification = LayoutParamPolicies.WrapContent;
            buttonBar.HeightSpecification = LayoutParamPolicies.WrapContent;
            buttonBar.Layout = createHbox();

            View childButtonsContainer= new View();
            childButtonsContainer.WidthSpecification = LayoutParamPolicies.WrapContent;
            childButtonsContainer.HeightSpecification = LayoutParamPolicies.WrapContent;
            childButtonsContainer.Layout = createVbox();
            createChildButtons();
            TextLabel childLabel = new TextLabel("Child size");
            childButtonsContainer.Add(childLabel);
            foreach (var button in childButtons)
            {
                childButtonsContainer.Add(button);
            }
            buttonBar.Add(childButtonsContainer);

            View parentButtonsContainer= new View();
            parentButtonsContainer.WidthSpecification = LayoutParamPolicies.WrapContent;
            parentButtonsContainer.HeightSpecification = LayoutParamPolicies.WrapContent;
            parentButtonsContainer.Layout = createVbox();
            TextLabel parentLabel = new TextLabel("Parent size");
            parentButtonsContainer.Add(parentLabel);
            buttonBar.Add(parentButtonsContainer);
            createParentButtons();

            foreach (var button in parentButtons)
            {
                parentButtonsContainer.Add(button);
            }

            buttonBar.PositionUsesPivotPoint = true;
            buttonBar.PivotPoint = PivotPoint.BottomLeft;
            buttonBar.ParentOrigin = ParentOrigin.BottomLeft;
        }

        public override void Create()
        {
            Window window = LayoutingExample.GetWindow();
            contentBackgroundShadow = new View()
            {
                Name = "contentBackgroundShadow",
                Size2D = new Size2D(window.Size.Width,400),
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
                Size2D = new Size2D((window.Size.Width -(shadowOffset*2)) , (400 -(shadowOffset*2))),
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
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            contentContainer = new View()
            {
                Name = "contentContainer",
                Layout = createHbox(),
                Size2D = new Size2D(80,80),
                BackgroundColor = Color.Blue
            };
            contentBackground.Add(contentContainer);

            const int NUMBER_OF_IMAGEVIEWS = 1;
            children = new ImageView[NUMBER_OF_IMAGEVIEWS];

            for ( int i = 0; i < NUMBER_OF_IMAGEVIEWS; i++ )
            {
                children[i] = new ImageView("./res/images/gallery-small-23.jpg");
                children[i].Size2D = new Size2D(60,60);
                children[i].Name = "imageView1stSet_" + i;
                contentContainer.Add(children[i]);
            };

            backgroundContainer.Add( contentBackground );
            contentBackgroundShadow.Add( backgroundContainer );

            window.Add(contentBackgroundShadow);

            // Create button toolbar to control size of Views.
            createButtonBar();
            window.Add(buttonBar);
        }

        public override void Remove()
        {
            Window window = LayoutingExample.GetWindow();
            childButtons.Clear();
            parentButtons.Clear();
            window.Remove(contentBackgroundShadow);
            window.Remove(buttonBar);
            buttonBar = null;
            contentBackgroundShadow = null;
        }

    };
}
