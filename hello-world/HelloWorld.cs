/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
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
using Tizen.NUI.BaseComponents;

class HelloWorldExample : NUIApplication
{
    bool animated;
    ScrollableBase scroll;
    View scrollContainer;
    Position prevPointPosition;
    PanGestureDetector detector;

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        scroll = new ScrollableBase()
        {
            Size = new Size(Window.Instance.WindowSize),
            ScrollingDirection = ScrollableBase.Direction.Horizontal,
            ScrollDuration = 600,
            SnapToPage = true,
        };
        scroll.ScrollAnimationEndEvent += OnScrollAnimationEnded;
        Window.Instance.GetDefaultLayer().Add(scroll);

        scrollContainer = new View()
        {
            WidthSpecification = LayoutParamPolicies.WrapContent,
            HeightSpecification = LayoutParamPolicies.WrapContent,
            Layout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Horizontal,
            },
        };
        scroll.Add(scrollContainer);

        for(int i = 0; i < 5; i++)
        {
            View page = new View()
            {
                Size = new Size(Window.Instance.WindowSize),
                BackgroundColor = i%2 == 0 ? Color.Cyan:Color.Magenta,
                Layout = new AbsoluteLayout(),
            };

            scrollContainer.Add(page);
        }

        View item = new View()
        {
            Size = new Size(100,100),
            BackgroundColor = Color.Yellow,
        };
        item.TouchEvent += OnItemTouched;
        scrollContainer.Children[0].Add(item);

        detector = new PanGestureDetector();
        detector.Attach(item);
    }

    void OnScrollAnimationEnded(object source, ScrollableBase.ScrollEventArgs args)
    {
        animated = false;
    }

    bool OnItemTouched(object source, View.TouchEventArgs args)
    {
        View target = source as View;
        Position pointPosition = new Position(args.Touch.GetScreenPosition(0));

        if(args.Touch.GetState(0) == PointStateType.Started)
        {
            target.BackgroundColor = Color.Red;
            // Move item to window
            Window.Instance.GetDefaultLayer().Add(target);
            // Initialize point position
            prevPointPosition = pointPosition;
        }
        else if(args.Touch.GetState(0) == PointStateType.Motion)
        {
            if(pointPosition.X < 50 && scroll.CurrentPage > 0 && animated == false)
            {
                animated = true;
                scroll.ScrollToIndex(scroll.CurrentPage - 1);
            }
            else if(pointPosition.X > 430 && scroll.CurrentPage < scrollContainer.Children.Count -1 && animated == false )
            {
                animated = true;
                scroll.ScrollToIndex(scroll.CurrentPage + 1);
            }
            else
            {
                float xDiff = pointPosition.X - prevPointPosition.X;
                float yDiff = pointPosition.Y - prevPointPosition.Y;

                target.Position = new Position( target.Position.X + xDiff, target.Position.Y + yDiff);
                prevPointPosition = pointPosition;
            }
        }
        else if(args.Touch.GetState(0) == PointStateType.Up || args.Touch.GetState(0) == PointStateType.Leave)
        {
            scrollContainer.Children[scroll.CurrentPage].Add(target);
            target.BackgroundColor = Color.Yellow;
        }

        return true;
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        HelloWorldExample example = new HelloWorldExample();
        example.Run(args);
    }
}

