# RadioButton in wearable theme
This example shows how a RadioButton will look on a wearable device.

When an application is running on a wearable device, the wearable styles will be applied to the component by default.

The wearable RadioButton uses Lottie to express touch animation.

The Lottie image would look like,

<div style="text-align:center;width:100%;"><img src="./res/icon.gif" /></div>

* Selection  :  0 ~ 12 frames
* Unselection: 13 ~ 25 frames

## Notice
* Available only in tizen devices!
* This example used predefined style provided by Tizen.NUI.Component package. For more detail, see `GetRadioButtonStyle()` code in [WearableTheme](https://github.com/rabbitfor/TizenFX/blob/master/src/Tizen.NUI.Components/PreloadStyle/WearableTheme.cs). You can refer this code to make your own style.
