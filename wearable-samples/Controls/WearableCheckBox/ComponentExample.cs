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
using Tizen.NUI.Components;
using Tizen.NUI.Components.Extension;
using Tizen.NUI.BaseComponents;

public class ComponentExample : NUIApplication
{
    public ComponentExample() : base()
    {
    }

    protected ButtonStyle GetWearableCheckBoxStyle()
    {
        return new LottieButtonStyle
        {
            LottieUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "nui_wearable_checkbox_icon.json",
            PlayRange = new Selector<LottieFrameInfo>
            {
                Selected = (0, 18),
                Normal = (19, 36)
            },
            Opacity = new Selector<float?>
            {
                Other = 1.0f,
                Pressed = 0.6f,
                Disabled = 0.3f,
            },
        };
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Tizen.NUI.Components.StyleManager.Instance.Theme = "wearable";

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        var button1 = new CheckBox(GetWearableCheckBoxStyle())
        {
            Size = new Size(100, 100),
            Position = new Position(0, -50),
            PositionUsesPivotPoint = true,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            IsSelected = true,
        };
        window.Add(button1);

        var button2 = new CheckBox(GetWearableCheckBoxStyle())
        {
            Size = new Size(100, 100),
            Position = new Position(0, 50),
            PositionUsesPivotPoint = true,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
        };
        window.Add(button2);

        var group = new CheckBoxGroup();
        group.Add(button1);
        group.Add(button2);
    }

    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}
