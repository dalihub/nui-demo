using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace WearableGallerySample
{
    class Program : NUIApplication
    {

        private WearableGallery gallery;
        private Window defaultWindow;
        private List<WearableGalleryLayoutManager> lm = new List<WearableGalleryLayoutManager>();
        private int galleryType = 0;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            defaultWindow = GetDefaultWindow();
            defaultWindow.BackgroundColor = Color.White;
            defaultWindow.KeyEvent += OnKeyEvent;

            View bgView = new View()
            {
                Size = new Size(360, 360),
                BackgroundColor = Color.Black,
                CornerRadius = 180,

                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true
            };

            gallery = new WearableGallery()
            {
                Size = new Size(360, 360),
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true
            };
            gallery.SetAdapter(new UserGalleryAdapter());
            gallery.SetLayoutManager(new DefaultLayoutManager());
            gallery.ChildTouchEvent += ChildItem_TouchEvent;

            bgView.Add(gallery);
            defaultWindow.Add(bgView);

            lm.Add(new DefaultLayoutManager());
            lm.Add(new ScaleDownLayoutManager());
            lm.Add(new GridLayoutManager());
            lm.Add(new CircularLayoutManager());

            defaultWindow.WheelEvent += DefaultWindow_WheelEvent;
        }

        private void DefaultWindow_WheelEvent(object sender, Window.WheelEventArgs e)
        {
            if(e.Wheel.Type == Wheel.WheelType.CustomWheel)
            {
                Tizen.Log.Error("MYLOG", "wheel: " + e.Wheel.Direction);
                Tizen.Log.Error("MYLOG", "galleryType: " + galleryType);
                if (e.Wheel.Direction == 1)
                {
                    if(galleryType + 1 <= 3)
                    {
                        galleryType++;
                        gallery.SetLayoutManager(lm[galleryType]);
                    }
                    else
                    {
                        gallery.Next();
                    }
                }
                else if (e.Wheel.Direction == -1)
                {
                    if (galleryType + 1 <= 3)
                    {
                        //galleryType++;
                        //gallery.SetLayoutManager(lm[galleryType]);
                    }
                    else
                    {
                        gallery.Prev();
                    }
                }
            }
        }

        private bool ChildItem_TouchEvent(object source, View.TouchEventArgs e)
        {
            if ((e.Touch.GetState(0) == PointStateType.Up))
            {
                galleryType = 0;
                WearableGallery.ViewHolder viewHolder = source as WearableGallery.ViewHolder;
                gallery.SetLayoutManager(lm[0], viewHolder);
            }
            return true;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                Tizen.Log.Error("MYLOG", "e.Key.KeyPressedName : " + e.Key.KeyPressedName + "\n");
                switch (e.Key.KeyPressedName)
                {
                    case "XF86Back":
                    case "Escape":
                        {
                            Exit();
                            break;
                        }
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                        {
                            int num = Int32.Parse(e.Key.KeyPressedName);
                            gallery.SetLayoutManager(lm[num]);
                            break;
                        }
                    case "Left":
                        {
                            gallery.Prev();
                            break;
                        }
                    case "Right":
                        {
                            gallery.Next();
                            break;
                        }
                }
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }


    public class UserGalleryAdapter : WearableGallery.Adapter
    {
        public List<CommonResource.ResourceData> imageFileList;

        public class ViewHolder : WearableGallery.ViewHolder
        {
            public ImageView imageView;

            public ViewHolder(View view) : base(view)
            {
                imageView = new ImageView()
                {
                    Size = new Size(360, 360),
                    CornerRadius = 180,
                };
                view.Add(imageView);
            }

        }

        public UserGalleryAdapter()
        {
            imageFileList = SaveImageIconList();
        }

        public override WearableGallery.ViewHolder OnCreateViewHolder()
        {
            View view = new View()
            {
                Size = new Size(360, 360),
                CornerRadius = 180,
            };
            UserGalleryAdapter.ViewHolder viewHolder = new UserGalleryAdapter.ViewHolder(view);
            return viewHolder;
        }

        public override void OnBindViewHolder(WearableGallery.ViewHolder holder, int position)
        {
            UserGalleryAdapter.ViewHolder vHolder = holder as UserGalleryAdapter.ViewHolder;
            vHolder.imageView.BackgroundImage = imageFileList[position].path;
        }

        public override int GetItemCount()
        {
            return imageFileList.Count;
        }

        public static string GetResourcePath()
        {
            return Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        }


        private List<CommonResource.ResourceData> SaveImageIconList(string resPath = "images/")
        {
            List<CommonResource.ResourceData> imageFileList = new List<CommonResource.ResourceData>();
            String FolderName = Tizen.Applications.Application.Current.DirectoryInfo.Resource + resPath;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".jpg") == 0)
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
    }

}
