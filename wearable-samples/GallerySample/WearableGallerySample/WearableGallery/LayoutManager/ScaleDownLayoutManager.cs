using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public class ScaleDownLayoutManager : WearableGalleryLayoutManager
    {
        private float scale = 0.57f;
        private int size = 0;

        public ScaleDownLayoutManager()
        {

        }
        
        public override void OrderByIndex(WearableGallery gallery)
        {
            List<WearableGallery.ViewHolder> viewHolderList = gallery.GetChild();
            int currentPage = gallery.GetCurrentPage();
            int idx  = 0;
            size = (int)(viewHolderList[0].GetView().Size.Width * scale) + 50;

            ClearAnimation();
            foreach(WearableGallery.ViewHolder vh in viewHolderList)
            {
                Position position = CreatePosition(idx);
                position.X -= size * (currentPage);
                Animate(vh.GetView(), position, scale);
                idx++;
            }
            PlayAnimation();
        }


        public override void CancelPage(WearableGallery gallery)
        {
            List<WearableGallery.ViewHolder> viewHolderList = gallery.GetChild();
            int currentPage = gallery.GetCurrentPage();
            int idx = 0;
            ClearAnimation();
            foreach(WearableGallery.ViewHolder vh in viewHolderList)
            {
                Position position = CreatePosition(idx);
                position.X -= (size * (currentPage));
                Animate(vh.GetView(), position, scale);
                idx++;
            }
            PlayAnimation();
        }

        private Position CreatePosition(int index)
        {
            return new Position((index * size), 0);
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
            return size;
        }
        
        public override int GetItemCountByLine()
        {
            return 1;
        }

    }
}