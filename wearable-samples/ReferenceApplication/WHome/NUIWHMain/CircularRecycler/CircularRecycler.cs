using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;
using NUIWHMain;

namespace WearableGallerySample
{
    public class CircularRecycler : View
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
            public abstract CircularRecycler.ViewHolder OnCreateViewHolder();
            public abstract void OnBindViewHolder(CircularRecycler.ViewHolder holder, int position);
        }

        //Constant value
        private int TOUCH_RANGE = 100;
        private int CENTER_INDEX = 0;

        //WearableGallery Controller
        private PanGestureDetector panDetector;
        private CircularRecyclerLayoutManager layoutManager;
        private Adapter adapter;
        private View containerView;
        private List<ViewHolder> viewHolderList;

        //Control paging
        private int panScreenPosition = 0;
        private int firstScreenPosition = 0;


        //Recycler variable
        private int currentIndex = 0;

        private int extraItemIdx = 0;

        //Widget/Notify View
        private View lastWidgetView;
        private View lastNotifyView;
        private Mode thisMode = Mode.WATCHFACE;
        private View blockTouchView;

        private enum Mode
        {
            WATCHFACE,
            WIDGET,
            WIDGET_LAST,
            NOITIFY,
            NOITIFY_LAST,
        }

        public CircularRecycler()
        {
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;

            containerView = new View()
            {
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = 360,
            };
            this.Add(containerView);

            panDetector = new PanGestureDetector();
            panDetector.Attach(this);
            panDetector.Detected += PanDetector_Detected;

            layoutManager = new DefaultLayoutManager();

            viewHolderList = new List<ViewHolder>();

            CreateLastPage();
        }

        private void CreateLastPage()
        {
            blockTouchView = new View()
            {
                Size2D = new Size2D(360, 360),
            };
            blockTouchView.TouchEvent += BlockTouchView_TouchEvent;
            
            //Widget
            lastWidgetView = new View()
            {
                Size = new Size(360, 360),
                CornerRadius = 180.0f,
                BackgroundColor = Color.Black,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };

            var widgetAddBtn = new Button(new OverlayAnimationButtonStyle())
            {
                Text = "Add Widget",
                IconURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/icons/icon.png",
                IconPadding = new Extents(0, 0, 50, 0),

                Size = new Size(200, 200),
                CornerRadius = 100,

                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true
            };
            widgetAddBtn.ClickEvent += WidgetAddBtn_ClickEvent;

            lastWidgetView.Add(widgetAddBtn);

            lastNotifyView = new View()
            {
                Size = new Size(360, 360),
                CornerRadius = 180.0f,
                BackgroundColor = Color.Black,
            };

            ImageView icon = new ImageView()
            {
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "icons/notification.png",
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
                Scale = new Vector3(0.7f, 0.7f, 0.7f),
            };
            lastNotifyView.Add(icon);

            TextLabel notifyText = new TextLabel()
            {
                Text = "No notifications",
                PointSize = 7.0f,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,

                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Position = new Position(0, 90),
                TextColor = Color.White,
            };
            lastNotifyView.Add(notifyText);

            TextLabel notifyTitle = new TextLabel()
            {
                Text = "Notification",
                PointSize = 7.5f,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,

                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Position = new Position(0, -100),
                TextColor = new Color(0.0f, 0.5f, 0.9f, 1.0f),
            };
            PropertyMap fontStyle = new PropertyMap();
            fontStyle.Add("weight", new PropertyValue("bold"));
            notifyTitle.FontStyle = fontStyle;
            lastNotifyView.Add(notifyTitle);

            notifyText.TouchEvent += Notify_AddItem;
        }

        private bool BlockTouchView_TouchEvent(object source, TouchEventArgs e)
        {
            return true;
        }

        private void WidgetAddBtn_ClickEvent(object sender, Button.ClickEventArgs e)
        {
            addWidgetHandler(sender, e);
        }

        private bool Notify_AddItem(object source, View.TouchEventArgs e)
        {
            if (addNotifyHandler != null && e.Touch.GetState(0) == PointStateType.Up)
            {
                CENTER_INDEX++;
                return addNotifyHandler(source, e);
            }

            return true;
        }


        private EventHandlerWithReturnType<object, Button.ClickEventArgs, bool> addWidgetHandler;
        public event EventHandlerWithReturnType<object, Button.ClickEventArgs, bool> AddWidgetEvent
        {
            add
            {
                addWidgetHandler += value;
            }
            remove
            {
                addWidgetHandler -= value;
            }
        }
        private EventHandlerWithReturnType<object, TouchEventArgs, bool> addNotifyHandler;
        public event EventHandlerWithReturnType<object, TouchEventArgs, bool> AddNotifyEvent
        {
            add
            {
                addNotifyHandler += value;
            }
            remove
            {
                addNotifyHandler -= value;
            }
        }

        private void PanDetector_Detected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            int mouse_nextX = (int)e.PanGesture.ScreenPosition.X;
            int distance = mouse_nextX - firstScreenPosition;
            int panning = mouse_nextX - panScreenPosition;

            switch (e.PanGesture.State)
            {
                case Gesture.StateType.Finished:
                    {
                        blockTouchView.Unparent();
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
                        if (thisMode != Mode.WIDGET_LAST && panning < 0)
                        {
                            layoutManager.DragPage(this, panning);
                        }
                        else if (thisMode != Mode.NOITIFY_LAST && panning > 0)
                        {
                            layoutManager.DragPage(this, panning);
                        }
                        panScreenPosition = mouse_nextX;
                        break;
                    }
                case Gesture.StateType.Started:
                    {

                        this.Add(blockTouchView);
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
            int sIdx = CENTER_INDEX;
            for (int i = 0; i < DefaultLayoutManager.CIRCLE_ITEM_COUNT; i++)
            {
                ViewHolder holder = adapter.OnCreateViewHolder();
                holder.BindingIndex = i;

                View childView = holder.GetView();

                viewHolderList.Add(holder);
                if (i < dataCount)
                {
                    adapter.OnBindViewHolder(holder, sIdx++);
                }
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

        public void SetLayoutManager(CircularRecyclerLayoutManager manager, ViewHolder selectVh = null)
        {
            if (layoutManager == manager)
            {
                return;
            }

            if (selectVh != null)
            {
                currentIndex = selectVh.BindingIndex;
            }
            else
            {
                //Show Watchface view
                currentIndex = CENTER_INDEX;
            }

            layoutManager = manager;
            layoutManager.animator.animationFinished += FinishAnimation;

            extraItemIdx = currentIndex;
            layoutManager.OrderByIndex(this);
            if (adapter.GetItemCount() == 1)
            {
                MakeLastWidgetPage(1);
            }
            MakeLastNotifyPage();
        }

        private void MakeLastWidgetPage(int sIdx)
        {
            lastWidgetView.Unparent();
            int idx = extraItemIdx + sIdx;
            if (idx >= viewHolderList.Count)
            {
                idx = idx % viewHolderList.Count;
            }
            View view = viewHolderList[idx].GetView();
            view.Add(lastWidgetView);
        }

        public void UpdateViewHolderOrder()
        {
            lastWidgetView.Unparent();
            BindingViewHolderData();

            MakeLastWidgetPage(1);
            //MakeLastWidgetPage();
            thisMode = Mode.WIDGET;
            //extraItemIdx++;
        }

        private void MakeLastNotifyPage()
        {
            //if(extraItemIdx - 1 < 0)
            {
                //lastNotifyView.Unparent();
                //lastWidgetView.Unparent();
                View view = viewHolderList[viewHolderList.Count - 1].GetView();
                view.Add(lastNotifyView);
            }
        }

        public void Next()
        {
            if (thisMode == Mode.WIDGET_LAST)
            {
                layoutManager.CancelPage(this);
                return;
            }

            extraItemIdx++;
            layoutManager.OrderByIndexAnimate(this, true);

            if (extraItemIdx < adapter.GetItemCount())
            {
                if (extraItemIdx == CENTER_INDEX)
                {
                    thisMode = Mode.WATCHFACE;
                }
                else
                {
                    thisMode = Mode.WIDGET;
                    lastNotifyView.Unparent();
                }

                BindingViewHolderData();
                //MakeLastWidgetPage();
            }
            else
            {
                thisMode = Mode.WIDGET_LAST;
            }

            //Pre-create Last page of Widget before 2 item.
            if (extraItemIdx + 1 == adapter.GetItemCount())
            {
                MakeLastWidgetPage(1);
            }
        }


        public void Prev()
        {
            if (thisMode == Mode.NOITIFY_LAST)
            {
                layoutManager.CancelPage(this);
                return;
            }

            extraItemIdx--;
            layoutManager.OrderByIndexAnimate(this, false);
            if (extraItemIdx >= 0)
            {
                if (extraItemIdx == CENTER_INDEX)
                {
                    thisMode = Mode.WATCHFACE;

                }
                else
                {
                    thisMode = Mode.WIDGET;
                    if (extraItemIdx == adapter.GetItemCount() - 2)
                    {
                        lastWidgetView.Unparent();
                    }
                }
                BindingViewHolderData();
                //MakeLastNotifyPage();
            }
            else
            {
                thisMode = Mode.NOITIFY_LAST;
            }

            //Pre-create Last page of Widget before 1 item.
            if (extraItemIdx - 1 == -1)
            {
                MakeLastNotifyPage();
            }
        }

        private void BindingViewHolderData()
        {
            int idx = extraItemIdx;
            if (idx >= viewHolderList.Count)
            {
                idx = idx % viewHolderList.Count;
            }
            adapter.OnBindViewHolder(viewHolderList[idx], extraItemIdx);
        }

        public void FinishAnimation()
        {
        }
    }
}