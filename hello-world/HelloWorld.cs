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

using System.Collections.Generic;

using Tizen.NUI.Constants;

class HelloWorldExample : NUIApplication
{
    class MyListItem : RecyclerList.ListItem
    {
        public MyListItem()
        {
            Layout = new FlexLayout()
            {
                ItemsAlignment = FlexLayout.AlignmentType.Center,
                Justification = FlexLayout.FlexJustification.Center,
            };

            WidthSpecification = 360;
            HeightSpecification = 115;
            BackgroundColor = Color.Cyan;
            Title = new TextLabel("[]")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                WidthSpecification = 360,
            };
            Add(Title);
        }

        public TextLabel Title{get;set;}
    }


    class MyAdapter : RecyclerList.ListAdapter
    {
        public override RecyclerList.ListItem CreateListItem()
        {
            return new MyListItem();
        }

        public override void BindData(RecyclerList.ListItem item, int index)
        {
            MyListItem target = item as MyListItem;
            target.Title.Text = "["+index+"]";
            Tizen.Log.Error("NUI","BIND DATA ====  "+index+"\n");
        }
    }

    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        Window window = Window.Instance;
        window.BackgroundColor = Color.White;

        MyAdapter adapter = new MyAdapter();
        List<object> data = new List<object>();

        for(int i = 0 ; i<100; i++)
        {
            data.Add("["+i+"]");
        }
        adapter.Data = data;

        RecyclerList list = new RecyclerList()
        {
            ScrollingDirection = ScrollableBase.Direction.Vertical,
            WidthSpecification = 360,
            HeightSpecification = 360,
            Layout = new LinearLayout(){
                LinearOrientation = LinearLayout.Orientation.Vertical
            },
            Adapter = adapter,
            BackgroundColor = Color.Green,
            FlickAnimationSpeed = 1.0f,
            FlickDistanceMultiplierRange = new Vector2(1.5f,4.0f),
        };

        window.GetDefaultLayer().Add(list);
    }

    private View test;

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

