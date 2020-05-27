using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI.Wearable;

namespace WearableSample
{
    class MessageItem : RecycleItem
    {
        private Animation mFocusAnimation;
        private bool isFocusGained = false;
        public MessageItem()
        {
            Size = new Size(360, 172);
            Layout = new AbsoluteLayout()
            {
                SetPositionByLayout = false,
            };

            BackgroundView = new View()
            {
                CornerRadius = 54,
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                BackgroundColor = new Color("#FFFFFF0F"),
                Opacity = 0.0f,
                Size = new Size(360, 150),
            };

            Title = new TextLabel()
            {
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                TextColor = Color.White,
                Opacity = 0.7f,
                PixelSize = 38.0f,
                Size = new Size(328, 50),
                Position = new Position(-2, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Layout = new AbsoluteLayout() { SetPositionByLayout = false },
                AutoScrollStopMode = AutoScrollStopMode.Immediate,
                AutoScrollLoopCount = 1,
            };

            SubTitle = new TextLabel()
            {
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                TextColor = Color.White,
                Opacity = 0.0f,
                MultiLine = true,
                PixelSize = 24.0f,
                Ellipsis = true,
                Size = new Size(340, 64),
                Position = new Position(-2, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Layout = new AbsoluteLayout() { SetPositionByLayout = false },
            };

            TimeStamp = new TextLabel()
            {
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                TextColor = Color.White,
                Opacity = 0.0f,
                PixelSize = 24.0f,
                Size = new Size(340, 32),
                Position = new Position(-2, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Layout = new AbsoluteLayout() { SetPositionByLayout = false },
            };

            Add(BackgroundView);
            Add(Title);
            Add(SubTitle);
            Add(TimeStamp);
        }

        public override void OnFocusGained()
        {
            Tizen.Log.Error("NUI", "[" + DataIndex + "] FOCUSE GAIN^ ===== \n");

            isFocusGained = true;
            if (mFocusAnimation == null)
            {
                mFocusAnimation = new Animation();
            }
            mFocusAnimation.Stop();
            mFocusAnimation.Clear();

            mFocusAnimation.Duration = 400;
            AlphaFunction timeCurve = new AlphaFunction(new Vector2(0.45f, 0.03f), new Vector2(0.41f, 1.0f));

            mFocusAnimation.AnimateTo(Title, "position", new Position(Title.Position.X, -50.0f), 0, 400, timeCurve);
            mFocusAnimation.AnimateTo(Title, "opacity", 1.0f, 0, 400, timeCurve);
            mFocusAnimation.AnimateTo(BackgroundView, "opacity", 1.00f, 0, 100, timeCurve);
            mFocusAnimation.AnimateTo(SubTitle, "opacity", 1.0f, 0, 400, timeCurve);
            mFocusAnimation.AnimateTo(SubTitle, "position", new Position(SubTitle.Position.X, 7f), 0, 400, timeCurve);
            mFocusAnimation.AnimateTo(TimeStamp, "opacity", 1.0f, 0, 400, timeCurve);
            mFocusAnimation.AnimateTo(TimeStamp, "position", new Position(SubTitle.Position.X, 58f), 0, 400, timeCurve);

            mFocusAnimation.Play();

            mFocusAnimation.Finished += (object sender, EventArgs e) =>
            {
                if (isFocusGained)
                {
                    Title.EnableAutoScroll = true;
                }
            };
        }

        public override void OnFocusLost()
        {
            Tizen.Log.Error("NUI", "[" + DataIndex + "] FOCUSE LOST ===== " + Title.EnableAutoScroll + "\n");
            isFocusGained = false;
            Title.EnableAutoScroll = false;

            if (mFocusAnimation == null)
            {
                mFocusAnimation = new Animation();
            }
            mFocusAnimation.Stop();
            mFocusAnimation.Clear();

            mFocusAnimation.Duration = 300;
            AlphaFunction timeCurve = new AlphaFunction(new Vector2(0.45f, 0.03f), new Vector2(0.41f, 1.0f));

            mFocusAnimation.AnimateTo(BackgroundView, "opacity", 0.00f, 0, 200, timeCurve);
            mFocusAnimation.AnimateTo(Title, "position", new Position(Title.Position.X, 0.0f), 0, 300, timeCurve);
            mFocusAnimation.AnimateTo(Title, "opacity", 0.7f, 0, 300, timeCurve);
            mFocusAnimation.AnimateTo(SubTitle, "opacity", 0.0f, 0, 300, timeCurve);
            mFocusAnimation.AnimateTo(SubTitle, "position", new Position(SubTitle.Position.X, 0.0f), 0, 300, timeCurve);
            mFocusAnimation.AnimateTo(TimeStamp, "opacity", 0.0f, 0, 300, timeCurve);
            mFocusAnimation.AnimateTo(TimeStamp, "position", new Position(SubTitle.Position.X, 0.0f), 0, 300, timeCurve);

            mFocusAnimation.Play();
        }

        public View BackgroundView { get; set; }
        public TextLabel Title { get; set; }
        public TextLabel SubTitle { get; set; }
        public TextLabel TimeStamp { get; set; }
    }

    class MessageListAdaptor : RecycleAdapter
    {
        public MessageListAdaptor()
        {

        }

        public override RecycleItem CreateRecycleItem()
        {
            return new MessageItem();
        }

        public override void BindData(RecycleItem item)
        {
            MessageItem target = item as MessageItem;
            Message data = Data[item.DataIndex] as Message;

            target.Title.Text = data.Sender;
            target.SubTitle.Text = data.Text;
            target.TimeStamp.Text = data.Time;
        }
    }

    class MessageList : WearableList
    {
        public MessageList() : base(new MessageListAdaptor() { Data = MessageDummy.Create(100) })
        {

        }
    }
}