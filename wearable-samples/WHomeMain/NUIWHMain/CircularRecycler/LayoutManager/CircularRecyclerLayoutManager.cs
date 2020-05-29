using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public abstract class CircularRecyclerLayoutManager
    {
        public CircularRecyclerItemAnimator animator;

        public CircularRecyclerLayoutManager()
        {
            animator = new CircularRecyclerItemAnimator();
        }

        //Order by index. if want using Animation, call Animate function.
        public abstract void OrderByIndex(CircularRecycler gallery);
        public abstract void OrderByIndexAnimate(CircularRecycler gallery, bool cw = true);
        public abstract void CancelPage(CircularRecycler gallery);
        
        public abstract void DragPage(CircularRecycler gallery, int distance);

        protected void PlayAnimation()
        {
            animator.Play();   
        }

        protected void ClearAnimation()
        {
            animator.ClearAnimation();   
        }
        protected void Animate(View view, Position position, float scale = 1.0f)
        {
            animator.Animate(view, position, scale);
        }
        protected void AnimatePath(View view, Path path)
        {
            animator.AnimatePath(view, path);
        }

        protected void AnimateRotate(View view, Rotation rot)
        {
            animator.AnimateRotate(view, rot);
        }
    }
}