
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
        private RotaryDeleteBadege deleteBadge;

        public void AddDeleteIcon(int idx)
        {
            if (deleteBadge == null)
            {
                deleteBadge = new RotaryDeleteBadege();
            }
            //if(idx % Common.ApplicationConstants.MAX_ITEM_COUNT <= 4)
            {
                deleteBadge.SetRightSide();
            }
            //else
            {
                // deleteBadge.SetLeftSide();
            }
            deleteBadge.TouchEvent += DeleteBadge_TouchEvent;
            this.Add(deleteBadge);
        }


        public void RemoveDeleteIcon()
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
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                Touch_DeleteBadgeHandler(this, e);
            }
            return true;
        }
        public delegate void ItemSelectedHandler(RotarySelectorItem item);
        public event ItemSelectedHandler OnItemSelected;

        internal void SelectedItem()
        {
            Tizen.Log.Error("MYLOG", "Selected Item");
            OnItemSelected(this);
            CallSelect();
        }

        internal void ClickedItem()
        {
            Tizen.Log.Error("MYLOG", "ClickedItem");
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
                Tizen.Log.Error("MYLOG", "clicked");
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


        //Need to add, "item,reordered": When the user reordered an item.
        private event EventHandler<EventArgs> reorderedHandler;

        internal void CallReordered()
        {
            if (reorderedHandler != null)
            {
                
                reorderedHandler(this, new EventArgs());
            }
        }

        public event EventHandler<EventArgs> Reordered
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