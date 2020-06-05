using NUIWHome.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWHome
{
    class AnimationManager
    {
        private Animation animationCore;
        private Animation stateAnimation = new Animation();

        private AlphaFunction alphaGlideOut;
        private AlphaFunction alphaSineOut33;
        private AlphaFunction alphaSineInOut80;

        private AnimationUtil mAniUtil;

        internal bool IsAnimating = false;

        public AnimationManager()
        {
            mAniUtil = new AnimationUtil();
            alphaGlideOut = mAniUtil.GetGlideOut();
            alphaSineOut33 = mAniUtil.GetSineOut33();
            alphaSineInOut80 = mAniUtil.GetSineInOut80();

            animationCore = new Animation();
            animationCore.Finished += AnimationCore_Finished;
            
            stateAnimation = new Animation();
            //stateAnimation.Finished += AnimationCore_Finished;
        }

        private void AnimationCore_Finished(object sender, EventArgs e)
        {
            IsAnimating = false;
        }

        //Need to fix -> Change function name
        internal void AnimateChangeState(RotaryLayerView layer, bool isEditMode, bool onlyIndicatorMoving = false)
        {
            stateAnimation.Clear();            
            stateAnimation.Duration = 350;

            if (isEditMode)
            {
                stateAnimation.AnimateTo(layer.GetContainer(), "Scale", new Vector3(0.9f, 0.9f, 1.0f), 0, 333, alphaGlideOut);
                stateAnimation.AnimateTo(layer.GetMainText(), "Opacity", 1.0f, 0, 333, alphaGlideOut);
            }
            else
            {
                if(!onlyIndicatorMoving)
                {
                    stateAnimation.AnimateTo(layer.GetContainer(), "Scale", new Vector3(1.0f, 1.0f, 1.0f), 0, 333, alphaGlideOut);
                    stateAnimation.AnimateTo(layer.GetMainText(), "Opacity", 1.0f, 0, 333, alphaGlideOut);
                }

                
                RotaryIndicator indicator = layer.GetIndicator();
                if(!indicator.isNotMoving())
                {
                    stateAnimation.AnimatePath(indicator, mAniUtil.GetIndicatorRotaryPath(indicator), Vector3.Zero, 0, 200, alphaSineOut33);
                    stateAnimation.AnimateTo(indicator, "Scale", new Vector3(1.1f, 1.1f, 1.1f), 0, 200, alphaSineOut33);
                    stateAnimation.AnimateTo(indicator, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 200, 350, alphaSineOut33);
                }
            }
            stateAnimation.Play();
        }

        internal void AnimateStarting(RotaryLayerView layer, List<RotaryItemWrapper> wrapperList)
        {
            animationCore.Duration = 350;
            animationCore.Clear();

            RotaryIndicator indicator = layer.GetIndicator();
            TextLabel mainText = layer.GetMainText();

            //Init value -> Animate position from 131(radius) to default value
            indicator.Position = indicator.GetRotaryPosition(indicator.CurrentIndex + 1, 131);
            indicator.Opacity = 0.0f;

            mainText.Opacity = 0.0f;
            mainText.Scale = new Vector3(1.2f, 1.2f, 1.2f);
            mainText.Text = wrapperList[0].RotaryItem.MainText;

            //Add animation
            animationCore.AnimateTo(indicator, "Position", indicator.GetRotaryPosition(indicator.CurrentIndex + 1), alphaSineInOut80);
            animationCore.AnimateTo(indicator, "Opacity", 1.0f, alphaSineInOut80);

            animationCore.AnimateTo(mainText, "Scale", new Vector3(1.0f, 1.0f, 1.0f), alphaSineInOut80);
            animationCore.AnimateTo(mainText, "Opacity", 1.0f, alphaSineInOut80);

            // Second Page -> hide
            for (int i = 0; i < wrapperList.Count; i++)
            {
                RotarySelectorItem item = wrapperList[i]?.RotaryItem;
                if(item != null)
                {
                    item.Opacity = 0.0f;
                    item.Position = wrapperList[i].GetRotaryPosition(wrapperList[i].CurrentIndex + 1, true, 170);

                    animationCore.AnimateTo(item, "Position", wrapperList[i].GetRotaryPosition(wrapperList[i].CurrentIndex + 1, true, 139), alphaSineInOut80);
                    animationCore.AnimateTo(item, "Opacity", 1.0f, alphaSineInOut80);
                }
            }

            animationCore.Play();
        }

        
        internal void AnimateEndEffect(View container)
        {
            animationCore.Duration = 999;
            animationCore.Clear();

            animationCore.AnimateTo(container, "Scale", new Vector3(0.9f, 0.9f, 1.0f), 0, 333, alphaGlideOut);
            animationCore.AnimateTo(container, "Opacity", 0.7f, 0, 333, alphaGlideOut);

            animationCore.AnimateTo(container, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 334, 666, alphaGlideOut);
            animationCore.AnimateTo(container, "Opacity", 1.0f, 334, 666, alphaGlideOut);

            animationCore.Play();
        }

        /// <summary>
        /// Play animation to show page
        /// </summary>
        internal void AnimateShowPage(RotaryItemWrapper wrapper, bool isReverse = true, PageAnimationType type = PageAnimationType.Slide)
        {
            RotarySelectorItem rotaryItem = wrapper.RotaryItem;
            int wrapperIndex = wrapper.CurrentIndex;

            if (type == PageAnimationType.Slide)
            {
                int sTime = isReverse ? mAniUtil.GetStartTime(wrapperIndex) : 240 - mAniUtil.GetStartTime(wrapperIndex);

                Position pos = wrapper.GetRotaryPosition(wrapperIndex + 1);

                rotaryItem.Position = new Position(pos.X + (isReverse ? 70 : -70), pos.Y); ;

                animationCore.AnimateTo(rotaryItem, "Position", pos, 167 + sTime, 84 + sTime + 334, alphaSineOut33);
                animationCore.AnimateTo(rotaryItem, "Opacity", 1.0f, 167 + sTime, 84 + sTime + 334, alphaSineOut33);
            }
            else
            {
                animationCore.AnimatePath(rotaryItem, mAniUtil.GetItemRotaryPath(wrapper, isReverse), Vector3.Zero, 200, 699, alphaGlideOut);
                animationCore.AnimateTo(rotaryItem, "Opacity", 1.0f, 200, 300, alphaGlideOut);
            }
        }

        /// <summary>
        /// Play animation to hide page
        /// </summary>
        internal void AnimateHidePage(RotaryItemWrapper wrapper, bool isReverse = true, PageAnimationType type = PageAnimationType.Slide)
        {
            RotarySelectorItem rotaryItem = wrapper.RotaryItem;

            rotaryItem.Opacity = 1.0f;

            if (type == PageAnimationType.Slide)
            {
                Position pos = wrapper.GetRotaryPosition(wrapper.CurrentIndex + 1);
                Position movePos = new Position(pos.X + (isReverse ? -70 : 70), pos.Y);

                animationCore.AnimateTo(rotaryItem, "Position", movePos, 0, 250, alphaGlideOut);
                animationCore.AnimateTo(rotaryItem, "Opacity", 0.0f, 0, 250, alphaGlideOut);
            }
            else
            {
                //Rotary hide animation
                animationCore.AnimatePath(rotaryItem, mAniUtil.GetRotaryPositionHidePath(wrapper, isReverse), Vector3.Zero, 0, 200, alphaSineOut33);
                animationCore.AnimateTo(rotaryItem, "Opacity", 0.0f, 0, 150, alphaSineOut33);

            }

        }


        internal void AnimatePageLayerContents(RotaryLayerView layer, bool cw)
        {
            float val = (cw ? 30.0f : -30.0f);

            animationCore.AnimateTo(layer.GetIndicator(), "Opacity", 0.0f, 0, 150);
            animationCore.AnimateTo(layer.GetIndicator(), "Opacity", 1.0f, 150, 400);

            animationCore.AnimateBy(layer.GetMainText(), "Position", new Position(val, 0.0f), 0, 83, alphaGlideOut);
            animationCore.AnimateTo(layer.GetMainText(), "Opacity", 0.0f, 0, 83, alphaGlideOut);
            animationCore.AnimateTo(layer.GetMainText(), "Position", new Position(-val, 0.0f), 84, 84);

            animationCore.AnimateBy(layer.GetMainText(), "Position", new Position(val, 0.0f), 85, 250, alphaSineOut33);
            animationCore.AnimateTo(layer.GetMainText(), "Opacity", 1.0f, 85, 250, alphaSineOut33);
        }

        /// <summary>
        /// Play rotary animation for editing mode (move to 
        /// </summary>
        internal void AnimatePathOnEdit(RotaryItemWrapper rotaryItemWrapper, bool cw = true)
        {
            animationCore.AnimatePath(rotaryItemWrapper.RotaryItem, mAniUtil.GetRotaryPositionPathIndex(rotaryItemWrapper, cw), Vector3.Zero);
        }

        internal void InitShowHideAnimation()
        {
            animationCore.Clear();
            animationCore.Duration = 167 + 334 + 198;
        }

        internal void InitRotaryPathAnimation()
        {
            animationCore.Clear();
            animationCore.Duration = 200;
        }

        internal void PlayCoreAnimation()
        {
            animationCore.Play();
        }

        public enum PageAnimationType
        {
            Slide,
            Rotary,
        }
    }
}
