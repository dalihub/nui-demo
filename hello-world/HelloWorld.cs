using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace ScrollableBaseIssue
{
    class Program : Tizen.NUI.NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.GetDefaultLayer().Add(CreateView());
        }

        private static View CreateView()
        {
            var view = new IssuePreviewView();

            view.ScrollTo(720,false); // it does not work

            return view;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }

        public class IssuePreviewView : ScrollableBase
        {
            private View viewContainer; 

            public IssuePreviewView() : base()
            {
                SetViewLayoutSettings();
                CreatePages(5);

            }

            private void SetViewLayoutSettings()
            {
                SizeHeight = 360;
                SizeWidth = 480;
                SnapToPage = true;
                ScrollingDirection = ScrollableBase.Direction.Horizontal;

                viewContainer = new View()
                {
                    WidthSpecification = LayoutParamPolicies.WrapContent,
                    HeightSpecification = 360,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Horizontal,
                    },
                };

                Add(viewContainer);
            }

            private void CreatePages(int n)
            {
                for (int i = 0; i < n; i++)
                {
                    var view = new View()
                    {
                        SizeHeight = 360,
                        SizeWidth = 480,
                        BackgroundColor = i%2 == 0 ? Color.Cyan:Color.Magenta,
                    };

                    view.Add(new TextLabel(i.ToString())
                    {
                        PointSize = 25,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        TextColor = Color.Yellow,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    });

                    viewContainer.Add(view);
                }
            }
         }
    }
}
