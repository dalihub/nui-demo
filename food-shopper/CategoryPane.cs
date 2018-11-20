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
            BackgroundColor = Color.Green;
            LayoutWidthSpecification = ChildLayoutData.MatchParent;
            LayoutHeightSpecificationFixed = 200;
            AddTiles();
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

                // Categories to be the height of the ContentPane whilst having a fixed width
                imageVisual.URL = categoriesDirectory + file.Name;
                imageView.Image = imageVisual.OutputVisualMap;
                imageView.LayoutWidthSpecificationFixed = 400;
                imageView.LayoutHeightSpecification = ChildLayoutData.MatchParent;

                imageView.Name = "ImageView_" + file.Name;
                Add(imageView);
            }
        }

    }; // Class CategoryPane
} // namespace FoodShopper