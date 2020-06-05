
using NUIWHome.Common;
using System;
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWHome
{
    /// <summary>
    /// RotaryItemWrapper
    /// </summary>
    public class RotaryItemWrapper
    {
        private int currentIndex;
        private RotarySelectorItem rotaryItem;

        /// <summary>
        /// Creates a new instance of a RotaryItemWrapper. Set current index.
        /// </summary>
        public RotaryItemWrapper(int index)
        {
            currentIndex = index;
        }

        /// <summary>
        /// Get current index
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
        }

        /// <summary>
        /// Set/Get current rotary item
        /// </summary>
        public RotarySelectorItem RotaryItem
        {
            set
            {
                rotaryItem = value;
                rotaryItem.AddToParent();
                rotaryItem.CurrentIndex = CurrentIndex;
                rotaryItem.Position = GetRotaryPosition(CurrentIndex + 1);
            }
            get
            {
                return rotaryItem;
            }
        }

        /// <summary>
        /// GetCurrent position by idx
        /// </summary>
        public Position GetRotaryPosition(float idx, bool cw = true, int rad = 139)
        {
            float rot = ((float)Math.Cos((float)idx / ApplicationConstants.MAX_TRAY_COUNT * 2 * Math.PI - Math.PI / 2)) * (cw ? 1 : -1);

            float x = (float)(ApplicationConstants.SCREEN_SIZE_RADIUS + rad * rot);
            float y = (float)(ApplicationConstants.SCREEN_SIZE_RADIUS + rad * Math.Sin((float)idx / ApplicationConstants.MAX_TRAY_COUNT * 2 * Math.PI - Math.PI / 2));
            return new Position(x, y);
        }
    }
}