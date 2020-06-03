
using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using NUIWHome.Common;

namespace NUIWHome
{
    public class HelloRotary : NUIApplication
    {
        //fix - icon size 62x62
        private int ICON_SIZE = 62;

        private RotarySelector rotarySelector;
        private Window defaultWindow;


        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private void Initialize()
        {
            defaultWindow = GetDefaultWindow();
            defaultWindow.BackgroundColor = Color.White;
            defaultWindow.KeyEvent += DefaultWindow_KeyEvent;

            InitializeDefaultUI();

            //Wait Loading time...
            Timer StartAnimationDelayTimer = new Timer(300);
            StartAnimationDelayTimer.Tick += DelayTimer_Tick;
            StartAnimationDelayTimer.Start();

        }

        private bool DelayTimer_Tick(object source, Timer.TickEventArgs e)
        {
            rotarySelector.StartAppsAnimation();
            return false;
        }

        //Create Default UI
        public void InitializeDefaultUI()
        {
            rotarySelector = new RotarySelector()
            {
                Size2D = defaultWindow.WindowSize,
                BackgroundColor = Color.Black,
            };

            List<CommonResource.ResourceData> imageFileList = SaveImageIconList();
            for (int i = 0; i < imageFileList.Count; i++)
            {
                RotarySelectorItem item = new RotarySelectorItem()
                {
                    ResourceUrl = imageFileList[i].path,
                    Size = new Size(ICON_SIZE, ICON_SIZE),
                    CornerRadius = ICON_SIZE / 2,
                    MainText = imageFileList[i].name,
                };
                item.Clicked += Item_Clicked;
                item.Selected += Item_Selected;
                //Icon init:opacity 0, for starting animation
                item.Opacity = 0.0f;
                rotarySelector.AppendItem(item);
            }
            defaultWindow.Add(rotarySelector);

        }

        private void Item_Clicked(object sender, EventArgs e)
        {
            RotarySelectorItem item = sender as RotarySelectorItem;
            Tizen.Log.Error("MYLOG", "clicked item text :" + item.MainText);
        }

        private void Item_Selected(object sender, EventArgs e)
        {
            RotarySelectorItem item = sender as RotarySelectorItem;
            Tizen.Log.Error("MYLOG", "Selected item text :" + item.MainText);
        }

        //Store Image all icons to list.
        private List<CommonResource.ResourceData> SaveImageIconList(string resPath = "images/icons/")
        {
            List<CommonResource.ResourceData> imageFileList = new List<CommonResource.ResourceData>();
            String FolderName = Tizen.Applications.Application.Current.DirectoryInfo.Resource + resPath;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".png") == 0)
                {
                    String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                    FileNameOnly = string.Format("{0}{1}", char.ToUpper(FileNameOnly[0]), FileNameOnly.Remove(0, 1));
                    //FileNameOnly = FileNameOnly.Substring(0, FileNameOnly.Length >= 6 ? 6 : FileNameOnly.Length);
                    String FullFileName = File.FullName;

                    imageFileList.Add(new CommonResource.ResourceData(FileNameOnly, FullFileName));
                }
            }
            imageFileList.Sort(delegate (CommonResource.ResourceData A, CommonResource.ResourceData B)
            {
                return A.name.CompareTo(B.name);
            });
            return imageFileList;
        }


        private void DefaultWindow_KeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Up && (e.Key.KeyPressedName == "XF86Back"))
            {
                if (rotarySelector.GetCurrentMode() == RotarySelector.Mode.EditMode)
                {
                    rotarySelector.SetNormalMode();
                }
                else
                {
                    Exit();
                }
            }
        }

        static void Main(string[] args)
        {
            HelloRotary example = new HelloRotary();
            example.Run(args);
        }
    }
}
