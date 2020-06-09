using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public class WearableGallery : View
    {
        public abstract class ViewHolder
        {
            public int BindingIndex = -1;
            private View view;
            public ViewHolder(View view)
            {
                this.view = view;
            }

            public View GetView()
            {
                return view;
            }
        }

        public abstract class Adapter
        {
            public abstract int GetItemCount();
            public abstract WearableGallery.ViewHolder OnCreateViewHolder();
            public abstract void OnBindViewHolder(WearableGallery.ViewHolder holder, int position);
        }

        //Constant value
        private int EXTRA_ITEM_COUNT = 3;
        private int TOUCH_RANGE = 100;

        //WearableGallery Controller
        private PanGestureDetector panDetector;
        private WearableGalleryLayoutManager layoutManager;
        private Adapter adapter;
        private View containerView;
        private List<ViewHolder> viewHolderList;

        //Control paging
        private int panScreenPosition = 0;
        private int firstScreenPosition = 0;
        private bool isAnimateProcessing = false;
        private bool isPagingProcessing = false;
        

        //Recycler variable
        private int currentIndex = 0;

        private int recycleItemCount = 4;

        private int extraItemIdx = 0;


        public WearableGallery()
        {
            extraItemIdx = EXTRA_ITEM_COUNT;

            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;

            containerView = new View()
            {                
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = 360,
                BackgroundColor = new Color(1, 0, 0, 0.3f),
            };
            this.Add(containerView);

            panDetector = new PanGestureDetector();
            panDetector.Attach(this);
            panDetector.Detected += PanDetector_Detected;
            
            layoutManager = new DefaultLayoutManager();
            
            viewHolderList = new List<ViewHolder>();

        }

        private void PanDetector_Detected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            int mouse_nextX = (int)e.PanGesture.ScreenPosition.X;
            int distance = mouse_nextX - firstScreenPosition;

            switch (e.PanGesture.State)
            {
                case Gesture.StateType.Finished:
                {
                    isAnimateProcessing = true;

                    if (distance > TOUCH_RANGE)
                    {
                        Prev();
                    }
                    else if (distance < -TOUCH_RANGE)
                    {
                        Next();
                    }
                    else
                    {
                        layoutManager.CancelPage(this);
                    }
                    break;
                }

                case Gesture.StateType.Continuing:
                {
                    isPagingProcessing = true;
                    layoutManager.DragPage(this, mouse_nextX - panScreenPosition);
                    panScreenPosition = mouse_nextX;
                    break;
                }
                case Gesture.StateType.Started:
                {
                    panScreenPosition = (int)e.PanGesture.ScreenPosition.X;
                    firstScreenPosition = panScreenPosition;
                    break;
                }
            }
        }
        public void SetAdapter(Adapter adapter)
        {
            this.adapter = adapter;
            int dataCount = adapter.GetItemCount();
            /*
            for(int i = 0; i < EXTRA_ITEM_COUNT; i++)
            {
                ViewHolder holder = adapter.OnCreateViewHolder();
                viewHolderList.Add(holder);
                containerView.Add(holder.GetView());

            } */
            //for(int i = EXTRA_ITEM_COUNT; i < recycleItemCount + EXTRA_ITEM_COUNT; i++)
            for(int i = 0; i < dataCount; i++)
            {
                ViewHolder holder = adapter.OnCreateViewHolder();
                holder.BindingIndex = i;

                View childView = holder.GetView();
                childView.TouchEvent += ChildItem_TouchEvent;
                
                viewHolderList.Add(holder);
                adapter.OnBindViewHolder(holder, holder.BindingIndex);
                containerView.Add(childView);
            }
            layoutManager.OrderByIndex(this);
        }


        public List<ViewHolder> GetChild()
        {
            return viewHolderList;
        }

        public int GetCurrentPage()
        {
            return extraItemIdx;
        }

        public void SetLayoutManager(WearableGalleryLayoutManager manager, ViewHolder selectVh = null)
        {
            if(layoutManager == manager)
            {
                return;
            }

            if(selectVh != null)
            {
                currentIndex = selectVh.BindingIndex;
            }

            layoutManager = manager;
            layoutManager.animator.animationFinished += FinishAnimation;

            extraItemIdx = currentIndex / layoutManager.GetItemCountByLine();
            layoutManager.OrderByIndex(this);
        }

        public void Next()
        {
            //if(layoutManager.animator.animationFinished == null)
            {
                NextPageProcess();
                layoutManager.animator.animationFinished += FinishAnimation;
            }
        }

        public void FinishAnimation()
        {
            Tizen.Log.Error("MYLOG", "finish animation");
            isAnimateProcessing = false;
            isPagingProcessing = false;
            //layoutManager.animator.animationFinished -= FinishAnimation;
        }

        public void Prev()
        {
            //if(layoutManager.animator.animationFinished == null)
            {
                PrevPageProcess();
            }
        }


        private void NextPageProcess()
        {
/*
            if(extraItemIdx + 1 <= EXTRA_ITEM_COUNT)
            {
                extraItemIdx++;
            }
            else
            {
                if(viewHolderList[viewHolderList.Count - 1].BindingIndex + 1 < adapter.GetItemCount())
                {
                    ViewHolder firstVh = viewHolderList[0];
                    ViewHolder lastVh = viewHolderList[viewHolderList.Count - 1];
                    viewHolderList.Remove(firstVh);
                    viewHolderList.Add(firstVh);

                    adapter.OnBindViewHolder(firstVh, lastVh.BindingIndex + 1);
                    firstVh.BindingIndex = lastVh.BindingIndex + 1;
                    firstVh.GetView().Position =  lastVh.GetView().Position;
                    Tizen.Log.Error("MYLOG", " lp : " + lastVh.GetView().Position.X + "\n");
                    firstVh.GetView().BackgroundColor = Color.Red;
                }
            }
             */
            if(extraItemIdx + 1 < (viewHolderList.Count / layoutManager.GetItemCountByLine()) )
            {
                extraItemIdx++;
                layoutManager.OrderByIndex(this);
            }
            else
            {
                layoutManager.CancelPage(this);
            }
        }

        private void PrevPageProcess()
        {
            /*
            ViewHolder firstVh = viewHolderList[0];
            ViewHolder lastVh = viewHolderList[viewHolderList.Count - 1];

            if(firstVh.BindingIndex - 1 >= 0)
            {
                viewHolderList.Remove(lastVh);
                viewHolderList.Insert(0, lastVh);

                adapter.OnBindViewHolder(lastVh, firstVh.BindingIndex - 1);
                lastVh.BindingIndex = firstVh.BindingIndex - 1;
                lastVh.GetView().Position = firstVh.GetView().Position;
            }
            else
            {
                if(extraItemIdx - 1 >= 0)
                {
                    extraItemIdx--;
                }
            } */
            if(extraItemIdx - 1 >= 0 )
            {
                extraItemIdx--;
                layoutManager.OrderByIndex(this);
            }
            else
            {
                layoutManager.CancelPage(this);
            }
        }

        private int GetLastPage()
        {
            int span = layoutManager.GetItemCountByLine();
            return containerView.Children.Count / span + ((containerView.Children.Count % span > 0) ? 1 : 0);
        }

        private EventHandlerWithReturnType<object, TouchEventArgs, bool> _childTouchDataEventHandler;
        public event EventHandlerWithReturnType<object, TouchEventArgs, bool> ChildTouchEvent
        {
            add
            {
                _childTouchDataEventHandler += value;
            }
            remove
            {
                _childTouchDataEventHandler -= value;
            }
        }

        private bool ChildItem_TouchEvent(object source, View.TouchEventArgs e)
        {
            //Paging...
            if(isPagingProcessing)
            {
                return true;
            }

            if(_childTouchDataEventHandler != null)
            {
                View selectItem = source as View;
                int idx = containerView.Children.IndexOf(selectItem);
                return _childTouchDataEventHandler(viewHolderList[idx], e);
            }
            return true;
        }
    }
}