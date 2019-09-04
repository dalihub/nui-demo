/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

class HelloWorldExample : NUIApplication
{
    PanGestureDetector panGestureDetector;
    float PanGestureDisplacementY;
    float RatioToScreenHeightToCompleteScroll = 0.6;

    Animation scrollAnimation;

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        // Get the window instance and change background color
        Window window = Window.Instance;
        window.BackgroundColor = Color.White;
        Size2D windowSize = new Size2D(window.Size.Width, window.Size.Height);

        View background = new View()
        {
            Size2D = windowSize,
        };
        window.Add(background);

        // Create a simple TextLabel
        TextLabel title = new TextLabel("Hello World");

        // Ensure TextLabel matches its parent's size (i.e. Window size)
        // By default, a TextLabel's natural size is the size of the text within it
        title.Size2D = new Size2D(100, 100);
        title.PositionY = windowSize.Height*0.5;

        // By default, text is aligned to the top-left within the TextLabel
        // Align it to the center of the TextLabel
        title.HorizontalAlignment = HorizontalAlignment.Center;
        title.VerticalAlignment = VerticalAlignment.Center;

        panGestureDetector = new PanGestureDetector();
        panGestureDetector.Attach(background);
        panGestureDetector.Detected += OnPanGestureDetected;

        scrollAnimation = new Animation();
        scrollAnimation.AnimateTo(title, "PositionY", 50.0f,
                        0,
                        800,
                        new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare) );

        // Add the text to the window
        window.Add(title);
    }

    private void OnPanGestureDetected(object source, PanGestureDetector.DetectedEventArgs e)
    {
        switch(e.PanGesture.State)
        {
            case Gesture.StateType.Finished :
            {
                Console.WriteLine("panned:{0} progress{1}", PanGestureDisplacementY,scrollAnimation.CurrentProgress );
                if( scrollAnimation.CurrentProgress > RatioToScreenHeightToCompleteScroll)
                {
                   // Panned enough to allow auto completion of animation.
                   scrollAnimation.SpeedFactor = 1;
                   scrollAnimation.EndAction = Animation.EndActions.StopFinal;
                   scrollAnimation.Play();
                }
                else
                {
                    // Reverse animation as have panned enought to warrant completion.
                    scrollAnimation.SpeedFactor = -1;
                    scrollAnimation.EndAction = Animation.EndActions.Cancel;
                    scrollAnimation.Play();
                }
            }
            break;
            case Gesture.StateType.Continuing :
            {
                PanGestureDisplacementY += e.PanGesture.ScreenDisplacement.Y;
                var progress = PanGestureDisplacementY/(windowSize.Height*0.5*RatioToScreenHeightToCompleteScroll);
                Console.WriteLine("panning:{0} progress{1} animiationProgress{2}", PanGestureDisplacementY,progress, scrollAnimation.CurrentProgress);
                scrollAnimation.CurrentProgress = progress;
            }
            break;
            case Gesture.StateType.Started :
            {
                scrollAnimation.EndAction = Animation.EndActions.Discard;
                scrollAnimation.Play();
                scrollAnimation.Pause();

                PanGestureDisplacementY = 0;
                Console.WriteLine("start_panning:{0}, total{1}", e.PanGesture.ScreenDisplacement.Y, PanGestureDisplacementY);
            }
            break;
        }

    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        // Do not remove this print out - helps with the TizenFX stub sync issue
        Console.WriteLine("Running Example...");
        HelloWorldExample example = new HelloWorldExample();
        example.Run(args);
    }
}

