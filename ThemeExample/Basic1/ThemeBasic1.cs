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

        var root = NUIApplication.GetDefaultWindow();
        root.BackgroundColor = Color.White;

        // Load and apply theme from xaml file.
        var resDir = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
        ThemeManager.ApplyTheme(new Theme(resDir + "Theme/CustomTheme.xaml"));

        root.Add(new TextLabel() {
            StyleName = "TextA",
            Text = "TextA",
            Position = new Position(10, 10)
        });

        root.Add(new TextLabel() {
            StyleName = "TextB",
            Text = "TextB",
            Position = new Position(10, 80)
        });

        root.Add(new TextLabel() {
            StyleName = "TextC",
            Text = "TextC",
            Position = new Position(10, 170)
        });

        root.KeyEvent += OnKeyEvent;
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
