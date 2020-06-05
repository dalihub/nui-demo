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
using Tizen.NUI.Wearable;

public class CircularSliderExample : NUIApplication
{
    Window window;
    CircularSlider slider;

    TextLabel label;
    TextLabel minusLabel;
    TextLabel plusLabel;
    ImageView iconImage;

    public CircularSliderExample() : base(new Size2D(360, 360), new Position2D(0, 0))
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        window = NUIApplication.GetDefaultWindow();

        slider = new CircularSlider();
        slider.CurrentValue = 0;

        // These properties have default values, so they're set to the following values
        // even if you don't set them separately.
        slider.MinValue = 0;
        slider.MaxValue = 10;
        slider.Thickness = 6;

        window.Add(slider);

        CreateLabelAndIcon();

        // Bezel event
        window.WheelEvent += Slider_WheelEvent;

        // If you want to change the colors of the track and the progress,
        // use TrackColor and ProgressColor properties.

        // If you want to change thumb color and size,
        // use ThumbColor and ThumbSize properties.
    }

    void CreateLabelAndIcon()
    {
        label = new TextLabel()
        {
            Text = slider.CurrentValue.ToString(),
            TextColor = Color.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            PositionUsesPivotPoint = true,
            ParentOrigin = Tizen.NUI.ParentOrigin.Center,
            PivotPoint = Tizen.NUI.PivotPoint.Center,
        };
        window.Add(label);

        iconImage = new ImageView()
        {
            Position = new Position(0, -70),
            PositionUsesPivotPoint = true,
            ParentOrigin = Tizen.NUI.ParentOrigin.Center,
            PivotPoint = Tizen.NUI.PivotPoint.Center,
            ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "sound_icon.png"
        };
        window.Add(iconImage);

        TextLabel fixedLabel = new TextLabel()
        {
            Text = "SOUND",
            TextColor = Color.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Position = new Position(0, 70),
            PositionUsesPivotPoint = true,
            ParentOrigin = Tizen.NUI.ParentOrigin.Center,
            PivotPoint = Tizen.NUI.PivotPoint.Center,
        };
        window.Add(fixedLabel);

        minusLabel = new TextLabel()
        {
            Text = "-",
            TextColor = Color.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            WidthResizePolicy = ResizePolicyType.UseNaturalSize,
            HeightResizePolicy = ResizePolicyType.UseNaturalSize,
            Position = new Position(-50, 0),
            PositionUsesPivotPoint = true,
            ParentOrigin = Tizen.NUI.ParentOrigin.Center,
            PivotPoint = Tizen.NUI.PivotPoint.Center,
        };
        minusLabel.TouchEvent += OnMinusTouch;
        window.Add(minusLabel);

        plusLabel = new TextLabel()
        {
            Text = "+",
            TextColor = Color.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            WidthResizePolicy = ResizePolicyType.UseNaturalSize,
            HeightResizePolicy = ResizePolicyType.UseNaturalSize,
            Position = new Position(50, 0),
            PositionUsesPivotPoint = true,
            ParentOrigin = Tizen.NUI.ParentOrigin.Center,
            PivotPoint = Tizen.NUI.PivotPoint.Center,
        };
        plusLabel.TouchEvent += OnPlusTouch;
        window.Add(plusLabel);
    }

    private void Slider_WheelEvent(object source, Window.WheelEventArgs e)
    {
        // CustomWheel means Bezel in wearable device.
        if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
        {
            // Direction 1 is CW and 2 is CCW
            if (e.Wheel.Direction == 1)
            {
                if (slider.CurrentValue < slider.MaxValue)
                {
                    slider.CurrentValue++;
                    label.Text = slider.CurrentValue.ToString();
                }
            }
            else
            {
                if (slider.CurrentValue > slider.MinValue)
                {
                    slider.CurrentValue--;
                    label.Text = slider.CurrentValue.ToString();
                }
            }
        }
    }

    private bool OnMinusTouch(object source, View.TouchEventArgs e)
    {
        if (e.Touch.GetState(0) == PointStateType.Down)
        {
            if (slider.CurrentValue > 0)
            {
                slider.CurrentValue -= 1;
                label.Text = slider.CurrentValue.ToString();
            }
        }
        return false;
    }

    private bool OnPlusTouch(object source, View.TouchEventArgs e)
    {
        if (e.Touch.GetState(0) == PointStateType.Down)
        {
            if (slider.CurrentValue < slider.MaxValue)
            {
                slider.CurrentValue += 1;
                label.Text = slider.CurrentValue.ToString();
            }
        }
        return false;
    }

    static void Main(string[] args)
    {
        CircularSliderExample example = new CircularSliderExample();
        example.Run(args);
    }
}
