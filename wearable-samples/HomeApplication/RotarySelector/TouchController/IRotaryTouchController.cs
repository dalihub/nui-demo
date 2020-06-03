
using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWHome
{
    public class RotaryTouchController
    { 
        public bool IsProcessing { get; set; }

        public RotarySelectorItem SelectedItem { get; set; }

        public delegate void NotifyManager(RotarySelectorItem item, int opt);
        public event NotifyManager OnNotify;

        private Timer processingCheckingTimer;

        public RotaryTouchController()
        {
            processingCheckingTimer = new Timer(180);
            processingCheckingTimer.Tick += FinishAnimationTick;
        }

        public bool ProcessTouchEvent(object source, View.TouchEventArgs e)
        {
            if (source is RotarySelectorItem)
            {
                RotarySelectorItem item = source as RotarySelectorItem;
                if ((e.Touch.GetState(0) == PointStateType.Down))
                {
                    if (ProcessTouchDownEvent(item))
                    {
                        CallOnNotify(item, 1);
                    }
                }
                else if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    Tizen.Log.Error("MYLOG", "touch up process");
                    if(ProcessTouchUpEvent(item))
                    {
                    }
                }
                else if ((e.Touch.GetState(0) == PointStateType.Motion))
                {
                    if(ProcessMotionEvent(item))
                    {
                        CallOnNotify(item, 0);
                    }
                }
            }
            return false;
        }

        public virtual bool ProcessTouchDownEvent(RotarySelectorItem item)
        {
            return false;
        }
        public virtual bool ProcessTouchUpEvent(RotarySelectorItem item)
        {
            return false;
        }

        public virtual bool ProcessMotionEvent(RotarySelectorItem item)
        {
            return false;
        }

        protected void StartCheckingTimer()
        {
            processingCheckingTimer.Stop();
            processingCheckingTimer.Start();
        }

        protected void StopCheckingTimer()
        {
            IsProcessing = false;
            processingCheckingTimer.Stop();
        }
        private bool FinishAnimationTick(object source, Timer.TickEventArgs e)
        {
            IsProcessing = false;
            return false;
        }

        protected void CallOnNotify(RotarySelectorItem item, int opt)
        {
            if(OnNotify != null)
            {
                OnNotify(item, opt);
            }
        }
    }
}