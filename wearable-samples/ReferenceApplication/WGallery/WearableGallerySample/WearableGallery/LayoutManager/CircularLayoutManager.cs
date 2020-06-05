using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;


namespace WearableGallerySample
{
    public class CircularLayoutManager : WearableGalleryLayoutManager
    {
        private float scale = 0.3f;
        private int pageSnap = 0;
        private int size = 0;
        public CircularLayoutManager()
        {

        }
        
        public override void OrderByIndex(WearableGallery gallery)
        {
            List<WearableGallery.ViewHolder> viewHolderList = gallery.GetChild();
            int currentPage = gallery.GetCurrentPage();
            int idx = 0;

            size = (int)(viewHolderList[0].GetView().Size.Width * scale) + 5;
            pageSnap = size * 2;
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


        private Position CreatePosition(int index)
        {
            int mod = index % 3;
            int xPos = size;
            int yPos = 0;
            int temp = size /2;
            int start = -(size / 2);
            int yLength = 15;

            if(mod == 0)
            {
                yPos = -xPos;
                yPos += yLength;
            }
            else if(mod == 1)
            {
                start -= temp;
            }
            else if(mod == 2)
            {
                yPos = xPos;
                yPos -= yLength;
            }

            return new Position(index / 3 * xPos + start, yPos);
        }


        public override int GetSnapPageWidth()
        {
            return pageSnap;
        }
        public override int GetItemCountByLine()
        {
            return 6;
        }

    }
}