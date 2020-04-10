
using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;
namespace NUIWHome
{
    public class RotaryTouchEditMode : RotaryTouchController
    {
        public RotaryTouchEditMode()
        {
        }

        public override bool ProcessTouchDownEvent(RotarySelectorItem item)
        {
            if(SelectedItem == null)
            {
                SelectedItem = item;
                SelectedItem.LowerToBottom();
                return true;
            }
            return false;

        }
        public override bool ProcessMotionEvent(RotarySelectorItem item)
        {
            if(item == SelectedItem)
            {
                return false;
            }

            if (!IsProcessing && SelectedItem != null)
            {
                IsProcessing = true;
                StartCheckingTimer();
                return true;
            }
            return false;
        }

    }
}