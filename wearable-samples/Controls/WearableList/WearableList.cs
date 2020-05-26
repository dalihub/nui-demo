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
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI.Wearable;

class WearableListExample : NUIApplication
{

    class ListData
    {
        public enum ItemType
        {
            Normal,
            Header,
        };

        public string Text { get; set; } = "";
        public ItemType Type { get; set; } = ItemType.Normal;
    }

    class ListItem : RecycleItem
    {

        public override void OnFocusLost()
        {
            BackgroundColor = Color.Transparent;
        }

        public override void OnFocusGained()
        {
            BackgroundColor = new Color("#333333A3");
        }

        public ListItem()
        {
            Size = new Size(360, 110);
            CornerRadius = 45;

            Text = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.White,
                PixelSize = 32,
            };
            Add(Text);

            Header = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.Green,
                PixelSize = 32,
            };
            Add(Header);
        }

        public TextLabel Text { get; set; }
        public TextLabel Header { get; set; }
    }

    class ListAdapter : RecycleAdapter
    {
        public override RecycleItem CreateRecycleItem()
        {
            ListItem item = new ListItem();
            return item;
        }

        public override void BindData(RecycleItem item)
        {
            ListItem target = item as ListItem;
            ListData data = Data[target.DataIndex] as ListData;

            if (data.Type == ListData.ItemType.Header)
            {
                target.Text.Hide();
                target.Header.Show();
                target.Header.Text = data.Text;
            }
            else
            {
                target.Header.Hide();
                target.Text.Show();
                target.Text.Text = data.Text;
                target.BackgroundColor = Color.Transparent;
            }
        }
    }

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();
        List<object> data = new List<object>();

        data.Add(new ListData()
        {
            Text = "LIST HEADER",
            Type = ListData.ItemType.Header,
        });

        for (int i = 0; i < 30; i++)
        {
            data.Add(new ListData()
            {
                Text = "LIST ITEM [" + i + "]",
            });
        }

        ListAdapter adapter = new ListAdapter();
        adapter.Data = data;

        WearableList wearableList = new WearableList(adapter);
        wearableList.Size = new Size(360, 360);
        wearableList.ScrollAvailableArea = new Vector2(
            wearableList.ListLayoutManager.StepSize,
            wearableList.ListLayoutManager.StepSize * (data.Count - 1));
        wearableList.SetFocus(1, false);

        NUIApplication.GetDefaultWindow().GetDefaultLayer().Add(wearableList);
        // Respond to key events
        NUIApplication.GetDefaultWindow().KeyEvent += OnKeyEvent;
    }

    /// <summary>
    /// Called when any key event is received.
    /// Will use this to exit the application if the Back or Escape key is pressed
    /// </summary>
    private void OnKeyEvent(object sender, Window.KeyEventArgs eventArgs)
    {
        if (eventArgs.Key.State == Key.StateType.Down)
        {
            switch (eventArgs.Key.KeyPressedName)
            {
                case "Escape":
                case "Back":
                    {
                        Exit();
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        WearableListExample example = new WearableListExample();
        example.Run(args);
    }
}

