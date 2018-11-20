using System;
using System.IO;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace FoodShopper
{
    class CategoryPane : View
    {
        private const string categoriesLocation = "./res/images/categories/";

        public CategoryPane()
        {
            Initialize();
        }

        public View GetCategory()
        {
          Console.WriteLine("GetCategory:" + GetChildAt(0).Name );

          return GetChildAt(0);
        }

        private void Initialize()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            BackgroundColor = Color.Green;
            LayoutWidthSpecification = ChildLayoutData.MatchParent;
            LayoutHeightSpecificationFixed = 200;
            AddTiles();
            Focusable = true;
            FocusGained += OnFocusGained;
        }

        protected virtual void OnFocusGained(object sourceObj, EventArgs args)
        {
            Console.WriteLine("OnFocusGained");
        }


        private void AddTiles()
        {
            Console.WriteLine("Load Categories");
            DirectoryInfo categoriesDirectory = new DirectoryInfo(categoriesLocation);
            foreach (var file in categoriesDirectory.GetFiles( ".", SearchOption.AllDirectories))
            {
                Console.WriteLine(file.Name);

                ImageView imageView = new ImageView();
                ImageVisual imageVisual = new ImageVisual();
                //imageView.Focusable = true;

                // Categories to be the height of the ContentPane whilst having a fixed width
                imageVisual.URL = categoriesDirectory + file.Name;
                imageView.Image = imageVisual.OutputVisualMap;
                imageView.LayoutWidthSpecificationFixed = 400;
                imageView.LayoutHeightSpecification = ChildLayoutData.MatchParent;

                imageView.Name = "ImageView_" + file.Name;
                Add(imageView);
            }

            for ( uint index = 0; index < Children.Count; index++ )
            {
              View currentView = GetChildAt( index );
              if( index+1 < Children.Count )
              {
                currentView.RightFocusableView = GetChildAt( index+1 );
              }

              if( index > 0)
              {
                currentView.LeftFocusableView = GetChildAt( index-1 );
              }
            }

        }

    }; // Class CategoryPane
} // namespace FoodShopper