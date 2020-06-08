using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI.Wearable;

namespace WearableSample
{
    class MenuItem : RecycleItem
    {
        public MenuItem()
        {
            Size = new Size(360, 108);

            BackgroundView = new View()
            {
                CornerRadius = 54,
                BackgroundColor = new Color("#FFFFFF0F"),
                Opacity = 0.0f,
                Size = new Size(360, 108),
            };

            View content = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    LinearAlignment = LinearLayout.Alignment.CenterVertical,
                },
                Padding = new Extents(16, 30, 0, 0)
            };

            Add(BackgroundView);
            Add(content);

            Icon = new ImageView()
            {
                Size = new Size(46, 46),
                Margin = new Extents(23, 31, 0, 0),
                Color = Color.White,
            };

            Title = new TextLabel()
            {
                PixelSize = 38.0f,
                Weight = 1,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextColor = Color.White,
            };

            content.Add(Icon);
            content.Add(Title);
        }

        public override void OnFocusGained()
        {
            BackgroundView.Opacity = 1.0f;
        }

        public override void OnFocusLost()
        {
            BackgroundView.Opacity = 0.0f;
        }

        public View BackgroundView { get; set; }
        public ImageView Icon { get; set; }
        public TextLabel Title { get; set; }
    }

    class MenuListAdapter : RecycleAdapter
    {
        public MenuListAdapter()
        {

        }

        public override RecycleItem CreateRecycleItem()
        {
            return new MenuItem();
        }

        public override void BindData(RecycleItem item)
        {
            MenuItem target = item as MenuItem;
            MenuData data = Data[item.DataIndex] as MenuData;

            target.Title.Text = data.Title;
            target.Icon.ResourceUrl = NUIApplication.Current.DirectoryInfo.Resource + "/images/" + data.ResourceUrl;
        }
    }

    class MenuData
    {
        public string Title { get; set; }
        public string ResourceUrl { get; set; }
    }


    class MenuList : WearableList
    {
        public MenuList()
        {
            Adapter = new MenuListAdapter()
            {
                Data = new List<object>{
                    new MenuData(){Title = "Write", ResourceUrl = "send_message.png"},
                    new MenuData(){Title = "Delete", ResourceUrl = "delete.png"},
                    new MenuData(){Title = "Settings", ResourceUrl = "settings.png"}
                }   
            };
        }
    }
}