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
using Tizen.NUI.Components;

public class ComponentExample : NUIApplication
{
    public ComponentExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        
        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        var button = new Button(new OverlayAnimationButtonStyle())
        {
            Text = "Hello World!",
            IconURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "icon.png",
            IconPadding = new Extents(0, 0, 50, 0),

            Size = new Size(200, 200),
            CornerRadius = 100,

            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PositionUsesPivotPoint = true
        };
        window.Add(button);
    }

    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}