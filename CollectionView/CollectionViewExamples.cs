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
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI.Binding;

namespace Example
{
    class CollectionViewExample : NUIApplication
    {
	    private CollectionView colView;
        object targetItem;
        class MyItem : OneLineLinearItem
        {
            public MyItem() : base()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent;
                HeightSpecification = 35;
            }
        }
			
        class MyItem2 : OutLineGridItem
        {
            public MyItem2() : base()
            {
                WidthSpecification = 120;
                HeightSpecification = 160;
            }
        }

        class MyHeader : OneLineLinearItem
        {
            public MyHeader() : base()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent;
				// WidthSpecification = 400;
                HeightSpecification = 80;
            }
            public MyHeader(ViewItemStyle style) : base(style)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent;
				// WidthSpecification = 400;
                HeightSpecification = 80;
            }
        }

        public void SelectionEvt(object sender, SelectionChangedEventArgs ev)
        {
            Console.WriteLine("SelectionChanged!");
            foreach (object item in ev.CurrentSelection)
            {
                Console.WriteLine("Selected: {0}", (item as Menu)?.IndexName);
            }
        }

        public void NearestClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Nearestbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, true);
        }

        public void FrontClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Frontbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, true, ItemsView.ItemScrollTo.Front);
        }

        public void CenterClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Centerbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, true, ItemsView.ItemScrollTo.Center);
        }

        public void EndClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Endbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, true, ItemsView.ItemScrollTo.End);
        }
        /// <summary>
        /// Override to create the required scene
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

			int ItemCount = 400;

            // Get the window instance and change background color
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

			View Box = new View()
			{
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent
			};
			Box.Layout = new LinearLayout()
			{
				LinearOrientation = LinearLayout.Orientation.Vertical
			};
			window.Add(Box);
			View ButtonBox = new View()
			{
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 0,
				Weight = 0.1F
			};
			ButtonBox.Layout = new LinearLayout()
			{
				LinearOrientation = LinearLayout.Orientation.Horizontal
			};
			Box.Add(ButtonBox);

			Button NearestButton = new Button()
			{
                WidthSpecification = 0,
                HeightSpecification = LayoutParamPolicies.MatchParent,
				Weight = 1,
				Text = "Nearest"
			};
            NearestButton.Clicked += NearestClickedEvt;
			ButtonBox.Add(NearestButton);

			Button FrontButton = new Button()
			{
                WidthSpecification = 0,
                HeightSpecification = LayoutParamPolicies.MatchParent,
				Weight = 1,
				Text = "Front"
			};
            FrontButton.Clicked += FrontClickedEvt;
			ButtonBox.Add(FrontButton);
			Button CenterButton = new Button()
			{
                WidthSpecification = 0,
                HeightSpecification = LayoutParamPolicies.MatchParent,
				Weight = 1,
				Text = "Center"
			};
            CenterButton.Clicked += CenterClickedEvt;
			ButtonBox.Add(CenterButton);
			Button EndButton = new Button()
			{
                WidthSpecification = 0,
                HeightSpecification = LayoutParamPolicies.MatchParent,
				Weight = 1,
				Text = "End"
			};
            EndButton.Clicked += EndClickedEvt;
			ButtonBox.Add(EndButton);


            var Data = Example.DummyData.CreateDummyMenu(ItemCount);

            targetItem = Data[50];

            var titleStyle = new ViewItemStyle()
            {
                Name = "titleStyle",
                BackgroundColor = new Selector<Color>()
                {
                    Normal = new Color(0.972F, 0.952F, 0.749F, 1),
                    Pressed = new Color(0.1F, 0.85F, 0.85F, 1),
                    Disabled = new Color(0.70F, 0.70F, 0.70F, 1),
                    Selected = new Color(0.701F, 0.898F, 0.937F, 1)
                }
            };
            /*
            CollectionView listView = colView = new CollectionView()
            {
				SizingStrategy = ItemSizingStrategy.MeasureFirst,
                ItemsSource = Data,
                ItemTemplate = new DataTemplate (() => {
						MyItem item = new MyItem();
						item.Label.SetBinding(TextLabel.TextProperty, "IndexName");
						item.Label.HorizontalAlignment=HorizontalAlignment.Begin;
						item.LabelPadding = new Extents(5, 5, 5, 5);
						item.Icon.SetBinding(ImageView.ResourceUrlProperty, "SubNameUrl");
						item.Icon.WidthSpecification = 25;
						item.Icon.HeightSpecification = 25;
						item.IconPadding = new Extents(4, 4, 4, 4);
					    return item;
				}),
				ItemsLayouter = new LinearLayouter(),
                Header = new OneLineLinearItem(titleStyle)
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 50,
                    Text = "Header!"
                },
                Footer = new OneLineLinearItem()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 50,
                    Text = "Count:["+Data.Count+"]"

                },
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 0,
                Weight = 0.5F,
				SelectionMode = ItemSelectionMode.MultipleSelections,
				BackgroundColor = Color.Cyan
            };
			Box.Add(listView);
*/
            CollectionView gridView = new CollectionView()
            {
				SizingStrategy = ItemSizingStrategy.MeasureFirst,
                ItemTemplate = new DataTemplate (() => {
						MyItem2 item = new MyItem2();
						item.Label.SetBinding(TextLabel.TextProperty, "IndexName");
						item.Label.HorizontalAlignment=HorizontalAlignment.Begin;
                        item.Label.PointSize = 10;
						item.LabelPadding = new Extents(5, 5, 5, 5);
						item.Icon.SetBinding(ImageView.ResourceUrlProperty, "SubNameUrl");
						item.Icon.WidthSpecification = 110;
						item.Icon.HeightSpecification = 110;
						item.IconPadding = new Extents(5, 5, 5, 5);
                        
					    return item;
				}),
                ItemsSource = Data,
				ItemsLayouter = new GridLayouter(),
                Header = new OneLineLinearItem(titleStyle)
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 80,
				    //WidthSpecification = 100,
                    //HeightSpecification = LayoutParamPolicies.MatchParent,
                    Text = "Header!"
                },
                Footer = new OneLineLinearItem()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 80,
                    //WidthSpecification = 200,
                    //HeightSpecification = LayoutParamPolicies.MatchParent,
                    Text = "Count:["+Data.Count+"]"
                },
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 0,
                Weight = 0.4F,
				SelectionMode = ItemSelectionMode.MultipleSelections,
				BackgroundColor = Color.Blue
            };
            gridView.SelectionChanged += SelectionEvt;
			Box.Add(gridView);
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
            CollectionViewExample example = new CollectionViewExample();
            example.Run(args);
        }
    }
}
