using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class ChildAddedToViewExample : Example
    {
        public ChildAddedToViewExample() : base( "Child to View" )
        {}

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

        private View linearView;
        private View view;
        private ImageView helpImageView;
        PushButton helpButton;
        bool helpShowing = false;
        private List<PushButton> buttons = new List<PushButton>();

        public override void Create()
        {
            Window window = Window.Instance;

            linearView = new View()
            {
                LayoutWidthSpecificationFixed = 480,
                LayoutHeightSpecification = ChildLayoutData.WrapContent,
                BackgroundColor = Color.Blue,
                Name = "LinearView",
                Position2D = new Position2D(0, 250),
            };
            var layout = new LinearLayout();
            linearView.Layout = layout;
            window.Add(linearView);

            view = new View()
            {
                BackgroundColor = Color.Green,
                Name = "GreenView",
                LayoutWidthSpecification = ChildLayoutData.WrapContent,
                LayoutHeightSpecification = ChildLayoutData.WrapContent,
            };
            linearView.Add(view);

            TextLabel textLabel = new TextLabel()
            {
                Name = "TextLabel",
                Text = "TextLabel in a View",
                PointSize = 20.0f
            };
            view.Add(textLabel);

            CreateHelpButton();
            LayoutingExample.GetToolbar().Add( helpButton );
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            if(helpImageView )
            {
                window.Remove(helpImageView);
                helpImageView = null;
            }
            helpShowing = false;
            LayoutingExample.GetToolbar().Remove(helpButton);
            linearView.Remove(view);
            window.Remove(linearView);
            view = null;
            helpButton = null;
            linearView = null;
        }

	// Shows a thumbnail of the expected output
        private void CreateHelpButton()
        {
            helpButton = new PushButton();
            helpButton.LabelText = "Help";
            helpButton.Clicked += (sender, e) =>
            {
                if ( ! helpShowing )
                {
                    Window window = Window.Instance;
                    helpImageView = LayoutingExample.CreateChildImageView("./res/images/child-added-to-view-example.png", new Size2D(200, 200));
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
