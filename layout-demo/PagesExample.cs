using System;
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutDemo
{
    class PagesExample : Example
    {
        public PagesExample() : base( "PagesExample" )
        {}

        static class TestImages
        {
            public const string iconsDir = "/images/applicationIcons";
        }

        private readonly string[] applicationNames = new string[]
        {
            "Gallery", "Music", "Calendar", "Recipes"
        };

        private readonly Color[] colors = { new Color(0.0f, 0.6f, 1.0f, 1.0f), new Color(0.7f, 0.0f, 1.0f, 1.0f),
                                            new Color(0.8f, 0.27f, 0.27f, 1.0f), new Color(0.0f, 0.8f, 0.4f, 1.0f) };

        private string[] applicationIconsArray;

        private View root = null;
        private View pagesContainer = null;

        private int numberOfPages = 4;

        private Tizen.NUI.Components.ScrollableBase scrollable = null;

        public override void Create()
        {
            Window.Instance.BackgroundColor = Color.White;
            float rootWidth = Window.Instance.WindowSize.Width;
            float rootHeight = Window.Instance.WindowSize.Height;
            root = new View()
            {
                Size = new Size(rootWidth, rootHeight, 0.0f),
            };
            Window.Instance.Add(root);

            CreatePagesContainer();
            PopulateContainerWithPages();
        }

        public void Deactivate()
        {
            if (root != null)
            {
                root.Dispose();
            }
        }

        private void GetFiles(string sourceDirectory, ref string[] targetArray)
        {
            targetArray = Directory.GetFiles(sourceDirectory);
        }

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(root);
        }

        private void CreatePagesContainer()
        {

            LinearLayout linear = new LinearLayout();

            linear.LinearOrientation = LinearLayout.Orientation.Horizontal;

            pagesContainer = new View()
            {
                Layout = linear,
                Name = "PageContainer",
                WidthSpecification = Window.Instance.WindowSize.Width * numberOfPages,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = new Color(0.4f,0.7f,1.0f,1.0f),
                Padding = new Extents(40, 40, 0, 0),
            };

            scrollable = new Tizen.NUI.Components.ScrollableBase()
            {
                Name = "ScrollableBase",
                FlickAnimationSpeed = 0.8f,
                FlickDistanceMultiplierRange = new Vector2(0.2f, 0.5f),
                ScrollingDirection = Tizen.NUI.Components.ScrollableBase.Direction.Horizontal,
                SnapToPage = true,
            };
            scrollable.Add(pagesContainer);

            scrollable.WidthSpecification = LayoutParamPolicies.MatchParent;
            scrollable.HeightSpecification = LayoutParamPolicies.MatchParent;

            root.Add(scrollable);
        }

        private View CreatePage(int imageIndex, string text)
        {
            var rand = new System.Random();

            LinearLayout linearLayout = new LinearLayout()
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
            };

            View page = new View()
            {
                WidthSpecification = Window.Instance.WindowSize.Width,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Layout = linearLayout,
                Padding = new Extents(15, 15, 40, 40),
                Name = "page-" + text,
                BackgroundColor = colors[(rand.Next(0, 3))],
            };

            TextLabel textLabel = new TextLabel()
            {
                Text = text,
                Name = "page-title-" + text,
                Margin = new Extents(10, 0, 0, 0),
                TextColor = new Color(0.6f, 0.6f, 0.6f, 1),
                PointSize = 38,
                FontFamily = "SamsungOneUI 500C",
            };
            page.Add(textLabel);

            if (imageIndex >= 0)
            {
                ImageView icon = new ImageView(applicationIconsArray[imageIndex])
                {
                    WidthSpecification = 200,
                    HeightSpecification = 200,
                };
                page.Add(icon);
            }
            return page;
        }

        private void PopulateContainerWithPages()
        {
            GetFiles("./res"+TestImages.iconsDir, ref applicationIconsArray);
            int numberOfIcons = applicationIconsArray.Length;
            var rand = new System.Random();

            for (int index=0; index < numberOfPages; index++)
            {
                pagesContainer.Add(CreatePage(rand.Next(0, numberOfIcons), applicationNames[index]));
            };
            scrollable.NumberOfPages = numberOfPages;

        }
    };
}
