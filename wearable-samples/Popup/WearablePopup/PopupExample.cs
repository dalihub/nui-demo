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

using L = Tizen.Log;
using W = Tizen.NUI.Wearable;

public class PopupExample : NUIApplication
{
    private W.Popup myPopup1;
    private Window myWindow;
    const string tag = "NUITEST";
    private string resourcePath;
    private const int BUTTON_WIDTH = 73;
    private const int BUTTON_HEIGHT = 121;
    private Color BUTTON_COLOR = new Color(11.0f / 255.0f, 6.0f / 255.0f, 92.0f / 255.0f, 1);
    private const int BUTTON_ICON_WIDTH = 50;
    private const int BUTTON_ICON_HEIGHT = 50;

    private TextLabel myContent1;
    private TextLabel t1;

    public PopupExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        resourcePath = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
        myWindow = NUIApplication.GetDefaultWindow();

        //two buttons popup, type1
        ButtonStyle buttonStyle = new ButtonStyle()
        {
            Icon = new ImageViewStyle()
            {
                ResourceUrl = new Selector<string>()
                {
                    All = resourcePath + "images/PopupTest/tw_ic_popup_btn_bg.png",
                },
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
                Color = new Selector<Color>()
                {
                    All = BUTTON_COLOR,
                },
            },
            Overlay = new ImageViewStyle()
            {
                ResourceUrl = new Selector<string>()
                {
                    All = resourcePath + "images/PopupTest/tw_ic_popup_btn_check.png",
                },
                Size = new Size(BUTTON_ICON_WIDTH, BUTTON_ICON_HEIGHT),
                Color = new Selector<Color>()
                {
                    All = Color.Cyan,
                },
            },
        };
        Button leftButton = new Button(buttonStyle)
        {
            Name = "LeftButton",
            Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
        };

        myPopup1 = new W.Popup();
        myPopup1.AppendButton("LeftButton", leftButton);

        buttonStyle.Overlay.ResourceUrl = new Selector<string>()
        {
            All = resourcePath + "images/PopupTest/tw_ic_popup_btn_delete.png",
        };
        Button rightButton = new Button(buttonStyle)
        {
            Name = "RightButton",
            Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
        };
        myPopup1.AppendButton("RightButton", rightButton);

        TextLabel t = myPopup1.GetTitle();
        t.Text = "User consent";

        myContent1 = new TextLabel();
        myContent1.Text = "Agree? \n GPS location \n and use of your \n location data \n are controlled \n by the applications you \n \n \n this is additional text!";
        myContent1.MultiLine = true;
        myContent1.Size = new Size(200, 800);
        myContent1.PointSize = 6;
        myContent1.HorizontalAlignment = HorizontalAlignment.Center;
        myContent1.VerticalAlignment = VerticalAlignment.Top;
        myContent1.TextColor = Color.White;
        myContent1.PositionUsesPivotPoint = true;
        myContent1.ParentOrigin = ParentOrigin.Center;
        myContent1.PivotPoint = PivotPoint.Center;
        myPopup1.AppendContent("ContentText", myContent1);
        leftButton.ClickEvent += LeftButton_ClickEvent;
        rightButton.ClickEvent += RightButton_ClickEvent;
        myPopup1.OutsideClicked += Mp_OutsideClicked;

        myPopup1.ContentContainer.WidthResizePolicy = ResizePolicyType.FitToChildren;
        myPopup1.ContentContainer.HeightResizePolicy = ResizePolicyType.FitToChildren;

        myPopup1.Post(myWindow);
        myPopup1.AfterDissmising += MyPopup1_AfterDissmising;
    }

    private void MyPopup1_AfterDissmising(object sender, EventArgs e)
    {
        t1 = new TextLabel("please go back to main menu!")
        {
            PositionUsesPivotPoint = true,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PointSize = 8,
            MultiLine = true,
        };
        NUIApplication.GetDefaultWindow().Add(t1);
    }

    private void RightButton_ClickEvent(object sender, Button.ClickEventArgs e)
    {
        myPopup1.Dismiss();
    }

    private void LeftButton_ClickEvent(object sender, Button.ClickEventArgs e)
    {
        myContent1.TextColor = Color.Yellow;
    }

    private void Mp_OutsideClicked(object sender, EventArgs e)
    {
        var popup = sender as W.Popup;
        if (popup != null)
        {
            myPopup1.Dismiss();
        }
    }

    static void Main(string[] args)
    {
        PopupExample example = new PopupExample();
        example.Run(args);
    }
}
