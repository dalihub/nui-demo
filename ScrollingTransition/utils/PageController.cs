using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;

namespace Demo
{
    class PageController
    {
        private static readonly PageController _instance = new PageController();

        private Dictionary<string, Type> pageTypeList;
        private List<Page> pageObjectList;
        private int currentPageIndex;
        private bool onChanging = false;

        public static PageController Instance
        {
            get
            {
                return _instance;
            }
        }

        private PageController()
        {
            pageTypeList = new Dictionary<string, Type>();
            pageObjectList = new List<Page>();
        }

        public void AddPage(string pageName, Type pageType)
        {
            if (pageObjectList.Count == 0)
            {
                Page firstPage = (Page)Activator.CreateInstance(pageType);
                firstPage.Position = new Position(0, 0);
                NUIApplication.GetDefaultWindow().GetDefaultLayer().Add(firstPage);
                pageObjectList.Add(firstPage);
            }

            pageTypeList.Add(pageName, pageType);
        }

        private void OnPageShowFinished(object source, EventArgs args)
        {
            onChanging = false;
        }

        private void OnPageHideFinished(object source, EventArgs args)
        {
            Page target = source as Page;
            target.Hide();
            onChanging = false;
        }


        public void GoBack()
        {
            if(onChanging == false && pageObjectList.Count > 0)
            {
                Page targetPage = pageObjectList[pageObjectList.Count - 1];
                targetPage.HidePage();
                onChanging = true;
                //remove targetPage at object list
                pageObjectList.Remove(targetPage);
            }
        }

        public void Go(string pageName, object data)
        {
            Type targetType = null;
            pageTypeList.TryGetValue(pageName, out targetType);

            if (onChanging == false && targetType != null)
            {
                Page targetPage = (Page)Activator.CreateInstance(targetType, data);
                pageObjectList.Add(targetPage);

                targetPage.OnShowFinished += OnPageShowFinished;
                targetPage.OnHideFinished += OnPageHideFinished;

                NUIApplication.GetDefaultWindow().GetDefaultLayer().Add(targetPage);
                targetPage.ShowPage();
                onChanging = true;
            }
        }
    }
}
