# Notification (Full size)
Tizen.NUI.Components provide a Notification class that helps to raise a notification window with a content View.

The users may set notification level by calling `SetLevel()`.

* Base (default) : The base nofitication level.
* Medium : The medium notification level than base.
* High : The higher notification level than medium.
* Top : The highest notification level.

Please note that you need to set [proper privilege](http://tizen.org/privilege/window.priority.set) to make the notification window be above other applciations.

This example shows how to use Notification and apply animation on post/dismiss.

## Notice
* It needs privilege `http://tizen.org/privilege/window.priority.set`.

## Sample Application
<div style="text-align:center;width:100%;"><img src="./res/preview.gif" /></div>

Please refer full application code [here](./ComponentExample.cs)

### Create a new Notification.
```C#
// Prepare a contentView to show.
var contentView = new TextLabel()
{
    Size = new Size(360, 360),
    Position = new Position(360, 0),
    CornerRadius = 180,
    BackgroundColor = Color.Blue,
    Text = "Click To Dismiss!",
    TextColor = Color.White,
    HorizontalAlignment = HorizontalAlignment.Center,
    VerticalAlignment = VerticalAlignment.Center,
};

// Create a new Notification with the contentView and post!

new Notification(contentView)
    // (Optional) Dismiss when user touch.
    .SetDismissOnTouch(true)
    // (Optional) Set a callback to be called when ready to post.
    .SetOnPostDelegate(OnNotificationPost)
    // (Optional) Set a callback to be called when dismiss.
    .SetOnDismissDelegate(OnNotificationDismiss)
    // You may set duration in ms like, Post(3000). If you don't it won't set timer for dismissal.
    .Post();

```

### Define onPost behavior.
```C#
void OnNotificationPost(Notification notification)
{
    // Animate contentView to move to (0, 0).
    var animation = new Animation(500);
    animation.AnimateTo(notification.ContentView, "Position", new Position(0, 0));
    animation.Play();
}
```

### Define onDismiss behavior.
```C#
uint OnNotificationDismiss(Notification notification)
{
    // Animate contentView to move to (360, 0).
    var animation = new Animation(500);
    animation.AnimateTo(notification.ContentView, "Position", new Position(360, 0));
    animation.Play();

    // Delay dismiss for animation playing duration
    return 500;
}
```