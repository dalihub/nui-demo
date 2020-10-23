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
    View root;

    protected override void OnCreate()
    {
        base.OnCreate();

        Initialize();
    }

    void Initialize()
    {
        NUIApplication.GetDefaultWindow().BackgroundColor = new Color(0.678f, 0.655f, 0.576f, 1.0f);
        root = new View()
        {
            Layout = new GridLayout()
            {
                Columns = 3,
                // ColumnSpacing = 16,
                // RowSpacing = 16,
            },
            Padding = new Extents(8),
        };
        NUIApplication.GetDefaultWindow().Add(root);

        AddRow(new Size(100, 100), 20);
        AddRow(new Size(100, 100), 50);
        AddRow(new Size(100, 60), 20);
        AddRow(new Size(100, 60), 30);

    }

    void AddRow(Size size, float cornerRadius)
    {
        AddItem(size, cornerRadius, null);
        AddItem(size, cornerRadius, null);
        AddItem(size, cornerRadius, null);
    }

    void AddItem(Size size, float cornerRadius, Shadow shadow)
    {
        var item = new View()
        {
            Size = new Size(140, 140),
            BackgroundColor = Color.White,
            Margin = new Extents(8),
        };

        item.Add(new View()
        {
            Size = size,
            CornerRadius = cornerRadius,
            BackgroundColor = new Color(0.537f, 0.725f, 0.78f, 1.0f),
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PositionUsesPivotPoint = true
            // BoxShadow = shadow,
        });

        root.Add(item);
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
