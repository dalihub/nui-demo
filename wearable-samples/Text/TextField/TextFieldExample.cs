/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd.
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
using Tizen.NUI.BaseComponents;

public class TextFieldExample : NUIApplication
{
    public TextFieldExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        // Create a TextField
        TextField field = new TextField()
        {
            Size = new Size(230, 47),
            Position =  new Position(0, 50),
            BackgroundColor = Color.White,

            // Set TextField properties, such as PlaceholderText and PrimaryCursorColor
            PointSize = 9.0f,
            PlaceholderText = "Tap to enter text",
            PlaceholderTextFocused = "Activated",
            PrimaryCursorColor = Color.Blue,

            // Positioning it to the top center
            ParentOrigin = ParentOrigin.TopCenter,
            PivotPoint = PivotPoint.TopCenter,
            PositionUsesPivotPoint = true
        };
        window.Add(field);
    }

    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        TextFieldExample example = new TextFieldExample();
        example.Run(args);
    }
}
