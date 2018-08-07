using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class NoLayoutExample : Example
    {
        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] s_images = new string[]
            {
                resources + "/images/application-icon-101.png",
                resources + "/images/application-icon-102.png",
                resources + "/images/application-icon-103.png",
                resources + "/images/application-icon-104.png"
            };
        }

        private View view;
        private ImageView helpImageView;
        PushButton helpButton;
        bool helpShowing = false;
        private List<PushButton> buttons = new List<PushButton>();

        public override void Create()
        {
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

            view = new View()
            {
                Size2D = new Size2D(476,500),
                BackgroundColor = Color.Green,
                Position2D = new Position2D(2, 0),
                ParentOrigin = ParentOrigin.BottomRight,
                PivotPoint = PivotPoint.BottomRight,
                PositionUsesPivotPoint = true,
            };
            window.Add(view);

            TextLabel textLabel = new TextLabel()
            {
                Size2D = new Size2D(274, 70),
                BackgroundColor = Color.Red,
                Position2D = new Position2D(0, -10),
                Text = "Enter password",
                ParentOrigin = ParentOrigin.BottomCenter,
                PivotPoint = PivotPoint.BottomCenter,
                PositionUsesPivotPoint = true,
            };
            view.Add(textLabel);

            TextField field = new TextField()
            {
                Size2D = new Size2D(120, 80),
                Position2D = new Position2D(150,85),
                BackgroundColor = Color.Cyan,
                PlaceholderText = "input something",
            };
            view.Add(field);

            CreateHelpButton();
            window.Add( helpButton );
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(helpImageView);
            helpShowing = false;
            window.Remove(helpButton);
            window.Remove(view);

            view = null;
        }

        private void CreateHelpButton()
        {
            helpButton = new PushButton();
            helpButton.LabelText = "Example Help";
            helpButton.PivotPoint = PivotPoint.TopLeft;
            helpButton.PositionUsesPivotPoint = true;
            helpButton.Clicked += (sender, e) =>
            {
                if ( ! helpShowing )
                {
                    Window window = Window.Instance;
                    helpImageView = LayoutingExample.CreateChildImageView("./res/images/no-layouts-example.png", new Size2D(200, 200));
                    helpImageView.Position2D = new Position2D( 0, helpButton.Size2D.Height );
                    helpShowing = true;
                    window.Add( helpImageView );
                }
                else
                {
                    Window window = Window.Instance;
                    window.Remove(  helpImageView );
                    helpShowing = false;
                }
                return true;
            };
        }

    };
}
