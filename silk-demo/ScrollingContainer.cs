using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Silk
{
    /// <summary>
    /// A Layouting container for scrolling a single child within it.
    /// The single child can be a layout container itself.
    /// </summary>
    public class ScrollingContainer : CustomView
    {

        public int PageWidth
        {
          get
          {
              return scrollingContainerLayout.PageWidth;
          }
          set
          {
              scrollingContainerLayout.PageWidth = value;
          }
        }

        private ScrollingContainerLayout scrollingContainerLayout;

        /// <summary>
        /// Creates an initialized ScrollingContainer.
        /// </summary>
        public ScrollingContainer() : base(typeof(ScrollingContainer).FullName, CustomViewBehaviour.DisableSizeNegotiation)
        {
            scrollingContainerLayout = new ScrollingContainerLayout();
            Layout = scrollingContainerLayout;
        }

        public void ScrollForward()
        {
            scrollingContainerLayout.CurrentPage--;
            scrollingContainerLayout.RequestLayout();
        }

        public void ScrollBackward()
        {
            scrollingContainerLayout.CurrentPage++;
            scrollingContainerLayout.RequestLayout();
        }
    }

    internal class ScrollingContainerLayout : LayoutGroup
    {
        private int currentPage = 0;
        public int CurrentPage
        {
          get
          {
              return currentPage;
          }
          set
          {
              currentPage = System.Math.Max(0,value);
          }
        }
        public int PageWidth {get; set;} = 0;
        protected override void OnMeasure( MeasureSpecification widthMeasureSpec, MeasureSpecification heightMeasureSpec )
        {
            // Scroll horizontally
            LayoutItem childToScroll = _children[0];

            if (childToScroll !=null)
            {
                int childDesiredHeight = childToScroll.Owner.HeightSpecification;

                MeasureSpecification childWidthMeasureSpec = new MeasureSpecification( new LayoutLength(PageWidth*4), MeasureSpecification.ModeType.Unspecified);

                MeasureSpecification childHeightMeasureSpec = GetChildMeasureSpecification(heightMeasureSpec,
                                         new LayoutLength(childToScroll.Padding.Top + childToScroll.Padding.Bottom),
                                         new LayoutLength(childDesiredHeight));

                MeasureChild( childToScroll, childWidthMeasureSpec, heightMeasureSpec );

                SetMeasuredDimensions( new MeasuredSize( childWidthMeasureSpec.Size, MeasuredSize.StateType.MeasuredSizeOK),
                                       new MeasuredSize( new LayoutLength(childDesiredHeight), MeasuredSize.StateType.MeasuredSizeOK) );
            }
        }

        protected override void OnLayout( bool changed, LayoutLength left, LayoutLength top, LayoutLength right, LayoutLength bottom )
        {
            int horizontallOffset = PageWidth*CurrentPage;
            LayoutItem childToScroll = _children[0];
            if (childToScroll !=null)
            {
                LayoutLength childWidth = childToScroll.MeasuredWidth.Size;
                LayoutLength childHeight = childToScroll.MeasuredHeight.Size;

                childToScroll.Layout( left - horizontallOffset,
                                      top,
                                      left - horizontallOffset + childWidth,
                                      top + childHeight );
            }

        }
    }
}