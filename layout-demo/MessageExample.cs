using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class MessageExample : Example
    {
        /**
         * Example Message application screen.
         * Shows: Linear layouts nested.
         *        Margin supoort.
         *        Horizontal linear layout (Toolbar)
         *        Vertical linear layout (To, Subject, Compose labels)
         *        Weight feature (Compose label fills extra space)
         *        MatchParent, so all labels are the width of the window
         *        WrapContent, "to" and "subject" labels are the height of the text
         */

        public MessageExample() : base("Message")
        {}

        private View _view;
        private List<View> toolbarItems = new List<View>();
        private const int YOffset = 40;

        public override void Create()
        {
            Window window = Window.Instance;

            _view = new View()
            {
                Name = "MessageExample",
                PositionY = YOffset,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = Window.Instance.Size.Height - YOffset,
            };

            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            _view.Layout = layout;

            window.Add(_view);

            // Create compose toolbar

            // Create compose label
            TextLabel composeLabel = new TextLabel("Compose");
            composeLabel.Name = "composeLabel";
            composeLabel.HeightSpecification = LayoutParamPolicies.MatchParent;
            composeLabel.WidthSpecification = LayoutParamPolicies.WrapContent;
            toolbarItems.Add(composeLabel);

            // Create attachment button
            PushButton attachmentButton = new PushButton();
            attachmentButton.Name = "attachmentButton";
            attachmentButton.HeightSpecification = LayoutParamPolicies.MatchParent;
            attachmentButton.WidthSpecification = 80;  // Could instead set the Weight to 1 along with sendButton

            var imageVisualAttachmentUnSelected = new ImageVisual();
            imageVisualAttachmentUnSelected.URL = "./res/images/paper-clip.png";
            attachmentButton.UnselectedBackgroundVisual = imageVisualAttachmentUnSelected.OutputVisualMap;
            var imageVisualAttachmentSelected = new ImageVisual();
            imageVisualAttachmentSelected.URL = "./res/images/paper-clip-selected.png";
            attachmentButton.SelectedBackgroundVisual = imageVisualAttachmentSelected.OutputVisualMap;
            toolbarItems.Add(attachmentButton);

            // Create send button
            PushButton sendButton = new PushButton();
            sendButton.Name = "sendButton";
            sendButton.HeightSpecification = LayoutParamPolicies.MatchParent;
            sendButton.WidthSpecification = 120; // Could instead set the Weight to 2 along with attachmentButton

            var imageVisualSendSelected = new ImageVisual();
            imageVisualSendSelected.URL = "./res/images/send-email-selected.png";
            sendButton.SelectedBackgroundVisual = imageVisualSendSelected.OutputVisualMap;
            var imageVisualSendUnselected = new ImageVisual();
            imageVisualSendUnselected.URL = "./res/images/send-email.png";
            sendButton.UnselectedBackgroundVisual = imageVisualSendUnselected.OutputVisualMap;
            toolbarItems.Add(sendButton);

            View titleToolbar = CreateTitleToolbar("Compose", toolbarItems);
            titleToolbar.HeightSpecification = 40;

            _view.Add(titleToolbar);

            // Create "To" area
            TextLabel toArea = new TextLabel("To")
            {
                Name = "toLabel",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Margin = new Extents( 5,0,10,0),
                BackgroundColor = new Color(232.0f, 222.0f, 232.0f, 0.7f),
            };

            _view.Add(toArea);

            // Create "Subject" area
            TextLabel subjectArea = new TextLabel("Subject")
            {
                Name = "subjectArea",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Margin = new Extents( 5,0,10,0),
                BackgroundColor = new Color(232.0f, 222.0f, 232.0f, 0.7f),
            };
            _view.Add(subjectArea);

            // Create "Body" area
            TextLabel bodyArea = new TextLabel("Compose")
            {
                Name = "bodyArea",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Margin = new Extents( 5,0,10,10),
                Weight = 1,
                BackgroundColor = new Color(232.0f, 222.0f, 232.0f, 0.7f),
            };
            _view.Add(bodyArea);

        }

        public override void Remove()
        {
            Window.Instance.Remove(_view);
            _view = null;
            toolbarItems.Clear();
        }

        private View CreateTitleToolbar(string title, IEnumerable<View> items)
        {
            View titleToolbar = new View();
            titleToolbar.Name = "titleToolbar";
            titleToolbar.WidthSpecification = LayoutParamPolicies.MatchParent;
            titleToolbar.Padding = new Extents(10, 10, 15, 0);
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            titleToolbar.Layout = layout;

            foreach (View item in items)
            {
                titleToolbar.Add(item);
            }

            return titleToolbar;
        }
    };
}
