using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace FoodShopper
{
    class ContentGroup : View
    {
        const uint MAX_ITEMS = 4;
        private uint itemsCount = 0;
        private ContentItemInfo[] contentItemsArray;

        public struct ContentItemInfo
        {
          public ContentItemInfo( uint _id, string _imageUrl )
          {
            id = _id;
            imageUrl = _imageUrl;
          }
          public uint id;
          public string imageUrl;
        }

        public ContentGroup()
        {
            Initialize();
        }

        private void Initialize()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            BackgroundColor = Color.Blue;
            WidthSpecification = LayoutParamPolicies.MatchParent;
            HeightSpecification = LayoutParamPolicies.MatchParent;
            contentItemsArray = new ContentItemInfo[ MAX_ITEMS ];
        }

        public void AddContentItem( ContentItemInfo itemInfo )
        {
            Console.WriteLine("AddContentItem at index:" + itemsCount );
            contentItemsArray[itemsCount++] = itemInfo;

            ImageView imageView = new ImageView();
            ImageVisual imageVisual = new ImageVisual();

            // Categories to be the height of the ContentPane whilst having a fixed width
            imageVisual.URL = itemInfo.imageUrl;
            imageView.Image = imageVisual.OutputVisualMap;
            imageView.LayoutWidthSpecificationFixed = 1000;
            imageView.HeightSpecification = LayoutParamPolicies.MatchParent;
            imageView.Padding = new Extents(40, 40, 0, 0);

            imageView.Name = "ImageView_" + itemInfo.imageUrl;
            Add(imageView);
        }
    }; // class ContentGroup

}// namespace FoodShopper