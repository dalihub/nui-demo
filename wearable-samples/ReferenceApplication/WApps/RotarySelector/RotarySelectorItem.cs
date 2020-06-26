
using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;

namespace NUIWHome
{
    /// <summary>
    /// RotarySelectorItem
    /// </summary>
    public class RotarySelectorItem : ImageView
    {
        /// <summary>
        /// Get/Set Main Text
        /// </summary>
        public string MainText { get; set; }

        /// <summary>
        /// Get/Set Sub Text
        /// </summary>
        public string SubText { get; set; }

        /// <summary>
        /// Get/Set Color of MainText
        /// </summary>
        public Color MainTextColor { get; set; }
        /// <summary>
        /// Get/Set Color of SubText
        /// </summary>
        public Color subTextColor { get; set; }

        internal int CurrentIndex { get; set; }


        private RotaryBadege itemBadge;
        public int Badge { get; set; }

        private RotaryBadege deleteBadge;
        public bool IsDeleteAble { get; set; }

        /// <summary>
        /// Creates a new instance of a RotarySelectorItem.
        /// </summary>
        public RotarySelectorItem()
        {
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;
            IsDeleteAble = true;
        }

        internal void EnableBadge()
        {
            if (Badge <= 0)
            {
                return;
            }
            if (itemBadge == null)
            {
                itemBadge = new RotaryBadege(1);
                itemBadge.SetRightSide();
                itemBadge.SetBadgeNumber(Badge);
            }
            this.Add(itemBadge);
        }
        internal void RemoveBadgeIcon()
        {
            if (itemBadge != null)
            {
                itemBadge.Unparent();
                itemBadge.Dispose();
                itemBadge = null;
            }
        }

        internal void AddDeleteIcon(int idx)
        {
            if(!IsDeleteAble)
            {
                return;
            }
            if (deleteBadge == null)
            {
                deleteBadge = new RotaryBadege(0);
                deleteBadge.SetRightSide();
                deleteBadge.TouchEvent += DeleteBadge_TouchEvent;
            }
            this.Add(deleteBadge);
        }

        internal void RemoveDeleteIcon()
        {
            if (deleteBadge != null)
            {
                deleteBadge.TouchEvent -= DeleteBadge_TouchEvent;
                deleteBadge.Unparent();
                deleteBadge.Dispose();
                deleteBadge = null;
            }
        }

        public void ShowDeleteIcon()
        {
            deleteBadge?.Show();
        }
        public void HideDeleteIcon()
        {
            deleteBadge?.Hide();
        }

        internal event EventHandler<EventArgs> Touch_DeleteBadgeHandler;

        private bool DeleteBadge_TouchEvent(object source, TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up && Touch_DeleteBadgeHandler != null)
            {
                CallDelete();
                Touch_DeleteBadgeHandler(this, e);
                return false;
            }
            if (e.Touch.GetState(0) == PointStateType.Motion)
            {
                return false;
            }
            return true;
        }
        public delegate void ItemSelectedHandler(RotarySelectorItem item);
        public event ItemSelectedHandler OnItemSelected;

        internal void SelectedItem()
        {
            OnItemSelected(this);
            CallSelect();
        }

        internal void ClickedItem()
        {
            OnItemSelected(this);
            CallSelect();
            CallClick();
        }

        private View currentParent { get; set; }

        internal void SetCurrentParent(View parent)
        {
            currentParent = parent;
        }

        internal void AddToParent()
        {
            if (!currentParent.Children.Contains(this))
            {
                currentParent.Add(this);
            }
        }

        //"item,selected": When the user selected an item.

        private event EventHandler<EventArgs> selectedHandler;

        private void CallSelect()
        {
            EventArgs e = new EventArgs();

            if (selectedHandler != null)
            {
                selectedHandler(this, e);
            }
        }

        public event EventHandler<EventArgs> Selected
        {
            add
            {
                selectedHandler += value;
            }
            remove
            {
                selectedHandler -= value;
            }
        }


        //"item,clicked": When selecting again the alredy selected item or selecting a selector.
        private event EventHandler<EventArgs> clickedHandler;

        private void CallClick()
        {
            EventArgs e = new EventArgs();

            if (clickedHandler != null)
            {
                clickedHandler(this, e);
            }
        }

        public event EventHandler<EventArgs> Clicked
        {
            add
            {
                clickedHandler += value;
            }
            remove
            {
                clickedHandler -= value;
            }
        }


        //Need to add, "item,deleted": When the user deleted an item. 
        private event EventHandler<EventArgs> deletedHandler;
        private void CallDelete()
        {
            EventArgs e = new EventArgs();

            if (deletedHandler != null)
            {
                deletedHandler(this, e);
            }
        }

        public event EventHandler<EventArgs> Deleted
        {
            add
            {
                deletedHandler += value;
            }
            remove
            {
                deletedHandler -= value;
            }
        }

        public class ReoderEventArgs : EventArgs 
        {
            public int PreIndex { get; set; }
            public int CurrentIndex { get; set; }
        }

        //Need to add, "item,reordered": When the user reordered an item.
        private event EventHandler<ReoderEventArgs> reorderedHandler;

        internal void CallReordered(int preIdx, int currentIdx)
        {
            if (reorderedHandler != null)
            {
                ReoderEventArgs args = new ReoderEventArgs()
                {
                    PreIndex = preIdx,
                    CurrentIndex = currentIdx,
                };
                reorderedHandler(this, args);
            }
        }

        public event EventHandler<ReoderEventArgs> Reordered
        {
            add
            {
                reorderedHandler += value;
            }
            remove
            {
                reorderedHandler -= value;
            }
        }
    }
}