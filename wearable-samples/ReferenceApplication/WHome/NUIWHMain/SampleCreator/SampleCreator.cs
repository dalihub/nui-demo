using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using Tizen.NUI.Components;
using System.Linq;

namespace NUIWHMain
{
    public class SampleCreator
    {
        public delegate View CreatorFunc();
        
        private Dictionary<string, CreatorFunc> creatorDictionary = new Dictionary<string, CreatorFunc>();
        public SampleCreator()
        {
            //Add("Title name", Function)
            creatorDictionary.Add("OverlayButton", this.CreateOverlayButton);
            creatorDictionary.Add("WearableButton", this.CreateWearableButton);
            creatorDictionary.Add("CheckBox", this.CreateCheckbox);
            creatorDictionary.Add("RadioButton", this.CreateRadioButton);
            creatorDictionary.Add("Switch", this.CreateSwitch);
            creatorDictionary.Add("Pagination", this.CreatePagination);
        }

        public int GetDictCount()
        {
            return creatorDictionary.Count;
        }

        public string GetDictString(int idx)
        {
            return creatorDictionary.Keys.ToList()[idx];
        }

        public View CallCreatorFunction(string key)
        {
            return creatorDictionary[key]();
        }

        public View CreateOverlayButton()
        {
            var button = new Button(new OverlayAnimationButtonStyle())
            {
                Text = "Hello World!",
                IconURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/icons/icon.png",
                IconPadding = new Extents(0, 0, 50, 0),

                Size = new Size(200, 200),
                CornerRadius = 100,

                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true
            };
            return button;
        }
        public View CreateWearableButton()
        {
            var button = new Button()
            {
                Text = "Hello World!",
                Size = new Size(210, 72),

                // Positioning it to the bottom
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                Position = new Position(0, -20)
            };
            return button;
        }

        public View CreateCheckbox()
        {
            View view = new View()
            {
                Size = new Size(360, 360),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
            };
            var button1 = new CheckBox()
            {
                Size = new Size(100, 100),
                Position = new Position(0, -50),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                IsSelected = true,
            };
            view.Add(button1);

            var button2 = new CheckBox()
            {
                Size = new Size(100, 100),
                Position = new Position(0, 50),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
            };
            view.Add(button2);

            var group = new CheckBoxGroup();
            group.Add(button1);
            group.Add(button2);

            return view;
        }

        public View CreateRadioButton()
        {

            View view = new View()
            {
                Size = new Size(360, 360),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
            };
            var button1 = new RadioButton()
            {
                Size = new Size(100, 100),
                Position = new Position(0, -50),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                IsSelected = true,
            };
            view.Add(button1);

            var button2 = new RadioButton()
            {
                Size = new Size(100, 100),
                Position = new Position(0, 50),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
            };
            view.Add(button2);

            var group = new RadioButtonGroup();
            group.Add(button1);
            group.Add(button2);
            return view;
        }

        public View CreateSwitch()
        {
            var button = new Switch()
            {
                Size = new Size(200, 200),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
            };

            return button;
        }

        public View CreateToast()
        {
            var text = new TextLabel()
            {
                Text = "Click to Post!",
                TextColor = Color.White,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                PositionUsesPivotPoint = true
            };
            text.TouchEvent += Text_TouchEvent;
            return text;
        }

        private bool Text_TouchEvent(object source, View.TouchEventArgs e)
        { 
            if(e.Touch.GetState(0) == PointStateType.Up)
            {

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
                
                var animationOnPost = new Animation(200);
                animationOnPost.AnimateTo(contentView, "Opacity", 0.8f);
                animationOnPost.AnimateTo(contentView, "Scale", new Vector3(1, 1, 1));

                var animationOnDismiss = new Animation(200);
                animationOnDismiss.AnimateTo(contentView, "Opacity", 0);
                animationOnDismiss.AnimateTo(contentView, "Scale", new Vector3(0, 0, 0));

                new Notification(contentView)
                    .SetAnimationOnPost(animationOnPost)            // (Optional) Set an animation to be played when post.
                    .SetAnimationOnDismiss(animationOnDismiss)      // (Optional) Set an animation to be played when dismiss.
                    .SetPositionSize(new Rectangle(90, 100, 180, 60)) // (Optional) Set notification window boundary.
                    .Post(2000); // Post for 2 seconds
            }

            return true;
        }
        private Pagination pagination;
        private ScrollableBase scrollable;

        private readonly int PAGE_COUNT = 6;

        public View CreatePagination()
        {
            View view = new View()
            {
                Size = new Size(360, 360),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
            };
            pagination = new Pagination()
            {
                Size = new Size(118, 10),
                Position = new Position(0, 60),

                // Set Pagination properties, such as Indicator size, count, and images.
                IndicatorSize = new Size(10, 10),
                IndicatorImageURL = new Selector<string>()
                {
                    Normal = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "normal_dot.png",
                    Selected = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "focus_dot.png",
                },
                IndicatorSpacing = 8,
                IndicatorCount = PAGE_COUNT,
                SelectedIndex = 0,

                // Positioning it to the top center
                ParentOrigin = ParentOrigin.TopCenter,
                PivotPoint = PivotPoint.TopCenter,
                PositionUsesPivotPoint = true
            };


            // To move on to the next page using Swipe gesture, add ScrollableBase and its View container.
            scrollable = new ScrollableBase()
            {
                Size = new Size(120, 120),
                Position = new Position(0, 10),
                ScrollingDirection = ScrollableBase.Direction.Horizontal,
                SnapToPage = true,

                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true
            };

            View container = new View()
            {
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = 120,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    SetPositionByLayout = false,
                },
                //BackgroundColor = Color.Black,
            };
            scrollable.Add(container);

            Tizen.Log.Error("MYLOG", "add scroll");
            for (int i = 0; i < PAGE_COUNT; i++)
            {
                View page = new View()
                {
                    Size = new Size(120, 120),
                    CornerRadius = 60.0f,
                    BackgroundColor = (i%2==0)?Color.Yellow:Color.Red,
                };

                container.Add(page);
            }
            view.Add(scrollable);
            view.Add(pagination);
            
            // Screen Touch Event
            scrollable.ScrollAnimationEndEvent += Scroll_AnimationEnd;
            return view;
        }

        void Scroll_AnimationEnd(object source, ScrollableBase.ScrollEventArgs e)
        {
            int index = scrollable.CurrentPage;

            if (index >= 0 && index < pagination.IndicatorCount)
            {
                pagination.SelectedIndex = index;
            }
        }

    }
}
