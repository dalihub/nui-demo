using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWHome
{
    public class RotaryPagination : View
    {
        private List<View> pageNavigatorList;

        public RotaryPagination()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            Layout = layout;
            pageNavigatorList = new List<View>();

            Position = new Position(0, 5);
            WidthResizePolicy = ResizePolicyType.FitToChildren;
            HeightResizePolicy = ResizePolicyType.FitToChildren;
            ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter;
            PivotPoint = Tizen.NUI.PivotPoint.TopCenter;
            PositionUsesPivotPoint = true;

        }

        public void CreatePagination(int pageCount)
        {
            if (pageNavigatorList.Count >= pageCount)
            {
                return;
            }


            if (pageNavigatorList.Count > 0)
            {
                UnSelectNavi(pageNavigatorList[pageNavigatorList.Count - 1]);
            }

            View pageNavi = new View()
            {
                Size = new Size(6, 6),
                CornerRadius = 3.0f,
                BackgroundColor = Color.White,
                Margin = new Extents(5, 5, 0, 0),
            };
            SelectNavi(pageNavi);

            pageNavigatorList.Add(pageNavi);
            this.Add(pageNavi);
        }

        public void DeletePage(int idx)
        {
            pageNavigatorList[idx].Unparent();
            pageNavigatorList.RemoveAt(idx);
        }

        public void SetCurrentPage(int currentPage)
        {
            int count = pageNavigatorList.Count;
            for (int i = 0; i < count; i++)
            {
                int idx = count - i - 1;
                if (idx == count - currentPage - 1)
                {
                    SelectNavi(pageNavigatorList[idx]);
                }
                else
                {
                    UnSelectNavi(pageNavigatorList[idx]);
                }
            }
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
