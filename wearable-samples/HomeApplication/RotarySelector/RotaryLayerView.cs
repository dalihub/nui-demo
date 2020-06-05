using System;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using NUIWHome.Common;

namespace NUIWHome
{
    /// <summary>
    /// RotaryLayerView
    /// </summary>
    internal class RotaryLayerView : View
    {
        private List<RotarySelectorItem> itemList;
        private RotaryIndicator rotaryIndicator;

        private TextLabel mainText;
        private TextLabel subText;

        private EditBGView editBGView;

        private View container;

        //for editing
        private View temporaryMovingIcon;

        private RotarySelectorItem item;
        private bool isEditMode;

        /// <summary>
        /// Creates a new instance of a RotaryLayerView.
        /// Contains objects that are actually displayed on the screen.
        /// </summary>
        internal RotaryLayerView()
        {
            this.BackgroundColor = Color.Black;

            itemList = new List<RotarySelectorItem>();

            container = new View()
            {
                Size = new Size(360, 360),
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
                CornerRadius = ApplicationConstants.SCREEN_SIZE_RADIUS,
            };
            this.Add(container);

            mainText = new TextLabel()
            {
                Size = new Size(210, 210),
                MultiLine = true,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
                TextColor = Color.White,
                PointSize = 8.0f,
            };
            mainText.TouchEvent += MainText_TouchEvent;
            container.Add(mainText);

            //Temporary code, Need to fix.
            //Screen configuration should be changed according to user settings.
            subText = new TextLabel()
            {
                Text = "Sub",

                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
                TextColor = Color.White,
                PointSize = 6.0f,
                Position = new Position(0, 50),
            };
            //container.Add(subText);

            //default opacity of indicator is 0.0f. after showing animation, change opacity.
            rotaryIndicator = new RotaryIndicator();
            rotaryIndicator.Opacity = 0.0f;
            container.Add(rotaryIndicator);

        }

        private bool MainText_TouchEvent(object source, TouchEventArgs e)
        {
            if (!this.isEditMode && e.Touch.GetState(0) == PointStateType.Up)
            {
                item?.ClickedItem();
            }
            return false;
        }

        internal List<RotarySelectorItem> RotaryItemList
        {
            get
            {
                return itemList;
            }
        }

        //Change position a->b
        internal void ChagneItemPosition(RotarySelectorItem a, RotarySelectorItem b)
        {
            int idx = itemList.IndexOf(b);
            itemList.Remove(a);
            itemList.Insert(idx, a);
        }


        internal void SetItem(RotarySelectorItem item)
        {
            this.item = item;
        }

        internal void SetText(string mainText, string subText)
        {
            this.mainText.Text = mainText;
            this.subText.Text = mainText;
        }


        internal void AppendItem(RotarySelectorItem item)
        {
            item.SetCurrentParent(container);
            itemList.Add(item);
        }

        internal void PrependItem(RotarySelectorItem item)
        {
            item.SetCurrentParent(container);
            itemList.Insert(0, item);
        }

        internal void InsertItem(int index, RotarySelectorItem item)
        {
            item.SetCurrentParent(container);
            itemList.Insert(index, item);
        }

        internal void DeleteItem(RotarySelectorItem item)
        {
            itemList.Remove(item);
            item.Unparent();
        }

        internal void DeleteItemIndex(int index)
        {
            itemList.RemoveAt(index);
        }

        internal void ClearItem()
        {
            itemList.Clear();
        }

        internal void CheckEditBG(int currentPage, int lastPage)
        {
            editBGView?.CheckNextPrevPage(currentPage, lastPage);
        }

        internal void AnimateBG(bool isRight)
        {
            editBGView?.AnimateBG(isRight);
        }

        internal void ChangeMode(bool isEditMode, int currentPage, int lastPage)
        {

            if (isEditMode)
            {
                BackgroundImage = CommonResource.GetResourcePath() + "/b_home_screen_container.png";
                //container.BackgroundColor = Color.Black;
                editBGView = new EditBGView(currentPage, lastPage);

                this.Add(editBGView);
                editBGView.LowerToBottom();


                this.mainText.Text = "Edit mode";
                this.mainText.Opacity = 0.0f;
                this.subText.Text = "";

                rotaryIndicator.Hide();
            }
            else
            {
                editBGView.DisposeChild();
                editBGView.Unparent();
                editBGView.Dispose();
                editBGView = null;

                this.BackgroundColor = Color.Black;
                this.mainText.Text = itemList[0].MainText;
                this.subText.Text = itemList[0].SubText;
                this.mainText.Opacity = 0.0f;

                //rotaryIndicator.SetRotaryPosition(0);
                rotaryIndicator.Show();
            }
            this.isEditMode = isEditMode;
        }

        internal void SetRotaryPosition(int toIdx)
        {
            rotaryIndicator.SetRotaryPosition(toIdx);
        }

        internal void SetIndicatorPosition()
        {
            rotaryIndicator.SetCurrentPosition();
        }

        internal void AddMovingIcon(RotarySelectorItem item)
        {
            if (temporaryMovingIcon == null)
            {
                Tizen.Log.Error("MYLOG", "res :" + item.BackgroundImage);
                temporaryMovingIcon = new View()
                {
                    Size = new Size(60, 60),
                    //BackgroundColor = Color.Red,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                    BackgroundImage = item.ResourceUrl,
                };

                this.Add(temporaryMovingIcon);
            }
        }


        internal void RemoveMovingIcon()
        {
            if (temporaryMovingIcon != null)
            {
                temporaryMovingIcon.Unparent();
                temporaryMovingIcon.Dispose();
                temporaryMovingIcon = null;
            }
        }

        private bool isRight = true;
        internal void SetRight()
        {
            isRight = true;
        }

        internal void SetLeft()
        {
            isRight = false;
        }

        internal int GetStartIndex()
        {
            return (isRight ? 0 : Common.ApplicationConstants.MAX_ITEM_COUNT - 1);
        }

        internal View GetMovingIcon()
        {
            return temporaryMovingIcon;
        }
        internal View GetContainer()
        {
            return container;
        }

        internal RotaryIndicator GetIndicator()
        {
            return rotaryIndicator;
        }
        internal TextLabel GetMainText()
        {
            return mainText;
        }

        internal int GetTotalItemCount()
        {
            return itemList.Count;
        }

        internal void AnimateRightCue()
        {
            editBGView.AnimateRightCue();
        }

        internal void AnimateLeftCue()
        {
            editBGView.AnimateLeftCue();
        }
        internal void AnimateReturn()
        {
            editBGView.AnimateReturn();
        }

        internal void RegisterPageMoveOnEdit(EditBGView.PageMoveDelegate delegateFunction)
        {
            if (editBGView != null)
            {
                editBGView.OnMovePageEditMode += delegateFunction;
            }
        }
    }
}