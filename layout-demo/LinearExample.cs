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

        private bool addedItem = false;

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

            ///////////////////////////////////////////////////////////////////////////////////////
            // Custom transitions for the View (LinearLayout) when changing layout
            //
            //
            ///////////////////////////////////////////////////////////////////////////////////////
            TransitionComponents slowEaseInOutSine = new TransitionComponents();
            slowEaseInOutSine.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine);
            slowEaseInOutSine.Duration = 800;
            slowEaseInOutSine.Delay = 0;

            view.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                          AnimatableProperties.Position,
                                                          0.0,
                                                          slowEaseInOutSine );

            TransitionComponents fadeIn = new TransitionComponents();
            fadeIn.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            fadeIn.Duration = 1200;
            fadeIn.Delay = 600;
            float targetOpacityIn = 1.0f;
            view.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                          AnimatableProperties.Opacity,
                                                          targetOpacityIn,
                                                          fadeIn);


            ///////////////////////////////////////////////////////////////////////////////////////
            //  Custom transitions for Adding an ImageView
            //  ImageView positioned instantly
            //  A delayed opacity increases to 1.0f after siblings moved to make space
            ///////////////////////////////////////////////////////////////////////////////////////
            TransitionComponents instantPosition = new TransitionComponents();
            instantPosition.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            instantPosition.Delay = 0;
            instantPosition.Duration = 1;

            view.LayoutTransition = new LayoutTransition( TransitionCondition.Add,
                                                          AnimatableProperties.Position,
                                                          0.0,
                                                          instantPosition );

            TransitionComponents delayedInsertion = new TransitionComponents();
            delayedInsertion.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            delayedInsertion.Delay = 1200;
            delayedInsertion.Duration = 1600;

            view.LayoutTransition = new LayoutTransition( TransitionCondition.Add,
                                                          AnimatableProperties.Opacity,
                                                          1.0f,
                                                          delayedInsertion );

            ///////////////////////////////////////////////////////////////////////////////////////
            //  Custom transitions for siblings after ADDing an ImageView to the View
            //  Siblings are moved using AlphaFunction.BuiltinFunctions.EaseInOutSine
            ///////////////////////////////////////////////////////////////////////////////////////
            view.LayoutTransition = new LayoutTransition( TransitionCondition.ChangeOnAdd,
                                                          AnimatableProperties.Position,
                                                          0.0,
                                                          slowEaseInOutSine );

            ///////////////////////////////////////////////////////////////////////////////////////
            //  Custom transitions for Removing an ImageView
            //  The opacity animates to .2f
            ///////////////////////////////////////////////////////////////////////////////////////
            TransitionComponents fadeOut = new TransitionComponents();
            fadeOut.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
            fadeOut.Duration = 600;
            fadeOut.Delay = 0;
            float targetOpacityOut = .2f;
            view.LayoutTransition = new LayoutTransition( TransitionCondition.Remove,
                                                          AnimatableProperties.Opacity,
                                                          targetOpacityOut,
                                                          fadeOut);

            // Add child image-views to the created view
            var index = 0;
            foreach (String image in TestImages.s_images)
            {
                // Set a delayed custom transition for each Image View so each moves into place after it's
                // adjacent sibbling.
                TransitionComponents easeInOutSineDelayed = new TransitionComponents();
                easeInOutSineDelayed.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine);
                easeInOutSineDelayed.Delay = 300 * index;
                easeInOutSineDelayed.Duration = 300 * TestImages.s_images.Length;

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

            PushButton addItemButton = new PushButton();
            LayoutingExample.SetUnselectedIcon(addItemButton, "./res/images/icon-plus.png");
            addItemButton.Name = "addItemButton";
            addItemButton.ParentOrigin = new Vector3(.9f, 1.0f, 0.5f);
            addItemButton.PivotPoint = PivotPoint.BottomCenter;
            addItemButton.PositionUsesPivotPoint = true;
            addItemButton.MinimumSize = new Vector2(75, 75);
            addItemButton.Clicked += (sender, e) =>
            {
                Button button = sender as Button;
                if (!addedItem)
                {
                    ImageView imageView = LayoutingExample.CreateChildImageView(TestImages.s_images[0], new Size2D(80, 80));
                    // Delay transition not applied to images added at run time.
                    imageView.Opacity = 0.2f;
                    imageView.Name = "ImageViewBeingAdded-png";
                    view.Add(imageView);
                    LayoutingExample.SetUnselectedIcon(button, "./res/images/icon-minus.png");
                    addedItem = true;
                }
                else
                {
                    foreach (View item in view.Children)
                    {
                        if (item.Name == "ImageViewBeingAdded-png")
                        {
                            view.Remove(item);
                            addedItem = false;
                            LayoutingExample.SetUnselectedIcon(button, "./res/images/icon-plus.png");
                            break;
                        }
                    }
                }
                return true;
            };

            LayoutingExample.GetWindow().Add(addItemButton);
            buttons.Add(addItemButton);
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
