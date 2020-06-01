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

public class LinearPaginationExample : NUIApplication
{
    private Pagination pagination;
    private ScrollableBase scrollable;

    private readonly int PAGE_COUNT = 6;
    public LinearPaginationExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        // Create Pagination component
        pagination = new Pagination()
        {
            Size = new Size(118, 10),
            Position = new Position(0, 24),

            // Set Pagination properties, such as Indicator size, count, and images.
            IndicatorSize = new Size(10, 10),
            IndicatorSpacing = 8,
            IndicatorCount = PAGE_COUNT,
            SelectedIndex = 0,

            // Positioning it to the top center
            ParentOrigin = ParentOrigin.TopCenter,
            PivotPoint = PivotPoint.TopCenter,
            PositionUsesPivotPoint = true
        };


        // To move on to the next page using Swipe gesture, add ScrollableBase and its View container.
        scrollable = new ScrollableBase()
        {
            Size = new Size(360,360),

            ScrollingDirection = ScrollableBase.Direction.Horizontal,
            SnapToPage = true,

            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PositionUsesPivotPoint = true
        };

        View container = new View()
        {
            WidthSpecification = LayoutParamPolicies.WrapContent,
            HeightSpecification = 360,
            Layout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Horizontal,
            },
            BackgroundColor = Color.Black,
        };
        scrollable.Add(container);

        for (int i = 0; i < PAGE_COUNT; i++)
        {
            View page = new View()
            {
                Size = new Size(360, 360),
                CornerRadius = 150.0f,
                BackgroundColor = Color.Black,
            };

            container.Add(page);
        }

        window.Add(scrollable);
        window.Add(pagination);

        // Bezel event
        window.WheelEvent += Pagination_WheelEvent;

        // Screen Touch Event
        scrollable.ScrollAnimationEndEvent  += Scroll_AnimationEnd;
    }

    private void Pagination_WheelEvent(object source, Window.WheelEventArgs e)
    {
        // CustomWheel means Bezel in wearable device.
        if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
        {
            // Direction 1 is CW and 2 is CCW
            if (e.Wheel.Direction == 1)
            {
                if (pagination.SelectedIndex < pagination.IndicatorCount - 1)
                {
                    pagination.SelectedIndex = pagination.SelectedIndex + 1;
                    scrollable.ScrollToIndex(pagination.SelectedIndex);
                }
            }
            else
            {
                if (pagination.SelectedIndex > 0)
                {
                    pagination.SelectedIndex = pagination.SelectedIndex - 1;
                    scrollable.ScrollToIndex(pagination.SelectedIndex);
                }
            }
        }
    }

    // There are two ways to set the current selected index using ScrollableBase event.
    void Scroll_AnimationEnd(object source, ScrollableBase.ScrollEventArgs e)
    {
        ///////////////////// 1. Using the parameter of the event /////////////////////
        // e.Position.X means the top-left position of ScrollableBase and Container.
        // So, if the page is passed once, X position becomes -360. And when the page is the second one, it's -720.
        // It can allow you to know the index of the page.

        //int index = (int)Math.Abs( e.Position.X / 360 );


        ///////////////////// 2. Using CurrentPage property of ScrollableBase /////////////////////
        // In this time, ScrollableBase variable should be global. Then, the user can get the value.

        int index = scrollable.CurrentPage;

        if (index >= 0 && index < pagination.IndicatorCount)
        {
            pagination.SelectedIndex = index;
        }
    }

    static void Main(string[] args)
    {
        LinearPaginationExample example = new LinearPaginationExample();
        example.Run(args);
    }
}
