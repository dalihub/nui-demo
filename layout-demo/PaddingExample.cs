using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class PaddingExample : Example
    {
        public PaddingExample() : base( "Padding Example" )
        {}

        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] s_images = new string[]
            {
                resources + "/images/gallery-small-23.jpg",
                resources + "/images/gallery-small-23.jpg",
                resources + "/images/gallery-small-23.jpg",
                resources + "/images/gallery-small-23.jpg",
            };
        }
        private View _view;
        private PushButton _nextFeatureButton;

        private int _featureIndex ;
        GradientVisual CreateGradientVisual()
        {
            // Create gradient visual that can be set as a background
            GradientVisual gradientVisualMap = new GradientVisual();
            PropertyArray stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(new Vector4(0.35f, 0.0f, 0.65f, 0.9f)));
            stopColor.Add(new PropertyValue(new Vector4(1.0f, 0.99f, 0.89f, 0.9f)));
            gradientVisualMap.StopColor = stopColor;
            gradientVisualMap.StartPosition = new Vector2(0.0f, -0.5f);
            gradientVisualMap.EndPosition = new Vector2(-0.5f, 0.5f);
            gradientVisualMap.PositionPolicy = VisualTransformPolicyType.Relative;
            gradientVisualMap.SizePolicy = VisualTransformPolicyType.Relative;
            return gradientVisualMap;
        }

        enum ExampleFeatures
        {
            REMOVE_LAYOUT = 0,
            ADD_MARGIN,
            ADD_LAYOUT,
            REMOVE_MARGIN,
        };

        struct Feature
        {
            public ExampleFeatures feature;
            public String featureName;
        }

        static Feature[] featureArray =  new Feature[]
        {
            new Feature() { feature = ExampleFeatures.REMOVE_LAYOUT, featureName = "remove layout" },
            new Feature() { feature = ExampleFeatures.ADD_MARGIN, featureName = "add margin" },
            new Feature() { feature = ExampleFeatures.ADD_LAYOUT, featureName = "add layout" },
            new Feature() { feature = ExampleFeatures.REMOVE_LAYOUT, featureName = "remove layout" },
            new Feature() { feature = ExampleFeatures.ADD_LAYOUT, featureName = "add layout" },
            new Feature() { feature = ExampleFeatures.REMOVE_MARGIN, featureName = "remove margin" },
        };

        public override void Create()
        {
            _view = new View();
            _view.Name = "PaddingExample";
            _view.Background = CreateGradientVisual().OutputVisualMap;
            _view.ParentOrigin = ParentOrigin.Center;
            _view.PivotPoint = PivotPoint.Center;
            _view.PositionUsesPivotPoint = true;
            _view.WidthSpecification = LayoutParamPolicies.WrapContent;
            _view.HeightSpecification = LayoutParamPolicies.WrapContent;

            _view.Layout = new LinearLayout();

            // Add child image-views to the created _view
            foreach (String image in TestImages.s_images)
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(100, 100));
                imageView.BackgroundColor = Color.Black;
                imageView.TouchEvent += (sender, e) =>
                {
                    if (sender is ImageView && e.Touch.GetState(0) == PointStateType.Down)
                    {
                        Console.WriteLine("ImageViewTouched\n");
                        ImageView touchedImageView = (ImageView)sender;
                        if (touchedImageView.Padding.EqualTo(new Extents(0, 0, 0, 0)))
                        {
                            Console.WriteLine("Adding Padding\n");
                            touchedImageView.Padding = new Extents(10, 10, 10, 10);
                        }
                        else
                        {
                            Console.WriteLine("Padding removed\n");
                            touchedImageView.Padding = new Extents(0, 0, 0, 0);
                        }
                    }
                    return true;
                };

                _view.Add(imageView);
            }

            Window window = LayoutingExample.GetWindow();
            window.Add(_view);

            _featureIndex = 0;
            _nextFeatureButton = new PushButton();
            _nextFeatureButton.ParentOrigin = ParentOrigin.BottomCenter;
            _nextFeatureButton.PivotPoint = PivotPoint.BottomCenter;
            _nextFeatureButton.PositionUsesPivotPoint = true;
            _nextFeatureButton.LabelText = featureArray[_featureIndex].featureName;
            _nextFeatureButton.Clicked += (sender, e) =>
            {
                ExampleFeature();
                return true;
            };

            window.Add(_nextFeatureButton);

        }

        private void ExampleFeature()
        {
            Feature currentFeature = featureArray[_featureIndex];

            Console.WriteLine("Performing feature:{0} at index:{1}\n", currentFeature.featureName, _featureIndex);

            switch( currentFeature.feature )
            {
                case ExampleFeatures.REMOVE_LAYOUT :
                {
                    RemoveLayout();
                    break;
                }
                case ExampleFeatures.ADD_MARGIN :
                {
                    AddMargin(4);
                    break;
                }
                case ExampleFeatures.ADD_LAYOUT :
                {
                    AddLayout();
                    break;
                }
                case ExampleFeatures.REMOVE_MARGIN :
                {
                    AddMargin(0);
                    break;
                }
            }

            // Show next button text
            _featureIndex = _featureIndex+1;
            if (_featureIndex >= featureArray.Length )
            {
                _featureIndex = 0;
            }

            _nextFeatureButton.LabelText = featureArray[_featureIndex].featureName;
        }

        private void RemoveLayout()
        {
            _view.Layout = new AbsoluteLayout();
            int position = -10;
            foreach (View child in _view.Children)
            {
               child.Position2D = new Position2D(position+=10, position);
            }
        }

        private void AddLayout()
        {
            _view.Layout = new LinearLayout();
        }

        private void AddMargin(ushort margin)
        {
            foreach (View child in _view.Children)
            {
                child.Margin = new Extents(margin,margin,margin,margin);
            }
        }

        public override void Remove()
        {
            Window window = LayoutingExample.GetWindow();
            window.Remove(this._view);
            window.Remove(_nextFeatureButton);
            _view = null;
            _nextFeatureButton = null;
            Array.Clear(featureArray, 0, featureArray.Length);
        }
    };
}
