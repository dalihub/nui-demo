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

        private ExampleFeature _featureIndex ;
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

        enum ExampleFeature
        {
            REMOVE_LAYOUT = 0,
            ADD_MARGIN,
            ADD_LAYOUT,
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

            var layout = new LinearLayout();
            _view.Layout = layout;
            _view.LayoutDirection = ViewLayoutDirectionType.LTR;

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

            _nextFeatureButton = new PushButton();
            _nextFeatureButton.ParentOrigin = ParentOrigin.BottomCenter;
            _nextFeatureButton.PivotPoint = PivotPoint.BottomCenter;
            _nextFeatureButton.PositionUsesPivotPoint = true;
            _nextFeatureButton.LabelText = "Remove Layout";
            _nextFeatureButton.Clicked += (sender, e) =>
            {
                NextGridFeature();
                return true;
            };

            window.Add(_nextFeatureButton);

        }

        private void NextGridFeature()
        {
            switch( _featureIndex )
            {
                case ExampleFeature.REMOVE_LAYOUT :
                {
                    RemoveLayout();
                    _nextFeatureButton.LabelText = "Add Margin";
                    _featureIndex = ExampleFeature.ADD_MARGIN;
                    break;
                }
                case ExampleFeature.ADD_MARGIN :
                {
                    AddMargin();
                    _featureIndex = ExampleFeature.ADD_LAYOUT;
                    _nextFeatureButton.LabelText = "Add Layout";
                    break;
                }
                case ExampleFeature.ADD_LAYOUT :
                {
                    AddLayout();
                    _nextFeatureButton.LabelText = "Remove Layout";
                    _featureIndex = ExampleFeature.REMOVE_LAYOUT;
                    break;
                }
                default :
                {
                    _featureIndex = ExampleFeature.REMOVE_LAYOUT;
                    break;
                }
            }
        }

        private void RemoveLayout()
        {
            _view.Layout = new LayoutGroup();
        }

        private void AddLayout()
        {
            _view.Layout = new LinearLayout();
        }

        private void AddMargin()
        {
            _view.Margin = new Extents(4,4,4,4);
        }

        public override void Remove()
        {
            Window window = LayoutingExample.GetWindow();
            window.Remove(this._view);
            _view = null;
        }
    };
}
