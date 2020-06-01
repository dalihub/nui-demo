using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
    public class CircularRecyclerItemAnimator
    {
        private Animation animation = new Animation(150);
        public delegate void animationFinishedHandler();
        public animationFinishedHandler animationFinished;

        public CircularRecyclerItemAnimator()
        {
            animation.Finished += Animation_Finished;
        }

        private void Animation_Finished(object sender, EventArgs e)
        {
            if (animationFinished != null)
            {
                animationFinished();
            }
        }

        public void Animate(View view, Position position, float scale = 1.0f)
        {
            //animation.Clear();
            animation.DefaultAlphaFunction = GetGlideOut();
            animation.AnimateTo(view, "Scale", new Vector3(scale, scale, 1.0f));
            animation.AnimateTo(view, "Position", position);
        }

        public void AnimatePath(View view, Path path)
        {
            //animation.Clear();
            animation.DefaultAlphaFunction = GetGlideOut();
            animation.AnimatePath(view, path, Vector3.Zero);
        }

        public void AnimateRotate(View view, Rotation rot)
        {
            //animation.Clear();
            animation.DefaultAlphaFunction = GetGlideOut();
            animation.AnimateTo(view, "Orientation", rot);
        }

        public void Play()
        {
            animation.Play();
        }

        public void ClearAnimation()
        {
            animation.Clear();
        }

        //Default Alpha Animation type
        private AlphaFunction GetGlideOut()
        {
            return new AlphaFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 1.0f));
        }
    }
}