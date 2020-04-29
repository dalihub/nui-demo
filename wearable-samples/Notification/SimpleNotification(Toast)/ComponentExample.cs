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

public class ComponentExample : NUIApplication
{
    public ComponentExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        var text = new TextLabel()
        {
            Text = "Click to Post!",
            TextColor = Color.White,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            PositionUsesPivotPoint = true
        };

        window.Add(text);
        window.TouchEvent += OnWindowTouch;
    }

    void OnWindowTouch(object target, Window.TouchEventArgs arg)
    {
        if (arg.Touch.GetState(0) != PointStateType.Down)
        {
            return;
        }

        var contentView = new TextLabel()
        {
            Size = new Size(360, 360),
            Position = new Position(360, 0),
            CornerRadius = 180,
            BackgroundColor = Color.Blue,
            Text = "Click To Dismiss!",
            TextColor = Color.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        new Notification(contentView)
            .SetDismissOnTouch(true)                     // (Optional) Dismiss when user touch.
            .SetOnPostDelegate(OnNotificationPost)       // (Optional) Set a callback to be called when ready to post.
            .SetOnDismissDelegate(OnNotificationDismiss) // (Optional) Set a callback to be called when dismiss.
            .Post();  // You may set duration in ms like, Post(3000). If you don't it won't set timer for dismissal.
    }

    void OnNotificationPost(Notification notification)
    {
        var animation = new Animation(500);
        animation.AnimateTo(notification.ContentView, "Position", new Position(0, 0));
        animation.Play();
    }

    uint OnNotificationDismiss(Notification notification)
    {
        var animation = new Animation(500);
        animation.AnimateTo(notification.ContentView, "Position", new Position(360, 0));
        animation.Play();

        // Delay dismiss for animation playing duration
        return 500;
    }

    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}