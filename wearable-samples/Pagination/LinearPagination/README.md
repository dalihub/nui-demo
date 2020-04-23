# Linear Pagination
This example shows how a linear `Pagination` will be positioned at a wearable device.

The `Pagination` class indicates the current position among the pages. `Tizen.NUI.Components` namespace contains the class.

In addition, `Pagination` class of `Tizen.NUI.Components` is for linear horizontal content only.
You can set various properties, such as indicator(dot) size, images, space, count, and focused index.

Please see the following example.

## Sample Application
<div style="text-align:center;width:100%;"><img src="./res/screenshot.png" /></div>

```C#
public class LinearPaginationExample : NUIApplication
{
    private Pagination pagination;

    private readonly int PAGE_COUNT = 6;
    public LinearPaginationExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        // Create Pagination component
        pagination = new Pagination()
        {
            Size = new Size(118, 10),
            Position = new Position(0, 24),

            // Set Pagination properties, such as Indicator size, count, and images.
            IndicatorSize = new Size(10, 10),
            IndicatorImageURL = new Selector<string>()
            {
                Normal = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "normal_dot.png",
                Selected = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "focus_dot.png",
            },
            IndicatorSpacing = 8,
            IndicatorCount = PAGE_COUNT,
            SelectedIndex = 0,

            // Positioning it to the top center
            ParentOrigin = ParentOrigin.TopCenter,
            PivotPoint = PivotPoint.TopCenter,
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
        LinearPaginationExample example = new LinearPaginationExample();
        example.Run(args);
    }
}


```