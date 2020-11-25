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
    private static string resourcePath = "./res/";//Tizen.Applications.Application.Current.DirectoryInfo.Resource;
    private static string fileName = "/gallery-large-19.jpg";

    private static BlendEquationType[] blendEquationTypes = {
            BlendEquationType.Multiply,
            BlendEquationType.Screen,
            BlendEquationType.Overlay,
            BlendEquationType.Darken,
            BlendEquationType.Lighten,
            BlendEquationType.ColorDodge,
            BlendEquationType.ColorBurn,
            BlendEquationType.HardLight,
            BlendEquationType.SoftLight,
            BlendEquationType.Difference,
            BlendEquationType.Exclusion,
            BlendEquationType.Hue,
            BlendEquationType.Saturation,
            BlendEquationType.Color,
            BlendEquationType.Luminosity
        };
    private static int advansedBlendEquationCount = 15;

    Window window;
    ScrollableBase contents;
    static readonly Size sampleBoxSize = new Size(120, 120);
    static readonly Color sampleBoxColor = new Color(0.537f, 0.725f, 0.78f, 1.0f);

    public NUISampleApplication() : base(new Size2D(480, 800), new Position2D(0, 0))
    { }

    protected override void OnCreate()
    {
        base.OnCreate();

        Initialize();
    }

    void Initialize()
    {
        window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = new Color(0.678f, 0.655f, 0.576f, 1.0f);

        View topView = new View()
        {
            HeightSpecification = LayoutParamPolicies.WrapContent,
            WidthSpecification = LayoutParamPolicies.MatchParent,
        };

        ImageView lowerImageView = CreateLowerImageView();
        lowerImageView.Size = new Size(120, 120);
        lowerImageView.Position = new Position(40, 40);
        topView.Add(lowerImageView);

        TextLabel lowerImageName = new TextLabel()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            PointSize = 12,
            Size = new Size(120, 30),
            Position = new Position(40, 160),
            TextColor = new Color(0.2f, 0.2f, 0.2f, 1.0f),
            Text = "Lower Image",
        };
        topView.Add(lowerImageName);

        // BlendEquationType.Min to show original image in this background color
        View upperColorView = CreateUpperColorView(new Size(120, 120), BlendEquationType.Min);
        upperColorView.Position = new Position(window.Size.Width - 160, 40);
        topView.Add(upperColorView);

        TextLabel upperColorHeading = new TextLabel()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            PointSize = 12,
            Size = new Size(120, 30),
            Position = new Position(window.Size.Width - 160, 160),
            TextColor = new Color(0.2f, 0.2f, 0.2f, 1.0f),
            Text = "Upper Image",
        };
        topView.Add(upperColorHeading);

        window.Add(topView);

        contents = new ScrollableBase()
        {
            Position = new Position(0, 200),
            HeightSpecification = window.Size.Height - 220,
            WidthSpecification = LayoutParamPolicies.MatchParent,
            ScrollingDirection = ScrollableBase.Direction.Vertical,

            Layout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = new Size2D(0, 20)
            },
            Padding = new Extents(20),
        };
        window.Add(contents);

        for (int i = 0; i < advansedBlendEquationCount; ++i)
        {
            AddBlendEquationItem(blendEquationTypes[i]);
        }
    }

    ImageView CreateLowerImageView()
    {
        ImageView imageView = new ImageView()
        {
            ResourceUrl = resourcePath + fileName,
        };

        return imageView;
    }

    View CreateUpperColorView(Size size, BlendEquationType blendEquationType)
    {
        View baseView = new View()
        {
            Size = size,
            Layout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
            },
        };

        float yPositionScale = (float)(size.Height) / 3.0f;
        View colorView1 = new View()
        {
            BlendEquation = blendEquationType,
            Size = new Size(size.Width, size.Height / 3),

            Background = new PropertyMap().Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color))
            .Add(ColorVisualProperty.MixColor, new PropertyValue(Color.Red))
            .Add(Visual.Property.PremultipliedAlpha, new PropertyValue(true)),
        };
        View colorView2 = new View()
        {
            BlendEquation = blendEquationType,
            Size = new Size(size.Width, size.Height / 3),

            Background = new PropertyMap().Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color))
            .Add(ColorVisualProperty.MixColor, new PropertyValue(Color.Green))
            .Add(Visual.Property.PremultipliedAlpha, new PropertyValue(true)),
        };
        View colorView3 = new View()
        {
            BlendEquation = blendEquationType,
            Size = new Size(size.Width, size.Height / 3),

            Background = new PropertyMap().Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color))
            .Add(ColorVisualProperty.MixColor, new PropertyValue(Color.Blue))
            .Add(Visual.Property.PremultipliedAlpha, new PropertyValue(true)),
        };
        baseView.Add(colorView1);
        baseView.Add(colorView2);
        baseView.Add(colorView3);

        return baseView;
    }

    void AddBlendEquationItem(BlendEquationType blendEquationType)
    {
        View baseView = new View()
        {
            Size = sampleBoxSize,
        };

        ImageView lowerImageView = CreateLowerImageView();
        lowerImageView.Size = sampleBoxSize;
        baseView.Add(lowerImageView);

        View upperColorView = CreateUpperColorView(sampleBoxSize, blendEquationType);
        baseView.Add(upperColorView);

        AddItem(baseView, $"view.BlendEquation = \n    BlendEquationType.{blendEquationType};");
    }

    void AddItem(View view, string desc)
    {
        view.ParentOrigin = ParentOrigin.CenterLeft;
        view.PivotPoint = PivotPoint.CenterLeft;
        view.PositionUsesPivotPoint = true;

        var item = new View()
        {
            Size = new Size(440, 140),
            BackgroundColor = Color.White,
            Padding = new Extents(20, 20, 0, 0),
            CornerRadius = 6,
            Layout = new LinearLayout() { CellPadding = new Size2D(30, 0) },
        };
        item.Add(view);

        var textArea = new View()
        {
            BackgroundColor = new Color(0, 0, 0, 0.8f),
            Size = new Size(250, 120),
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
            Text = desc,
            Padding = new Extents(10, 10, 0, 0),
        });
        contents.Add(item);
    }

    static void Main(string[] args)
    {
        NUISampleApplication example = new NUISampleApplication();
        example.Run(args);
    }
}
