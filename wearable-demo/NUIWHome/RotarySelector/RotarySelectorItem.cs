
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
            OnItemSelected(this);
        }

        private View currentParent { get; set; }

        internal void SetCurrentParent(View parent)
        {
            currentParent = parent;
        }

        internal void AddToParent()
        {
            currentParent.Add(this);
        }
    }
}