using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    // Example overriding the Layouting animation for changing layouts so
    // can control the progress of the animation using a Pan gesture.
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

        private const string instructions = "Stretch to slowly transition to next layout or return to original.";

        private View view;

        private View panControl;

        private TextLabel instructionsLabel;

        private int panControlOriginalWidth = 100;

        private PanGestureDetector panGestureDetector;
        private float PanGestureDisplacementX;
        private float RatioToScreenWidthToCompleteScroll = 0.6f;

        private int ITEM_MOVE_DURATION = 100;

        private float windowWidth;

        // Prevent input to the pan control whilst it's animating back.
        bool panControlAnimating = false;
        bool panningStarted = false;
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

            instructionsLabel = new TextLabel(instructions)
            {
                Position2D = new Position2D(10, (int)(LayoutingExample.GetWindow().WindowSize.Height*0.90f)),
                MultiLine = true,
            };
            LayoutingExample.GetWindow().Add(instructionsLabel);
        }

        private void PanControlAnimationFinished(object sender, EventArgs e)
        {
            panControlAnimating = false;  // Allow a new pan gesture to start.
        }

        private void OnPanGestureDetected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            if (panControlAnimating)
            {
              return;  // Ignore pan gestures whilst PanControl is animating back.
            }

            Animation animationController = Window.Instance.LayoutController.GetCoreAnimation();

            // Prevent Layouting system animating the layout change automatically.
            Window.Instance.LayoutController.OverrideCoreAnimation = true;

            Animation panControlAnimation = new Animation();
            panControlAnimation.Finished += PanControlAnimationFinished;

            switch(e.PanGesture.State)
            {
                case Gesture.StateType.Finished :
                {
                    if (true != panningStarted)
                    {
                        // Don't run finish operations if never started.
                        // (If user tried to pan whilst the pan control was animating back)
                        return;
                    }

                    if( animationController.CurrentProgress > RatioToScreenWidthToCompleteScroll)
                    {
                        // Panned enough to allow auto completion of animation.
                        animationController.SpeedFactor = 1;
                        animationController.EndAction = Animation.EndActions.StopFinal;
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
                    }

                    panControlAnimation.AnimateTo(panControl, "SizeWidth",
                                                  panControlOriginalWidth,
                                                  0,
                                                  512,
                                                  new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutBack));

                    panControlAnimating = true;
                    panControlAnimation.Play();
                    animationController.Play();

                    // Return control of the layout animations to the Layouting system.
                    Window.Instance.LayoutController.OverrideCoreAnimation = false;
                    panningStarted = false;
                }
                break;
                case Gesture.StateType.Continuing :
                {
                    if (true == panningStarted)
                    {
                      PanGestureDisplacementX += e.PanGesture.ScreenDisplacement.X;
                      float progress = PanGestureDisplacementX/windowWidth * 1.2f;
                      animationController.CurrentProgress = progress;
                      panControl.Size2D.Width = panControlOriginalWidth+(int)PanGestureDisplacementX;
                    }
                }
                break;
                case Gesture.StateType.Started :
                {
                    panningStarted = true;
                    LinearLayout layout = view.Layout as LinearLayout;
                    if (layout.LinearOrientation == LinearLayout.Orientation.Horizontal)
                    {
                        layout.LinearOrientation = LinearLayout.Orientation.Vertical;
                    }
                    else
                    {
                        layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                    }

                    // Start Layouting animations and pause instantly, this also the use of CurrentProgress
                    // to control the animation with the gesture displacement.
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
                Size2D = new Size2D(panControlOriginalWidth,100),
                Background = imageVisual,
            };

            Size2D windowSize = LayoutingExample.GetWindow().WindowSize;
            panControl.Position2D = new Position2D(10, (int)(windowSize.Height*0.75f));
            LayoutingExample.GetWindow().Add(panControl);

        }
        public override void Remove()
        {
            LayoutingExample.GetWindow().Remove(view);
            LayoutingExample.GetWindow().Remove(panControl);
            LayoutingExample.GetWindow().Remove(instructionsLabel);
            view = null;
            view = panControl;
            view = instructionsLabel;
        }
    }
}
