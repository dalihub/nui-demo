# NUI Styling with Xaml Theme

## Notice

Note that, the NUI xaml styling is for the NUI `View` and derived classes in Tizen.NUI.BaseComponents and Tizen.NUI.Components.

It is supported limitedly according to the device profile.

|Profile|Tizen 6.0|Tizen 6.5|
|---|---|---|
|Mobile/FHub|Draft|O|
|TV|X|O|

<br/>
<br/>


## Samples
* Sample: [Basic1](./Basic1)
* Sample: [Basic2](./Basic2)

<br/>
<br/>

## Basic Styling
The NUI `View` and the derived classes can be decorated with a corresponding `ViewStyle` class.
For example,
* `ViewStyle` decorates `View`
* `TextLabelStyle` decorates `TextLabel`
* `ButtonStyle` decorates `Button`


Here's a simple example that styles a `TextLabel`.

```C#
TextLabelStyle testStyle = new TextLabelStyle()
{
  TextColor = Color.White,
  PointSize = 20,
};

TextLabel textLabel = new TextLabel();
textLabel.ApplyStyle(teststyle);
```
<br/>
<br/>

## Writing Style in Xaml
In the above example, the TextLabelStyle is written in C#. The same code can be written in xaml like below.

```xml
<TextLabelStyle x:Key="testStyle" TextColor="White" PointSize="20" />
```
This describes a `TextLabelStyle` instance that has white text color and point size 20. Note that, it has a `x:Key` attribute that will be a key to match it to a TextLabel. A TextLabel can specify this key using `StyleName` property.
```C#
TextLabel textLabel = new TextLabel()
{
  StyleName = "testStyle"
};
```

We named a collection of styles `Theme`. One xaml file represents one theme. So the full xaml code of the above example is,

```xml
<?xml version="1.0" encoding="UTF-8"?>
<Theme
  xmlns="http://tizen.org/Tizen.NUI/2018/XAML"
  xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">

  <TextLabelStyle x:Key="testStyle" TextColor="White" PointSize="20" />

  <!-- Can list other styles here -->

</Theme>
```

To load a xaml file and apply it to the current NUI application, `Theme` and `ThemeManager` are used.

```C#
// Sample.cs
// Load theme from the xaml file
Theme theme = new Theme("full_path_of_the_xaml_file.xaml");

// Apply it to the current NUI Application
ThemeManager.ApplyTheme(theme);
```

For more xaml information, please visit [NUI Xaml style syntax](./docs/NUIXamlStyleSyntax.md).
<br/>
<br/>

## Event and Callback

Since applying a new theme may reset properties set by user, so users may want to be notified when the theme changed.
For the users who want to control unintentionally overwritten properties, the theme system provides two ways to get notified of theme change.

* `ThemeManager.ThemeChanged`
* `View.OnThemeChanged`
<br/>

### ThemeManager.ThemeChanged
`ThemeManager` provides an event that is raised after theme changed.

```C#
ThemeManager.ThemeChanged += (s, e)
{
  // Theme has changed!
  // Set text color to blue again.
  textLabel.TextColor = Color.Blue;
}
```
<br/>

### View.OnThemeChanged
The other way of getting this is to make a derived class of the View and override `OnThemeChanged` method.

Overriding `OnThemeChanged` enables user to control style changing in View.

```C#
class AlwaysBlueTextLabel : TextLabel
{
  protected override void OnThemeChanged(object sender, ThemeChangedEventArgs e)
  {
    // Theme has changed but the new style is not applied to this view yet.

    // Call base method to apply new style in new theme to this view.
    base.OnThemeChanged(sender, e);

    // Set text color to blue.
    textLabel.TextColor = Color.Blue;
  }
}
```
<br/>
<br/>

## More Information
* [NUI Xaml style syntax](./docs/NUIXamlStyleSyntax.md)
