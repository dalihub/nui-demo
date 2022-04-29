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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.NUI.Binding;

namespace Example
{
    class CollectionViewExample : NUIApplication
    {
	    private CollectionView colView;
        ObservableCollection<Menu> menuData;
        ObservableCollection<MenuGroup> menuGroupData;
        bool isGroup;
        bool isGrid;
        object targetItem;
        private ItemSelectionMode selectionMode;

        public void SelectionEvt(object sender, SelectionChangedEventArgs ev)
        {
            Console.WriteLine("SelectionChanged! {0}", selectionMode);
            if (selectionMode is ItemSelectionMode.SingleSelection)
            {
                foreach (object item in ev.PreviousSelection)
                {
                    SimpleMenu menuItem = item as SimpleMenu;
                    if (menuItem == null) return;
                    Console.WriteLine("Unselected: {0}", menuItem.IndexName);
                    menuItem.Selected = false;
                }
                foreach (object item in ev.CurrentSelection)
                {
                    SimpleMenu menuItem = item as SimpleMenu;
                    if (menuItem == null) return;
                    Console.WriteLine("Selected: {0}", menuItem.IndexName);
                    menuItem.Selected = true;
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// Override to create the required scene
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
			//int ItemCount = 20;

            isGrid = false;//true;
            isGroup = true;

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

            IEnumerable Data;
            if (isGroup) Data = menuGroupData = Example.DummyData.CreateDummyMenuGroup(2);
            else Data = menuData = Example.DummyData.CreateDummyMenu(50);


            if (isGroup) targetItem = menuGroupData[0];
            else targetItem = menuData[30];

            var titleStyle = new DefaultLinearItemStyle()
            {
                BackgroundColor = new Selector<Color>()
                {
                    Normal = new Color(0.972F, 0.952F, 0.749F, 1),
                    Pressed = new Color(0.1F, 0.85F, 0.85F, 1),
                    Disabled = new Color(0.70F, 0.70F, 0.70F, 1),
                    Selected = new Color(0.701F, 0.898F, 0.937F, 1)
                }
            };

            DefaultLinearItem headerItem = new DefaultLinearItem(titleStyle)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 80,
                //WidthSpecification = 100,
                //HeightSpecification = LayoutParamPolicies.MatchParent,
                Text = "Header!"
            };

            DefaultLinearItem footerItem = new DefaultLinearItem()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 50,
                Text = "Count:[" + (isGroup ? menuGroupData.Count : menuData.Count) + "]"

            };

            //ItemsLayouter
            ItemsLayouter viewLayouter;
            isGrid = true;
            if (isGrid) viewLayouter = new GridLayouter();
            else viewLayouter = new LinearLayouter();
            selectionMode = ItemSelectionMode.SingleSelection;

            if (viewLayouter is LinearLayouter)
            {
                colView = new CollectionView()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = LayoutParamPolicies.MatchParent,
                    Weight = 1F,
                    SizingStrategy = ItemSizingStrategy.MeasureFirst,
                    ItemsSource = Data,
                    ItemsLayouter = viewLayouter,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        DefaultLinearItem item = new DefaultLinearItem();
                        item.WidthSpecification = LayoutParamPolicies.MatchParent;

                        item.Label.SetBinding(TextLabel.TextProperty, "IndexName");
                        item.Label.HorizontalAlignment = HorizontalAlignment.Begin;

                        item.SubLabel.SetBinding(TextLabel.TextProperty, "Price");
                        item.SubLabel.HorizontalAlignment = HorizontalAlignment.Begin;

                        item.Icon.SetBinding(ImageView.ResourceUrlProperty, "Url");
                        item.Icon.WidthSpecification = 48;
                        item.Icon.HeightSpecification = 48;

                        var radio = new RadioButton();
                        item.Extra = radio;
                        radio.SetBinding(RadioButton.IsSelectedProperty, "Selected");
                        item.Extra.WidthSpecification = 48;
                        item.Extra.HeightSpecification = 48;

                        item.SetBinding(DefaultLinearItem.IsEnabledProperty, "Enabled");

                        return item;
                    }),
                    GroupHeaderTemplate = new DataTemplate(() =>
                    {
                        DefaultTitleItem item = new DefaultTitleItem()
                        {
                            WidthSpecification = LayoutParamPolicies.MatchParent,
                        };
                        item.Label.SetBinding(TextLabel.TextProperty, "GroupName");
                        item.Label.HorizontalAlignment = HorizontalAlignment.Begin;
                        return item;
                    }),
                    GroupFooterTemplate = new DataTemplate(() =>
                    {
                        DefaultTitleItem item = new DefaultTitleItem()
                        {
                            WidthSpecification = LayoutParamPolicies.MatchParent,
                        };
                        item.Label.SetBinding(TextLabel.TextProperty, "Count");
                        item.Label.HorizontalAlignment = HorizontalAlignment.Begin;
                        return item;
                    }),
                    IsGrouped = isGroup,
                    Header = headerItem,
                    ScrollingDirection = ScrollableBase.Direction.Vertical,
                    SelectionMode = selectionMode,
                    HideScrollbar = false,
                };
            }
            else if (viewLayouter is GridLayouter)
            {
                colView = new CollectionView()
                {
                    SizingStrategy = ItemSizingStrategy.MeasureFirst,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        DefaultGridItem item = new DefaultGridItem();

                        item.WidthSpecification = 400;
                        item.HeightSpecification = 400;
                        item.CaptionRelativeOrientation = DefaultGridItem.CaptionOrientation.InsideBottom;   
                        item.Caption.SetBinding(TextLabel.TextProperty, "IndexName");
                        item.Caption.HorizontalAlignment = HorizontalAlignment.Begin;
                        item.Caption.BackgroundColor = new Color(0.3f, 0.3f, 0.3f, 0.6f);
                        item.Image.SetBinding(ImageView.ResourceUrlProperty, "Url");
                        item.Image.WidthSpecification = 400;
                        item.Image.HeightSpecification = 400;
                        item.Badge = new CheckBox();
                        item.Badge.WidthSpecification = 41;
                        item.Badge.HeightSpecification = 41;
                        return item;
                    }),
                    GroupHeaderTemplate = new DataTemplate(() =>
                    {
                        DefaultTitleItem item = new DefaultTitleItem()
                        {
                            WidthSpecification = LayoutParamPolicies.MatchParent,
                        };
                        item.Label.SetBinding(TextLabel.TextProperty, "GroupName");
                        item.Label.HorizontalAlignment = HorizontalAlignment.Begin;
                        return item;
                    }),
                    GroupFooterTemplate = new DataTemplate(() =>
                    {
                        DefaultLinearItem item = new DefaultLinearItem(titleStyle)
                        {
                            WidthSpecification = LayoutParamPolicies.MatchParent,
                            HeightSpecification = 90
                        };
                        item.Label.SetBinding(TextLabel.TextProperty, "Count");
                        item.Label.HorizontalAlignment = HorizontalAlignment.Begin;
                        item.BackgroundColor = new Color(0.3F, 0.3F, 0.3F, 1F);
                        return item;
                    }),
                    ItemsSource = Data,
                    ItemsLayouter = viewLayouter,
                    Header = headerItem,
                    Footer = footerItem,
                    IsGrouped = isGroup,
                    ScrollingDirection = ScrollableBase.Direction.Vertical,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = 0,
                    Weight = 0.4F,
                    SelectionMode = selectionMode,
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
