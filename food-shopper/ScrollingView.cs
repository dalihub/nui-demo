using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using System;
using System.Collections.Generic;

namespace Scrolling
{
    /// <summary>
    /// Can only have one child.
    /// Needs to intercept focus events for child but still able to focus a child.
    /// Stores current child that is focused, on intercepting focus events it focuses and
    /// scrolls to that child.
    /// </summary>
    class ScrollingView : View
    {
        private int currentItem;
        private int itemCount;

        private View childView;

        private bool scrollingStepsOutdated;

        private List<int> scrollingSteps;

        public ScrollingView()
        {
            // LayoutGroup needed as Scrolling View is a dervied View class so will
            // not propogate layouting unless given a layout.
            var groupingLayout = new LayoutGroup();
            Layout = groupingLayout;
            LayoutWidthSpecification = ChildLayoutData.MatchParent;
            LayoutHeightSpecification = ChildLayoutData.WrapContent;
            AddedToWindow += OnWindow;
        }

        public void OnWindow(object source, EventArgs e)
        {
            Console.WriteLine("OnWindow ");
        }

        public override void Add( View child )
        {
            if(childView)
            {
              Remove(childView);
              itemCount = 0;
              childView = null;
            }
            Console.WriteLine("Adding child "+ child.Name + " to ScrollingView");
            childView = child;
            base.Add( childView );
            itemCount = (int)childView.ChildCount; // elements added to child after this will not be counted.
            scrollingStepsOutdated = true;
        }

        public int Scroll( bool reverse )
        {
            // Get child's item positions
            // Only retrieve from the child if the layout/View dirty otherwise use cached positions.
            if ( scrollingStepsOutdated )
            {
                UpdateScrollingSteps();
            }
            // Scroll to the position of the required item.
            if ( reverse )
            {
                currentItem-= 1;
            }
            else
            {
                currentItem++;
            }

            currentItem = Math.Min( Math.Max( currentItem, 0 ), itemCount-1 );

            Animation scrollAnimation = new Animation();
            scrollAnimation.SetDuration(1.0f);
            float childViewPositionX = scrollingSteps[currentItem] - scrollingSteps[(childView.ChildCount>1)?1:0];
            scrollAnimation.AnimateTo(childView, "PositionX", childViewPositionX);
            scrollAnimation.Play();
            return currentItem;
        }
        // Get position of each child in the container that will be scrolled.
        // These positions will be the scrolling steps, scrolling with jump to
        // these steps.
        private void UpdateScrollingSteps()
        {
            scrollingSteps = new List<int>();
            Console.WriteLine("UpdateScrollingSteps child count:"+ childView.ChildCount);
            for( uint i = 0; i < childView.ChildCount; ++i )
            {
                var item = childView.GetChildAt( i );
                scrollingSteps.Add( (int)Math.Floor( item.PositionX) );
            }
            scrollingStepsOutdated = false;
        }

    }
}