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
using Tizen.NUI.Wearable;

namespace Example
{
    class RecyclerViewExample2 : NUIApplication
    {
        class PictureItem : RecycleItem
        {
            public PictureItem()
            {
                WidthSpecification = 120;
                HeightSpecification = 120;

                Picture = new ImageView()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = LayoutParamPolicies.MatchParent,
                    DesiredWidth = 120,
                    DesiredHeight = 120,
                };
                Add(Picture);

                Number = new TextLabel()
                {
                    WidthSpecification = 36,
                    HeightSpecification = 36,
                    Position = new Position(10, 10),
                    CornerRadius = 18,
                    PointSize = 12,
                    BackgroundColor = new Color("#FFFFFFDF"),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                Add(Number);
            }
            public ImageView Picture { get; set; }
            public TextLabel Number { get; set; }
        }

        class SampleAdapter : RecycleAdapter
        {
            public override RecycleItem CreateRecycleItem()
            {
                return new PictureItem();
            }

            public override void BindData(RecycleItem item)
            {
                PictureItem target = item as PictureItem;
                PictureData data = Data[target.DataIndex] as PictureData;

                target.Picture.ResourceUrl = data.FilePath;
                target.Picture.FittingMode = FittingModeType.Center;
                target.Number.Text = (target.DataIndex + 1).ToString();
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
            window.BackgroundColor = new Color("#141414");

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

            TextLabel horizontalLabel = new TextLabel("Horizontal Grid")
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 60,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.White,
                Margin = new Extents(20, 0, 10, 0),
                PointSize = 18,
            };
            root.Add(horizontalLabel);

            var horizontalGrid = new Tizen.NUI.Wearable.RecyclerView()
            {
                Adapter = new SampleAdapter()
                {
                    Data = DummyDataGridLayout.CreateDummyPictureData(73)
                },
                LayoutManager = new GridRecycleLayoutManager()
                {
                    LayoutOrientation = RecycleLayoutManager.Orientation.Vertical,
                    Rows = 2,
                },
                ScrollingDirection = ScrollableBase.Direction.Horizontal,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 240,
                TotalItemCount = 20,
            };
            root.Add(horizontalGrid);

            TextLabel verticalLabel = new TextLabel("Vertical Grid")
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 60,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.White,
                Margin = new Extents(20, 0, 10, 0),
                PointSize = 18,
            };
            root.Add(verticalLabel);

            var verticalGrid = new Tizen.NUI.Wearable.RecyclerView()
            {
                Adapter = new SampleAdapter()
                {
                    Data = DummyDataGridLayout.CreateDummyPictureData(132)
                },
                LayoutManager = new GridRecycleLayoutManager()
                {
                    LayoutOrientation = RecycleLayoutManager.Orientation.Horizontal,
                    Columns = 4,
                },
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                Weight = 1,
                TotalItemCount = 40,
            };
            root.Add(verticalGrid);

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
        // [STAThread] // Forces app to use one thread to access NUI
        // static void Main(string[] args)
        // {
        //     new RecyclerViewExample2().Run(args);
        // }
    }
}