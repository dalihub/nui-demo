using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace LayoutDemo
{

    class LayoutScroller : Control
    {
        View container;
        private float containerPosition = 0.0f;
        private bool isVertical;

        public LayoutScroller(bool isVertical):base()
        {
            this.isVertical = isVertical;
            Initialize();
        }

        private void Initialize()
        {
            Size = isVertical ? new Size(1000, 1000): new Size(1000,100);
            ClippingMode = ClippingModeType.ClipToBoundingBox;

            container = new View();
            container.Size = isVertical?new Size(1000, 6000): new Size(6000, 100);
            container.BackgroundColor = Color.Black;
            Add(container);

            CreateItem();
 
            PanGestureDetector detector = new PanGestureDetector();
            detector.Attach(this);
            detector.AddDirection(isVertical?PanGestureDetector.DirectionVertical: PanGestureDetector.DirectionHorizontal);
            detector.Detected += PanDetected;
        }

        private void PanDetected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            if (e.PanGesture.State == Gesture.StateType.Started)
            {
                Console.WriteLine("Pan Started");
            }
            else if (e.PanGesture.State == Gesture.StateType.Continuing )
            {
                Console.WriteLine("Pan Continuing");
                FollowPanning(isVertical ? e.PanGesture.Displacement.Y : e.PanGesture.Displacement.X);
            }
            else if (e.PanGesture.State == Gesture.StateType.Finished)
            {
                Console.WriteLine("Pan Ended");
            }
        }

        public void FollowPanning(float displacement)
        {
            if(isVertical)
            {
                containerPosition = containerPosition + displacement;
                containerPosition = Math.Min(0, containerPosition);
                containerPosition = Math.Max(-(container.Size.Height - Size.Height), containerPosition);
                container.PositionY = containerPosition;
            }
            else
            {
                containerPosition = containerPosition + displacement;
                containerPosition = Math.Min(0, containerPosition);
                containerPosition = Math.Max(-(container.Size.Width - Size.Width), containerPosition);
                container.PositionX = containerPosition;
            }
        }


        private void CreateItem()
        {
            if(isVertical)
            {
                for (int i = 0; i < 60; i++)
                {
                    if(i%10 == 0)
                    {
                        LayoutScroller item = new LayoutScroller(false);
                        item.Position = new Position(0, i * 100);
                        container.Add(item);
                    }
                    else
                    {
                        View item = new View();
                        item.BackgroundColor = i % 2 == 0 ? Color.Cyan : Color.Magenta;
                        item.Size = new Size(1000, 100);
                        item.Position = new Position(0, i * 100);
                        container.Add(item);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    View item = new View();
                    item.BackgroundColor = i % 2 == 0 ? Color.Cyan : Color.Magenta;
                    item.Size = new Size(600, 100);
                    item.Position = new Position(i * 600,0);
                    container.Add(item);
                }
            }
        }
    }

    class AxisPanningExample : Example
    {
        public AxisPanningExample() : base("Axis Panning")
        {}
        public override void Create()
        {
            Window.Instance.BackgroundColor = Color.White;
            
            Window.Instance.GetDefaultLayer().Add(new LayoutScroller(true));
        }

        // static void Main(string[] args)
        // {
        //     var app = new Program();
        //     app.Run(args);
        // }
        public override void Remove()
        {

        }
    }
}