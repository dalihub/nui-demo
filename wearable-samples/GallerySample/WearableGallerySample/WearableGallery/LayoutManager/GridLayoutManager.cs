using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public class GridLayoutManager : WearableGalleryLayoutManager
    {
        private float scale = 0.385f;
        private int pageSnap = 0;
        private int size = 0;
        public GridLayoutManager()
        {

        }
        
        public override void OrderByIndex(WearableGallery gallery)
        {
            List<WearableGallery.ViewHolder> viewHolderList = gallery.GetChild();
            int currentPage = gallery.GetCurrentPage();
            int idx  = 0;

            size = (int)(viewHolderList[0].GetView().Size.Width * scale) + 10;

            ClearAnimation();
            foreach(WearableGallery.ViewHolder vh in viewHolderList)
            {
                Position position = CreatePosition(idx);
                position.X -= pageSnap * (currentPage);
                Animate(vh.GetView(), position, scale);
                idx++;
            }
            PlayAnimation();
            pageSnap = size;
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
                position.X -= pageSnap * (currentPage);
                Animate(vh.GetView(), position, scale);
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
            return pageSnap;
        }

        private Position CreatePosition(int index)
        {
            int half_size = (size / 2);
            return new Position(index / 2 * size - half_size, index % 2  == 0 ? -half_size : half_size);
        }
        
        public override int GetItemCountByLine()
        {
            return 2;
        }

    }
}