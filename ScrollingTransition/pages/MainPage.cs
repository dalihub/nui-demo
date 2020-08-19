using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;
using System.Numerics;

namespace Demo
{
    class MainPage : Page
    {
        public MainPage() : base()
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

            infoScroll.ContentContainer.Padding = new Extents(0, 0, 80, 0);

            List<object> dummyData = ItemData.CreateDummyItemData(50);
            Random r = new Random();

            foreach (ItemData data in dummyData)
            {
                View item = new View()
                {
                    Size = new Size(Size.Width, 260),
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Vertical,
                    },
                    Padding = new Extents(15, 15, 0, 0),
                    Margin = new Extents(0, 0, 15, 0),
                };
                infoScroll.Add(item);

                View mainInformation = new View()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 200,
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Horizontal,
                    }
                };
                item.Add(mainInformation);

                ImageView itemImage = new ImageView()
                {
                    Size = new Size(200, 200),
                    ResourceUrl = "./res/images/" + data.ImageUrl,
                    DesiredWidth = 200,
                    DesiredHeight = 200,
                    BackgroundColor = new Color("#f0f0f0"),
                };
                mainInformation.Add(itemImage);

                View detailInformation = new View()
                {
                    WidthSpecification = 250,
                    HeightSpecification = 200,
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Vertical,
                    }
                };
                mainInformation.Add(detailInformation);

                View details = new View()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 160,
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Vertical,
                    }
                };
                detailInformation.Add(details);

                TextLabel itemName = new TextLabel()
                {
                    Text = data.Name,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    MultiLine = true,
                    PixelSize = 16,
                    Margin = new Extents(15, 15, 15, 15)
                };
                details.Add(itemName);

                TextLabel itemPrice = new TextLabel()
                {
                    Text = data.Price,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    PixelSize = 24,
                    Margin = new Extents(15, 15, 0, 0)
                };
                details.Add(itemPrice);

                detailInformation.Add(new View()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 1,
                    BackgroundColor = new Color("#f0f0f0"),
                });

                View icons = new View()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 40,
                    Layout = new FlexLayout()
                    {
                        Direction = FlexLayout.FlexDirection.Row,
                        ItemsAlignment = FlexLayout.AlignmentType.Center,
                        Justification = FlexLayout.FlexJustification.FlexEnd,
                    },
                };
                detailInformation.Add(icons);

                ImageView heart = new ImageView()
                {
                    Size = new Size(40, 32),
                    ResourceUrl = "./res/images/" + (r.Next(0, 10) % 2 == 0 ? "heart.png" : "heart_selected.png"),
                    FittingMode = FittingModeType.ScaleToFill,
                    Margin = new Extents(0, 10, 0, 0)
                };
                icons.Add(heart);

                ImageView cart = new ImageView()
                {
                    Size = new Size(40, 40),
                    ResourceUrl = "./res/images/cart.svg",
                    FittingMode = FittingModeType.ScaleToFill
                };
                icons.Add(cart);

                detailInformation.Add(new View()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 1,
                    BackgroundColor = new Color("#f0f0f0"),
                });

                View additionalInformation = new View()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 60,
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Horizontal,
                        CellPadding = new Size2D(15, 15),
                        LinearAlignment = LinearLayout.Alignment.CenterVertical,
                    },
                };
                item.Add(additionalInformation);

                TextLabel point = new TextLabel()
                {
                    Text = "☆ " + ((float)r.Next(30, 50) / 10.0f) + "점",
                    PixelSize = 16.0f,
                };
                additionalInformation.Add(point);

                TextLabel views = new TextLabel()
                {
                    Text = (r.Next(300, 4000)) + "건",
                    PixelSize = 16.0f,
                };
                additionalInformation.Add(views);

                if (r.Next(0, 10) % 2 == 0)
                {
                    TextLabel quick = new TextLabel()
                    {
                        Text = "퀵배송",
                        PixelSize = 16.0f,
                        TextColor = new Color("#6c7080"),
                    };
                    additionalInformation.Add(quick);
                }

                if (r.Next(0, 10) % 2 == 0)
                {
                    TextLabel discount = new TextLabel()
                    {
                        Text = r.Next(0, 20) + "% 카드할인",
                        PixelSize = 16.0f,
                        TextColor = new Color("#6c7080"),
                    };
                    additionalInformation.Add(discount);
                }

                mainInformation.TouchEvent += (object source, TouchEventArgs args) =>
                {
                    bool result = false;
                    if (args.Touch.GetState(0) == PointStateType.Finished)
                    {
                        PageController.Instance.Go("ItemDetail", data);
                        result = true;
                    }

                    return result;
                };

                View header = new View()
                {
                    Size = new Size(NUIApplication.GetDefaultWindow().Size.Width, 80),
                    BackgroundColor = new Color("#ffd040"),
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Horizontal,
                        LinearAlignment = LinearLayout.Alignment.CenterVertical,
                        CellPadding = new Tizen.NUI.Vector2(15, 0),
                    },
                    Padding = new Extents(15, 15, 0, 0),
                };
                Add(header);

                TextLabel logo = new TextLabel()
                {
                    Text = "Y-MART",
                    PixelSize = 24,
                };
                header.Add(logo);
            }

            BackKeyPressed += (object source, EventArgs args) =>
            {
                NUIApplication.Current.Exit();
            };
        }
    }
}
