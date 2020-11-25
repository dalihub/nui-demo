using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;

namespace Demo
{
    class ItemDetailPage : Page
    {
        private ushort PADDING = 15;
        private float BACK_BUTTON_IMAGE_SIZE = 30;
        private float ITEM_IMAGE_SMALL_SIZE = 50;
        private Animation iconSlideAnimation;

        public ItemDetailPage(ItemData data) : base(data)
        {
            ScrollableBase infoScroll = new ScrollableBase()
            {
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                Size = new Size(Size),
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
            };
            Add(infoScroll);

            View information = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Margin = new Extents(0, 0, (ushort)NUIApplication.GetDefaultWindow().Size.Width, 0),
                Padding = new Extents(PADDING, PADDING, 0, 0),
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                }
            };
            infoScroll.Add(information);

            TextLabel itemName = new TextLabel()
            {
                Text = data.Name,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PixelSize = 24.0f,
                Margin = new Extents(0, 0, PADDING, 0),
                MultiLine = true,
            };
            information.Add(itemName);

            TextLabel origin = new TextLabel()
            {
                Text = "원산지 : " + data.Origin,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PixelSize = 16.0f,
                Margin = new Extents(0, 0, PADDING, 0),
                TextColor = new Color("#777777"),
            };
            information.Add(origin);

            TextLabel price = new TextLabel()
            {
                Text = data.Price,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PixelSize = 24.0f,
                Margin = new Extents(0, 0, PADDING, 0),
            };
            information.Add(price);

            View line = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 1,
                Margin = new Extents(0, 0, PADDING, PADDING),
                BackgroundColor = new Color("#f0f0f0")
            };
            information.Add(line);

            TextLabel detailHeader = new TextLabel()
            {
                Text = "상품에 대한 자세한 정보",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PixelSize = 24.0f,
                Margin = new Extents(0, 0, 0, PADDING),
            };
            information.Add(detailHeader);

            View fakeDetails = new View()
            {
                WidthSpecification = 480,
                HeightSpecification = 4000,
                BackgroundColor = Color.White,
            };
            information.Add(fakeDetails);

            View header = new View()
            {
                Size = new Size(NUIApplication.GetDefaultWindow().Size.Width, 80),
                BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.9f),
                Layout = new AbsoluteLayout()
                {
                    SetPositionByLayout = false,
                }
            };
            Add(header);

            ImageView itemImage = new ImageView()
            {
                ResourceUrl = "./res/images/" + data.ImageUrl,
                Size = new Size(NUIApplication.GetDefaultWindow().Size.Width, NUIApplication.GetDefaultWindow().Size.Width),
                DesiredWidth = NUIApplication.GetDefaultWindow().Size.Width,
                DesiredHeight = NUIApplication.GetDefaultWindow().Size.Width,
                PositionUsesPivotPoint = true,
                PivotPoint = Tizen.NUI.PivotPoint.TopCenter,
                ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter,
                Layout = new AbsoluteLayout()
                {
                    SetPositionByLayout = false,
                }
            };
            header.Add(itemImage);

            ImageView backButton = new ImageView()
            {
                Size = new Size(BACK_BUTTON_IMAGE_SIZE, BACK_BUTTON_IMAGE_SIZE),
                Color = Color.Black,
                Position = new Position(PADDING, 0),
                PositionUsesPivotPoint = true,
                PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                ResourceUrl = "./res/images/back.png",
            };
            header.Add(backButton);
            backButton.TouchEvent += (object source, TouchEventArgs args) =>
            {
                if (args.Touch.GetState(0) == PointStateType.Finished || args.Touch.GetState(0) == PointStateType.Up)
                {
                    PageController.Instance.GoBack();
                    return true;
                }
                else
                {
                    return false;
                }
            };

            TextLabel headerItemName = new TextLabel()
            {
                Text = data.Name,
                Size = new Size(NUIApplication.GetDefaultWindow().Size.Width - (PADDING * 2 + backButton.Size.Width) - (PADDING * 2 + ITEM_IMAGE_SMALL_SIZE), header.Size.Height),
                PixelSize = 18.0f,
                VerticalAlignment = VerticalAlignment.Center,
                Position = new Position(PADDING * 2 + backButton.Size.Width, 0),
                Opacity = 0.0f,
            };
            header.Add(headerItemName);

            iconSlideAnimation = new Animation(200);
            iconSlideAnimation.DefaultAlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut);

            infoScroll.Scrolling += (object source, ScrollEventArgs args) =>
            {
                float scrollPosition = Math.Abs(args.Position.Y);
                if (scrollPosition <= NUIApplication.GetDefaultWindow().Size.Width - header.Size.Height)
                {
                    float process = scrollPosition / (NUIApplication.GetDefaultWindow().Size.Width - header.Size.Height);
                    float targetRadius = ITEM_IMAGE_SMALL_SIZE / 2.0f * process;
                    targetRadius = targetRadius < 1f ? 1f : targetRadius;
                    float targetSize = -(NUIApplication.GetDefaultWindow().Size.Width - ITEM_IMAGE_SMALL_SIZE) * process + NUIApplication.GetDefaultWindow().Size.Width;
                    float targetPosition = PADDING * process;

                    itemImage.CornerRadius = targetRadius;
                    itemImage.Size = new Size(targetSize, targetSize);
                    itemImage.PositionY = targetPosition;
                    headerItemName.Opacity = 0.0f;
                    headerItemName.EnableAutoScroll = false;

                    if (itemImage.CurrentPosition.X == (NUIApplication.GetDefaultWindow().Size.Width / 2.0f - (PADDING + ITEM_IMAGE_SMALL_SIZE / 2.0f)))
                    {
                        header.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.9f);
                        iconSlideAnimation.Reset();
                        iconSlideAnimation.AnimateTo(itemImage, "positionX", 0.0f);
                        iconSlideAnimation.Play();
                    }
                }
                else if (scrollPosition <= NUIApplication.GetDefaultWindow().Size.Width + header.Size.Height)
                {
                    float process = (scrollPosition - NUIApplication.GetDefaultWindow().Size.Width + header.Size.Height) / (header.Size.Height * 2.0f);
                    float targetPosition = (NUIApplication.GetDefaultWindow().Size.Width / 2.0f - (PADDING + ITEM_IMAGE_SMALL_SIZE / 2.0f)) * process;
                    float targetButtonColor = -process + 1.0f;
                    float targetHeaderBackgroundColor = process > 0.8f ? 0.8f : process;

                    itemImage.CornerRadius = ITEM_IMAGE_SMALL_SIZE / 2.0f;
                    itemImage.Size = new Size(ITEM_IMAGE_SMALL_SIZE, ITEM_IMAGE_SMALL_SIZE);
                    itemImage.PositionY = PADDING;
                    headerItemName.Opacity = process;
                    headerItemName.EnableAutoScroll = true;

                    if (itemImage.CurrentPosition.X == 0.0f)
                    {
                        header.BackgroundColor = new Color("#ffd040");
                        iconSlideAnimation.Reset();
                        iconSlideAnimation.AnimateTo(itemImage, "positionX", (NUIApplication.GetDefaultWindow().Size.Width / 2.0f - (PADDING + ITEM_IMAGE_SMALL_SIZE / 2.0f)));
                        iconSlideAnimation.Play();
                    }
                }
                else
                {
                    itemImage.CornerRadius = ITEM_IMAGE_SMALL_SIZE / 2.0f;
                    itemImage.Size = new Size(ITEM_IMAGE_SMALL_SIZE, ITEM_IMAGE_SMALL_SIZE);
                    headerItemName.Opacity = 1.0f;
                }
            };

            BackKeyPressed += (object source, EventArgs args) =>
            {
                PageController.Instance.GoBack();
            };
        }
    }
}
