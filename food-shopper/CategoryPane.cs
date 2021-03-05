using System;
using System.IO;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace FoodShopper
{
    /// <summary>
    /// Horizontall scrolling food categories.
    /// </summary>
    class CategoryPane : View
    {
        private const string categoriesLocation = "./res/images/categories/";

        public CategoryPane()
        {
            Initialize();
        }

        public View GetCategory(int index)
        {
          Console.WriteLine("GetCategory:" + GetChildAt((uint)index).Name );

          return GetChildAt((uint)index);
        }

        private void Initialize()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            WidthSpecification = LayoutParamPolicies.MatchParent;
            HeightSpecification = 200;
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
                imageView.WidthSpecification = 400;
                imageView.HeightSpecification = LayoutParamPolicies.MatchParent;
                imageView.Padding = new Extents(25, 25, 0, 0);
                imageView.Name = "Category_" + file.Name;
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