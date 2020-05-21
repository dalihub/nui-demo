# Notification (Full size)
Tizen.NUI.Components provide a Notification class that helps to raise a notification window with a content View.

The users may set notification level by calling `SetLevel()`.

* Base (default) : The base nofitication level.
* Medium : The medium notification level than base.
* High : The higher notification level than medium.
* Top : The highest notification level.

Please note that you need to set [proper privilege](http://tizen.org/privilege/window.priority.set) to make the notification window be above other applications.

This example shows how to use Notification and apply animation on post/dismiss.

## Notice
* It needs privilege `http://tizen.org/privilege/window.priority.set`.

## Sample Application
<div style="text-align:center;width:100%;"><img src="./res/preview.gif" /></div>

Please refer full application code [here](./ComponentExample.cs).

### Create a new Notification.
```C#
// Prepare a contentView to show.
var contentView = new TextLabel()
{
    Size = new Size(360, 360),
    Position = new Position(360, 0),
    CornerRadius = 180,
    BackgroundColor = Color.Blue,
    Text = "Hello World!",
    TextColor = Color.White,
    HorizontalAlignment = HorizontalAlignment.Center,
    VerticalAlignment = VerticalAlignment.Center,
};

var animationOnPost = new Animation(500);
animationOnPost.AnimateTo(contentView, "Position", new Position(0, 0));

var animationOnDismiss = new Animation(500);
animationOnDismiss.AnimateTo(contentView, "Position", new Position(360, 0));

// Create a new Notification with the contentView and post!
new Notification(contentView)
    // (Optional) Dismiss when user touches it.
    .SetDismissOnTouch(true)
    // (Optional) Set an animation to be played when post.
    .SetAnimationOnPost(animationOnPost)
    // (Optional) Set an animation to be played when dismiss.
    .SetAnimationOnDismiss(animationOnDismiss)
    // You may set duration in ms when calling Post().
    // e.g. Post(3000/*duration*/)
    // If you don't set duration, it won't set timer for dismissal.
    .Post();

```