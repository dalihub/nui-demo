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

public class ActivityIndicatorExample : NUIApplication
{
    Loading loading;

    public ActivityIndicatorExample() : base(new Size2D(360, 360), new Position2D(0, 0))
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        var window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        string[] imageArray = new string[90];
        for (int i = 0; i < 90; i++)
        {
            imageArray[i] = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "activityindicator_small" + i.ToString("00000") + ".png";
        }

        loading = new Loading();
        loading.Size = new Size(76, 76);

        // Set the image array to ImageArray property
        loading.ImageArray = imageArray;

        // Positioning it to the center
        loading.ParentOrigin = ParentOrigin.Center;
        loading.PivotPoint = PivotPoint.Center;
        loading.PositionUsesPivotPoint = true;

        window.Add(loading);
    }

    static void Main(string[] args)
    {
        ActivityIndicatorExample example = new ActivityIndicatorExample();
        example.Run(args);
    }
}
