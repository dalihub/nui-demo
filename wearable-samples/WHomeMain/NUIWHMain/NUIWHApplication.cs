/*
* Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
using System.Collections.Generic;
using WearableGallerySample;
using Tizen.Applications;

namespace NUIWHMain
{
    class NUIWHApplication : NUIApplication
    {
        private CircularRecycler homeMain;
        private NUIWHAdapter homeMainAdaper;
        private WidgetManagerViewer wmViewer;
        private Window defaultWindow;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
            InitializeDefaultUI();
        }

        public void Initialize()
        {
            defaultWindow = GetDefaultWindow();
            defaultWindow.KeyEvent += OnKeyEvent;

            wmViewer = new WidgetManagerViewer();
            wmViewer.AddWidgetEvent += Wmv_AddWidgetEvent;
        }

        public void InitializeDefaultUI()
        {
            homeMainAdaper = new NUIWHAdapter(new WidgetViewManager(this, ApplicationInfo.ApplicationId));

            homeMain = new CircularRecycler()
            {
                Size = new Size(360, 360),
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };

            homeMain.SetAdapter(homeMainAdaper);
            homeMain.SetLayoutManager(new DefaultLayoutManager());

            homeMain.AddWidgetEvent += OnWidgetAdd;

            homeMain.WheelEvent += OnWheelEvent;
            Window.Instance.Add(homeMain);
        }

        private bool Wmv_AddWidgetEvent(object source, View.TouchEventArgs e)
        {
            View view = source as View;
            homeMainAdaper.AddWidget(new FaceData(FaceData.FaceType.WIDGET, view.Name));
            homeMain.UpdateViewHolderOrder();
            return true;
        }

        public bool OnWidgetAdd(object sender, Button.ClickEventArgs e)
        {
            wmViewer.ShowViewer(Window.Instance);
            return false;
        }

        public bool OnNotifyAdd(object sender, View.TouchEventArgs e)
        {
            homeMainAdaper.AddNotify(new FaceData(FaceData.FaceType.NOTIFY, "New Message"));
            return false;
        }

        private bool OnWheelEvent(object sender, View.WheelEventArgs e)
        {
            if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
            {
                if (e.Wheel.Direction == 1)
                {
                    homeMain.Next();
                }
                else if (e.Wheel.Direction == -1)
                {
                    homeMain.Prev();
                }
            }
            return false;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                switch (e.Key.KeyPressedName)
                {
                    case "XF86Back":
                    case "Escape":
                    {
                        if(wmViewer.IsShowing())
                        {
                            wmViewer.HideViewer();
                            return;
                        }
                        Exit();
                        break;
                    }
                }
            }
        }
    }


}
