using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Collections.Generic;

namespace WearableGallerySample
{
        public class WearableGalleryItemAnimator
        {
            private Animation animation = new Animation(600);
            public delegate void animationFinishedHandler();
            public animationFinishedHandler animationFinished;

            public WearableGalleryItemAnimator()
            {
                animation.Finished += Animation_Finished;
            }

            private void Animation_Finished(object sender, EventArgs e)
            {
                if(animationFinished != null)
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