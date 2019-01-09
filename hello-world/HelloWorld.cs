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
    TextLabel title;
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

        // Create a simple TextLabel
        title = new TextLabel("Hello World");

        // Ensure TextLabel matches its parent's size (i.e. Window size)
        // By default, a TextLabel's natural size is the size of the text within it
        title.LayoutWidthSpecification = ChildLayoutData.MatchParent;
        title.LayoutHeightSpecification = ChildLayoutData.MatchParent;

        // By default, text is aligned to the top-left within the TextLabel
        // Align it to the center of the TextLabel
        title.HorizontalAlignment = HorizontalAlignment.Center;
        title.VerticalAlignment = VerticalAlignment.Center;

        // Add the text to the window
        window.Add(title);
        Window.Instance.KeyEvent += WindowKeyEvent;


    }

    private void WindowKeyEvent(object sender, Window.KeyEventArgs e)
    {
        if (e.Key.State == Key.StateType.Down)
        {
            switch( e.Key.KeyPressedName )
            {
                case "Left":
                    LayoutController localController2 = new LayoutController();
                break;
                case "Right" :
                    title.Text = "Bye World";
                break;
                case "Up" :
                break;
                case "Down" :
                break;
            }

        }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        HelloWorldExample example = new HelloWorldExample();
        example.Run(args);
    }
}
