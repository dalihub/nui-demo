using Tizen.NUI.BaseComponents;

namespace Scrolling
{
    /// <summary>
    /// Can only have one child.
    /// Needs to intercept focus events for child but still able to focus a child.
    /// Stores current child that is focused, on intercepting focus events it focuses and
    /// scrolls to that child.
    /// </summary>
    class ScrollingView : View
    {

    }
}