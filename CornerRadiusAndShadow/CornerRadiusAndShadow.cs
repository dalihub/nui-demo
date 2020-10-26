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
    static readonly Size sampleBoxSize = new Size(80, 80);
    static readonly Color sampleBoxColor = new Color(0.537f, 0.725f, 0.78f, 1.0f);

    public NUISampleApplication() : base(new Size2D(480, 800), new Position2D(0, 0))
    {}

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
            Layout = new LinearLayout() { LinearOrientation = LinearLayout.Orientation.Vertical, CellPadding = new Size2D(0, 20) },
            Padding = new Extents(20),
        };
        NUIApplication.GetDefaultWindow().Add(root);

        AddCornerRadiusExample(20);
        AddCornerRadiusExample(40);
        AddShadowExample(15, new Vector2(10, 10));
        AddShadowExample(5, new Vector2(10, 10));
        AddShadowExample(5, new Vector2(-10, -10));
    }

    void AddCornerRadiusExample(float cornerRadius)
    {
        AddItem(new View()
        {
            Size = sampleBoxSize,
            BackgroundColor = sampleBoxColor,
            CornerRadius = cornerRadius,
        }, $"view.CornerRadius = {cornerRadius};");
    }


    void AddShadowExample(float blurRadius, Vector2 offset)
    {
        AddItem(new View()
        {
            Size = sampleBoxSize,
            BackgroundColor = sampleBoxColor,
            BoxShadow = new Shadow(blurRadius, offset, null, null),
        }, $"view.BoxShadow = new Shadow() {{\n    BlurRadius = {blurRadius},\n    Offset = new Vector({offset.Width}, {offset.Height}),\n}};");
    }

    void AddItem(View view, string desc)
    {
        view.ParentOrigin = ParentOrigin.CenterLeft;
        view.PivotPoint = PivotPoint.CenterLeft;
        view.PositionUsesPivotPoint = true;

        var item = new View()
        {
            Size = new Size(440, 130),
            BackgroundColor = Color.White,
            Padding = new Extents(20, 20, 0, 0),
            CornerRadius = 6,
            Layout = new LinearLayout() { CellPadding = new Size2D(30, 0) },
        };
        item.Add(view);

        var textArea = new View()
        {
            BackgroundColor = new Color(0, 0, 0, 0.8f),
            Size = new Size(290, 90),
            ParentOrigin = ParentOrigin.CenterLeft,
            PivotPoint = PivotPoint.CenterLeft,
            PositionUsesPivotPoint = true,
            CornerRadius = 4,
        };
        item.Add(textArea);

        textArea.Add(new TextLabel()
        {
            ParentOrigin = ParentOrigin.CenterLeft,
            PivotPoint = PivotPoint.CenterLeft,
            PositionUsesPivotPoint = true,
            MultiLine = true,
            PointSize = 10,
            TextColor = Color.White,
            Text = $"view.Size = new Size({sampleBoxSize.Width}, {sampleBoxSize.Height});\n" + desc,
            Padding = new Extents(10, 10, 0, 0),
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
