# Circular Progress

The `CircularProgress` is a class that shows the ongoing status with a circular bar.
It can be counted in its percentage and shows the length of the process. In particular, `CircularProgress` class is available only in wearable devices.

Basically, `CircularProgress` is for full screen. (360 x 360) But, it also can be displayed on the button or the list for small size. User can set the length for size as parameter of constructor.

This example shows how to use CircularProgress.

## Notice
* Available only in wearable devices

## Sample Application
<div style="text-align:center;width:100%;"><img src="./res/circularprogress.gif" /></div>


```C#

public class CircularProgressExample : NUIApplication
{
    CircularProgress circular;

    public CircularProgressExample() : base(new Size2D(360, 360), new Position2D(0, 0))
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        var window = NUIApplication.GetDefaultWindow();

        // You can set the size of CircularProgress as parameter here.
        circular = new CircularProgress();
        circular.CurrentValue = 40;

        // These properties are default values, so they're set to the following values
        // even if you don't set them separately.
        circular.MinValue = 0;
        circular.MaxValue = 100;
        circular.Thickness = 6;

        window.Add(circular);

        // Bezel event
        window.WheelEvent += Progress_WheelEvent;

        // If you want to change the colors of the track and the progress,
        // use TrackColor and ProgressColor properties.

    }

    private void Progress_WheelEvent(object source, Window.WheelEventArgs e)
    {
        // CustomWheel means Bezel in wearable device.
        if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
        {
            // Direction 1 is CW and 2 is CCW
            if (e.Wheel.Direction == 1)
            {
                if (circular.CurrentValue < circular.MaxValue)
                {
                    circular.CurrentValue = circular.CurrentValue + 10;
                }
            }
            else
            {
                if (circular.CurrentValue > circular.MinValue)
                {
                    circular.CurrentValue = circular.CurrentValue - 10;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        CircularProgressExample example = new CircularProgressExample();
        example.Run(args);
    }
}

```