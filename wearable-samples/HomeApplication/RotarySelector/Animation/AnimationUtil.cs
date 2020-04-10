using NUIWHome.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;

namespace NUIWHome
{
    internal class AnimationUtil
    {

        /// <summary>
        /// GlideOut, 0.25, 0.46, 0.45, 1.0
        /// </summary>
        internal AlphaFunction GetGlideOut()
        {
            return new AlphaFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 1.0f));
        }

        /// <summary>
        /// SineInOut80, 0.33, 0.0, 0.2, 1.0
        /// </summary>
        internal AlphaFunction GetSineInOut80()
        {
            return new AlphaFunction(new Vector2(0.33f, 0.0f), new Vector2(0.2f, 1.0f));
        }

        /// <summary>
        /// GetSineOut33, 0.17, 0.17, 0.67, 1.0
        /// </summary>
        internal AlphaFunction GetSineOut33()
        {
            return new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.67f, 1.0f));
        }

        internal AlphaFunction GetEaseOutSquare()
        {
            return new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare);
        }

        internal int GetStartTime(int idx)//Need to calcuate
        {
            switch (idx)
            {
                case 8:
                    return 0;
                case 7:
                case 9:
                    return 33;
                case 6:
                case 10:
                    return 66;
                case 5:
                    return 99;
                case 0:
                case 4:
                    return 132;
                case 1:
                case 3:
                    return 165;
                case 2:
                    return 198;
            }
            return 0;
        }



        internal Path GetRotaryPositionPathIndex(RotaryItemWrapper wrapper, bool cw = true)
        {
            Path path = new Path();
            float fIndex = (float)(cw ? wrapper.CurrentIndex + 2 : wrapper.CurrentIndex);
            for (int j = 0; j <= 10; j++)
            {
                path.AddPoint(wrapper.GetRotaryPosition(fIndex));
                fIndex += (cw ? -0.1f : 0.1f);
            }
            path.GenerateControlPoints(0);
            return path;
        }

        internal Path GetItemRotaryPath(RotaryItemWrapper wrapper, bool isReverse = true)
        {
            Path path = new Path();
            float fIndex = wrapper.CurrentIndex + 1;

            if (isReverse)
            {
                for (int j = 0; j <= fIndex; j++)
                {
                    path.AddPoint(wrapper.GetRotaryPosition(j));
                }
            }
            else
            {
                for (int j = ApplicationConstants.MAX_TRAY_COUNT; j >= fIndex; j--)
                {
                    path.AddPoint(wrapper.GetRotaryPosition(j));
                }
            }
            path.GenerateControlPoints(0);
            return path;
        }


        internal Path GetIndicatorRotaryPath(RotaryIndicator indiacotr)
        {
            int sidx = indiacotr.PrevIndex + 1;
            int eidx = indiacotr.CurrentIndex + 1;

            Path path = new Path();

            if (sidx < eidx)
            {
                for (int j = sidx; j <= eidx; j++)
                {
                    path.AddPoint(indiacotr.GetRotaryPosition(j));
                }
            }
            else
            {
                for (int j = sidx; j >= eidx; j--)
                {
                    path.AddPoint(indiacotr.GetRotaryPosition(j));
                }
            }
            path.GenerateControlPoints(0);
            return path;
        }

        internal Path GetRotaryPositionPath(RotaryItemWrapper wrapper, int s, int e, bool isReverse = true)
        {
            Path path = new Path();
            float fIndex = wrapper.CurrentIndex + 1;

            if (isReverse)
            {
                for (int j = s; j <= fIndex; j++)
                {
                    path.AddPoint(wrapper.GetRotaryPosition(j));
                }
            }
            else
            {
                for (int j = e; j >= fIndex; j--)
                {
                    path.AddPoint(wrapper.GetRotaryPosition(j));
                }
            }
            path.GenerateControlPoints(0);
            return path;
        }

        internal Path GetRotaryPositionHidePath(RotaryItemWrapper wrapper, bool isReverse = true)
        {
            Path path = new Path();
            float fIndex = wrapper.CurrentIndex + 1;

            if (isReverse)
            {
                for (int j = (int)fIndex; j >= 0; j--)
                {
                    path.AddPoint(wrapper.GetRotaryPosition(j, false));
                }
            }
            else
            {
                for (int j = (int)fIndex; j <= ApplicationConstants.MAX_TRAY_COUNT; j++)
                {
                    path.AddPoint(wrapper.GetRotaryPosition(j, false));
                }
            }
            path.GenerateControlPoints(0);
            return path;
        }

    }
}
