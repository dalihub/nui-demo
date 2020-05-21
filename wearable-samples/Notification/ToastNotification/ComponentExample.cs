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

    void OnWindowTouch(object target, Window.TouchEventArgs args)
    {
        if (args.Touch.GetState(0) != PointStateType.Down)
        {
            return;
        }

        // Prepare content to notify
        var contentView = new TextLabel()
        {
            Size = new Size(180, 60),
            CornerRadius = 30,
            BackgroundColor = Color.White,
            Opacity = 0,
            Text = "Hello World!",
            PixelSize = 24,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PositionUsesPivotPoint = true,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Scale = new Vector3(0, 0, 0),
        };

        var animationOnPost = new Animation(200);
        animationOnPost.AnimateTo(contentView, "Opacity", 0.8f);
        animationOnPost.AnimateTo(contentView, "Scale", new Vector3(1, 1, 1));

        var animationOnDismiss = new Animation(200);
        animationOnDismiss.AnimateTo(contentView, "Opacity", 0);
        animationOnDismiss.AnimateTo(contentView, "Scale", new Vector3(0, 0, 0));

        new Notification(contentView)
            .SetAnimationOnPost(animationOnPost)            // (Optional) Set an animation to be played when post.
            .SetAnimationOnDismiss(animationOnDismiss)      // (Optional) Set an animation to be played when dismiss.
            .SetPositionSize(new Rectangle(90, 20, 180, 60)) // (Optional) Set notification window boundary.
            .Post(2000); // Post for 2 seconds
    }

    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}