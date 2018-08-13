using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace LayoutDemo
{
    class GridExample : Example
    {
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
        const int INITAL_NUMBER_OF_COLUMNS = 2;
        const int MAX_NUMBER_OF_IMAGE_ITEMS = 9;

        enum ExampleFeature
        {
            GRID_EXACT_WIDTH = 0,
            ITEMS_WITH_MARGINS,
            GRID_MATCH_PARENT,
            GRID_WRAP_CONTENT,
            ADD_ITEMS,
            CHANGE_TO_3_COLUMNS
        };

        private View view;
        private PushButton nextFeatureButton;
        ExampleFeature featureIndex;

        public override void Create()
        {
            // Absolute Layout is created automatically in Window.
            view = new View();
            view.Name = "GridExample";
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.PositionUsesPivotPoint = true;
            view.ParentOrigin = ParentOrigin.Center;
            view.PivotPoint = PivotPoint.Center;
            view.SetProperty(LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue(-2));
            view.SetProperty(LayoutItemWrapper.ChildProperty.HEIGHT_SPECIFICATION, new PropertyValue(-2));
            view.BackgroundColor = Color.Blue;

            var layout = new GridLayout();
            layout.Columns = INITAL_NUMBER_OF_COLUMNS;
            view.Layout = layout;

            // Add child image-views to the created view

            for( int i=0; i < INITIAL_NUMBER_OF_ITEMS; i++ )
            {
                ImageView imageView = LayoutingExample.CreateChildImageView(sample_images[0], new Size2D(100, 100), new Position2D (0, 0));
                view.Add(imageView);
            }

            Window window = Window.Instance;

            this.view = view;

            nextFeatureButton = new PushButton();
            nextFeatureButton.ParentOrigin = ParentOrigin.BottomCenter;
            nextFeatureButton.PivotPoint = PivotPoint.BottomCenter;
            nextFeatureButton.PositionUsesPivotPoint = true;
            nextFeatureButton.LabelText = "next feature";
            nextFeatureButton.Clicked += (sender, e) =>
            {
                NextGridFeature();
                return true;
            };

            window.Add(view);
            window.Add(sizeButton);
        }

        public void NextGridFeature()
        {
            switch( featureIndex )
            {
                case ExampleFeature.GRID_EXACT_WIDTH :
                {
                    SetExactWidth();
                    nextFeatureButton.LabelText = "Set Child Margin";
                    featureIndex = ExampleFeature.ITEMS_WITH_MARGINS;
                    break;
                }
                case ExampleFeature.ITEMS_WITH_MARGINS :
                {
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
                    //if( mGridContainer.GetChildCount() < MAX_NUMBER_OF_IMAGE_VIEWS )
                    {
                        AddItemsInteractively();
                    }

                    //if( mGridContainer.GetChildCount() == MAX_NUMBER_OF_IMAGE_VIEWS )
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
                    featureIndex = ExampleFeature.GRID_EXACT_WIDTH;
                    Window window = Window.Instance;
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

        public override void Remove()
        {
            Window window = Window.Instance;
            window.Remove(view);
            view = null;
            window.Remove(nextFeatureButton);
            nextFeatureButton = null;
        }

        void ChangeTo3Columns()
        {
            GridLayout gridLayout = view.Layout as GridLayout;

            if ( gridLayout )
            {
                gridLayout.Columns = 3;
            }
        }

        void AddItemsInteractively()
        {
            if( mImageViews.size() < MAX_NUMBER_OF_IMAGE_VIEWS )
            {
                ImageView imageView;
                LayoutingExample.CreateChildImageView( imageView, sample_images[0] , new Size2D(100, 100), new Position2D (0, 0) );
                mImageViews.push_back( imageView );
                mGridContainer.Add( imageView);

                // Add item button shows how many items left to add.
                uint numberOfAdditonalImageViews = MAX_NUMBER_OF_ITEMS-INITIAL_NUMBER_OF_ITEMS;
                uint remainingImageViews = numberOfAdditonalImageViews - ( ( mImageViews.size() - INITIAL_NUMBER_OF_ITEMS) );
                string buttonLabel = "Add item[";//+ ToString( numberOfAdditonalImageViews-remainingImageViews ) +"/"+
                                                 //ToString( numberOfAdditonalImageViews)+"]" );

                nextFeatureButton.LabelText = buttonLabel;
            }
        }

        void AddMarginToItems()
        {
            for( uint x = 0; x < INITIAL_NUMBER_OF_ITEMS; x++ )
            {
                mImageViews[x].Margin = new Extents( 20,20,20,10);
            }
        }

        void RemoveMarginsFromItems()
        {
            for( uint x = 0; x < INITIAL_NUMBER_OF_ITEMS; x++ )
            {
                mImageViews[x].Margin = new Extents();
            }
        }

        void MatchParentOnWidth()
        {
            view.SetProperty( LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue( -1 ) );
        }

        void WrapContentOnWidth()
        {
            view.SetProperty( LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION, new PropertyValue( -2 ) );
        }

        void SetExactWidth()
        {
            view.SetProperty( LayoutItemWrapper.ChildProperty.WIDTH_SPECIFICATION,  new PropertyValue( 300 ) );
        }
    };
}
