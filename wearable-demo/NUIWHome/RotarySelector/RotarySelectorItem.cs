
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
        public string MainText {get; set;}

        /// <summary>
        /// Get/Set Sub Text
        /// </summary>
        public string SubText {get; set;}

        /// <summary>
        /// Get/Set Color of MainText
        /// </summary>
        public Color MainTextColor{get; set; }
        /// <summary>
        /// Get/Set Color of SubText
        /// </summary>
        public Color subTextColor{get; set;}

        internal int CurrentIndex { get; set; }

        private bool isSelected = false;

        /// <summary>
        /// Creates a new instance of a RotarySelectorItem.
        /// </summary>
        public RotarySelectorItem()
        {
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;
        }

        public delegate void ItemSelectedHandler(RotarySelectorItem item);
        public event ItemSelectedHandler OnItemSelected;

        internal void SelectItem()
        {
            if (!isSelected)
            {
                isSelected = true;
                OnItemSelected(this);
                CallSelect();
            }
            else
            {
                //Clicked
                CallClick();
            }
        }

        internal void UnSelectItem()
        {
            isSelected = false;
        }

        private View currentParent { get; set; }

        internal void SetCurrentParent(View parent)
        {
            currentParent = parent;
        }

        internal void AddToParent()
        {
            if(!currentParent.Children.Contains(this))
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


        //Need to add, "item,reordered": When the user reordered an item.
        private event EventHandler<EventArgs> reorderedHandler;

        private void CallReordered()
        {
            EventArgs e = new EventArgs();

            if (reorderedHandler != null)
            {
                reorderedHandler(this, e);
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