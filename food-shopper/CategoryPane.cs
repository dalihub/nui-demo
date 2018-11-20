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

        private void Initialize()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            //LayoutWidthSpecification = ChildLayoutData.MatchParent;

            AddTiles();
        }

        private void AddTiles()
        {
            Console.WriteLine("Load Categories");
            DirectoryInfo categoriesDirectory = new DirectoryInfo(categoriesLocation);
            foreach (var fi in categoriesDirectory.GetFiles( ".", SearchOption.AllDirectories))
            {
                Console.WriteLine(fi.Name);

                ImageView imageView = new ImageView();
                ImageVisual imageVisual = new ImageVisual();

                imageVisual.URL = categoriesDirectory + fi.Name;
//                imageVisual.DesiredWidth = 400;
  //              imageVisual.DesiredHeight = contentPaneHeight;
                imageView.Image = imageVisual.OutputVisualMap;
                imageView.LayoutWidthSpecificationFixed = 400;
                imageView.LayoutHeightSpecification = ChildLayoutData.MatchParent;

                imageView.Name = "ImageView_" + fi.Name;
                Add(imageView);
            }
        }
    };
}