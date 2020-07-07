/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Example
{
    class RecyclerViewExample : NUIApplication
    {
        class MenuTapItem : RecycleItem
        {
            public MenuTapItem()
            {
                WidthSpecification = 180;
                HeightSpecification = 60;

                Name = new TextLabel()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = LayoutParamPolicies.MatchParent,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    PixelSize = 16,
                    TextColor = Color.White,
                };
                BackgroundColor = new Color("#141414");

                Add(Name);
            }
            public TextLabel Name { get; set; }
        }

        class SampleMenuTapAdapter : RecycleAdapter
        {
            public override RecycleItem CreateRecycleItem()
            {
                return new MenuTapItem();
            }

            public override void BindData(RecycleItem item)
            {
                MenuTapItem target = item as MenuTapItem;
                MenuTap menuTap = Data[target.DataIndex] as MenuTap;

                target.Name.Text = menuTap.Name;
            }
        };


        class MenuItem : RecycleItem
        {
            public MenuItem()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent;
                HeightSpecification = 120;

                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    LinearAlignment = LinearLayout.Alignment.CenterVertical,
                };
                Padding = new Extents(20, 20, 20, 20);

                Picture = new ImageView()
                {
                    Size = new Size(80, 80),
                    Margin = new Extents(0, 20, 0, 0),
                    CornerRadius = 40,
                };
                Add(Picture);

                View infoContainer = new View()
                {
                    Weight = 1,
                    Layout = new LinearLayout()
                    {
                        LinearOrientation = LinearLayout.Orientation.Vertical,
                        LinearAlignment = LinearLayout.Alignment.CenterVertical,
                    },
                };
                Add(infoContainer);

                Name = new TextLabel()
                {
                    PixelSize = 24,
                };
                infoContainer.Add(Name);

                SubName = new TextLabel()
                {
                    PixelSize = 14,
                    Margin = new Extents(0, 0, 5, 0),
                    TextColor = new Color("#d47b3f"),
                };
                infoContainer.Add(SubName);

                Price = new TextLabel()
                {
                    PixelSize = 14,
                    Margin = new Extents(0, 0, 5, 0),
                };
                infoContainer.Add(Price);
            }

            public ImageView Picture { get; set; }
            public TextLabel Name { get; set; }
            public TextLabel SubName { get; set; }
            public TextLabel Price { get; set; }
        }

        class SampleMenuAdapter : RecycleAdapter
        {
            public override RecycleItem CreateRecycleItem()
            {
                return new MenuItem();
            }

            public override void BindData(RecycleItem item)
            {
                MenuItem target = item as MenuItem;
                Menu menu = Data[target.DataIndex] as Menu;

                target.Picture.ResourceUrl = "./res/" + menu.SubName + ".jpg";
                target.Picture.FittingMode = FittingModeType.ScaleToFill;

                target.Name.Text = "[" + (target.DataIndex + 1) + "] " + menu.Name;
                target.SubName.Text = menu.SubName;
                target.Price.Text = menu.Price;

                target.BackgroundColor = target.DataIndex % 2 == 1 ? new Color("#e0e0e0") : Color.White;
            }
        };



        /// <summary>
        /// Override to create the required scene
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            // Get the window instance and change background color
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

            View root = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
            };
            window.Add(root);

            RecyclerView menuTapList = new RecyclerView()
            {
                Adapter = new SampleMenuTapAdapter()
                {
                    Data = DummyData.CreateDummyMenuTap(20)
                },
                LayoutManager = new LinearRecycleLayoutManager()
                {
                    LayoutOrientation = RecycleLayoutManager.Orientation.Horizontal
                },
                ScrollingDirection = ScrollableBase.Direction.Horizontal,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 60,
            };
            root.Add(menuTapList);

            RecyclerView menuList = new RecyclerView()
            {
                Adapter = new SampleMenuAdapter()
                {
                    Data = DummyData.CreateDummyMenu(50),
                },
                LayoutManager = new LinearRecycleLayoutManager(),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Weight = 1,
            };
            root.Add(menuList);

            window.KeyEvent += OnKeyEvent;
        }

        /// <summary>
        /// Called when any key event is received.
        /// Will use this to exit the application if the Back or Escape key is pressed
        /// </summary>
        private void OnKeyEvent(object sender, Window.KeyEventArgs eventArgs)
        {
            if (eventArgs.Key.State == Key.StateType.Down)
            {
                switch (eventArgs.Key.KeyPressedName)
                {
                    case "Up":
                        {
                            break;
                        }
                    case "Escape":
                    case "Back":
                        {
                            Exit();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] // Forces app to use one thread to access NUI
        static void Main(string[] args)
        {
            RecyclerViewExample example = new RecyclerViewExample();
            example.Run(args);
        }
    }
}