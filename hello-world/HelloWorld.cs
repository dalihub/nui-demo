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
using Tizen.NUI.Constants;

class HelloWorldExample : NUIApplication
{


    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        // Get the window instance and change background color
        Window window = Window.Instance;
        window.BackgroundColor = Color.White;

        TextLabel view1 = new TextLabel()
        {
            Size = new Size(200,200),
            BackgroundColor = Color.Red,
            Name = "1",
            Text = "1",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        view1.BackKeyPressed += OnBackKeyPressed;
        window.GetDefaultLayer().Add(view1);

        TextLabel view2 = new TextLabel()
        {
            Size = new Size(80,80),
            Position = new Position(90,10),
            BackgroundColor = Color.Green,
            Name = "2",
            Text = "2",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        view2.BackKeyPressed += OnBackKeyPressed;
        window.GetDefaultLayer().Add(view2);

        TextLabel view3 = new TextLabel()
        {
            Size = new Size(150,150),
            BackgroundColor = Color.Yellow,
            Name = "3",
            Text = "3",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        view3.BackKeyPressed += OnBackKeyPressed;
        view1.Add(view3);

        TextLabel view4 = new TextLabel()
        {
            Size = new Size(100,100),
            BackgroundColor = Color.White,
            Name = "4",
            Text = "4",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        view4.BackKeyPressed += OnBackKeyPressed;
        view3.Add(view4);

        TextLabel view5 = new TextLabel()
        {
            Size = new Size(60,60),
            Position = new Position(10,10),
            BackgroundColor = Color.Magenta,
            Name = "5",
            Text = "5",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        view5.BackKeyPressed += OnBackKeyPressed;
        view2.Add(view5);

        Layer newLayer = new Layer();
        newLayer.Name = "layer2";
        window.AddLayer(newLayer);

        TextLabel view6 = new TextLabel()
        {
            Size = new Size(300,300),
            BackgroundColor = Color.Cyan,
            Name = "6",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Text = "6",
            Opacity = 0.8f,
        };
        view6.BackKeyPressed += OnBackKeyPressed;

        newLayer.Add(view6);

        Layer newLayer2 = new Layer();
        newLayer2.Name = "layer3";
        window.AddLayer(newLayer2);
        newLayer2.LowerToBottom();

        TextLabel view7 = new TextLabel()
        {
            Size = new Size(350,350),
            BackgroundColor = Color.Black,
            Position = new Position(100,0),
            TextColor = Color.White,
            Name = "7",
            Text = "7",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Opacity = 0.8f,
        };
        view7.BackKeyPressed += OnBackKeyPressed;
        newLayer2.Add(view7);

        View buttonGroup = new View()
        {
            Size = new Size(480,100),
            BackgroundColor = Color.Red,
            Layout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Horizontal,
            },
            PositionUsesPivotPoint = true,
            ParentOrigin = Tizen.NUI.ParentOrigin.BottomCenter,
            PivotPoint = Tizen.NUI.PivotPoint.BottomCenter,
        };

        window.GetDefaultLayer().Add(buttonGroup);

        Button button1 = new Button()
        {
            Weight = 1,
            Text = "Chnage 6",
        };
        button1.ClickEvent += (object source, Button.ClickEventArgs args)=>
        {
            if(view6.IsOnWindow)
            {
                view6.Unparent();
            }
            else
            {
                newLayer.Add(view6);
            }
        };
        buttonGroup.Add(button1);

        Button button2 = new Button()
        {
            Weight = 1,
            Text = "Chnage 7",
        };
        button2.ClickEvent += (object source, Button.ClickEventArgs args)=>
        {
            if(view7.Visibility)
            {
                view7.Hide();
            }
            else
            {
                view7.Show();
            }
        };
        buttonGroup.Add(button2);

        Button button3 = new Button()
        {
            Weight = 1,
            Text = "Chnage 5",
        };
        button3.ClickEvent += (object source, Button.ClickEventArgs args)=>
        {
            if(view5.Visibility)
            {
                view5.Hide();
            }
            else
            {
                view5.Show();
            }
        };
        buttonGroup.Add(button3);
    }

    private void OnBackKeyPressed(object source, EventArgs args)
    {
        View target = source as View;
        Tizen.Log.Error("NUI","BACK KEY EVENT==== ["+target.Name+"]\n\n");
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