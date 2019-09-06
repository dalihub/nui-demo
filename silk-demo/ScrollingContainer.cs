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

        public void ScrollForwardBy()
        {
            scrollingContainerLayout.CurrentPage++;
        }

        public void ScrollBackwardBy()
        {
            scrollingContainerLayout.CurrentPage--;
        }
    }

    internal class ScrollingContainerLayout : LayoutGroup
    {
        public int CurrentPage {get; set;} = 0;
        public int PageWidth {get; set;} = 0;
        protected override void OnMeasure( MeasureSpecification widthMeasureSpec, MeasureSpecification heightMeasureSpec )
        {
            // Scroll horizontally
            LayoutItem childToScroll = _children[0];

            if (childToScroll !=null)
            {
                int childDesiredHeight = childToScroll.Owner.HeightSpecification;

                MeasureSpecification childWidthMeasureSpec = GetChildMeasureSpecification(widthMeasureSpec,
                            new LayoutLength(childToScroll.Padding.Start + childToScroll.Padding.End),
                            new LayoutLength(LayoutParamPolicies.WrapContent));

                MeasureSpecification childHeightMeasureSpec = GetChildMeasureSpecification(heightMeasureSpec,
                                        new LayoutLength(childToScroll.Padding.Top + childToScroll.Padding.Bottom),
                                        new LayoutLength(childDesiredHeight));

                childToScroll.Measure( childWidthMeasureSpec, childHeightMeasureSpec);

                SetMeasuredDimensions( new MeasuredSize( childToScroll.MeasuredWidth.Size, MeasuredSize.StateType.MeasuredSizeOK),
                                      new MeasuredSize( childToScroll.MeasuredHeight.Size, MeasuredSize.StateType.MeasuredSizeOK) );
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

                childToScroll.Layout( left + horizontallOffset,
                                      top,
                                      left + horizontallOffset + childWidth,
                                      childHeight );
            }

        }
    }
}