
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

        private Mode mode = Mode.NormalMode;
        private int panScreenPosition = 0;


        public RotarySelector()
        {

            rotarySelectorManager = new RotarySelectorManager(new Size(360, 360));
            this.Add(rotarySelectorManager.GetRotaryLayerView());

            longPressDetector = new LongPressGestureDetector();
            longPressDetector.Detected += Detector_Detected;
            longPressDetector.Attach(rotarySelectorManager.GetRotaryLayerView().GetMainText());

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
            RotaryTouchController controller = rotarySelectorManager.GetRotaryTouchController();
            if (mode == Mode.EditMode && controller.SelectedItem != null)
            {
                panScreenPosition = 0;
                rotarySelectorManager.IsPaging = false;
                return;
            }

            if (controller.IsProcessing || rotarySelectorManager.isAnimating() || !rotarySelectorManager.isDetector())
            {
                panScreenPosition = 0;
                rotarySelectorManager.IsPaging = false;
                rotarySelectorManager.SetPanDetector();
                return;
            }
            switch (e.PanGesture.State)
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
                        rotarySelectorManager.IsPaging = false;
                        break;
                    }

                case Gesture.StateType.Continuing:

                    break;
                case Gesture.StateType.Started:
                    {
                        rotarySelectorManager.IsPaging = true;
                        panScreenPosition = (int)e.PanGesture.ScreenPosition.X;
                        break;
                    }
            }

        }

        private void Detector_Detected(object source, LongPressGestureDetector.DetectedEventArgs e)
        {
            if (GetCurrentMode() == Mode.NormalMode)
            {
                SetEditMode();
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
            longPressDetector.Attach(item);
        }

        public void InsertItem(int index, RotarySelectorItem item)
        {
            rotarySelectorManager.InsertItem(index, item);
            longPressDetector.Attach(item);
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

        public Mode GetCurrentMode()
        {
            return mode;
        }

        public void SetEditMode()
        {
            mode = Mode.EditMode;
            rotarySelectorManager.SetRotarySelectorMode(true);
            CallEditModeEntered();
        }

        public void SetNormalMode()
        {
            mode = Mode.NormalMode;
            rotarySelectorManager.SetRotarySelectorMode(false);
            CallEditModExited();
        }

        internal void StartAppsAnimation()
        {
            rotarySelectorManager.StartAppsAnimation();
        }

        public enum Mode
        {
            NormalMode,
            EditMode
        }

        //When the user entered to the editing mode. 
        private event EventHandler<EventArgs> editModeEnteredHandler;

        private void CallEditModeEntered()
        {
            EventArgs e = new EventArgs();

            if (editModeEnteredHandler != null)
            {
                editModeEnteredHandler(this, e);
            }
        }

        public event EventHandler<EventArgs> EditModeEntered
        {
            add
            {
                editModeEnteredHandler += value;
            }
            remove
            {
                editModeEnteredHandler -= value;
            }
        }


        private event EventHandler<EventArgs> editModeExitedHandler;

        private void CallEditModExited()
        {
            EventArgs e = new EventArgs();

            if (editModeExitedHandler != null)
            {
                editModeExitedHandler(this, e);
            }
        }

        public event EventHandler<EventArgs> EditModeExited
        {
            add
            {
                editModeExitedHandler += value;
            }
            remove
            {
                editModeExitedHandler -= value;
            }
        }

    }
}