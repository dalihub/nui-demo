using Tizen.NUI;
using Tizen.NUI.Components;

namespace NUISample
{
    class PannelButton : Button
    {
        private Direction mType;

        public enum Direction
        {
            TopLeft,
            TopRight,
            Center,
            BottomLeft,
            BottomRight,
        };

        public PannelButton()
        {
            PositionUsesPivotPoint = true;
        }

        public Direction Type
        {
            get
            {
                return mType;
            }
            set
            {

                mType = value;

                switch (mType)
                {
                    case Direction.TopLeft:
                        {
                            PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
                            ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                            Text = "↖";
                            Position = new Position(20, 20);
                            break;
                        }
                    case Direction.TopRight:
                        {
                            PivotPoint = Tizen.NUI.PivotPoint.TopRight;
                            ParentOrigin = Tizen.NUI.ParentOrigin.TopRight;
                            Text = "↗";
                            Position = new Position(-20, 20);
                            break;
                        }
                    case Direction.Center:
                        {
                            PivotPoint = Tizen.NUI.PivotPoint.Center;
                            ParentOrigin = Tizen.NUI.ParentOrigin.Center;
                            Text = "C";
                            break;
                        }
                    case Direction.BottomLeft:
                        {
                            PivotPoint = Tizen.NUI.PivotPoint.BottomLeft;
                            ParentOrigin = Tizen.NUI.ParentOrigin.BottomLeft;
                            Text = "↙";
                            Position = new Position(20, -20);
                            break;
                        }
                    case Direction.BottomRight:
                        {
                            PivotPoint = Tizen.NUI.PivotPoint.BottomRight;
                            ParentOrigin = Tizen.NUI.ParentOrigin.BottomRight;
                            Text = "↘";
                            Position = new Position(-20, -20);
                            break;
                        }
                }
            }
        }
    }
}