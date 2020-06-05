# RadioButton in wearable theme
This example shows how a RadioButton will look on a wearable device.

When an application is running on a wearable device, the wearable styles will be applied to the component by default.

The wearable RadioButton uses Lottie to express touch animation.

The Lottie image would look like,

<div style="text-align:center;width:100%;"><img src="./res/icon.gif" /></div>

* Selection  :  0 ~ 12 frames
* Unselection: 13 ~ 25 frames

## Notice
* Available only in wearable devices!
* This example used predefined style provided by Tizen.NUI.Component package. For more detail, see `GetRadioButtonStyle()` code in [WearableTheme](https://github.com/rabbitfor/TizenFX/blob/master/src/Tizen.NUI.Components/PreloadStyle/WearableTheme.cs). You can refer this code to make your own style.

## Sample Application


```C#
public class ComponentExample : NUIApplication
{
    public ComponentExample() : base()
    {
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        Window window = NUIApplication.GetDefaultWindow();
        window.BackgroundColor = Color.Black;

        var button1 = new RadioButton()
        {
            Size = new Size(100, 100),
            Position = new Position(0, -50),
            PositionUsesPivotPoint = true,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
            IsSelected = true,
        };
        window.Add(button1);

        var button2 = new RadioButton()
        {
            Size = new Size(100, 100),
            Position = new Position(0, 50),
            PositionUsesPivotPoint = true,
            ParentOrigin = ParentOrigin.Center,
            PivotPoint = PivotPoint.Center,
        };
        window.Add(button2);

        var group = new RadioButtonGroup();
        group.Add(button1);
        group.Add(button2);
    }

    static void Main(string[] args)
    {
        ComponentExample example = new ComponentExample();
        example.Run(args);
    }
}
```