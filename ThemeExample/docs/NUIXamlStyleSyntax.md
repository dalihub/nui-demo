# Xaml Syntax for NUI Theme

This document shows basic rules to write NUI xaml style code. If you want to see complicate sample code of currently working in NUI framework, please visit [here](https://github.com/Samsung/TizenFX/blob/master/src/Tizen.NUI.Components/res/Theme/Tizen.NUI.Components_Tizen.NUI.Theme.Common.xaml).


## Basic Structure

### Theme
One xaml file represents one theme. Therefore, the most basic outer class is a `Theme`.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<Theme>

  <!-- Can list style items here -->

</Theme>
```

### Namespace
As in C# code, the namespace of classes used in the file must be specified. Without proper namespace, the application throws xaml parsing errors.

Here are mandatory namespaces.

* `http://tizen.org/Tizen.NUI/2018/XAML`
    * It includes most of basic namespaces such as `Tizen.NUI` and `Tizen.NUI.BaseComponents`.

* `http://schemas.microsoft.com/winfx/2009/xaml`
    * It's also needed for basic xaml syntax for C#.

You may need other namespace such as `Tizen.NUI.Components`.
The additional namespaces can be specified with assembly name.
`Tizen.NUI.Components` for example,
```
"clr-namespace:Tizen.NUI.Components;assembly=Tizen.NUI.Components"
```

Here's an example that uses all namespaces introduced here.
```xml
<?xml version="1.0" encoding="UTF-8"?>
<Theme
    xmlns="http://tizen.org/Tizen.NUI/2018/XAML"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:c="clr-namespace:Tizen.NUI.Components;assembly=Tizen.NUI.Components">

  <!-- Can list styles here -->

</Theme>
```

### Styles

You can define multiple styles, each with a unique key in the `Theme` tag.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<Theme
    xmlns="http://tizen.org/Tizen.NUI/2018/XAML"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:c="clr-namespace:Tizen.NUI.Components;assembly=Tizen.NUI.Components">

  <!-- smallText -->
  <TextLabelStyle x:Key="smallText" PointSize="10" />

  <!-- mediumText -->
  <TextLabelStyle x:Key="mediumText" PointSize="20" />

  <!-- largeText -->
  <TextLabelStyle x:Key="largeText" PointSize="30" />

  <!-- blueButton -->
  <!-- ButtonStyle is in Tizen.NUI.Components namespace, prefix c: is needed-->
  <c:ButtonStyle x:Key="blueButton" BackgroundColor="Blue" />

</Theme>
```

## Properties

Properties of a style item can be listed as a child tags.

```xml
<TextLabelStyle x:Key="greenText">
    <TextLabelStyle.TextColor>White</TextLabelStyle.TextColor>
    <TextLabelStyle.BackgroundColor>Green</TextLabelStyle.BackgroundColor>
</TextLabelStyle>
```

If the value can be expressed as a simple string, it can be in a line. This is same code as above.

```xml
<TextLabelStyle x:Key="greenText" TextColor="White" BackgroundColor="Green" />
```

In case the value cannot be a simple string, it needs to use multiple nested tags.

```xml
<c:ButtonStyle x:Key="blueTextButton">
  <c:ButtonStyle.Text>  <!--ButtonStyle.Text is TextLabelStyle type-->
    <TextLabelStyle TextColor="Blue" PointSize="20"/>
  </c:ButtonStyle.Text>
</c:ButtonStyle>
```

The following is the same code in C#.

```C#
var blueTextButton = new ButtonStyle()
{
  Text = new TextLabelStyle()
  {
    TextColor = Color.Blue,
    PointSize = 20
  }
};
```

## Position, Size and Color

Both position and size can be expressed as "`x, y`".

```xml
<c:ButtonStyle x:Key="bigButton" Size="200,200" Position="10,10"/>
```

For `Color`, there are 3 ways to express it.
* Hex
  * 6 digit hex code starts with #.
  * e.g. "#AAFF00"
* RGBA
  * [0, 1] ranged 4 float numbers consisting of red, green, blue and alpha.
  * e.g. "0.6, 0.1, 1.0, 1.0"
* Reserved words
  * Black, White, Red, Green, Blue, Yellow, Magenta, Cyan and Transparent

```xml
<TextLabel x:Key="coloredText" TextColor="Yellow" BackgroundColor="0.2, 0.3, 1.0, 0.5" />
```


## Selector

Some properties in ViewStyle are `Selector` types that can have multiple values by control's state.

```C#
var selector = new Selector<Color>()
{
  Normal = Color.Black,
  Pressed = Color.Blue,
  Disabled = Color.Red
};
```

The same xaml code is,
```xml
<Selector x:TypeArguments="Color" Normal="Black" Pressed="Blue" Disabled="Red"/>
```

Please note that, selectors works when the control state is enabled in a View. Control state are enabled for all controls in Tizen.NUI.Components by default, but not for those in Tizen.NUI.BaseComponents, such as View and TextLabel. If you want to enable control state for them, please set `EnableControlState` property to true.

```xml
<ViewStyle x:Key="viewColoredByTouch" EnableControlState="true">
  <ViewStyle.BackgroundColor>
    <Selector x:TypeArguments="Color" Normal="Black" Pressed="Blue" Disabled="Red"/>
  </ViewStyle.BackgroundColor>
</ViewStyle>
```
