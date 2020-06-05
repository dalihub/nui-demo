using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using static Tizen.NUI.BaseComponents.View;

namespace NUIWHMain
{
    class WidgetManagerViewer
    {
        private List<string> appStr;
        public ScrollableBase scrollable;
        private bool isShowing = false;

        public WidgetManagerViewer()
        {
            appStr = new List<string>();

            appStr.Add("app.png");
            appStr.Add("alaram.png");
            appStr.Add("contacts.png");
        }

        public void CreatePage()
        {
            scrollable = new ScrollableBase()
            {
                Size = new Size(360, 360),
                SnapToPage = true,
                
                ScrollingDirection = ScrollableBase.Direction.Horizontal,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };

            View view = new View()
            {
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = 360,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    SetPositionByLayout = false,
                },
                BackgroundColor = new Color(0.15f, 0.15f, 0.15f, 1.0f),
            };
            scrollable.Add(view);

            List<string> appList = WidgetApplicationInfo.LoadAllParameters();
            for (int i = 0; i < appStr.Count; i++)
            {

                ImageView viewer = new ImageView()
                {
                    Name = appList[i],
                    //ParentOrigin = ParentOrigin.Center,
                    //PivotPoint = PivotPoint.Center,
                    //PositionUsesPivotPoint = true,
                    Size = new Size(360, 360),
                    CornerRadius = 180.0f,
                    Scale = new Vector3(0.6f, 0.6f, 0.6f),
                    BackgroundImage = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "widget_capture/" + appStr[i],
                };
                viewer.TouchEvent += Viewer_TouchEvent;

                view.Add(viewer);
            }

        }

        private bool isMoving = false;
        private Animation ani = new Animation(200);
        private int prePosX;

        private bool Viewer_TouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                prePosX = (int)e.Touch.GetScreenPosition(0).X;
                isMoving = false;
            }
            else if (e.Touch.GetState(0) == PointStateType.Motion)
            {
                int posX = (int)e.Touch.GetScreenPosition(0).X;
                if (Math.Abs(prePosX - posX) > 10)
                {
                    isMoving = true;
                }
                return true;
            }
            else if (addWidgetHandler != null && e.Touch.GetState(0) == PointStateType.Up)
            {
                if(!isMoving)
                {
                    addWidgetHandler(source, e);
                    ani.Clear();
                    ani.DefaultAlphaFunction = new AlphaFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 1.0f));
                    ani.AnimateTo(source as View, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                    ani.Finished += Ani_Finished; ;
                    ani.Play();
                }
            }
            return true;
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            HideViewer();
        }

        public void ShowViewer(Window window)
        {
            isShowing = true;
            CreatePage();
            window.Add(scrollable);
        }

        public void HideViewer()
        {
            isShowing = false;
            scrollable.Unparent();
        }

        public bool IsShowing()
        {
            return isShowing;
        }

        public View GetViewer()
        {
            return scrollable;
        }


        private EventHandlerWithReturnType<object, TouchEventArgs, bool> addWidgetHandler;
        public event EventHandlerWithReturnType<object, TouchEventArgs, bool> AddWidgetEvent
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

    }
}
