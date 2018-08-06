/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
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

            View clipView = new View();
             clipView.Size2D = new Size2D(300, 300);
             clipView.Position2D = new Position2D(300, 300);
            //test!
             //clipView.BackgroundColor = new Color(1.0f, 0.0f, 0.0f, 0.2f);//  >>>>> if this is removed, clipping is not working!
             clipView.ClippingMode = ClippingModeType.ClipToBoundingBox;


             TextLabel title = new TextLabel("Hello World");
             title.ClippingMode = ClippingModeType.ClipToBoundingBox;
             title.TextColor = Color.Yellow;
             title.Size2D = new Size2D(600, 600);
             title.PointSize = 100;


             TextLabel title2 = new TextLabel("Hello again");
             title2.TextColor = Color.Green;
             title2.Position2D = new Position2D(30, 30);
             title2.Size2D = new Size2D(600, 600);
             title2.PointSize = 100;
             title.Add(title2);

            // By default, text is aligned to the top-left within the TextLabel
             // Align it to the center of the TextLabel
             title.HorizontalAlignment = HorizontalAlignment.Center;
             title.VerticalAlignment = VerticalAlignment.Center;

            clipView.Add(title);
             window.Add(clipView);
             return;

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

