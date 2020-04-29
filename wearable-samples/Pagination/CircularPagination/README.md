# Circular Pagination
This example shows how a `CircularPagination` will be positioned at a wearable device.

The `CircularPagination` class indicates the current position among the pages. `Tizen.NUI.Wearable` namespace contains the class.
In particular, `CircularPagination` class locates the indicators specifically in wearable device. So, the indicators of `CircularPagination` has to be located in the middle of the top in `Window`.

You can set various properties, such as indicator(dot) size, images, space, count, and focused index.

Please see the following example.

## Sample Application
<div style="text-align:center;width:100%;"><img src="./res/screenshot.png" /></div>

```C#

public class CircularPaginationExample : NUIApplication
{
    private CircularPagination pagination;

    private readonly int PAGE_COUNT = 19;
    public CircularPaginationExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        // Create CircularPagination
        pagination = new CircularPagination()
        {
            Size = new Size(360, 360),

            // Set CircularPagination properties, such as Indicator size, count, and images.
            IndicatorSize = new Size(10, 10),
            IndicatorImageURL = new Selector<string>()
            {
                Normal = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "normal_dot.png",
                Selected = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "focus_dot.png",
            },
            IndicatorCount = PAGE_COUNT,
            SelectedIndex = 0,

            // Positioning it to the center
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PositionUsesPivotPoint = true
        };

        window.Add(pagination);

        // Bezel event
        window.WheelEvent += Pagination_WheelEvent;
    }

    private void Pagination_WheelEvent(object source, Window.WheelEventArgs e)
    {
        // CustomWheel means Bezel in wearable device.
        if (e.Wheel.Type == Wheel.WheelType.CustomWheel)
        {
            // Direction 1 is CW and 2 is CCW
            if (e.Wheel.Direction == 1)
            {
                if (pagination.SelectedIndex < pagination.IndicatorCount - 1)
                {
                    pagination.SelectedIndex = pagination.SelectedIndex + 1;
                }
            }
            else
            {
                if (pagination.SelectedIndex > 0)
                {
                    pagination.SelectedIndex = pagination.SelectedIndex - 1;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        CircularPaginationExample example = new CircularPaginationExample();
        example.Run(args);
    }
}

```