using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class LinearExample : Example
    {
        public LinearExample() : base( "Linear Layout" )
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

        private View view;
        private List<PushButton> buttons = new List<PushButton>();

        public override void Create()
        {
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

            // Set custom transition for changes to this Linear Layout
            TransitionComponents slowEaseInOutSine = new TransitionComponents();
            slowEaseInOutSine.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine);
            slowEaseInOutSine.Duration = 1200;
            slowEaseInOutSine.Delay = 0;

            LayoutTransition customPositionTransition = new LayoutTransition(
                                                              TransitionCondition.LayoutChanged,
                                                              AnimatableProperties.Position,
                                                              0.0,
                                                              slowEaseInOutSine );
            view.LayoutTransition = customPositionTransition;


            // Set custom transition for changes to this Linear Layout
            TransitionComponents fadeOut = new TransitionComponents();
            fadeOut.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            fadeOut.Duration = 1200;
            fadeOut.Delay = 0;
            float targetOpacityOut = .2f;
            LayoutTransition customOpacityTransitionOut = new LayoutTransition(
                                                                TransitionCondition.LayoutChanged,
                                                                AnimatableProperties.Opacity,
                                                                targetOpacityOut,
                                                                fadeOut,
                                                                false );
            view.LayoutTransition = customOpacityTransitionOut;


            // Set custom transition for changes to this Linear Layout
            TransitionComponents fadeIn = new TransitionComponents();
            fadeIn.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            fadeIn.Duration = 1200;
            fadeIn.Delay = 600;
            float targetOpacityIn = 1.0f;
            LayoutTransition customOpacityTransitionIn = new LayoutTransition(
                                                                TransitionCondition.LayoutChanged,
                                                                AnimatableProperties.Opacity,
                                                                targetOpacityIn,
                                                                fadeIn,
                                                                false );
            view.LayoutTransition = customOpacityTransitionIn;


            var index = 0;
            // Add child image-views to the created view
            foreach (String image in TestImages.s_images)
            {
                // Set a delayed custom transition for each Image View so each moves into place after it's
                // adjacent sibbling.
                TransitionComponents easeInOutSineDelayed = new TransitionComponents();
                easeInOutSineDelayed.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine);
                easeInOutSineDelayed.Delay = 300 * index;
                easeInOutSineDelayed.Duration = 300 * TestImages.s_images.Length;

                LayoutTransition customPositionTransitionDelayed = new LayoutTransition(
                                                              TransitionCondition.LayoutChanged,
                                                              AnimatableProperties.Position,
                                                              0.0,
                                                              easeInOutSineDelayed );

                ImageView imageView = LayoutingExample.CreateChildImageView(image, new Size2D(80, 80));
                imageView.LayoutTransition = customPositionTransitionDelayed;
                imageView.TouchEvent += (sender, e) =>
                {
                    if (sender is ImageView && e.Touch.GetState(0) == PointStateType.Down)
                    {
                        ImageView touchedImageView = (ImageView)sender;
                        if (touchedImageView.Weight == 1.0f)
                        {
                            touchedImageView.Weight = 0.0f;
                        }
                        else
                        {
                            touchedImageView.Weight = 1.0f;
                        }
                    }
                    return true;
                };

                view.Add(imageView);
                index++;
            }

            LayoutingExample.GetWindow().Add(view);

            PushButton directionButton = new PushButton();
            LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-reverse.png");
            LayoutingExample.SetSelectedIcon(directionButton, "./res/images/icon-reverse-selected.png");
            directionButton.Name = "directionButton";
            directionButton.ParentOrigin = new Vector3(0.33f, 1.0f, 0.5f);
            directionButton.PivotPoint = PivotPoint.BottomCenter;
            directionButton.PositionUsesPivotPoint = true;
            directionButton.MinimumSize = new Vector2(75, 75);
            directionButton.Clicked += (sender, e) =>
            {
                if (this.view.LayoutDirection == ViewLayoutDirectionType.LTR)
                {
                    this.view.LayoutDirection = ViewLayoutDirectionType.RTL;
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-play.png");
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-play-selected.png");
                }
                else
                {
                    this.view.LayoutDirection = ViewLayoutDirectionType.LTR;
                    LayoutingExample.SetUnselectedIcon(directionButton, "./res/images/icon-reverse.png");
                    LayoutingExample.SetSelectedIcon(directionButton, "./res/images/icon-reverse-selected.png");
                }
                return true;
            };
            LayoutingExample.GetWindow().Add(directionButton);
            buttons.Add(directionButton);

            PushButton rotateButton = new PushButton();
            LayoutingExample.SetUnselectedIcon(rotateButton, "./res/images/icon-reset.png");
            LayoutingExample.SetSelectedIcon(rotateButton, "./res/images/icon-reset-selected.png");
            rotateButton.Name = "rotateButton";
            rotateButton.ParentOrigin = new Vector3(0.66f, 1.0f, 0.5f);
            rotateButton.PivotPoint = PivotPoint.BottomCenter;
            rotateButton.PositionUsesPivotPoint = true;
            rotateButton.MinimumSize = new Vector2(75, 75);
            rotateButton.Clicked += (sender, e) =>
            {
                LinearLayout linearLayout = (LinearLayout)this.view.Layout;
                if (linearLayout.LinearOrientation == LinearLayout.Orientation.Horizontal)
                {
                    linearLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
                }
                else
                {
                    linearLayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                }
                return true;
            };

            LayoutingExample.GetWindow().Add(rotateButton);
            buttons.Add(rotateButton);
        }

        public override void Remove()
        {
            LayoutingExample.GetWindow().Remove(view);

            view = null;
            foreach (PushButton button in buttons)
            {
                LayoutingExample.GetWindow().Remove(button);
            }
            buttons.Clear();
        }
    };
}
