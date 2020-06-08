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

        string[] imageArray = new string[120];
        for (int i = 0; i < 120; i++)
        {
            imageArray[i] = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "activityindicator_full" + i.ToString("00000") + ".png";
        }

        loading = new Loading();
        loading.Size = new Size(360, 360);

        // Set the image array to Images property
        loading.Images = imageArray;

        window.Add(loading);
    }

    static void Main(string[] args)
    {
        ActivityIndicatorExample example = new ActivityIndicatorExample();
        example.Run(args);
    }
}
