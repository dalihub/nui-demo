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
        private List<PushButton> buttons = new List<PushButton>();

        public override void Create()
        {
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

            View view = new View()
            {
                Size2D = new Size2D(476,500),
                BackgroundColor = Color.Green,
                Position2D = new Position2D(2, 0),
                ParentOrigin = ParentOrigin.BottomRight,
                PivotPoint = PivotPoint.BottomRight,
                PositionUsesPivotPoint = true,
            };

            TextLabel view3 = new TextLabel()
            {
                Size2D = new Size2D(274, 70),
                BackgroundColor = Color.Red,
                Position2D = new Position2D(0, 10),
                Text = "Enter password",
                //ParentOrigin = ParentOrigin.Center,
            };
            view.Add(view3);

            TextField field = new TextField();
            field.Size2D = new Size2D(120, 80);
            field.Position2D = new Position2D(150,85);
            field.BackgroundColor = Color.Cyan;
            field.PlaceholderText = "input something";
            view.Add(field);


            window.Add(view);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(view);
            view = null;
        }
    };
}
