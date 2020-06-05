using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public class DefaultLayoutManager : CircularRecyclerLayoutManager
    {
        private int CIRCLE_RADIUS = 700;
        private int CIRECLE_X_POSITION = 180;
        private int CIRECLE_Y_POSITION = 700;
        private int RESPONSIVIENESS = 250;

        public static int CIRCLE_ITEM_COUNT = 12;
        static float storedPositionX = 0;

        public DefaultLayoutManager() : base()
        {

        }
        public override void OrderByIndex(CircularRecycler gallery)
        {
            List<CircularRecycler.ViewHolder> viewHolderList = gallery.GetChild();
            //currentPage == extraIdx

            int currentIndex = gallery.GetCurrentPage();
            int count = viewHolderList.Count;

            for (int i = 0; i < count; i++)
            {
                View view = viewHolderList[i].GetView();
                view.Position = GetRotaryPosition(i - currentIndex);
                view.Orientation = GetRotation(i - currentIndex);

            }
        }

        public override void OrderByIndexAnimate(CircularRecycler gallery, bool cw = true)
        {
            List<CircularRecycler.ViewHolder> viewHolderList = gallery.GetChild();
            //currentPage == extraIdx
            
            int toIndex = gallery.GetCurrentPage();
            int fromIndex = (!cw) ? toIndex + 1 : toIndex - 1;

            int count = viewHolderList.Count;

            float val = (!cw) ? 1.0f : -1.0f;
            float span = (Math.Abs(storedPositionX + toIndex - fromIndex) / 10.0f) * val;

            ClearAnimation();
            for(int i = 0; i < count ; i++)
            {
                View view = viewHolderList[i].GetView();
                Path path = new Path();
                float startIdx = storedPositionX + i - fromIndex;
                for (int j = 0; j <= 10; j++)
                {
                    Position position = GetRotaryPosition(startIdx);
                    path.AddPoint(position);
                    startIdx += span;
                }
                //SetAngle(view, i - toIndex);
                path.GenerateControlPoints(0);
                AnimatePath(view, path);
                Radian radi = new Radian(new Degree(30 * (i - toIndex)));
                AnimateRotate(view, GetRotation(i - toIndex));
            }
            PlayAnimation();
            storedPositionX = 0;
        }

        private Rotation GetRotation(float idx)
        {
            Radian radian = new Radian(new Degree(30 * (idx)));
            return new Rotation(radian, new Vector3(0, 0, 1));

        }

        public Position GetRotaryPosition(float idx)
        {
            float x = (float)(CIRECLE_X_POSITION + CIRCLE_RADIUS * Math.Cos((float)idx / CIRCLE_ITEM_COUNT * 2 * Math.PI - Math.PI / 2));
            float y = (float)(CIRECLE_Y_POSITION + CIRCLE_RADIUS * Math.Sin((float)idx / CIRCLE_ITEM_COUNT * 2 * Math.PI - Math.PI / 2));
            return new Position(x, y);
        }

        public override void DragPage(CircularRecycler gallery, int distance)
        {
            List<CircularRecycler.ViewHolder> viewHolderList = gallery.GetChild();
            int count = viewHolderList.Count;
            int toIndex = gallery.GetCurrentPage();
            storedPositionX += ((float)distance / RESPONSIVIENESS);
            for (int i = 0; i < count ; i++)
            {
                View view = viewHolderList[i].GetView();
                view.Position = GetRotaryPosition(i - toIndex + storedPositionX);
                view.Orientation = GetRotation(i - toIndex + storedPositionX);
            }
        }

        public override void CancelPage(CircularRecycler gallery)
        {
            storedPositionX = 0;
            List<CircularRecycler.ViewHolder> viewHolderList = gallery.GetChild();
            //currentPage == extraIdx

            int toIndex = gallery.GetCurrentPage();

            int count = viewHolderList.Count;

            ClearAnimation();
            for (int i = 0; i < count; i++)
            {
                View view = viewHolderList[i].GetView();
                Path path = new Path();
                float startIdx = i - toIndex;

                Position position = GetRotaryPosition(startIdx);
                for (int j = 0; j <= 10; j++)
                {
                    path.AddPoint(position);
                }
                path.GenerateControlPoints(0);
                Animate(view, position);
                AnimateRotate(view, GetRotation(startIdx));
            }
            PlayAnimation();
        }

    }

}