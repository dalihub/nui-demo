using System;
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace TextLabelSample
{
    class Program : NUIApplication
    {
        TextLabel category;
        string testtext = "test";
        protected override void OnCreate()
        {
            base.OnCreate();
            test();
        }

        public async Task test()
        {
            Window.Instance.TouchEvent += Instance_TouchEvent;
            View view = new View();
            view.Size2D = new Size2D(720, 1280);
            view.Position = new Position(0, 0, 0);
            view.BackgroundColor = Color.Cyan;
            Window.Instance.GetDefaultLayer().Add(view);

            Window.Instance.BackgroundColor = Color.Red;
            category = new TextLabel(testtext);
            category.TextColor = Color.White;
            category.PointSize = 19;
            category.VerticalAlignment = VerticalAlignment.Center;
            category.MultiLine = true;
            category.Position = new Position(0, 0, 0);
            category.BackgroundColor = Color.Blue;

            category.Size2D = new Size2D(300,300);
            category.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            category.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            category.TextColor = new Color(0.75f, 0.75f, 0.75f, 1.0f);
            category.Opacity = 1.0f;
            view.Add(category);

            //Timer timer = new Timer(1000);
            //timer.Tick += Timer_Tick;
            //timer.Start();
        }

        private void Instance_TouchEvent(object sender, Window.TouchEventArgs e)
        {
            testtext += "a";
            category.Text = testtext;
            //ategory.SizeWidth = 200;
            //category.SizeHeight = 500;
            Tizen.Log.Error("MYLOG", "size width : " + category.SizeWidth + ", sizeHeight : " + category.SizeHeight);
        }

        private bool Timer_Tick(object source, Timer.TickEventArgs e)
        {
            testtext += "a";
            category.Text = testtext;
            Tizen.Log.Error("MYLOG", "size width : " + category.SizeWidth + ", sizeHeight : " + category.SizeHeight);
            return true;
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
    }
}

