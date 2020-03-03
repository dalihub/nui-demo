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
    bool isEditMode = false;
    bool editing = false;
    float SIZE_FACTOR = 0.7f;
    int EDIT_PADDING = 50;
    int ANIMATION_PLAY_TIME = 200;
    LongPressGestureDetector detector;
    Animation editModeAnimation;

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        scroll = new ScrollableBase()
        {
            WidthSpecification = Window.Instance.WindowSize.Width,
            HeightSpecification = LayoutParamPolicies.WrapContent,
            ScrollingDirection = ScrollableBase.Direction.Horizontal,
            ScrollDuration = 600,
            SnapToPage = true,
        };
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
        detector = new LongPressGestureDetector();

        for(int i = 0; i < 5; i++)
        {
            View page = new View()
            {
                Size = new Size(Window.Instance.WindowSize),
                BackgroundColor = Color.Black,
                Layout = new FlexLayout()
                {
                    ItemsAlignment = FlexLayout.AlignmentType.Center,
                    Justification = FlexLayout.FlexJustification.Center,
                },
            };

            TextLabel label = new TextLabel()
            {
                Text = "[ page "+i+" ]",
                PixelSize = 24,
                TextColor = Color.White,
            };
            page.Add(label);

            scrollContainer.Add(page);

            detector.Attach(page);
            detector.Detected += OnLongPressDetected;
        }
    }

    void OnLongPressDetected(object source, LongPressGestureDetector.DetectedEventArgs args)
    {
        Tizen.Log.Error("NUI","LongPressDetected !!!\n");

        if(editing)
        {
            return;
        }

        editing = true;
        detector.Detach(scrollContainer.Children[scroll.CurrentPage]);

        if(!isEditMode)
        {
            isEditMode = true;

            if(editModeAnimation == null)
            {
                editModeAnimation = new Animation(ANIMATION_PLAY_TIME);
                editModeAnimation.Finished += OnAnimationFinished;
            }

            editModeAnimation.AnimateTo(scroll,"ScaleY", SIZE_FACTOR);

            float currentPage = scroll.CurrentPage;
            float oldPageSize = Window.Instance.WindowSize.Width;
            float newPageSize = oldPageSize * SIZE_FACTOR;
            float pageSizeDiff = (oldPageSize - newPageSize) / 2.0f;
            float newPositionXOfCenter = oldPageSize * currentPage + pageSizeDiff;
            float expectedMargin = newPositionXOfCenter - (EDIT_PADDING + newPageSize) * currentPage;

            for( int i = 0; i< scrollContainer.Children.Count; i++)
            {
                scrollContainer.Children[i].BackgroundColor = Color.White;
                TextLabel label = scrollContainer.Children[i].Children[0] as TextLabel;
                label.TextColor = Color.Black;

                float postionX = expectedMargin + (EDIT_PADDING + newPageSize)*i - pageSizeDiff;
                editModeAnimation.AnimateTo(scrollContainer.Children[i],"ScaleX", SIZE_FACTOR);
                editModeAnimation.AnimateTo(scrollContainer.Children[i],"PositionX", postionX);
            }

            editModeAnimation.Play();
        }
        else if(isEditMode)
        {
            isEditMode = false;

            editModeAnimation.AnimateTo(scroll,"ScaleY", 1.0f);
            editModeAnimation.AnimateTo(scrollContainer,"PositionX", -Window.Instance.WindowSize.Width * scroll.CurrentPage);

            for( int i = 0; i< scrollContainer.Children.Count; i++)
            {
                float postionX = Window.Instance.WindowSize.Width * i;
                editModeAnimation.AnimateTo(scrollContainer.Children[i],"ScaleX", 1.0f);
                editModeAnimation.AnimateTo(scrollContainer.Children[i],"PositionX", postionX);
            }
            editModeAnimation.Play();
        }
    }

    private void OnAnimationFinished(object sender, EventArgs e)
    {
        editModeAnimation.Clear();
        detector.Attach(scrollContainer.Children[scroll.CurrentPage]);
        editing = false;

        if(!isEditMode)
        {
            for( int i = 0; i< scrollContainer.Children.Count; i++)
            {
                scrollContainer.Children[i].BackgroundColor = Color.Black;
                TextLabel label = scrollContainer.Children[i].Children[0] as TextLabel;
                label.TextColor = Color.White;
            }
        }
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

