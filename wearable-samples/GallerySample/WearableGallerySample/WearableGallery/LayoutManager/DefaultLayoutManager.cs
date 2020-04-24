using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public class DefaultLayoutManager : WearableGalleryLayoutManager
    {

        public DefaultLayoutManager() : base()
        {

        }
        
        public override void OrderByIndex(WearableGallery gallery)
        {
            List<WearableGallery.ViewHolder> viewHolderList = gallery.GetChild();
            int currentPage = gallery.GetCurrentPage();
            int count = viewHolderList.Count;

            ClearAnimation();
            for(int i = 0; i < count ; i++)
            {
                View view = viewHolderList[i].GetView();
                Animate(view, new Position((i * view.SizeWidth) - (view.SizeWidth * currentPage), 0));
            }
            PlayAnimation();
        }

        public override void CancelPage(WearableGallery gallery)
        {
            ClearAnimation();
            List<WearableGallery.ViewHolder> viewHolderList = gallery.GetChild();
            int currentPage = gallery.GetCurrentPage();
            int idx = 0;
            foreach(WearableGallery.ViewHolder vh in viewHolderList)
            {
                View view = vh.GetView();
                Animate(view, new Position((idx * view.SizeWidth) - (view.SizeWidth * (currentPage)), 0));
                idx++;
            }
            
            PlayAnimation();
        }

        public override void DragPage(WearableGallery gallery, int distance)
        {
            List<WearableGallery.ViewHolder> viewHolderList = gallery.GetChild();
            foreach(WearableGallery.ViewHolder vh in viewHolderList)
            {
                vh.GetView().PositionX += distance;
            }
        }

        public override int GetSnapPageWidth()
        {
            return 0;
        }


        public override int GetItemCountByLine()
        {
            return 1;
        }

        
    }

}