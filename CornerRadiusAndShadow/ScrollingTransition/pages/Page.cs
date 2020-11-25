using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace Demo
{
    class Page : View
    {
        private Animation showAnimation;
        private Animation hideAnimation;
        public int PageNumber = 0;
        public object PageData { get; set; }

        public Page(object data)
        {
            PageData = data;
            Initialize();
        }

        public Page()
        {
            PageData = null;
            Initialize();
        }

        private void Initialize()
        {
            BackgroundColor = Color.White;
            Size = new Size(NUIApplication.GetDefaultWindow().WindowSize);
            Position = new Position(NUIApplication.GetDefaultWindow().WindowSize.Width, 0);
            Layout = new AbsoluteLayout()
            {
                SetPositionByLayout = false
            };

            showAnimation = new Animation(300);
            showAnimation.AnimateTo(this, "PositionX", 0 , new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine));
            showAnimation.Finished += (object source, EventArgs args) =>
            {
                OnShowFinished.Invoke(this, new EventArgs());
            };

            hideAnimation = new Animation(300);
            hideAnimation.AnimateTo(this, "PositionX", Size.Width, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine));
            hideAnimation.Finished += (object source, EventArgs args) =>
            {
                OnHideFinished.Invoke(this, new EventArgs());
            };
        }

        public void ShowPage()
        {
            hideAnimation.Stop();
            showAnimation.Play(); 
        }

        public void HidePage()
        {
            showAnimation.Stop();
            hideAnimation.Play();
        }

        public event EventHandler OnShowFinished;
        public event EventHandler OnHideFinished;
    }
}
