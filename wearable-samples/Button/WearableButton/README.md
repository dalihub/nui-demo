# Button in wearable theme
This example shows how a button will look on a wearable device.

When an application is running on a wearable device, the wearable styles will be applied to the component by default.

## Notice
* Available only in wearable devices
* This example used predefined style provided by Tizen.NUI.Component package. For more detail, see `GetButtonStyle()` code in [WearableTheme](https://github.com/rabbitfor/TizenFX/blob/master/src/Tizen.NUI.Components/PreloadStyle/WearableTheme.cs). You can refer this code to make your own style.

## Sample Application
<div style="text-align:center;width:100%;"><img src="./res/bottom_button.gif" /></div>


```C#
public class ComponentExample : NUIApplication
{
    public ComponentExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        // Set theme to wearable.
        // (It is not needed in the wearable device)
        Tizen.NUI.Components.StyleManager.Instance.Theme = "wearable";

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;        

        // Create a button with a predefined style
        var button = new Button()
        {
            Text = "Hello World!",

            // Positioning it to the bottom
            ParentOrigin = ParentOrigin.BottomCenter,
            PivotPoint = PivotPoint.BottomCenter,
            PositionUsesPivotPoint = true,
            Position = new Position(0, -20)
        };
        window.Add(button);
    }

    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}
```