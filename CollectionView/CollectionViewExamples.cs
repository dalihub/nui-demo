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
        private ItemSelectionMode selectionMode;
        class MyItem : OneLineLinearItem
        {
            public MyItem() : base()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent;
                HeightSpecification = 80;
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
            Console.WriteLine("SelectionChanged! {0}", selectionMode);
            if (selectionMode is ItemSelectionMode.SingleSelection)
            {
                foreach (object item in ev.PreviousSelection)
                {
                    Menu menuItem = item as Menu;
                    if (menuItem == null) return;
                    Console.WriteLine("Unselected: {0}", menuItem.IndexName);
                    menuItem.Selected = false;
                }
                foreach (object item in ev.CurrentSelection)
                {
                    Menu menuItem = item as Menu;
                    if (menuItem == null) return;
                    Console.WriteLine("Selected: {0}", menuItem.IndexName);
                    menuItem.Selected = true;
                }
            }
            else
            {

            }
        }

        public void NearestClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Nearestbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, false);
        }

        public void StartClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Startbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, false, CollectionView.ItemScrollTo.Start);
        }

        public void CenterClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Centerbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, false, CollectionView.ItemScrollTo.Center);
        }

        public void EndClickedEvt(object sender, ClickedEventArgs ev)
        {
            Console.WriteLine("Endbutton Clicked {0}!", colView);
            colView.ScrollTo(targetItem, false, CollectionView.ItemScrollTo.End);
        }
        /// <summary>
        /// Override to create the required scene
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

			int ItemCount = 2;

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

			Button StartButton = new Button()
			{
                WidthSpecification = 0,
                HeightSpecification = LayoutParamPolicies.MatchParent,
				Weight = 1,
				Text = "Start"
			};
            StartButton.Clicked += StartClickedEvt;
			ButtonBox.Add(StartButton);
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


            var Data = Example.DummyData.CreateDummyMenuGroup(ItemCount);
            targetItem = Data[3];

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

            ItemsLayouter viewLayouter = new LinearLayouter(); // GridLayouter();
            RadioButtonGroup group = new RadioButtonGroup();
            selectionMode = ItemSelectionMode.SingleSelection;

            if (viewLayouter is LinearLayouter)
            {
                colView = new CollectionView()
                {
                    SizingStrategy = ItemSizingStrategy.MeasureFirst,
                    ItemsSource = Data,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        MyItem item = new MyItem();
                        item.Label.SetBinding(TextLabel.TextProperty, "IndexName");
                        item.Label.HorizontalAlignment = HorizontalAlignment.Begin;
                        item.LabelPadding = new Extents(10, 10, 10, 10);
/*
                        item.Icon.SetBinding(ImageView.ResourceUrlProperty, "Url");
                        item.Icon.WidthSpecification = 50;
                        item.Icon.HeightSpecification = 50;
                        item.IconPadding = new Extents(10, 10, 10, 10);
*/
                        var radio = new RadioButton();
                        item.Extra = radio;
                        radio.SetBinding(RadioButton.IsSelectedProperty, "Selected");
                        item.Extra.WidthSpecification = 50;
                        item.Extra.HeightSpecification = 50;
                        item.ExtraPadding = new Extents(10, 10, 10, 10);
                        return item;
                    }),
                    ItemsLayouter = viewLayouter,
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
                        Text = "Count:[" + Data.Count + "]"

                    },
                    ScrollingDirection = ScrollableBase.Direction.Vertical,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 0,
                    Weight = 0.5F,
                    SelectionMode = selectionMode,
                    BackgroundColor = Color.Cyan
                };
            }
            else if (viewLayouter is GridLayouter)
            {
                colView = new CollectionView()
                {
                    SizingStrategy = ItemSizingStrategy.MeasureFirst,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        MyItem2 item = new MyItem2();
                        item.Label.SetBinding(TextLabel.TextProperty, "IndexName");
                        item.Label.HorizontalAlignment = HorizontalAlignment.Begin;
                        item.Label.PointSize = 10;
                        item.LabelPadding = new Extents(5, 5, 5, 5);
                        item.Image.SetBinding(ImageView.ResourceUrlProperty, "Url");
                        item.Image.WidthSpecification = 110;
                        item.Image.HeightSpecification = 110;
                        item.ImagePadding = new Extents(5, 5, 5, 5);
                        item.Badge = new CheckBox();
                        item.Badge.WidthSpecification = 20;
                        item.Badge.HeightSpecification = 20;
                        item.BadgePadding = new Extents(2, 2, 2, 2);
                        return item;
                    }),
                    ItemsSource = Data,
                    ItemsLayouter = viewLayouter,
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
                        Text = "Count:[" + Data.Count + "]"
                    },
                    ScrollingDirection = ScrollableBase.Direction.Vertical,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 0,
                    Weight = 0.4F,
                    SelectionMode = selectionMode,
                    BackgroundColor = Color.Blue
                };
            }
            colView.SelectionChanged += SelectionEvt;
			Box.Add(colView);

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
