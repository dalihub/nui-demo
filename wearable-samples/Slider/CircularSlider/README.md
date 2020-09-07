# Circular Slider

The `CircularSlider` is a class that shows the current value with the length of the line.
The thumb of slider shows the exact value at the track. In particular, `CircularSlider` class is available only in wearable devices.

This example shows how to use CircularSlider.

## Notice
* Available only in wearable devices

## Sample Application
<div style="text-align:center;width:100%;"><img src="./preview/circularSlider.gif" /></div>

```C#

public class CircularSliderExample : NUIApplication
{
    Window window;
    CircularSlider slider;

    public CircularSliderExample() : base(new Size2D(360, 360), new Position2D(0, 0))
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        window = NUIApplication.GetDefaultWindow();

        slider = new CircularSlider();
        slider.CurrentValue = 0;

        // These properties have default values, so they're set to the following values
        // even if you don't set them separately.
        slider.MinValue = 0;
        slider.MaxValue = 10;
        slider.Thickness = 6;

        window.Add(slider);

        // Bezel event
        window.WheelEvent += Slider_WheelEvent;

        // If you want to change the colors of the track and the progress,
        // use TrackColor and ProgressColor properties.

        // If you want to change thumb color and size,
        // use ThumbColor and ThumbSize properties.
    }

    private void Slider_WheelEvent(object source, Window.WheelEventArgs e)
    {
        // CustomWheel means Bezel in wearable device.
        if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
        {
            // Direction 1 is CW and 2 is CCW
            if (e.Wheel.Direction == 1)
            {
                if (slider.CurrentValue < slider.MaxValue)
                {
                    slider.CurrentValue++;
                }
            }
            else
            {
                if (slider.CurrentValue > slider.MinValue)
                {
                    slider.CurrentValue--;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        CircularSliderExample example = new CircularSliderExample();
        example.Run(args);
    }
}

```