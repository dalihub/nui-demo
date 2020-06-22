
/* Copyright (c) 2020 Samsung Electronics Co., Ltd.
.*
.* Licensed under the Apache License, Version 2.0 (the "License");
.* you may not use this file except in compliance with the License.
.* You may obtain a copy of the License at
.*
.* http://www.apache.org/licenses/LICENSE-2.0
.*
.* Unless required by applicable law or agreed to in writing, software
.* distributed under the License is distributed on an "AS IS" BASIS,
.* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
.* See the License for the specific language governing permissions and
.* limitations under the License.
.*
.*/

using NUIWHome.Common;
using System;
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWHome
{
    /// <summary>
    /// RotarySelector delete badge
    /// </summary>
    public class RotaryBadege : View
    {
        /// <summary>
        /// </summary>
        public int CurrentIndex { get; set; }
        public int PrevIndex { get; set; }
        public TextLabel number;

        /// <summary>
        /// Creates a new instance of a RotaryDeleteBadege.
        /// </summary>
        public RotaryBadege(int res)
        {
            Size = new Size(30, 30);
            ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;
            //BackgroundColor = Color.Red;
            if (res == 0)
            {
                BackgroundImage = CommonResource.GetResourcePath() + "b_home_badge_delete_icon.png";
            }
            else if(res == 1)
            {
                BackgroundImage = CommonResource.GetResourcePath() + "b_home_screen_badge.png";
                number = new TextLabel()
                {
                    Size = new Size(30, 30),
                    TextColor = Color.White,
                    PointSize = 4.0f,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                };
                this.Add(number);
            }
            PositionUsesPivotPoint = true;
        }

        public void SetBadgeNumber(int i)
        {
            number.Text = "" + i;
        }

        public void SetRightSide()
        {
            Position = new Position(8, -9);
            PivotPoint = Tizen.NUI.PivotPoint.TopRight;
            ParentOrigin = Tizen.NUI.ParentOrigin.TopRight;
        }

        public void SetLeftSide()
        {
            Position = new Position(-12, -9);
            PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
        }
    }
}