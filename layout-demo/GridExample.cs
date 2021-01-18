using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    // Shows different features of the GridLayout and generic layout system
    class GridExample : Example
    {
        public GridExample() : base( "Grid Layout" )
        {}

        static class TestImages
        {
            private const string resources = "./res";

            /// Child image filenames
            public static readonly string[] sample_images = new string[]
            {
                resources + "/images/gallery-small-23.jpg",
            };
        }

        const int INITIAL_NUMBER_OF_ITEMS = 5;
        const int INITIAL_NUMBER_OF_COLUMNS = 2;
        const int MAX_NUMBER_OF_ITEMS = 9;

        // States to match features
        enum ExampleFeature
        {
            GRID_EXACT_WIDTH = 0,
            EXPAND_CHILD,
            ITEMS_WITH_MARGINS,
            GRID_MATCH_PARENT,
            GRID_WRAP_CONTENT,
            ADD_ITEMS,
            CHANGE_TO_3_COLUMNS,
            ADD_SPACING,
            ADD_ITEM_WITH_COLUMN_ROW,
            ITEMS_WITH_FILL,
            ITEMS_WITH_SPAN,
        };

        private View view;
        private PushButton nextFeatureButton;
        ExampleFeature featureIndex;
        List<ImageView> childItems;

        // Create GridLayout Example
        public override void Create()
        {
            // Absolute Layout is created automatically in Window.
            view = new View();
            view.Name = "GridExample";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.WidthSpecification = LayoutParamPolicies.WrapContent;
            view.HeightSpecification = LayoutParamPolicies.WrapContent;
            view.BackgroundColor = Color.Blue;

            var layout = new GridLayout();
            layout.Columns = INITIAL_NUMBER_OF_COLUMNS;
            view.Layout = layout;

            // Add child image-views to the created view
	          childItems = new List<ImageView>();
            for( int i=0; i < INITIAL_NUMBER_OF_ITEMS; i++ )
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(TestImages.sample_images[0], new Size2D(100, 100) );
                childItems.Add(imageView);
                view.Add(imageView);
            }

            Window window = LayoutingExample.GetWindow();

            nextFeatureButton = new PushButton();
            nextFeatureButton.ParentOrigin = ParentOrigin.BottomCenter;
            nextFeatureButton.PivotPoint = PivotPoint.BottomCenter;
            nextFeatureButton.PositionUsesPivotPoint = true;
            nextFeatureButton.LabelText = " Set exact width to Grid ";
            nextFeatureButton.Clicked += (sender, e) =>
            {
                NextGridFeature();
                return true;
            };

            window.Add(view);
            window.Add(nextFeatureButton);
        }

        // Demonstrate a different feature depending on state
        public void NextGridFeature()
        {
            switch( featureIndex )
            {
                case ExampleFeature.GRID_EXACT_WIDTH :
                {
                    SetExactWidth();
                    nextFeatureButton.LabelText = "Expand Child";
                    featureIndex = ExampleFeature.EXPAND_CHILD;
                    break;
                }
                case ExampleFeature.EXPAND_CHILD :
                {
                    ExpandChild();
                    nextFeatureButton.LabelText = "Set Child Margin";
                    featureIndex = ExampleFeature.ITEMS_WITH_MARGINS;
                    break;
                }
                case ExampleFeature.ITEMS_WITH_MARGINS :
                {
                    ImageView imageView = childItems[1];
                    GridLayout.SetHorizontalStretch(imageView, GridLayout.StretchFlags.None);
                    AddMarginToItems();
                    featureIndex = ExampleFeature.GRID_MATCH_PARENT;
                    nextFeatureButton.LabelText = "Set width MATCH_PARENT";
                    break;
                }
                case ExampleFeature.GRID_MATCH_PARENT :
                {
                    RemoveMarginsFromItems();
                    MatchParentOnWidth();
                    nextFeatureButton.LabelText = "Set width WRAP_CONTENT";
                    featureIndex = ExampleFeature.GRID_WRAP_CONTENT;
                    break;
                }
                case ExampleFeature.GRID_WRAP_CONTENT :
                {
                    WrapContentOnWidth();
                    nextFeatureButton.LabelText = "Add item";
                    featureIndex = ExampleFeature.ADD_ITEMS;
                    break;
                }
                case ExampleFeature.ADD_ITEMS :
                {
                    if( childItems.Count < MAX_NUMBER_OF_ITEMS )
                    {
                        AddItemsInteractively();
                    }

                    if( childItems.Count == MAX_NUMBER_OF_ITEMS )
                    {
                        // Remove button when no more items to add
                        featureIndex = ExampleFeature.CHANGE_TO_3_COLUMNS;
                        nextFeatureButton.LabelText = "Change Columns";
                    }
                    break;
                }
                case ExampleFeature.CHANGE_TO_3_COLUMNS :
                {
                    ChangeTo3Columns();
                    featureIndex = ExampleFeature.ADD_SPACING;
                    nextFeatureButton.LabelText = "Set spacing";
                    break;
                }
                case ExampleFeature.ADD_SPACING :
                {
                    AddSpacing();
                    featureIndex = ExampleFeature.ADD_ITEM_WITH_COLUMN_ROW;
                    nextFeatureButton.LabelText = "Add item with column,row";
                    break;
                }
                case ExampleFeature.ADD_ITEM_WITH_COLUMN_ROW :
                {
                    ResetSpacing();
                    AddItemWithColumnRow();
                    featureIndex = ExampleFeature.ITEMS_WITH_FILL;
                    nextFeatureButton.LabelText = "Set item Fill";
                    break;
                }
                case ExampleFeature.ITEMS_WITH_FILL :
                {
                    SetItemFill();
                    featureIndex = ExampleFeature.ITEMS_WITH_SPAN;
                    nextFeatureButton.LabelText = "Change item Span";
                    break;
                }
                case ExampleFeature.ITEMS_WITH_SPAN :
                {
                    ChangeTo2Span();
                    featureIndex = ExampleFeature.GRID_EXACT_WIDTH;
                    Window window = LayoutingExample.GetWindow();
                    window.Remove(nextFeatureButton);
                    nextFeatureButton = null;
                    break;
                }
                default :
                {
                    featureIndex = ExampleFeature.GRID_EXACT_WIDTH;
                    break;
                }
            }
        }

        //Clean up after examples ends
        public override void Remove()
        {
            Window window = LayoutingExample.GetWindow();
            window.Remove(view);
            view = null;
            if(nextFeatureButton)
            {
                window.Remove(nextFeatureButton);
                nextFeatureButton = null;
            }
        }

        // Features of Grid Layout that are demonstrated

        void ChangeTo3Columns()
        {
            GridLayout gridLayout = view.Layout as GridLayout;

            if ( gridLayout != null )
            {
                gridLayout.Columns = 3;
            }
        }

        void AddSpacing()
        {
            GridLayout gridLayout = view.Layout as GridLayout;

            if ( gridLayout != null )
            {
                gridLayout.ColumnSpacing = 30;
                gridLayout.RowSpacing = 60;
            }
        }

        void ResetSpacing()
        {
            GridLayout gridLayout = view.Layout as GridLayout;

            if ( gridLayout != null )
            {
                gridLayout.ColumnSpacing = 0;
                gridLayout.RowSpacing = 0;
            }
        }

        void AddItemWithColumnRow()
        {
            GridLayout gridLayout = view.Layout as GridLayout;

            if ( gridLayout != null )
            {
                gridLayout.Columns = 4;
            }

            ImageView imageView = LayoutingExample.CreateChildImageView( TestImages.sample_images[0] , new Size2D(200, 200));
            childItems.Add( imageView );
            view.Add( imageView);
            GridLayout.SetColumn(imageView, 3);
            GridLayout.SetRow(imageView, 2);

            imageView = LayoutingExample.CreateChildImageView( TestImages.sample_images[0] , new Size2D(100, 100));
            childItems.Add( imageView );
            view.Add( imageView);
            GridLayout.SetColumn(imageView, 1);
            GridLayout.SetRow(imageView, 2);
        }

        void SetItemFill()
        {
            ImageView imageView = childItems[childItems.Count - 1];
            GridLayout.SetVerticalStretch(imageView, GridLayout.StretchFlags.Fill);
        }

        void ChangeTo2Span()
        {
            ImageView imageView = childItems[childItems.Count - 1];
            GridLayout.SetHorizontalStretch(imageView, GridLayout.StretchFlags.Fill);
            GridLayout.SetColumnSpan(imageView, 2);
        }

        void AddItemsInteractively()
        {
            if( childItems.Count < MAX_NUMBER_OF_ITEMS )
            {
                ImageView imageView = LayoutingExample.CreateChildImageView( TestImages.sample_images[0] , new Size2D(100, 100));
                childItems.Add( imageView );
                view.Add( imageView);

                // Add item button shows how many items left to add.
                int numberOfAdditonalImageViews = MAX_NUMBER_OF_ITEMS-INITIAL_NUMBER_OF_ITEMS;
                int remainingImageViews = MAX_NUMBER_OF_ITEMS - ( childItems.Count );
                string buttonLabel = "Add item[" + ( numberOfAdditonalImageViews - remainingImageViews ).ToString() +"/"
                                                 + ( numberOfAdditonalImageViews).ToString() + "]";

                nextFeatureButton.LabelText = buttonLabel;
            }
        }

        void AddMarginToItems()
        {
            foreach (ImageView imageView in childItems)
            {
                imageView.Margin = new Extents( 20, 20, 20, 10 );
            }
        }

        void RemoveMarginsFromItems()
        {
            foreach (ImageView imageView in childItems)
            {
                imageView.Margin = new Extents();
            }
        }

        void MatchParentOnWidth()
        {
            view.WidthSpecification = LayoutParamPolicies.MatchParent;
        }

        void WrapContentOnWidth()
        {
            view.WidthSpecification = LayoutParamPolicies.WrapContent;
        }

        void SetExactWidth()
        {
            view.WidthSpecification = 300;
        }

        void ExpandChild()
        {
            ImageView imageView = childItems[1];
            GridLayout.SetHorizontalStretch(imageView, GridLayout.StretchFlags.ExpandAndFill);
        }
    };
}
