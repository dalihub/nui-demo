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

public class CircularProgressExample : NUIApplication
{
    CircularProgress circular;

    public CircularProgressExample() : base(new Size2D(360, 360), new Position2D(0, 0))
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        var window = NUIApplication.GetDefaultWindow();

        circular = new CircularProgress();
        circular.CurrentValue = 40;

        // These properties are default values, so they're set to the following values
        // even if you don't set them separately.
        circular.MinValue = 0;
        circular.MaxValue = 100;
        circular.Thickness = 6;

        window.Add(circular);

        // Bezel event
        window.WheelEvent += Pagination_WheelEvent;

        // If you want to change the colors of the track and the progress,
        // use TrackColor and ProgressColor properties.

    }

    private void Pagination_WheelEvent(object source, Window.WheelEventArgs e)
    {
        // CustomWheel means Bezel in wearable device.
        if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
        {
            // Direction 1 is CW and 2 is CCW
            if (e.Wheel.Direction == 1)
            {
                if (circular.CurrentValue < circular.MaxValue)
                {
                    circular.CurrentValue = circular.CurrentValue + 10;
                }
            }
            else
            {
                if (circular.CurrentValue > circular.MinValue)
                {
                    circular.CurrentValue = circular.CurrentValue - 10;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        CircularProgressExample example = new CircularProgressExample();
        example.Run(args);
    }
}
