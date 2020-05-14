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
using Tizen.NUI.Wearable;

public class ComponentExample : NUIApplication
{
    CircularScrollBar scrollbar;
    float currentPosition = 0;
    float contentLength = 800;
    float lastY = 0;
    Color normalThumbColor;
    Color touchedThumbColor = new Color(0.0f, 0.549f, 1.0f, 1.0f);

    public ComponentExample() : base(new Size2D(360, 360), new Position2D(0, 0))
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        var window = NUIApplication.GetDefaultWindow();
        var fullSize = Math.Min(window.Size.Width, window.Size.Height) - 1;

        scrollbar = new CircularScrollBar(contentLength, fullSize, currentPosition)
        {
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PositionUsesPivotPoint = true,
        };

        window.Add(scrollbar);
        window.TouchEvent += OnTouch;

        normalThumbColor = scrollbar.ThumbColor;
    }

    void OnTouch(object target, Window.TouchEventArgs args)
    {
        var currentY = args.Touch.GetScreenPosition(0).Y;

        if (args.Touch.GetState(0) == PointStateType.Down)
        {
            scrollbar.ThumbColor = touchedThumbColor;
        }
        else if (args.Touch.GetState(0) == PointStateType.Up)
        {
            scrollbar.ThumbColor = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        }
        else if (args.Touch.GetState(0) == PointStateType.Motion)
        {
            var nextPosition = currentPosition + currentY - lastY;

            if (nextPosition < 0)
            {
                nextPosition = 0;
            }
            else if (nextPosition > contentLength)
            {
                nextPosition = contentLength;
            }

            scrollbar.ScrollTo(nextPosition);

            currentPosition = nextPosition;
        }

        lastY = currentY;
    }

    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}
