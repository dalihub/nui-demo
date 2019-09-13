using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class PanningExample : Example
    {
        public PanningExample() : base( "Panning Transition" )
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
        private const string panningButtonImage = "./res/images/left-right-arrows.png";

        private View view;

        private View panControl;

        private Position2D panControlOrigin;

        private PanGestureDetector panGestureDetector;
        private float PanGestureDisplacementX;
        private float RatioToScreenWidthToCompleteScroll = 0.6f;

        private int ITEM_MOVE_DURATION = 100;

        private float windowWidth;
        public override void Create()
        {
            windowWidth = LayoutingExample.GetWindow().WindowSize.Width;


            view = new View();
            view.Name = "MainLinearLayout";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.WidthSpecification = LayoutParamPolicies.MatchParent;
            view.HeightSpecification = LayoutParamPolicies.MatchParent;

            var layout = new LinearLayout();
            layout.LinearAlignment = LinearLayout.Alignment.Center;
            view.Layout = layout;
            view.LayoutDirection = ViewLayoutDirectionType.LTR;

            // Add child image-views to the created view
            var index = 0;
            foreach (String image in TestImages.s_images)
            {
                // Set a delayed custom transition for each Image View so each moves into place after it's
                // adjacent sibbling.
                TransitionComponents easeInOutSineDelayed = new TransitionComponents();
                easeInOutSineDelayed.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine);
                easeInOutSineDelayed.Delay = ITEM_MOVE_DURATION * index;
                easeInOutSineDelayed.Duration = ITEM_MOVE_DURATION * TestImages.s_images.Length;

                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(80, 80));

                // Override LayoutChanged transition so different for each ImageView in the Linear layout.
                // In this case each moves with a increasing delay.
                imageView.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                                   AnimatableProperties.Position,
                                                                   0.0,
                                                                   easeInOutSineDelayed );

                imageView.LayoutTransition = new LayoutTransition( TransitionCondition.ChangeOnRemove,
                                                                   AnimatableProperties.Position,
                                                                   0.0,
                                                                   easeInOutSineDelayed );

                view.Add(imageView);
                index++;
            }

            LayoutingExample.GetWindow().Add(view);

            InitializePanControl();
            LayoutingExample.GetWindow().Add(panControl);

            panGestureDetector = new PanGestureDetector();
            panGestureDetector.Attach(panControl);
            panGestureDetector.Detected += OnPanGestureDetected;


        }

        private void OnPanGestureDetected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            Animation animationController = Window.Instance.LayoutController.GetCoreAnimation();
            Window.Instance.LayoutController.OverrideCoreAnimation = true;

            switch(e.PanGesture.State)
            {
                case Gesture.StateType.Finished :
                {
                    if( animationController.CurrentProgress > RatioToScreenWidthToCompleteScroll)
                    {
                        // Panned enough to allow auto completion of animation.
                        animationController.SpeedFactor = 1;
                        animationController.EndAction = Animation.EndActions.StopFinal;
                        animationController.Play();
                    }
                    else
                    {
                        LinearLayout layout = view.Layout as LinearLayout;
                        if (layout.LinearOrientation == LinearLayout.Orientation.Horizontal)
                        {
                            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
                        }
                        else
                        {
                            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                        }
                        animationController.SpeedFactor = 1;
                        animationController.EndAction = Animation.EndActions.StopFinal;
                        animationController.Play();
                    }

                    animationController.AnimateTo(panControl, "PositionX",
                                                  panControlOrigin.X,
                                                  0,
                                                  16,
                                                  new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));

                    Window.Instance.LayoutController.OverrideCoreAnimation = false;
                }
                break;
                case Gesture.StateType.Continuing :
                {
                    PanGestureDisplacementX += e.PanGesture.ScreenDisplacement.X;
                    float progress = PanGestureDisplacementX/windowWidth;
                    animationController.CurrentProgress = progress;
                    panControl.PositionX = panControlOrigin.X+PanGestureDisplacementX;
                }
                break;
                case Gesture.StateType.Started :
                {
                    LinearLayout layout = view.Layout as LinearLayout;
                    if (layout.LinearOrientation == LinearLayout.Orientation.Horizontal)
                    {
                        layout.LinearOrientation = LinearLayout.Orientation.Vertical;
                    }
                    else
                    {
                        layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                    }

                    //animationController.EndAction = Animation.EndActions.Discard;
                    animationController.Play();
                    animationController.Pause();

                    PanGestureDisplacementX = 0;
                }
                break;
            }

        }

        private void InitializePanControl()
        {
            PropertyMap imageVisual = new PropertyMap();
            imageVisual.Add( Visual.Property.Type, new PropertyValue( (int)Visual.Type.Image ));
            imageVisual.Add(ImageVisualProperty.URL,  new PropertyValue( panningButtonImage ));

            panControl = new View()
            {
                Size2D = new Size2D(100,100),
                Background = imageVisual,
            };

            Size2D windowSize = LayoutingExample.GetWindow().WindowSize;
            panControlOrigin = new Position2D(10, (int)(windowSize.Height*0.75f));
            panControl.Position2D = panControlOrigin;
            LayoutingExample.GetWindow().Add(panControl);

        }
        public override void Remove()
        {
            LayoutingExample.GetWindow().Remove(view);

            view = null;
        }
    }
}
