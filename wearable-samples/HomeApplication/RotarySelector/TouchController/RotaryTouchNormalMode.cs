using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;
using Tizen.NUI;

namespace NUIWHome
{
    internal class RotaryTouchNormalMode : RotaryTouchController
    {
        private Animation ani;

        public RotaryTouchNormalMode()
        {
        }

        public override bool ProcessTouchDownEvent(RotarySelectorItem item)
        {
            if(!IsProcessing)
            {
                IsProcessing = true;
                if(ani != null)
                {
                    ani.Reset();
                    ani.Clear();
                    ani.Dispose();
                    ani = null;
                }

                ani = new Animation(400);
                ani.DefaultAlphaFunction = GetSineOut33();

                if (SelectedItem == null)
                {
                    ani.AnimateTo(item, "Scale", new Vector3(1.17f, 1.17f, 1.17f));
                    ani.Play();
                    SelectedItem = item;
                    SelectedItem.ClickedItem();
                    ani.Finished += Ani_Finished;
                    return true;
                }
                else
                {
                    ani.AnimateTo(SelectedItem, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                    ani.AnimateTo(item, "Scale", new Vector3(1.17f, 1.17f, 1.17f));
                    ani.Play();

                    SelectedItem = item;
                    SelectedItem.ClickedItem();
                    ani.Finished += Ani_Finished;
                    return true;
                }
            }
            return false;
        }

        private AlphaFunction GetSineOut33()
        {
            return new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.67f, 1.0f));
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            IsProcessing = false;
        }
    }
}