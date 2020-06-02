/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
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
        class SampleItem : RecycleItem
        {
            public SampleItem()
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
                    Margin = new Extents(0,0,5,0),
                    TextColor = new Color("#d47b3f"),
                };
                infoContainer.Add(SubName);

                Price = new TextLabel()
                {
                    PixelSize = 14,
                    Margin = new Extents(0,0,5,0),
                };
                infoContainer.Add(Price);
            }

            public ImageView Picture { get; set; }
            public TextLabel Name { get; set; }
            public TextLabel SubName { get; set; }
            public TextLabel Price { get; set; }
        }

        class SampleAdapter : RecycleAdapter
        {
            public override RecycleItem CreateRecycleItem()
            {
                return new SampleItem();
            }

            public override void BindData(RecycleItem item)
            {
                SampleItem target = item as SampleItem;
                Menu menu = Data[target.DataIndex] as Menu;

                target.Picture.ResourceUrl = "./res/"+menu.SubName+".jpg";
                target.Picture.FittingMode = FittingModeType.ScaleToFill;

                target.Name.Text = "["+(target.DataIndex+1)+"] "+menu.Name;
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

            SampleAdapter sampleAdapter = new SampleAdapter();
            sampleAdapter.Data = DummyData.CreateDummyData(50);

            recyclerView = new RecyclerView(sampleAdapter, new LinearListLayoutManager())
            {
                // Size = new Size(480, 800),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            window.Add(recyclerView);

            window.KeyEvent += OnKeyEvent;
        }

        private RecyclerView recyclerView;

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
                        recyclerView.ScrollTo(50000,false);
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