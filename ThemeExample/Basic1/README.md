# Manage your own theme
This example shows how to create Theme instance from the xaml file and how to apply it.


<img src="./preview/preview.png"/>


```C#
// Load theme file and apply it to the application.
var theme = new Theme(resDir + "Theme/CustomTheme.xaml");
ThemeManager.ApplyTheme(theme);

root.Add(new TextLabel() {
    StyleName = "TextA",
    Text = "TextA",
    Position = new Position(10, 10)
});

root.Add(new TextLabel() {
    StyleName = "TextB",
    Text = "TextB",
    Position = new Position(10, 80)
});

root.Add(new TextLabel() {
    StyleName = "TextC",
    Text = "TextC",
    Position = new Position(10, 170)
});
```
(Please find full c# code [here](./ThemeBasic1.cs))


```xaml
<!-- CustomTheme.xaml -->

<TextLabelStyle x:Key="TextA" BackgroundColor="#662000" PointSize="20" TextColor="White"/>

<TextLabelStyle x:Key="TextB" BackgroundColor="#476600" PointSize="30" TextColor="White"/>

<TextLabelStyle x:Key="TextC" BackgroundColor="#002066" PointSize="40" TextColor="White"/>
```
(Please find full xaml code [here](./res/Theme/CustomTheme.xaml))
