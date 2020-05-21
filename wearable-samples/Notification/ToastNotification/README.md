# Notification as a Toast
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
    Size = new Size(180, 60),
    CornerRadius = 30,
    BackgroundColor = Color.White,
    Opacity = 0,
    Text = "Hello World!",
    PixelSize = 24,
    ParentOrigin = ParentOrigin.Center,
    PivotPoint = PivotPoint.Center,
    PositionUsesPivotPoint = true,
    HorizontalAlignment = HorizontalAlignment.Center,
    VerticalAlignment = VerticalAlignment.Center,
    Scale = new Vector3(0, 0, 0),
};

// Define animations
var animationOnPost = new Animation(200);
animationOnPost.AnimateTo(contentView, "Opacity", 0.8f);
animationOnPost.AnimateTo(contentView, "Scale", new Vector3(1, 1, 1));

var animationOnDismiss = new Animation(200);
animationOnDismiss.AnimateTo(contentView, "Opacity", 0);
animationOnDismiss.AnimateTo(contentView, "Scale", new Vector3(0, 0, 0));

new Notification(contentView)
    .SetAnimationOnPost(animationOnPost)            // (Optional) Set an animation to be played when post.
    .SetAnimationOnDismiss(animationOnDismiss)      // (Optional) Set an animation to be played when dismiss.
    .SetPositionSize(new Rectangle(90, 20, 180, 60)) // (Optional) Set notification window boundary.
    .Post(2000); // Post for 2 seconds

```