using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class NestedLayoutTestExample : Example
    {
        public NestedLayoutTestExample() : base( "Test Nested Layouts (Complex)" )
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

        // States to match features
        enum ExampleFeature
        {
            SET_PARENT_VERTICAL_LAYOUT = 0,
            SET_PARENT_HORIZONTAL_LAYOUT = 1,
            ADD_CHILD_VIEW_WITH_NO_LAYOUT = 2,
            ADD_LINEAR_LAYOUT_TO_LAST_ADDED_CHILD = 3,
        };

        View _parentContainer;
        View _childView;
        View _imageViewContainer2;
        private PushButton nextFeatureButton;
        ExampleFeature featureIndex = ExampleFeature.SET_PARENT_HORIZONTAL_LAYOUT;
        private ImageView helpImageView;
        PushButton helpButton;
        bool helpShowing = false;
        private List<PushButton> buttons = new List<PushButton>();
        uint imageViewTally = 0;

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

        public View CreateImageView(string namePrefix)
        {
            ImageView imageView = new ImageView("./res/images/gallery-small-23.jpg");
            imageView.PivotPoint = PivotPoint.Center;
            imageView.LayoutWidthSpecificationFixed = 70;
            imageView.LayoutHeightSpecificationFixed = 70;
            imageView.Name = "imageView_" + namePrefix + imageViewTally++;
            return imageView;
        }

        public override void Create()
        {
            /*
             *                  parentContainer
             *                   |            |
             *         pureViewRed            pureViewBlue
             *              |                         |
             *       imageViewContainer         imageViewContainer2
             *       |       |       |            |              |
             *  ImageView ImageView ImageView  ImageView      ImageView
             *
             *
             *  parentContainer assigned a Layout after tree already built.
             */

            _parentContainer = new View()
            {
                Name = "linearViewGreen",
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                BackgroundColor = Color.Green,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            View pureViewRed = new View()
            {
                Name = "pureViewRed",
                BackgroundColor = Color.Red,
            };

            View pureViewBlue = new View()
            {
                Name = "pureViewBlue",
                BackgroundColor = Color.Blue,
            };

            View imageViewContainer = new View()
            {
                Name = "linearViewYellow",
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                BackgroundColor = Color.Yellow,
                Layout = createVbox(),
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            _imageViewContainer2 = new View()
            {
                Name = "linearViewGreen",
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                BackgroundColor = Color.Green,
                Layout = createVbox(),
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            for( int i = 0; i < 3; i++ )
            {
                imageViewContainer.Add(CreateImageView("1stSet"));
            };

            for( int i = 0; i < 2; i++ )
            {
                _imageViewContainer2.Add(CreateImageView("2ndSet"));
            };

            pureViewRed.Add(imageViewContainer);
            pureViewBlue.Add(_imageViewContainer2);

            LayoutingExample.GetWindow().Add(_parentContainer);
            _parentContainer.Add(pureViewRed);
            _parentContainer.Add(pureViewBlue);

            CreateHelpButton();
            CreateNextFeatureButton();
            LayoutingExample.GetWindow().Add(nextFeatureButton);
        }

        void CreateNextFeatureButton()
        {
            nextFeatureButton = new PushButton();
            nextFeatureButton.ParentOrigin = ParentOrigin.BottomCenter;
            nextFeatureButton.PivotPoint = PivotPoint.BottomCenter;
            nextFeatureButton.PositionUsesPivotPoint = true;
            nextFeatureButton.LabelText = "Set Horizontal Layout";
            nextFeatureButton.Clicked += (sender, e) =>
            {
                NextFeature();
                return true;
            };
        }

        // Execute different features to test
        public void NextFeature()
        {
            switch( featureIndex )
            {
                // Parent container assigned a layout after tree constructed.
                // Children should now be laid out horizontally .
                case ExampleFeature.SET_PARENT_HORIZONTAL_LAYOUT :
                {
                    _parentContainer.Layout = createHbox();
                    nextFeatureButton.LabelText = "Add child with no layout";
                    featureIndex = ExampleFeature.ADD_CHILD_VIEW_WITH_NO_LAYOUT;
                    break;
                }
                // Add a View without a layout but with 2 children.
                // 2 children will be overlapping.
                case ExampleFeature.ADD_CHILD_VIEW_WITH_NO_LAYOUT :
                {
                    _childView = new View();
                    for( var i=0; i<2; i++)
                    {
                        _childView.Add(CreateImageView("3rdSet"));
                    }
                    _imageViewContainer2.Add(_childView);
                    nextFeatureButton.LabelText = "Add linear layout to last added child";
                    featureIndex = ExampleFeature.ADD_LINEAR_LAYOUT_TO_LAST_ADDED_CHILD;
                    break;
                }
                // A horizontal layout added to the View that had no layout.
                // It's children should now be laid out horizontally too.
                case ExampleFeature.ADD_LINEAR_LAYOUT_TO_LAST_ADDED_CHILD :
                {
                    LayoutingExample.GetToolbar().Add( helpButton );
                    _childView.Layout = createHbox();
                    LayoutingExample.GetWindow().Remove(nextFeatureButton);
                    break;
                }
                default :
                {
                    break;
                }
            }
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
            window.Remove(_parentContainer);
            window.Remove(nextFeatureButton);
            nextFeatureButton = null;
            helpButton = null;
            _parentContainer = null;
            _imageViewContainer2 = null;
            _childView = null;
            featureIndex = ExampleFeature.SET_PARENT_VERTICAL_LAYOUT;
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
                    helpImageView = LayoutingExample.CreateChildImageView("./res/images/nestedLayoutTestHelp3.png", new Size2D(200, 200));
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
