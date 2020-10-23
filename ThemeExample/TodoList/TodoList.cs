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

public class NUISampleApplication : NUIApplication
{
    protected override void OnCreate()
    {
        base.OnCreate();

        Initialize();
    }

    Theme themeCommon, themeGreen;
    int clickCount = 0;
    TextField inputField;
    View itemContainer;

    void Initialize()
    {
        var root = NUIApplication.GetDefaultWindow();
        var resourcePath = Tizen.Applications.Application.Current.DirectoryInfo.Resource;

        // Load and apply theme from xaml file.
        themeCommon = new Theme(resourcePath + "Theme/Common.xaml");
        themeGreen = new Theme(resourcePath + "Theme/Green.xaml");
        ThemeManager.ApplyTheme(themeCommon);


        var themeChangeButton = new Button() {
            WidthResizePolicy = ResizePolicyType.FillToParent,
            SizeHeight = 70,
            Text = "Click to change theme",
        };
        themeChangeButton.Clicked += OnClicked;
        root.Add(themeChangeButton);
        root.BackgroundColor = Color.White;

        root.Add(new TextLabel
        {
            Text = "TODO List",
            StyleName = "Title",
            Position = new Position(30, 120)
        });

        itemContainer = new View
        {
            StyleName = "Background",
            Size = new Size(410, 500),
            Position = new Position(30, 170),
            Layout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Vertical
            },
        };
        root.Add(itemContainer);

        var inputs = new View()
        {
            Margin = new Extents(20),
        };
        itemContainer.Add(inputs);

        inputField = new TextField
        {
            StyleName = "InputField",
            Size = new Size(280, 40),
            PlaceholderText = " Next to do...",
            VerticalAlignment = VerticalAlignment.Center,
        };
        inputs.Add(inputField);

        var addButton = new Button
        {
            StyleName = "AddButton",
            Size = new Size(80, 40),
            Text = "Add",
            PositionX = 290,
        };
        addButton.Clicked += OnClickedAdd;
        inputs.Add(addButton);

        AddItem("Grocery order");
        AddItem("Get tickets for Friday graduation", true);
        AddItem("Call about summer camp enrollment");
    }

    private void OnClicked(object target, ClickedEventArgs args)
    {
        clickCount++;

        if ((clickCount) % 2 == 0) ThemeManager.ApplyTheme(themeCommon);
        else ThemeManager.ApplyTheme(themeGreen);
    }

    private void OnClickedAdd(object target, ClickedEventArgs args)
    {
        // Tizen.Log.Info("JYJY", $"{inputField.Text}\n");
        var newText = inputField.Text;
        if (String.IsNullOrEmpty(newText)) return;

        AddItem(newText);

        inputField.Text = "";
    }

    public void AddItem(string text, bool done = false)
    {
        var newItem = new View()
        {
            Margin = new Extents(20, 20, 10, 10),
            WidthResizePolicy = ResizePolicyType.FillToParent,
        };
        itemContainer.Add(newItem);

        newItem.Add(new CheckBox()
        {
            StyleName = "ItemCheckBox",
            Text = text,
            SizeHeight = 30,
            WidthResizePolicy = ResizePolicyType.FillToParent,
            IsSelected = done,
        });
    }

    public void OnKeyEvent(object sender, Window.KeyEventArgs e)
    {
        if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
        {
            Exit();
        }
    }


    static void Main(string[] args)
    {
        NUISampleApplication example = new NUISampleApplication();
        example.Run(args);
    }
}
