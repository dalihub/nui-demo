using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public abstract class WearableGalleryLayoutManager
    {
        public WearableGalleryItemAnimator animator;
        private WearableGallery gallery;

        public WearableGalleryLayoutManager()
        {
            animator = new WearableGalleryItemAnimator();
            
        }
        
        //Order by index. if want using Animation, call Animate function.
        public abstract void OrderByIndex(WearableGallery gallery);
        public abstract void CancelPage(WearableGallery gallery);
        
        public abstract void DragPage(WearableGallery gallery, int distance);

        public abstract int GetSnapPageWidth();

        public abstract int GetItemCountByLine();
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
        
    }
}