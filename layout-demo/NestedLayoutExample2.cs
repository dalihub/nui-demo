using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    public class PopupLayout : View
    {
        View popupBG;
        View popupBody;
        View popupButtonArea;
        TextLabel textlabel;
        //LinearLayout layout;

        LinearLayout[] linearlayout;
        ImageView[] childhorizontal;
        ImageView[] childhorizontal2;

        View[] childView;
        TextLabel contentText;

        string url = "./res/images/gallery-small-23.jpg";

        public void createView()
        {
            linearlayout = new LinearLayout[5];
            popupBG = new View();
            popupBG.Position2D = new Position2D(2, 2);
            popupBG.BackgroundColor = Color.Yellow;
            popupBG.LayoutWidthSpecification = ChildLayoutData.WrapContent;
            popupBG.LayoutHeightSpecification = ChildLayoutData.WrapContent;
            popupBG.Focusable = true;

            textlabel = new TextLabel();
            textlabel.Size2D = new Size2D(914, 90);
            textlabel.HorizontalAlignment = HorizontalAlignment.Center;
            textlabel.Text = "Make popup class";
            textlabel.PointSize = 34;
            popupBG.Add(textlabel);

            popupBody = new View();
            popupBody.LayoutWidthSpecification = ChildLayoutData.WrapContent;
            popupBody.LayoutHeightSpecification = ChildLayoutData.WrapContent;
            popupBody.BackgroundColor = new Color(1.0f, 0.0f, 0.0f, 0.2f);
            popupBG.Add(popupBody);

            popupButtonArea = new View();
            popupButtonArea.LayoutWidthSpecification = ChildLayoutData.WrapContent;
            popupButtonArea.LayoutHeightSpecification = ChildLayoutData.WrapContent;
            popupBG.Add(popupButtonArea);

            contentText = new TextLabel();
            contentText.HorizontalAlignment = HorizontalAlignment.Center;
            contentText.MultiLine = true;
            contentText.Text = "Short text ";
            contentText.PointSize = 34;
            popupBody.Add(contentText);

            childhorizontal = new ImageView[3];
            for (int i = 0; i < 3; i++)
            {
                childhorizontal[i] = new ImageView(url);

                childhorizontal[i].LayoutWidthSpecificationFixed = 70;
                childhorizontal[i].LayoutHeightSpecificationFixed = 70;
                childhorizontal[i].BackgroundColor = new Color(i * 0.25f, i * 0.25f, 1.0f, 1.0f);
                popupBody.Add(childhorizontal[i]);
            }

            childhorizontal2 = new ImageView[5];
            childView = new View[5];
            for (int i = 0; i < 3; i++)
            {
                childView[i] = new View();
                childView[i].BackgroundColor = new Color(i * 0.25f, i * 0.25f, 1.0f, 1.0f);
                childView[i].LayoutWidthSpecificationFixed = 200;
                childView[i].LayoutHeightSpecificationFixed = 70;
                popupButtonArea.Add(childView[i]);
            }

            for (int i = 0; i < 2; i++)
            {
                linearlayout[i] = new LinearLayout();
                linearlayout[i].LinearOrientation = LinearLayout.Orientation.Vertical;
                linearlayout[i].LinearAlignment = LinearLayout.Alignment.CenterHorizontal;
            }

            for (int i = 2; i < 5; i++)
            {
                linearlayout[i] = new LinearLayout();
                linearlayout[i].LinearOrientation = LinearLayout.Orientation.Horizontal;
                linearlayout[i].LinearAlignment = LinearLayout.Alignment.CenterHorizontal;
            }

            popupButtonArea.Layout = linearlayout[2];
            popupBody.Layout = linearlayout[0];
            popupBG.Layout = linearlayout[1];
            this.Add(popupBG);
        }
    } // popupLayout

    class NestedLayoutExample2 : Example
    {
        public NestedLayoutExample2() : base( "Nested2" )
        {
        }

        View popupShadow;
        View popupBG;
        View popupBody;
        View popupButtonArea;
        TextLabel textlabel;

        //LinearLayout layout;

        ImageView[] childhorizontal;
        ImageView[] childhorizontal2;

        View[] childView;
        LinearLayout[] linearlayout;

        PopupLayout popup;
        TextLabel contentText;
        string url = "res/images/application-icon-101.png";

        public override void Create()
        {
            Activate();
        }

        private void Activate()
        {

            popupShadow = new View();
            popupShadow.Name = "popupShadow";
            popupShadow.PositionUsesPivotPoint = true;
            popupShadow.PivotPoint = PivotPoint.CenterRight;
            popupShadow.ParentOrigin = ParentOrigin.CenterRight;
            popupShadow.BackgroundColor = Color.Magenta;
            popupShadow.LayoutWidthSpecification = ChildLayoutData.WrapContent;
            popupShadow.LayoutHeightSpecification = ChildLayoutData.WrapContent;

            popupBG = new View();
            popupBG.Name = "popupBG";
            popupBG.Position2D = new Position2D(2, 2);
            popupBG.BackgroundColor = Color.Yellow;
            popupBG.LayoutWidthSpecification = ChildLayoutData.WrapContent;
            popupBG.LayoutHeightSpecification = ChildLayoutData.WrapContent;
            popupBG.Focusable = true;
            popupBG.Padding = new Extents( 10, 10, 0, 0 );

            textlabel = new TextLabel();
            textlabel.Name = "main-textlabel";
            textlabel.Size2D = new Size2D(914, 90);
            textlabel.HorizontalAlignment = HorizontalAlignment.Center;
            textlabel.Text = "Main Popup Title";
            textlabel.PointSize = 34;
            popupBG.Add(textlabel);

            popupBody = new View();
            popupBody.Name = "popupBody";
            popupBody.LayoutWidthSpecification = ChildLayoutData.WrapContent;
            popupBody.LayoutHeightSpecification = ChildLayoutData.WrapContent;
            popupBody.BackgroundColor = new Color(1.0f, 0.0f, 0.0f, 0.2f);
            popupBG.Add(popupBody);

            popupButtonArea = new View();
            popupButtonArea.Name = "popupButtonArea";
            popupButtonArea.LayoutWidthSpecification = ChildLayoutData.WrapContent;
            popupButtonArea.LayoutHeightSpecification = ChildLayoutData.WrapContent;
            popupBG.Add(popupButtonArea);

            contentText = new TextLabel();
            contentText.Name = "contentText";
            contentText.HorizontalAlignment = HorizontalAlignment.Center;
            contentText.MultiLine = true;
            contentText.Text = "Short text";
            contentText.PointSize = 34;
            contentText.LineWrapMode = LineWrapMode.Word;
            popupBody.Add(contentText);

            childhorizontal = new ImageView[3];
            for (int i = 0; i < 3; i++)
            {
                childhorizontal[i] = new ImageView(url);
                childhorizontal[i].Name = "childhorizontal" + i;
                childhorizontal[i].PivotPoint = PivotPoint.Center;
                childhorizontal[i].LayoutWidthSpecificationFixed = 70;
                childhorizontal[i].LayoutHeightSpecificationFixed = 70;
                childhorizontal[i].BackgroundColor = new Color(i * 0.25f, i * 0.25f, 1.0f, 1.0f);
                popupBody.Add(childhorizontal[i]);
            }

            childhorizontal2 = new ImageView[5];
            childView = new View[5];
            for (int i = 0; i < 3; i++)
            {
                childView[i] = new View();
                childView[i].Name = "childView" + i;
                childView[i].PivotPoint = PivotPoint.Center;
                childView[i].BackgroundColor = new Color(i * 0.25f, i * 0.25f, 1.0f, 1.0f);
                childView[i].LayoutWidthSpecificationFixed = 200;
                childView[i].LayoutHeightSpecificationFixed = 70;
                popupButtonArea.Add(childView[i]);
            }

            linearlayout = new LinearLayout[5];
            for (int i = 0; i < 2; i++)
            {
                linearlayout[i] = new LinearLayout();
                linearlayout[i].LinearOrientation = LinearLayout.Orientation.Vertical;
                linearlayout[i].LinearAlignment = LinearLayout.Alignment.CenterHorizontal;
            }

            for (int i = 2; i < 5; i++)
            {
                linearlayout[i] = new LinearLayout();
                linearlayout[i].LinearOrientation = LinearLayout.Orientation.Horizontal;
                linearlayout[i].LinearAlignment = LinearLayout.Alignment.CenterHorizontal;
            }

            popupButtonArea.Layout = linearlayout[2];
            popupBody.Layout = linearlayout[0];
            popupBG.Layout = linearlayout[1];
            popupShadow.Add(popupBG);
            Window.Instance.GetDefaultLayer().Add(popupShadow);

            // Make Class Popup//////////////////////////////////////////////////////
            popup = new PopupLayout();
            popup.Size2D = new Size2D(918, 1000);
            popup.Name = "popupClass";
            popup.PositionUsesPivotPoint = true;
            popup.PivotPoint = PivotPoint.CenterLeft;
            popup.ParentOrigin = ParentOrigin.CenterLeft;
            popup.BackgroundColor = Color.Red;
            popup.Layout = new LinearLayout();
            popup.createView();
            Window.Instance.GetDefaultLayer().Add(popup);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            // if(helpImageView )
            // {
            //     window.Remove(helpImageView);
            //     helpImageView = null;
            // }
            // helpShowing = false;
            // LayoutingExample.GetToolbar().Remove(helpButton);
            window.BackgroundColor = Color.White;
            window.GetDefaultLayer().Remove(popup);
            //helpButton = null;
            popup = null;
        }

	    // Shows a thumbnail of the expected output
        // private void CreateHelpButton()
        // {
        //     helpButton = new PushButton();
        //     helpButton.LabelText = "Help";
        //     helpButton.Clicked += (sender, e) =>
        //     {
        //         if ( ! helpShowing )
        //         {
        //             Window window = Window.Instance;
        //             helpImageView = LayoutingExample.CreateChildImageView("./res/images/nested-layers-help.png", new Size2D(200, 200));
        //             helpImageView.Position2D = new Position2D( 0, helpButton.Size2D.Height );
        //             helpShowing = true;
        //             window.Add( helpImageView );
        //         }
        //         else
        //         {
        //             Window window = Window.Instance;
        //             window.Remove(  helpImageView );
        //             helpShowing = false;
        //         }
        //         return true;
        //     };
        // }

    };
}
