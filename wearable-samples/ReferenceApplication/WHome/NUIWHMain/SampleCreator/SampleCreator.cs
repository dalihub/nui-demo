using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using Tizen.NUI.Components;
using System.Linq;
using Tizen.NUI.Wearable;
using Popup = Tizen.NUI.Wearable.Popup;
using WearableSample;

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
            //creatorDictionary.Add("WearableList", this.GetWearableList);
            creatorDictionary.Add("WearablePopup", this.CreateWearablePopup);
            creatorDictionary.Add("Message", this.CreateMessageList);
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
                Position = new Position(0, 50),

                Size = new Size(190, 190),
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
                Position = new Position(0, -35),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                IsSelected = true,
            };
            view.Add(button1);

            var button2 = new CheckBox()
            {
                Size = new Size(100, 100),
                Position = new Position(0, 60),
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
                Position = new Position(0, -40),
                PositionUsesPivotPoint = true,
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                IsSelected = true,
            };
            view.Add(button1);

            var button2 = new RadioButton()
            {
                Size = new Size(100, 100),
                Position = new Position(0, 40),
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
        private CircularPagination pagination;
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
            pagination = new CircularPagination()
            {
                Size = new Size(360, 360),
                Position = new Position(0, 100),

                // Set Pagination properties, such as Indicator size, count, and images.
                IndicatorSize = new Size(10, 10),
                IndicatorImageURL = new Selector<string>()
                {
                    Normal = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "normal_dot.png",
                    Selected = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "focus_dot.png",
                },
                //IndicatorSpacing = 8,
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


        class ListData
        {
            public string Text { get; set; } = "";
            public ListItem.ItemType Type { get; set; } = ListItem.ItemType.Normal;
        }

        class ListItem : RecycleItem
        {
            public enum ItemType
            {
                Normal,
                Header,
            };

            public override void OnFocusLost()
            {
                BackgroundColor = Color.Transparent;
            }

            public override void OnFocusGained()
            {
                BackgroundColor = Type == ItemType.Normal ? new Color("#333333A3") : Color.Transparent;
            }

            public ListItem()
            {
                Size = new Size(360, 180);
                CornerRadius = 45;

                Text = new TextLabel()
                {
                    HeightSpecification = LayoutParamPolicies.MatchParent,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextColor = Color.White,
                    PixelSize = 32,
                };
                Add(Text);

                Header = new TextLabel()
                {
                    HeightSpecification = LayoutParamPolicies.MatchParent,
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextColor = Color.Green,
                    PixelSize = 32,
                };
                Add(Header);
            }

            public TextLabel Text { get; set; }
            public TextLabel Header { get; set; }
            public ItemType Type { get; set; } = ItemType.Normal;
        }

        class ListAdapter : RecycleAdapter
        {
            public override RecycleItem CreateRecycleItem()
            {
                ListItem item = new ListItem();
                return item;
            }

            public override void BindData(RecycleItem item)
            {
                ListItem target = item as ListItem;
                ListData data = Data[target.DataIndex] as ListData;

                if (data.Type == ListItem.ItemType.Header)
                {
                    target.Text.Hide();
                    target.Header.Show();
                    target.Header.Text = data.Text;
                    target.Type = ListItem.ItemType.Header;
                }
                else
                {
                    target.Header.Hide();
                    target.Text.Show();
                    target.Text.Text = data.Text;
                    target.BackgroundColor = Color.Transparent;
                    target.Type = ListItem.ItemType.Normal;
                }
            }
        }

        public View GetWearableList()
        {
            List<object> data = new List<object>();

            data.Add(new ListData()
            {
                Text = "Wearable List",
                Type = ListItem.ItemType.Header,
            });

            for (int i = 0; i < 30; i++)
            {
                data.Add(new ListData()
                {
                    Text = "LIST ITEM [" + i + "]",
                });
            }

            ListAdapter adapter = new ListAdapter();
            adapter.Data = data;

            WearableList wearableList = new WearableList(adapter);
            wearableList.Size = new Size(360, 360);
            wearableList.SetFocus(1, false);
            return wearableList;
        }

        private Popup myPopup1;
        const string tag = "NUITEST";
        private string resourcePath;
        private const int BUTTON_WIDTH = 73;
        private const int BUTTON_HEIGHT = 121;
        private Color BUTTON_COLOR = new Color(11.0f / 255.0f, 6.0f / 255.0f, 92.0f / 255.0f, 1);
        private const int BUTTON_ICON_WIDTH = 50;
        private const int BUTTON_ICON_HEIGHT = 50;

        private TextLabel myContent1;

        public void PopupPost()
        {

            resourcePath = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
            //two buttons popup, type1
            ButtonStyle buttonStyle = new ButtonStyle()
            {
                Icon = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>()
                    {
                        All = resourcePath + "images/tw_ic_popup_btn_bg.png",
                    },
                    Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
                    Color = new Selector<Color>()
                    {
                        All = BUTTON_COLOR,
                    },
                },
                Overlay = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>()
                    {
                        All = resourcePath + "images/tw_ic_popup_btn_check.png",
                    },
                    Size = new Size(BUTTON_ICON_WIDTH, BUTTON_ICON_HEIGHT),
                    Color = new Selector<Color>()
                    {
                        All = Color.White,
                    },
                },
            };
            Button leftButton = new Button(buttonStyle)
            {
                Name = "LeftButton",
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
            };

            myPopup1 = new Popup();
            myPopup1.AppendButton("LeftButton", leftButton);

            buttonStyle.Overlay.ResourceUrl = new Selector<string>()
            {
                All = resourcePath + "images/tw_ic_popup_btn_delete.png",
            };
            Button rightButton = new Button(buttonStyle)
            {
                Name = "RightButton",
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
            };
            myPopup1.AppendButton("RightButton", rightButton);

            TextLabel t = myPopup1.GetTitle();
            t.Text = "User consent";

            myContent1 = new TextLabel();
            myContent1.Text = "Agree? \n GPS location \n and use of your \n location data \n are controlled \n by the applications you \n \n \n this is additional text!";
            myContent1.MultiLine = true;
            myContent1.Size = new Size(200, 800);
            myContent1.PointSize = 6;
            myContent1.HorizontalAlignment = HorizontalAlignment.Center;
            myContent1.VerticalAlignment = VerticalAlignment.Top;
            myContent1.TextColor = Color.White;
            myContent1.PositionUsesPivotPoint = true;
            myContent1.ParentOrigin = ParentOrigin.Center;
            myContent1.PivotPoint = PivotPoint.Center;
            myPopup1.AppendContent("ContentText", myContent1);
            leftButton.ClickEvent += LeftButton_ClickEvent;
            rightButton.ClickEvent += RightButton_ClickEvent;
            myPopup1.OutsideClicked += Mp_OutsideClicked;

            myPopup1.ContentContainer.WidthResizePolicy = ResizePolicyType.FitToChildren;
            myPopup1.ContentContainer.HeightResizePolicy = ResizePolicyType.FitToChildren;

            myPopup1.Post(Window.Instance);
            //myPopup1.AfterDissmising += MyPopup1_AfterDissmising;
        }

        public View CreateWearablePopup()
        {
            var button = new Button()
            {
                Text = "Popup",
                Size = new Size(210, 72),

                // Positioning it to the bottom
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                Position = new Position(0, -20)
            };
            button.ClickEvent += Button_ClickEvent;
            return button;

        }


        private void RightButton_ClickEvent(object sender, Button.ClickEventArgs e)
        {
            myPopup1.Dismiss();
        }

        private void LeftButton_ClickEvent(object sender, Button.ClickEventArgs e)
        {
            myPopup1.Dismiss();
        }

        private void Mp_OutsideClicked(object sender, EventArgs e)
        {
            var popup = sender as Popup;
            if (popup != null)
            {
                myPopup1.Dismiss();
            }
        }

        private void Button_ClickEvent(object sender, Button.ClickEventArgs e)
        {
            PopupPost();
        }

        private GaussianBlurView contentBlurView;
        private MessageList message;
        private MenuList menuPopup;
        private MoreOption optionButton;
        private Animation popupAnimation;

        private MessageListAdaptor Adapter;

        public View CreateMessageList()
        {
            Adapter = new MessageListAdaptor() { Data = MessageDummy.Create(100) };
            //Adapter.FlickAnimationSpeed = 0.7f;
            //Adapter.FlickDistanceMultiplierRange = new Vector2(0.2f, 1.3f);
            //Adapter.FlickThreshold = 0.1f;

            popupAnimation = new Animation(100);

            Layer root = NUIApplication.GetDefaultWindow().GetDefaultLayer();

            contentBlurView = new GaussianBlurView(40, 3.0f, PixelFormat.RGBA8888, 1.0f, 1.0f, false)
            {
                Size = new Size(360, 360),
            };

            message = new MessageList(Adapter)
            {
                Size = new Size(360, 360),
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
            };
            contentBlurView.Add(message);

            MenuListAdapter menuListAdapter = new MenuListAdapter()
            {
                Data = new List<object>{
                    new MenuData(){Title = "Write", ResourceUrl = "send_message.png"},
                    new MenuData(){Title = "Delete", ResourceUrl = "delete.png"},
                    new MenuData(){Title = "Settings", ResourceUrl = "settings.png"}
                }
            };
            menuPopup = new MenuList(menuListAdapter)
            {
                Size = new Size(360, 360),
            };
            menuPopup.Hide();

            menuPopup.TouchEvent += (object source, View.TouchEventArgs args) =>
            {
                if (args.Touch.GetState(0) == PointStateType.Finished)
                {
                    HidePopup();
                }
                return true;
            };

            //contentBlurView.Add(menuPopup);

            optionButton = new MoreOption()
            {
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
            };
            optionButton.TouchEvent += (object sender, View.TouchEventArgs args) =>
            {
                MoreOption target = args.Touch.GetHitView(0) as MoreOption;

                if (target)
                {
                    if (args.Touch.GetState(0) == PointStateType.Down && args.Touch.GetLocalPosition(0).X > 50)
                    {
                        optionButton.ShowTouchEffect();
                    }
                    else if (args.Touch.GetState(0) == PointStateType.Finished)
                    {
                        if (optionButton.IsPressed && args.Touch.GetLocalPosition(0).X > 50)
                        {
                            ShowPopup();
                        }
                        optionButton.HideTouchEffect();
                    }
                    else if (args.Touch.GetState(0) == PointStateType.Interrupted)
                    {
                        optionButton.HideTouchEffect();
                    }
                }

                return true;
            };

            contentBlurView.Add(optionButton);

            return contentBlurView;
        }

        public void ShowPopup()
        {
            popupAnimation.Stop();
            popupAnimation.Clear();

            optionButton.Hide();
            contentBlurView.Activate();

            popupAnimation.Duration = 200;

            AlphaFunction timeCurve = new AlphaFunction(new Vector2(0.45f, 0.03f), new Vector2(0.41f, 1.0f));
            popupAnimation.AnimateTo(message, "scale", new Vector3(0.8f, 0.8f, 1.0f), 0, 200, timeCurve);
            popupAnimation.Play();

            Window.Instance.Add(menuPopup);
            menuPopup.Show();
        }

        public void HidePopup()
        {
            popupAnimation.Stop();
            popupAnimation.Clear();

            optionButton.Show();
            contentBlurView.Deactivate();

            popupAnimation.Duration = 200;

            AlphaFunction timeCurve = new AlphaFunction(new Vector2(0.45f, 0.03f), new Vector2(0.41f, 1.0f));
            popupAnimation.AnimateTo(message, "scale", new Vector3(1.0f, 1.0f, 1.0f), 0, 200, timeCurve);
            popupAnimation.Play();

            menuPopup.Unparent();
            menuPopup.Hide();
        }

    }
}
