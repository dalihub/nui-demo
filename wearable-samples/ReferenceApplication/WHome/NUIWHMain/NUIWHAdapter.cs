using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using WearableGallerySample;

namespace NUIWHMain
{
    public class FaceData
    {
        public FaceType type;
        public string res;

        public FaceData(FaceType type, string res)
        {
            this.type = type;
            this.res = res;
        }

        public enum FaceType
        {
            WATCH = 0,
            WIDGET = 1,
            NOTIFY = 2,
            SAMPLE = 3,
        }
    }

    public class NUIWHAdapter : CircularRecycler.Adapter
    {
        private WidgetView _widgetView = null;

        private List<string> widgetList;
        private Dictionary<string, WidgetView> widgetViewList = new Dictionary<string, WidgetView>();
        private WidgetViewManager _viewManager = null;

        public List<FaceData> dataList;


        public class ViewHolder : CircularRecycler.ViewHolder
        {
            public View mainView;
            public TextLabel textView;

            public View widgetContainerView;

            public View notifyView;
            public TextLabel notifyText;
            public TextLabel notifyTitle;
            public ImageView notifyIcon;

            public TextLabel sampleTitle;

            private View blockView;
            public ViewHolder(View view) : base(view)
            {
                blockView = new View()
                {
                    Size = new Size(360, 360),
                    CornerRadius = 180,
                    ParentOrigin = ParentOrigin.Center,
                    PivotPoint = PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                };
                blockView.TouchEvent += BlockView_TouchEvent;

                mainView = new View()
                {
                    Size = new Size(360, 360),
                    CornerRadius = 180,
                    ParentOrigin = ParentOrigin.Center,
                    PivotPoint = PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                };
                view.Add(mainView);

                textView = new TextLabel()
                {
                    ParentOrigin = ParentOrigin.Center,
                    PivotPoint = PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                mainView.Add(textView);

                widgetContainerView = new View()
                {
                    Size = new Size(360, 360),
                    CornerRadius = 180,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                };

                sampleTitle = new TextLabel()
                {
                    PointSize = 7.0f,
                    ParentOrigin = ParentOrigin.TopCenter,
                    PivotPoint = PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                    Position = new Position(0, 60),
                    TextColor = Color.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                widgetContainerView.Add(sampleTitle);

                notifyView = new View()
                {
                    Size = new Size(360, 360),
                    CornerRadius = 180,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                    BackgroundColor = new Color(0.1f, 0.1f, 0.1f, 1.0f),
                };


                notifyIcon = new ImageView()
                {
                    ResourceUrl = GetResourcePath() + "icons/email.png",
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true,
                    Scale = new Vector3(0.7f, 0.7f, 0.7f),
                };
                notifyView.Add(notifyIcon);

                notifyText = new TextLabel()
                {
                    PointSize = 7.0f,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true,

                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Position = new Position(0, 90),
                    TextColor = Color.White,
                };
                notifyView.Add(notifyText);

                notifyTitle = new TextLabel()
                {
                    Text = "email",
                    PointSize = 7.5f,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    PositionUsesPivotPoint = true,

                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Position = new Position(0, -100),
                    TextColor = new Color(0.9f, 0.2f, 0.2f, 1.0f),
                };
                PropertyMap fontStyle = new PropertyMap();
                fontStyle.Add("weight", new PropertyValue("bold"));
                notifyTitle.FontStyle = fontStyle;
                notifyView.Add(notifyTitle);

            }

            private bool BlockView_TouchEvent(object source, View.TouchEventArgs e)
            {
                return true;
            }

            public void SetWatchFace()
            {
                blockView.Unparent();
                widgetContainerView.Unparent();
                ResetWidgetContainer();

                //Draw WatchFaceView.
                mainView.BackgroundColor = new Color(0.15f, 0.15f, 0.15f, 1.0f);
                textView.Show();
                textView.Text = "WatchFace";
                textView.TextColor = Color.White;
            }

            public void SetNotifyView(string notifyTtext)
            {
                blockView.Unparent();
                widgetContainerView.Unparent();
                ResetWidgetContainer();
                textView.Hide();
                mainView.Add(notifyView);
                mainView.BackgroundColor = Color.Black;
                notifyText.Text = notifyTtext;
            }

            public void SetWidgetView(WidgetView wv)
            {

                ResetWidgetContainer();
                textView.Hide();
                sampleTitle.Hide();
                mainView.BackgroundColor = Color.Black;

                widgetContainerView.Add(wv);
                mainView.Add(widgetContainerView);
                mainView.Add(blockView);
            }
            public void SetSampleView(Dictionary<string, View> sampleViewList, string title)
            {
                blockView.Unparent();
                ResetWidgetContainer();
                sampleTitle.Show();
                textView.Hide();
                mainView.BackgroundColor = Color.Black;

                sampleTitle.Text = title;
                if(title == "WearableList" || title == "Message")
                {
                    sampleTitle.Hide();
                }
                else
                {
                    sampleTitle.Show();
                }
                View view = sampleViewList[title];
                view.Position = new Position(0, 0);
                widgetContainerView.Add(view);
                mainView.Add(widgetContainerView);
            }

            private void ResetWidgetContainer()
            {
                if (widgetContainerView.Children.Count > 1)
                {
                    widgetContainerView.Children[1].Unparent();
                }
            }
        }

        public void AddNotify(FaceData faceData)
        {
            dataList.Insert(0, faceData);
        }

        public void AddWidget(FaceData faceData)
        {
            if(!widgetViewList.ContainsKey(faceData.res))
            {
                widgetViewList.Add(faceData.res, CreateWidget(faceData.res));
            }
            dataList.Add(faceData);
        }
        private SampleCreator sampleCreator;
        private Dictionary<string, View> sampleViewList;

        public NUIWHAdapter(WidgetViewManager manager)
        {
            _viewManager = manager;
            sampleViewList = new Dictionary<string, View>();

            dataList = new List<FaceData>();
            dataList.Add(new FaceData(FaceData.FaceType.WATCH, ""));
            sampleCreator = new SampleCreator();
            int count = sampleCreator.GetDictCount();

            for (int i = 0; i < count; i++)
            {
                string res = sampleCreator.GetDictString(i);
                dataList.Add(new FaceData(FaceData.FaceType.SAMPLE, res));
                sampleViewList.Add(res, sampleCreator.CallCreatorFunction(res));
            }

            widgetList = WidgetApplicationInfo.LoadAllParameters();
        }

        public WidgetView CreateWidget(string res)
        {
            _widgetView = _viewManager.AddWidget(res, "", 360, 360, 0);
            _widgetView.WidgetContentUpdated += OnWidgetContentUpdated;
            _widgetView.Size2D = new Size2D(360, 360);
            _widgetView.ParentOrigin = ParentOrigin.TopLeft;
            _widgetView.PivotPoint = PivotPoint.TopLeft;
            _widgetView.PositionUsesPivotPoint = true;
            return _widgetView;
        }

        private void OnWidgetContentUpdated(object sender, WidgetView.WidgetViewEventArgs e)
        {
            Tizen.Log.Fatal("NUIWidget", "Widget view content updated triggered");
        }

        public override CircularRecycler.ViewHolder OnCreateViewHolder()
        {
            View view = new View()
            {
                Size = new Size(360, 360),
                CornerRadius = 180,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };

            NUIWHAdapter.ViewHolder viewHolder = new NUIWHAdapter.ViewHolder(view);
            return viewHolder;
        }

        public override void OnBindViewHolder(CircularRecycler.ViewHolder holder, int position)
        {
            NUIWHAdapter.ViewHolder vHolder = holder as NUIWHAdapter.ViewHolder;
            vHolder.notifyView.Unparent();
            Tizen.Log.Error("MYLOG", "OnBinde position : " + position);

            vHolder.textView.Text = "" + position;
            
            FaceData faceDate = dataList[position];
            switch (faceDate.type)
            {
                case FaceData.FaceType.WATCH:
                    vHolder.SetWatchFace();
                    Tizen.Log.Error("MYLOG", "Draw Watch Face");
                    break;
                case FaceData.FaceType.WIDGET:
                    //TEMPORARY CODE -->Unblocking touch NUIWidget
                    vHolder.SetWidgetView(widgetViewList[faceDate.res]);
                    Tizen.Log.Error("MYLOG", "Draw Widget : " + faceDate.res);
                    break;
                case FaceData.FaceType.NOTIFY:
                    vHolder.SetNotifyView(faceDate.res);
                    Tizen.Log.Error("MYLOG", "Draw Notify");
                    break;
                case FaceData.FaceType.SAMPLE:
                    vHolder.SetSampleView(sampleViewList, faceDate.res);
                    Tizen.Log.Error("MYLOG", "Draw Sample");
                    break;
            }
        }
        public override int GetItemCount()
        {
            return dataList.Count;
        }

        public static string GetResourcePath()
        {
            return Tizen.Applications.Application.Current.DirectoryInfo.Resource;
        }

    }
}
