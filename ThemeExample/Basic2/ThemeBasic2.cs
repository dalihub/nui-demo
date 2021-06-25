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
using Tizen.NUI.Components;

public class NUISampleApplication : NUIApplication
{
    protected override void OnCreate()
    {
        base.OnCreate();

        Initialize();
    }

    Theme themeBlack, themeGreen;
    int clickCount = 0;

    void Initialize()
    {
        var root = NUIApplication.GetDefaultWindow();
        var resourcePath = Tizen.Applications.Application.Current.DirectoryInfo.Resource;

        // Load and apply theme from xaml file.
        themeBlack = new Theme(resourcePath + "Theme/Black.xaml");
        themeGreen = new Theme(resourcePath + "Theme/Green.xaml");
        ThemeManager.ApplyTheme(themeBlack);


        var themeChangeButton = new Button() {
            WidthResizePolicy = ResizePolicyType.FillToParent,
            SizeHeight = 70,
            Text = "Click to change theme",
        };
        themeChangeButton.Clicked += OnClicked;
        root.Add(themeChangeButton);
        root.BackgroundColor = Color.White;


        // [Sample 1]
        // Set style name you want to apply.
        root.Add(new TextLabel() {
            StyleName = "ColorText",
            Position = new Position(30, 120),
            Text = "Hello World!",
        });

        // [Sample 2]
        root.Add(new Button() {
            StyleName = "ButtonDefault",
            Position = new Position(30, 180),
            Text = "ButtonA",
        });

        // [Sample 3]
        root.Add(new Button() {
            StyleName = "ButtonDefault",
            Position = new Position(200, 180),
            Text = "ButtonB",
        });

        // [Sample 4-1]
        root.Add(new Switch() {
            StyleName = "SwitchFancy",
            Position = new Position(30, 280),
        });

        // [Sample 4-2]
        // Set ThemeChangedSensitive to false if you don't want this view to be affected by theme changes.
        // Note that the ThemeChangedSensitive is "false" by default, but turned to true when setting StyleName explitcitly.
        root.Add(new Switch() {
            StyleName = "SwitchFancy",
            Position = new Position(200, 280),
            ThemeChangeSensitive = false,
        });

        root.KeyEvent += OnKeyEvent;
    }

    private void OnClicked(object target, ClickedEventArgs args)
    {
        clickCount++;

        if ((clickCount) % 2 == 0) ThemeManager.ApplyTheme(themeBlack);
        else ThemeManager.ApplyTheme(themeGreen);
    }

    public void OnKeyEvent(object sender, Window.KeyEventArgs e)
    {
        if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
        {
            Exit();
        }
    }


    static void Main(string[] args)
    {
        NUISampleApplication example = new NUISampleApplication();
        example.Run(args);
    }
}
