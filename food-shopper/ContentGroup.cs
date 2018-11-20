using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace FoodShopper
{
    class ContentGroup : View
    {
        const uint MAX_ITEMS = 3;
        static private uint itemsCount = 0;
        private ContentItemInfo[] contentItemsArray;

        public struct ContentItemInfo
        {
            public uint id;
            public string imageUrl;
        }

        public ContentGroup()
        {
            Initialise();
        }

        private void Initialise()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            BackgroundColor = Color.Blue;
            LayoutWidthSpecification = ChildLayoutData.MatchParent;
            LayoutHeightSpecification = ChildLayoutData.MatchParent;
            contentItemsArray = new ContentItemInfo[ MAX_ITEMS ];
        }

        private void AddContentItem( ContentItemInfo itemInfo )
        {
            contentItemsArray[itemsCount++] = itemInfo;

                ImageView imageView = new ImageView();
                ImageVisual imageVisual = new ImageVisual();

                // Categories to be the height of the ContentPane whilst having a fixed width
                imageVisual.URL = itemInfo.imageUrl;
                imageView.Image = imageVisual.OutputVisualMap;
                imageView.LayoutWidthSpecificationFixed = 1000;
                imageView.LayoutHeightSpecification = ChildLayoutData.MatchParent;

                imageView.Name = "ImageView_" + itemInfo.imageUrl;
                Add(imageView);
        }
    }; // class ContentGroup

}// namespace FoodShopper