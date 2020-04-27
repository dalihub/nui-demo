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
using Tizen.NUI.Components.Extension;

public class ComponentExample : NUIApplication
{
    public ComponentExample() : base()
    {
    }

    protected SwitchStyle GetSwitchStyle()
    {
        return new LottieSwitchStyle
        {
            LottieUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "nui_wearable_switch_icon.json",
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

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        var button = new Switch(GetSwitchStyle())
        {
            Size = new Size(200, 200),
            PositionUsesPivotPoint = true,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
        };
        window.Add(button);
    }

    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}
