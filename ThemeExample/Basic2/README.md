# Manage your own theme
This example shows how to create Theme instance from the xaml file and how to apply it.

The basic code to using xaml for a theme, please refer below code.

```C#
var theme = new Theme(resPath + "FileName.xaml");
ThemeManager.ApplyTheme(theme);
```

## Preview
<table style="text-align:center;">
  <tr>
    <th><img src="./preview/before.png"/></th>
    <th><img src="./preview/after.png"/></th>
  </tr>
</table>

## Description
After run the application, please click the button at the top.
Then you see the styles applied to the application are changed overall.
The theme files can be found in the [res/Theme](./res/Theme) directory.

