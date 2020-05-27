using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI.Wearable;

namespace WearableSample
{
    class MoreOption : Control
    {
        private ImageView buttonImage;
        private View tocuhEffectView;
        private Animation touchEffectAnimation;

        public bool IsPressed { get; private set; } = false;

        public void ShowTouchEffect()
        {
            IsPressed = true;
            touchEffectAnimation.Stop();
            touchEffectAnimation.Clear();

            touchEffectAnimation.AnimateTo(tocuhEffectView, "scale", new Vector3(1.0f, 1.0f, 1.0f));
            touchEffectAnimation.Play();
        }

        public void HideTouchEffect()
        {
            IsPressed = false;
            touchEffectAnimation.Stop();
            touchEffectAnimation.Clear();

            touchEffectAnimation.AnimateTo(tocuhEffectView, "scale", new Vector3(0.0f, 0.0f, 0.0f));
            touchEffectAnimation.Play();
        }

        public MoreOption()
        {
            Size = new Size(100, 140);
            tocuhEffectView = new View()
            {
                Size = new Size(200, 200),
                CornerRadius = 100,
                // BackgroundColor = new Color("#FFFFFF0F"),
                BackgroundColor = new Color("#FFFFFF0F"),
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                Scale = new Vector3(0.0f, 0.0f, 0.0f),
            };

            Add(tocuhEffectView);

            buttonImage = new ImageView()
            {
                ResourceUrl = "./res/images/more_option.png",
                Size = new Size(9, 36),
                Position = new Position(-16, 0),
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
            };

            tocuhEffectView.RightFocusableView = buttonImage;

            touchEffectAnimation = new Animation(100);

            Add(buttonImage);
        }
    }
}