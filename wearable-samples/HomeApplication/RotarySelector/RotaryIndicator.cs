
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
    /// RotarySelector Indicator
    /// </summary>
    public class RotaryIndicator : View
    {
        /// <summary>
        /// Current selected index value, defalut value is 0(first item)
        /// </summary>
        public int CurrentIndex { get; set; }
        public int PrevIndex { get; set; }

        /// <summary>
        /// Creates a new instance of a RotaryIndicator.
        /// </summary>
        public RotaryIndicator()
        {
            Size = new Size(10, 10);
            BackgroundColor = Color.White;
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;
            CornerRadius = 5.0f;
        }

        /// <summary>
        /// Set current position of indicator using rotary animation. call PlayRotaryAnimation function.
        /// </summary>
        public void SetRotaryPosition(int toIdx)
        {
            PrevIndex = CurrentIndex;
            CurrentIndex = toIdx;
        }

        public void SetCurrentPosition()
        {
            this.Position = GetRotaryPosition(CurrentIndex + 1);
        }

        public bool isNotMoving()
        {
            return (PrevIndex == CurrentIndex);
        }

        /// <summary>
        /// GetCurrent position by idx
        /// </summary>
        public Position GetRotaryPosition(float idx, int rad = 90)
        {
            float x = (float)(ApplicationConstants.SCREEN_SIZE_RADIUS + rad * Math.Cos((float)idx / ApplicationConstants.MAX_TRAY_COUNT * 2 * Math.PI - Math.PI / 2));
            float y = (float)(ApplicationConstants.SCREEN_SIZE_RADIUS + rad * Math.Sin((float)idx / ApplicationConstants.MAX_TRAY_COUNT * 2 * Math.PI - Math.PI / 2));
            return new Position(x, y);
        }
    }
}