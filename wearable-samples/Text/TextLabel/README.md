# TextLabel in wearable
This example shows how a `TextLabel` is displayed at a wearable device.

The `TextLabel` class displays a short text string. `Tizen.NUI.BaseComponents` namespace contains the class. `TextLabel` is non-editable so that it just can show the text.

For more information, see [NUI TextLabel](https://docs.tizen.org/application/dotnet/guides/nui/text) guide.
Also, please refer to the following example.

## Sample Application
<div style="text-align:center;width:100%;"><img src="./res/screenshot.png" /></div>


```C#
public class TextLabelExample : NUIApplication
{
    public TextLabelExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.White;

        // Create a TextLabel
        TextLabel label = new TextLabel()
        {
            Size = new Size(window.Size),
            Text = "Hello World",

            // Set TextLabel alignment properties to align the text horizontally or vertically
            // to the beginning, center, or end of the available area.
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,

            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            PositionUsesPivotPoint = true
        };
        window.Add(label);
    }

    static void Main(string[] args)
    {
        TextLabelExample example = new TextLabelExample();
        example.Run(args);
    }
}
```