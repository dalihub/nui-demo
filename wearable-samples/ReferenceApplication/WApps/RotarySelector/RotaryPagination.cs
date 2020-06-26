using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Wearable;

namespace NUIWHome
{
    public class RotaryPagination : CircularPagination
    {
        //private List<View> pageNavigatorList;

        public RotaryPagination()
        {
            Size = new Size(360, 360);

            // Set CircularPagination properties, such as Indicator size, count, and images.
            IndicatorSize = new Size(10, 10);
            IndicatorCount = 0;
            SelectedIndex = 0;

            // Positioning it to the center
            ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;
        }
        
        public void SetIndicatorCount(int pageCount)
        {
            IndicatorCount = pageCount;
        }
        
        public void SetCurrentPage(int currentPage)
        {
            SelectedIndex = IndicatorCount - currentPage - 1;
        }

        private void UnSelectNavi(View navi)
        {
            navi.Scale = new Vector3(1.0f, 1.0f, 1.0f);
            navi.BackgroundColor = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        }

        private void SelectNavi(View navi)
        {
            navi.Scale = new Vector3(1.3f, 1.3f, 1.3f);
            navi.BackgroundColor = Color.White;
        }
    }
}
