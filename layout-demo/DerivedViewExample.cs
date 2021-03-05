using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace LayoutDemo
{
    class DerivedViewExample : Example
    {
        public DerivedViewExample() : base( "Derived View assigned layout" )
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

        private View _derivedView;
        private ImageView helpImageView;
        Button helpButton;
        bool helpShowing = false;
        private Button nextFeatureButton;

        // Class derived from a View
        class DerivedView : View
        {
            public DerivedView() : base(){}
        }

        public override void Create()
        {
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

            _derivedView = new DerivedView()
            {
                Size2D = new Size2D(window.Size.Width,500),
                BackgroundColor = Color.Green,
                ParentOrigin = ParentOrigin.TopLeft,
                PivotPoint = PivotPoint.TopLeft,
                PositionY = 200,
                PositionUsesPivotPoint = true,
            };
            window.Add(_derivedView);

            TextLabel textLabel = new TextLabel()
            {
                Size2D = new Size2D(200, 70),
                BackgroundColor = Color.Red,
                Name = "TextLabel",
                Text = "Left TextLabel",
            };
            _derivedView.Add(textLabel);

            TextLabel textLabel2 = new TextLabel()
            {
                Size2D = new Size2D(200, 70),
                BackgroundColor = Color.Yellow,
                Name = "TextLabel",
                Text = "Right TextLabel",
            };
            _derivedView.Add(textLabel2);

            CreateHelpButton();
            CreateNextFeatureButton();

            window.Add(nextFeatureButton);
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
            window.Remove(nextFeatureButton);
            window.Remove(_derivedView);
            helpButton = null;
            _derivedView = null;
        }

	      // Shows a thumbnail of the expected output
        private void CreateHelpButton()
        {
            helpButton = new Button();
            helpButton.Text = "Help";
            helpButton.Clicked += (sender, e) =>
            {
                if ( ! helpShowing )
                {
                    Window window = Window.Instance;
                    helpImageView = LayoutingExample.CreateChildImageView("./res/images/derivedViewExampleHelp.png", new Size2D(200, 200));
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
            };
        }
        void CreateNextFeatureButton()
        {
            nextFeatureButton = new Button();
            nextFeatureButton.ParentOrigin = ParentOrigin.BottomCenter;
            nextFeatureButton.PivotPoint = PivotPoint.BottomCenter;
            nextFeatureButton.PositionUsesPivotPoint = true;
            nextFeatureButton.Text = "Add Horizontal Layout";
            nextFeatureButton.Clicked += (sender, e) =>
            {
                NextFeature();
            };
        }


        public void NextFeature()
        {
            if(_derivedView.Layout == null)
            {
                _derivedView.Layout = new LinearLayout();
                _derivedView.HeightSpecification = LayoutParamPolicies.WrapContent;
            }
            Window.Instance.Remove(nextFeatureButton);
            LayoutingExample.GetToolbar().Add(helpButton);
        }
    };
}
