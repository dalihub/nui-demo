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
    private bool _large = false;

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        // Get the window instance and change background color
        Window window = Window.Instance;
        window.BackgroundColor = Color.Red;

        View linearLayout = new View()
        {
            Layout = new LinearLayout(),
            WidthSpecification = (int)(window.Size.Width * 0.5),
            HeightSpecification = LayoutParamPolicies.MatchParent,
            PositionX = window.Size.Width * 0.5f,
            BackgroundColor = Color.White,
            Name = "linearLayout"
        };

        window.Add(linearLayout);

        // Create a simple TextLabel
        TextLabel title = new TextLabel("Hello")
        {
            BackgroundColor = Color.Cyan,
            WidthSpecification = LayoutParamPolicies.WrapContent,
            HeightSpecification = LayoutParamPolicies.WrapContent,
            Name = "textLabel"
        };

        title.TouchEvent += (sender, e) =>
        {
            if (sender is TextLabel && e.Touch.GetState(0) == PointStateType.Down)
            {
                if (_large)
                {
                    _large = false;
                    TextLabel touchedTextLabel = (TextLabel)sender;
                    touchedTextLabel.Text = "Hello";
                }
                else
                {
                    _large = true;
                    TextLabel touchedTextLabel = (TextLabel)sender;
                    touchedTextLabel.Text = "Hello World";
                }
            }
            return true;
        };

        // By default, text is aligned to the top-left within the TextLabel
        // Align it to the center of the TextLabel
        title.HorizontalAlignment = HorizontalAlignment.Center;
        title.VerticalAlignment = VerticalAlignment.Center;

        // Add the text to the window
        linearLayout.Add(title);
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

