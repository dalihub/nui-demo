
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace WearableSample
{
    public class MessageApplication : NUIApplication
    {
        private GaussianBlurView contentBlurView;
        private MessageList message;
        private MenuList menuPopup;
        private MoreOption optionButton;
        private Animation popupAnimation;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private void Initialize()
        {
            popupAnimation = new Animation(100);

            Layer root = NUIApplication.GetDefaultWindow().GetDefaultLayer();

            contentBlurView = new GaussianBlurView(40, 3.0f, PixelFormat.RGBA8888, 1.0f, 1.0f, false)
            {
                Size2D = new Size2D(360, 360),
            };
            root.Add(contentBlurView);

            message = new MessageList()
            {
                Size = new Size(360, 360),
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
            };
            contentBlurView.Add(message);

            menuPopup = new MenuList()
            {
                Size = new Size(360, 360),
            };
            menuPopup.Hide();

            menuPopup.TouchEvent += (object source, View.TouchEventArgs args) =>
            {
                if (args.Touch.GetState(0) == PointStateType.Finished)
                {
                    HidePopup();
                }
                return true;
            };

            root.Add(menuPopup);

            optionButton = new MoreOption()
            {
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
            };
            optionButton.TouchEvent += (object sender, View.TouchEventArgs args) =>
            {
                MoreOption target = args.Touch.GetHitView(0) as MoreOption;

                if (target)
                {
                    if (args.Touch.GetState(0) == PointStateType.Down && args.Touch.GetLocalPosition(0).X > 50)
                    {
                        optionButton.ShowTouchEffect();
                    }
                    else if (args.Touch.GetState(0) == PointStateType.Finished)
                    {
                        if (optionButton.IsPressed && args.Touch.GetLocalPosition(0).X > 50)
                        {
                            ShowPopup();
                        }
                        optionButton.HideTouchEffect();
                    }
                    else if (args.Touch.GetState(0) == PointStateType.Interrupted)
                    {
                        optionButton.HideTouchEffect();
                    }
                }

                return true;
            };

            contentBlurView.Add(optionButton);
        }

        public void ShowPopup()
        {
            popupAnimation.Stop();
            popupAnimation.Clear();

            optionButton.Hide();
            contentBlurView.Activate();

            popupAnimation.Duration = 200;

            AlphaFunction timeCurve = new AlphaFunction(new Vector2(0.45f, 0.03f), new Vector2(0.41f, 1.0f));
            popupAnimation.AnimateTo(message, "scale", new Vector3(0.8f, 0.8f, 1.0f), 0, 200, timeCurve);
            popupAnimation.Play();

            menuPopup.Show();
        }

        public void HidePopup()
        {
            popupAnimation.Stop();
            popupAnimation.Clear();

            optionButton.Show();
            contentBlurView.Deactivate();

            popupAnimation.Duration = 200;

            AlphaFunction timeCurve = new AlphaFunction(new Vector2(0.45f, 0.03f), new Vector2(0.41f, 1.0f));
            popupAnimation.AnimateTo(message, "scale", new Vector3(1.0f, 1.0f, 1.0f), 0, 200, timeCurve);
            popupAnimation.Play();

            menuPopup.Hide();
        }

        static void Main(string[] args)
        {
            MessageApplication example = new MessageApplication();
            example.Run(args);
        }
    }
}
