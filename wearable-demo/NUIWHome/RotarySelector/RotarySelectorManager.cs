
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using NUIWHome.Common;

namespace NUIWHome
{
    internal class RotarySelectorManager
    {
        private AnimationManager animationManager;
        private RotaryTouchController rotaryTouchController;
        private List<RotaryItemWrapper> wrapperList;
        private RotaryLayerView rotaryLayerView;
        private RotaryPagination pagination;

        private int currentPage = 0;
        private int lastPage = 0;
        private int currentWrapIdx = 0;
        private int currentSelectIdx = 0;
        private Size rotarySize;

        private bool isEditMode = false;

        internal RotarySelectorManager(Size rotarySize)
        {
         
            this.rotarySize = rotarySize;

            rotaryTouchController = new RotaryTouchNormalMode();
            wrapperList = new List<RotaryItemWrapper>();

            // First Page
            for(int i = 0; i < ApplicationConstants.MAX_ITEM_COUNT; i++)
            {
                RotaryItemWrapper rotaryItemWrapper = new RotaryItemWrapper(i);
                wrapperList.Add(rotaryItemWrapper);
            }

            // Second Page
            for(int i = 0; i < ApplicationConstants.MAX_ITEM_COUNT; i++)
            {
                RotaryItemWrapper rotaryItemWrapper = new RotaryItemWrapper(i);
                wrapperList.Add(rotaryItemWrapper);
            }

            rotaryLayerView = new RotaryLayerView()
            {
                WidthResizePolicy = ResizePolicyType.FillToParent,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };

            animationManager = new AnimationManager();

            pagination = new RotaryPagination();
        }

        internal void StartAppsAnimation()
        {
            animationManager.AnimateStarting(rotaryLayerView, wrapperList);
        }

        internal void NextPage(bool isEndEffect = true, AnimationManager.PageAnimationType type = AnimationManager.PageAnimationType.Slide)
        {
            if (animationManager.IsAnimating)
            {
                return;
            }
            animationManager.IsAnimating = true;

            if (currentPage + 1 < lastPage)
            {

                //For editing mode
                rotaryLayerView.CheckEditBG(currentPage+1, lastPage);
                rotaryLayerView.AnimateBG(true);


                PlayPageAnimation(rotaryLayerView.RotaryItemList, currentPage + 1, false, type);
                currentPage++;
                pagination.SetCurrentPage(currentPage);

            }
            else
            {
                if(isEndEffect)
                {
                    animationManager.AnimateEndEffect(rotaryLayerView.GetContainer());
                }
            }
        }

        internal void PrevPage(bool isEndEffect = true, AnimationManager.PageAnimationType type = AnimationManager.PageAnimationType.Slide)
        {
            if (animationManager.IsAnimating)
            {
                return;
            }
            animationManager.IsAnimating = true;

            if (currentPage > 0)
            {
                //For editing mode
                rotaryLayerView.CheckEditBG(currentPage-1, lastPage);
                rotaryLayerView.AnimateBG(false);

                PlayPageAnimation(rotaryLayerView.RotaryItemList, currentPage - 1, true, type);
                currentPage--;
                pagination.SetCurrentPage(currentPage);

            }
            else
            {
                if (isEndEffect)
                {
                    animationManager.AnimateEndEffect(rotaryLayerView.GetContainer());
                }
            }
        }

        internal void SetRotarySelectorMode(bool isEditMode)
        {
            if (isEditMode)
            {
                Tizen.Log.Error("MYLOG", "Change editMode");
                rotaryTouchController = new RotaryTouchEditMode();
                rotaryTouchController.OnNotify += RotaryTouchController_OnNotify;
            }
            else
            {
                Tizen.Log.Error("MYLOG", "Change NormalMode");
                rotaryTouchController = new RotaryTouchNormalMode();
            }
            rotaryLayerView.ChangeMode(isEditMode, currentPage, lastPage);
            animationManager.AnimateChangeState(rotaryLayerView, isEditMode);
            this.isEditMode = isEditMode;
            rotaryLayerView.RegisterTest(TestFunction);
        }

        public void TestFunction(bool isTest)
        {
            if(isTest)
            {
                NextPage(false);
            }
            else
            {
                PrevPage(false);
            }

            Window.Instance.TouchEvent -= Instance_TouchEvent;
            rotaryTouchController.SelectedItem = null;
        }
        
        internal void SelectItemByWheel(int direction)
        {
            //Not set 'IsAnimating' to true. Just Block while paging animation.
            if (animationManager.IsAnimating)
            {
                return;
            }

            //direction 1 - cw, 2 - ccw
            if (direction == 1)
            {
                if (currentSelectIdx + 1 < ApplicationConstants.MAX_TRAY_COUNT - 1)
                {
                    currentSelectIdx++;
                    SelectItem();
                }
                else
                {
                    NextPage(true, AnimationManager.PageAnimationType.Rotary);
                }
            }
            else
            {
                if(currentSelectIdx - 1 >= 0)
                {
                    currentSelectIdx--;
                    SelectItem();
                }
                else
                {
                    PrevPage(true, AnimationManager.PageAnimationType.Rotary);
                }
            }

        }
        private void SelectItem(RotarySelectorItem item = null, bool isAnimating = true)
        {
            if(item == null)
            {
                item = wrapperList[currentSelectIdx].RotaryItem;
            }

            currentSelectIdx = item.CurrentIndex;
            rotaryLayerView.SetText(item.MainText, item.SubText);
            rotaryLayerView.SetRotaryPosition(item.CurrentIndex);

            if(isAnimating)
            {
                //for moving indicator
                animationManager.AnimateChangeState(rotaryLayerView, false, true);

            }
        }

        private void OnItemSelected(RotarySelectorItem item)
        {
            SelectItem(item);
        }

        private bool Item_TouchEvent(object source, View.TouchEventArgs e)
        {
            return rotaryTouchController.ProcessTouchEvent(source, e);
        }

        private void InitItem(RotarySelectorItem item)
        {
            item.OnItemSelected += OnItemSelected;
            item.TouchEvent += Item_TouchEvent;
        }

        internal void AppendItem(RotarySelectorItem item)
        {
            rotaryLayerView.AppendItem(item);
            InitItem(item);
            ReWrappingAllItems();
            //WrappingAppendItem(item);
        }

        internal void PrependItem(RotarySelectorItem item)
        {
            rotaryLayerView.PrependItem(item);
            InitItem(item);
            ReWrappingAllItems();
        }

        internal void InsertItem(int index, RotarySelectorItem item)
        {
            rotaryLayerView.InsertItem(index, item);
            InitItem(item);
            ReWrappingAllItems();
        }


        //Need to add function
        internal void DeleteItem(RotarySelectorItem item)
        {
            rotaryLayerView.Remove(item);
            ReWrappingAllItems();
        }


        //Need to add function
        internal void DeleteItemIndex(int index)
        {
            rotaryLayerView.DeleteItemIndex(index);
            ReWrappingAllItems();
        }


        //Need to add function
        internal void ClearItem()
        {
            rotaryLayerView.ClearItem();
            ReWrappingAllItems();
        }

        internal RotaryLayerView GetRotaryLayerView()
        {
            return rotaryLayerView;
        }

        private void GeneratePage()
        {
            int remainder = 0;
            lastPage = Math.DivRem(rotaryLayerView.GetTotalItemCount(), ApplicationConstants.MAX_ITEM_COUNT, out remainder);
            lastPage += (remainder > 0) ? 1 : 0;

            pagination.CreatePagination(lastPage);

        }

        private void WrappingAppendItem(RotarySelectorItem item)
        {
            if (currentWrapIdx < ApplicationConstants.MAX_ITEM_COUNT)
            {
                wrapperList[currentWrapIdx].RotaryItem = item;
                currentWrapIdx++;
            }
            else
            {
                item.Hide();
            }

            GeneratePage();
        }

        //Needs improvement for performance.
        private void ReWrappingAllItems()
        {
            if(rotaryLayerView.RotaryItemList.Count == 0)
            {
                //Clear
            }

            currentWrapIdx = 0;
            foreach (RotarySelectorItem item in rotaryLayerView.RotaryItemList)
            {
                if (currentWrapIdx < ApplicationConstants.MAX_ITEM_COUNT)
                {
                    wrapperList[(int)currentWrapIdx].RotaryItem = item;
                    currentWrapIdx++;
                }
                else
                {
                    item.Hide();
                }
            }

            GeneratePage();
        }


        private void PlayPageAnimation(List<RotarySelectorItem> itemList, int vIdx, bool cw, AnimationManager.PageAnimationType type = AnimationManager.PageAnimationType.Slide)
        {

            animationManager.InitShowHideAnimation();

            int sIdx = GetViewIndex(currentPage) * ApplicationConstants.MAX_ITEM_COUNT;
            int eIdx = GetViewIndex(vIdx) * ApplicationConstants.MAX_ITEM_COUNT;
            int setIdx = vIdx * ApplicationConstants.MAX_ITEM_COUNT;
            int totalItemCount = rotaryLayerView.GetTotalItemCount();

            for (int i = sIdx, j = eIdx; i < sIdx + ApplicationConstants.MAX_ITEM_COUNT; i++, j++)
            {
                animationManager.AnimateHidePage(wrapperList[i], cw, type);
                //wrapperList[i].PlayPageHideAnimation(hideAnimation, cw);

                if (setIdx < totalItemCount)
                {
                    wrapperList[j].RotaryItem = itemList[setIdx++];
                    wrapperList[j].RotaryItem.Show();
                    animationManager.AnimateShowPage(wrapperList[j], cw, type);
                    //wrapperList[j].PlayPageShowAnimation(showAnimation, cw);
                }
            }
            animationManager.AnimatePageLayerContents(rotaryLayerView, !cw);
            animationManager.PlayCoreAnimation();
        }

        private void RotaryTouchController_OnNotify(RotarySelectorItem item, int opt)
        {
            switch (opt)
            {
                case 0:
                {
                    if (isEditMode)
                    {
                        animationManager.InitRotaryPathAnimation();

                        RotarySelectorItem SelectedItem = rotaryTouchController.SelectedItem;
                        RotarySelectorItem collisionItem = item;

                        int page = (currentPage % 2) * ApplicationConstants.MAX_ITEM_COUNT;
                        int selIdx = (int)SelectedItem?.CurrentIndex;
                        int colIdx = (int)collisionItem?.CurrentIndex;
                        if (selIdx < colIdx)
                        {
                            for (int i = selIdx; i < colIdx; i++)
                            {
                                int idx = page + i;
                                wrapperList[idx].RotaryItem = wrapperList[idx + 1].RotaryItem;
                                animationManager.AnimatePathOnEdit(wrapperList[idx]);
                            }
                        }
                        else
                        {
                            for (int i = selIdx; i > colIdx; i--)
                            {
                                int idx = page + i;
                                wrapperList[idx].RotaryItem = wrapperList[idx - 1].RotaryItem;
                                animationManager.AnimatePathOnEdit(wrapperList[idx], false);
                            }
                        }

                        rotaryLayerView.ChagneItemPosition(SelectedItem, collisionItem);
                        wrapperList[page + colIdx].RotaryItem = SelectedItem;
                        animationManager.PlayCoreAnimation();
                    }
                    break;
                }
                case 1:
                {
                    Window.Instance.TouchEvent += Instance_TouchEvent;
                    break;
                }
          
            }
        }

        

        private void Instance_TouchEvent(object sender, Window.TouchEventArgs e)
        {
            if (isEditMode && rotaryTouchController.SelectedItem != null)
            {
                if ((e.Touch.GetState(0) == PointStateType.Up))
                {
                    Tizen.Log.Error("MYLOG", "Finish edit mode\n");
                    rotaryTouchController.SelectedItem.RaiseToTop();
                    rotaryTouchController.SelectedItem.Scale = new Vector3(1.0f, 1.0f, 1.0f);
                    Window.Instance.TouchEvent -= Instance_TouchEvent;

                    int wrapperIdx = GetViewIndex(currentPage) * ApplicationConstants.MAX_ITEM_COUNT;
                    wrapperList[wrapperIdx + (int)rotaryTouchController.SelectedItem.CurrentIndex].RotaryItem = rotaryTouchController.SelectedItem;

                    rotaryTouchController.SelectedItem = null;
                }
                else if ((e.Touch.GetState(0) == PointStateType.Motion))
                {
                    Position mousePosition = new Position(e.Touch.GetScreenPosition(0).X, e.Touch.GetScreenPosition(0).Y);
                    rotaryTouchController.SelectedItem.Position = mousePosition;


                    if (animationManager.IsAnimating)
                    {
                        return;
                    }
                    //animationManager.IsAnimating = true;

                    Tizen.Log.Error("MYLOG", "mouse X:" + mousePosition.X + ", Y:" + mousePosition.Y);
                    if(mousePosition.X <= 50 && mousePosition.Y >= 100 && mousePosition.Y <= 260)
                    {
                        rotaryLayerView.AnimateLeftCue();
                    }
                    else if(mousePosition.X >= 310 && mousePosition.Y >= 100 && mousePosition.Y <= 260)
                    {
                        rotaryLayerView.AnimateRightCue();
                    }
                    else
                    {
                        rotaryLayerView.AnimateReturn();
                    }
                    /*
                    else
                    {
                        ImageView view = rotaryLayerView.GetMovingIcon();
                        view.ResourceUrl = rotaryTouchController.SelectedItem.ResourceUrl;
                        view.Position = mousePosition;
                    }
                    */
                }

            }
        }

        private RotarySelectorItem GetCurrentItem()
        {
            return wrapperList[GetViewIndex(currentPage) * ApplicationConstants.MAX_ITEM_COUNT].RotaryItem;
        }

        private int GetViewIndex(int currentPage)
        {
            return currentPage % 2;
        }

        internal RotaryPagination GetRotaryPagination()
        {
            return pagination;
        }
    }
}