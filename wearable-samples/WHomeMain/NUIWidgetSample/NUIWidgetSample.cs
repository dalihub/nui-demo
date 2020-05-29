using System;
using System.Collections.Generic;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWidgetSample
{
    class WHomeNUIWidget : Widget
    {
        private TextLabel contents;
        private ImageView selectIcon = null;

        protected override void OnCreate(string contentInfo, Window window)
        {
            base.OnCreate(contentInfo, window);
            window.SetTransparency(true);
            Initialize();
        }

        void Initialize()
        {
            View backgroundView = new View()
            {
                Size = new Size(360, 360),
                CornerRadius = 180.0f,
            };
            Window.Instance.GetDefaultLayer().Add(backgroundView);

            TextLabel title = new TextLabel("NUI Samples")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.White,
                PointSize = 9.0f,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
                Position = new Position(0, -100),
            };
            PropertyMap fontStyle = new PropertyMap();
            fontStyle.Add("weight", new PropertyValue("bold"));
            title.FontStyle = fontStyle;
            backgroundView.Add(title);

            TextLabel descript = new TextLabel("Select apps")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.White,
                PointSize = 5.0f,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
                Position = new Position(0, -60),
            };
            backgroundView.Add(descript);

            View containerView = new View()
            {
                
                Size = new Size(150, 100),
                Position = new Position(-20, 30),
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                },
            };
            backgroundView.Add(containerView);


            List<string> appStr = new List<string>();
            appStr.Add("apps.png");
            appStr.Add("gallery.png");

            for (int i = 0; i < appStr.Count; i++)
            {
                ImageView iconView = new ImageView()
                {
                    Name = appStr[i].Substring(0, appStr[i].Length - 4),
                    BackgroundImage = Tizen.Applications.Application.Current.DirectoryInfo.Resource + appStr[i],
                    Size = new Size(72, 72),
                    Margin = new Extents(10, 10, 0, 0),
                };
                iconView.TouchEvent += IconView_TouchEvent;
                containerView.Add(iconView);
            }

            contents = new TextLabel("-")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.White,
                PointSize = 7.0f,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
                Position = new Position(0, 100),
            };
            backgroundView.Add(contents);
        }
        
        private bool IconView_TouchEvent(object source, View.TouchEventArgs e)
        {
            if(e.Touch.GetState(0) == PointStateType.Up)
            {
                ImageView preIcon = selectIcon;
                selectIcon = source as ImageView;

                Animation ani = new Animation(200);
                ani.DefaultAlphaFunction = new AlphaFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 1.0f));

                if (preIcon == selectIcon)
                {
                    AppControl appcontrol = new AppControl();
                    appcontrol.Operation = AppControlOperations.Default;
                    if (selectIcon.Name == "apps")
                    {
                        appcontrol.ApplicationId = "org.tizen.example.NUIWHome";
                    }
                    else if (selectIcon.Name == "gallery")
                    {

                        appcontrol.ApplicationId = "org.tizen.example.WearableGallerySample";
                    }
                    else
                    {
                        return true;
                    }
                    AppControl.SendLaunchRequest(appcontrol);

                    return true;
                }

                contents.Opacity = 0.0f;
                contents.Text = selectIcon.Name;
                if (preIcon != null)
                {
                    ani.AnimateTo(preIcon, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                }
                ani.AnimateTo(selectIcon, "Scale", new Vector3(1.2f, 1.2f, 1.2f));
                ani.AnimateTo(contents, "Opacity", 1.0f);
                ani.Play(); 

            }


            return true;
        }

    }

    class Program : NUIWidgetApplication
    {
        public Program(System.Type type) : base(type)
        {
        }

        protected override void OnCreate()
        {
            base.OnCreate();
        }

        public override void Run(string[] args)
        {
            base.Run(args);
        }

        static void Main(string[] args)
        {
            var app = new Program(typeof(WHomeNUIWidget));
            app.Run(args);
            app.Exit();
        }
    }
}
