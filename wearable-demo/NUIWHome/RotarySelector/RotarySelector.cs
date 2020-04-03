
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;

namespace NUIWHome
{
    /// <summary>
    /// RotarySelector for w-home
    /// </summary>
    public class RotarySelector : View
    {
        private int DRAG_DISTANCE = 80;

        private RotarySelectorManager rotarySelectorManager;

        private LongPressGestureDetector longPressDetector;
        private PanGestureDetector panDetector;

        private bool isEditMode = false;
        private int panScreenPosition = 0;


        public RotarySelector()
        {
            
            rotarySelectorManager = new RotarySelectorManager(new Size(360, 360));
            this.Add(rotarySelectorManager.GetRotaryLayerView());

            longPressDetector = new LongPressGestureDetector();
            longPressDetector.Detected += Detector_Detected;

            panDetector = new PanGestureDetector();
            panDetector.Attach(this);
            panDetector.Detected += PanDetector_Detected;

            this.WheelEvent += RotarySelector_WheelEvent;

            this.Add(rotarySelectorManager.GetRotaryPagination());
        }

        private bool RotarySelector_WheelEvent(object source, WheelEventArgs e)
        {
            if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
            {
                rotarySelectorManager.SelectItemByWheel(e.Wheel.Direction);
            }
            return false;
        }

        private void PanDetector_Detected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            if(isEditMode)
            {
                return;
            }
            switch(e.PanGesture.State)
            {
                case Gesture.StateType.Finished:
                {

                    int mouse_nextX = (int)e.PanGesture.ScreenPosition.X;
                    int distance = mouse_nextX - panScreenPosition;
                    if (distance > DRAG_DISTANCE)
                    {
                        rotarySelectorManager.NextPage();
                    }
                    else if (distance < -DRAG_DISTANCE)
                    {
                        rotarySelectorManager.PrevPage();
                    }
                    
                    panScreenPosition = 0;
                    break;
                }

                case Gesture.StateType.Continuing:
                    break;
                case Gesture.StateType.Started:
                {
                    panScreenPosition = (int)e.PanGesture.ScreenPosition.X;
                    break;
                }
            }
            
        }

        private void Detector_Detected(object source, LongPressGestureDetector.DetectedEventArgs e)
        {
            if(!isEditMode)
            {
                IsEditMode = true;
            }
        }

        public List<RotarySelectorItem> GetRotarySelectorItems()
        {
            return rotarySelectorManager.GetRotaryLayerView().RotaryItemList;
        }

        public void AppendItem(RotarySelectorItem item)
        {
            rotarySelectorManager.AppendItem(item);
            longPressDetector.Attach(item);
        }

        public void PrependItem(RotarySelectorItem item)
        {
            rotarySelectorManager.PrependItem(item);
        }

        public void InsertItem(int index, RotarySelectorItem item)
        {
            rotarySelectorManager.InsertItem(index, item);
        }

        public void DeleteItem(RotarySelectorItem item)
        {
            rotarySelectorManager.DeleteItem(item);
        }

        public void DeleteItemIndex(int index)
        {
            rotarySelectorManager.DeleteItemIndex(index);
        }

        public void ClearItem()
        {
            rotarySelectorManager.ClearItem();
        }

        public bool IsEditMode
        {
            get
            {
                return isEditMode;
            }
            set
            {
                isEditMode = value;
                rotarySelectorManager.SetRotarySelectorMode(isEditMode);
            }
        }

        internal void StartAppsAnimation()
        {
            rotarySelectorManager.StartAppsAnimation();
        }

    }
}