/* Copyright (c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;
using Tizen.NUI.BaseComponents;
using System.ComponentModel;
using System.Diagnostics;
using Tizen.NUI;

namespace LayoutDemo
{
    /// <summary>
    /// [Draft] This class provides a View that can scroll a single View with a layout.
    /// </summary>
    internal class LayoutScroller : CustomView
    {
        static bool LayoutDebugScroller = true; // Debug flag
        private Animation scrollAnimation;
        private float maxScrollDistance;
        private PanGestureDetector mPanGestureDetector;

        private View mScrollingChild;

        private bool verticalScrolling = true;

        /// <summary>
        /// [Draft] Constructor
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public LayoutScroller() : base(typeof(VisualView).FullName, CustomViewBehaviour.ViewBehaviourDefault | CustomViewBehaviour.RequiresTouchEventsSupport)
        {
            mPanGestureDetector = new PanGestureDetector();
            mPanGestureDetector.Attach(this);
            mPanGestureDetector.Detected += OnPanGestureDetected;

            ClippingMode = ClippingModeType.ClipToBoundingBox;

            mScrollingChild = new View();
        }

        public void AddLayoutToScroll(View child)
        {
            mScrollingChild = child;
            Add(mScrollingChild);
        }


        /// <summary>
        /// Scroll vertically by displacement pixels in screen coordinates.
        /// </summary>
        /// <param name="displacement">distance to scroll in pixels. Y increases as scroll position approaches the top.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public float ScrollVerticallyBy(float displacement)
        {
            Debug.WriteLineIf( LayoutDebugScroller, "ScrollVerticallyBy displacement:" + displacement);
            return ScrollBy(displacement);
        }

        internal void StopScroll()
        {
            if (scrollAnimation != null && scrollAnimation.State == Animation.States.Playing)
            {
                scrollAnimation.Stop(Animation.EndActions.StopFinal);
                scrollAnimation.Clear();
            }
        }

        // static constructor registers the control type (for user can add kinds of visuals to it)
        static LayoutScroller()
        {
            // ViewRegistry registers control type with DALi type registry
            // also uses introspection to find any properties that need to be registered with type registry
            CustomViewRegistry.Instance.Register(CreateInstance, typeof(LayoutScroller));
        }

        internal static CustomView CreateInstance()
        {
            return new LayoutScroller();
        }

        public void OffsetChildVertically(float displacement)
        {
            if (scrollAnimation == null)
            {
                scrollAnimation = new Animation();
            }
            else if (scrollAnimation.State == Animation.States.Playing)
            {
                scrollAnimation.Stop(Animation.EndActions.StopFinal);
                scrollAnimation.Clear();
            }
            scrollAnimation.Duration = 500;
            scrollAnimation.DefaultAlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare);


            float targetPosition = mScrollingChild.PositionY + displacement;
            targetPosition = Math.Min(0,targetPosition);
            targetPosition = Math.Max(-maxScrollDistance,targetPosition);

            Debug.WriteLineIf( LayoutDebugScroller, "OffsetChildVertically targetYPos:" + targetPosition);

            scrollAnimation.AnimateTo(mScrollingChild, "PositionY", targetPosition);
            scrollAnimation.Play();
        }

        /// <summary>
        /// you can override it to clean-up your own resources.
        /// </summary>
        /// <param name="type">DisposeTypes</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                StopScroll();

                if (mPanGestureDetector != null)
                {
                    mPanGestureDetector.Detected -= OnPanGestureDetected;
                    mPanGestureDetector.Dispose();
                    mPanGestureDetector = null;
                }
            }
            base.Dispose(type);
        }

        private float ScrollBy(float displacement)
        {
            if (GetChildCount() == 0 || displacement == 0)
            {
                return 0;
            }

            maxScrollDistance = mScrollingChild.CurrentSize.Height -CurrentSize.Height;

            Debug.WriteLineIf( LayoutDebugScroller, "ScrollBy maxScrollDistance:" + maxScrollDistance +
                                                    " parent length:" + CurrentSize.Height +
                                                    " scrolling child length:" + mScrollingChild.CurrentSize.Height);

            float absDisplacement = Math.Abs(displacement);

            OffsetChildVertically(displacement);

            return absDisplacement;
        }

        private void OnPanGestureDetected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            if (e.PanGesture.State == Gesture.StateType.Started)
            {
                StopScroll();
            }
            else if (e.PanGesture.State == Gesture.StateType.Continuing)
            {
                {
                    ScrollVerticallyBy(e.PanGesture.Displacement.Y);
                }
            }
            else if (e.PanGesture.State == Gesture.StateType.Finished)
            {
                //if (mLayout.CanScrollVertically())
                {
                    ScrollVerticallyBy(e.PanGesture.Velocity.Y * 600);
                }
            }
        }

    }

} // namespace
