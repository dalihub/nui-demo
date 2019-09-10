using System;
using System.IO;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Silk
{
    /// <summary>
    /// Demo showing layouting in an silky application scenario.
    /// </summary>
    class Demo : NUIApplication
    {
        private enum Action
        {
            ScrollBackward,
            ScrollForward,
            ShowContent,
            ShowList
        }

        private const int CategoryStackHeight = 240;
        private const float ItemImageRatioToFrameHeight = 0.40f;

        private const float ItemImageRatioToFrameWidth = 0.60f;
        private const string resources = "./res/";
        private const int TitlePointSize = 30;

        private const float AxisLockThreshold = 0.36f;

        // Width of the scrolling frame, defaults to window size.
        private int FrameWidth;
        // Height of the scrolling frame, defaults to window size.
        private int FrameHeight;

        // Ratio of Width of title TextLabels compared to FrameWidth.
        private int TitleWidthToFrameWidthRatio = 2;

        // Ratio of of thumb nail to frame size
        private int ThumbnailSizeRatio = 8;

        private float RatioToScreenDimensionToCompleteScroll = 0.6f;

        // Container for categories. Transitions between vertical and horizontal
        private View categoryStack;

        // Category titles that transition between vertical and horizontal
        private View titleStack;

        // Area of the product, corresponds to the category
        private View contentArea;

        // List of Views, one for each category.
        private List<View> categoryList;

        // List of the catergory titles
        private List<string> titleList = new List<string>{ "Cakes", "Fruit", "Vegetables", "Bread" };

        // List of a list of Views to ne used for the content
        private List<List<View>> contentStore;
        private ScrollingContainer contentScrollingContainer;

        private ScrollingContainer categoryScrollingContainer;

        private int currentView;

        private bool listView;

        private bool Animating;

        private bool AxisYLock;

        private float PanGestureDisplacementY;

        private float PanGestureDisplacementX;

        Animation scrollAnimation;

        private TapGestureDetector tapGestureDetector;

        private PanGestureDetector panGestureDetector;

        // Helper functions
        public static ImageView CreateImageViewFromUrl(String url, Size2D size)
        {
            ImageVisual imageVisual = new ImageVisual()
            {
                URL = url,
            };

            ImageView imageView = new ImageView()
            {
                Name = "ImageView" + url,
                WidthSpecification = size.Height, // Force to be a square
                HeightSpecification = size.Height,
                Image = imageVisual.OutputVisualMap,
            };
            return imageView;
        }

        // end of Helper functions

        // Thumbnail beside a review box.
        //
        // | ------- ||---------------------
        // | [image] ||Review         Now |
        // |         ||    * * * *        |
        // | ------- ||-------------------|
        public static View CreateReviewEntry(string thumbnailFilename, int width, Size2D thumbnailSize )
        {
            LinearLayout linearLayout = new LinearLayout();

            View entry = new View()
            {
                Name = "Entry",
                //Layout = linearLayout,
                Padding = new Extents(10,10,10,10),
                WidthSpecification = width,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            View thumbnail = CreateImageViewFromUrl(thumbnailFilename, thumbnailSize);
            thumbnail.Name = "Thumbnail";
            thumbnail.Margin = new Extents(10,10,10,10);

            entry.Add(thumbnail);

            //  ---------------------
            //  |Review         Now |
            //  |    * * * *        |
            //  |-------------------|
            FlexLayout flexLayout = new FlexLayout();
            flexLayout.Direction = FlexLayout.FlexDirection.Row;
            flexLayout.Justification = FlexLayout.FlexJustification.SpaceBetween;
            View entryReview = new View()
            {
                Name = "EntryReview",
                Layout = flexLayout,
                Weight = 0.7f,
                Padding = new Extents(20,20,10,10),
            };
            TextLabel reviewText = new TextLabel("Review")
            {
                PointSize = 18,
            };
            TextLabel timeText = new TextLabel("Just now")
            {
                Margin = new Extents(0,0,10,0),
                PointSize = 12,
                Color = new Vector4(0.8f,0.8f,0.8f,.9f),
            };
            entryReview.Add(reviewText);
            entryReview.Add(timeText);

            entry.Add(entryReview);
            return entry;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            InitializeMainScreen();
            currentView = 0;
            Animating = false;
        }

        private void SetupCategoryItems()
        {
            categoryList = new List<View>();

            View itemA = new View()
            {
                Name = "ItemA",
                Background = CreateGradientVisual(new Vector4(127.0f/255.0f, 95.0f/255.0f, 215.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = FrameWidth,
            };

            View itemB = new View()
            {
                Name = "ItemB",
                Background = CreateGradientVisual(new Vector4(192.0f/255.0f, 68.0f/255.0f, 127.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = FrameWidth
            };
            View itemC = new View()
            {
                Name = "ItemC",
                Background = CreateGradientVisual(new Vector4(239.0f/255.0f, 188.0f/255.0f, 66.0f/255.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = FrameWidth
            };

            View itemD = new View()
            {
                Name = "ItemD",
                Background = CreateGradientVisual(new Vector4(1.0f, 0.0f, 0.0f, 1.0f)).OutputVisualMap,
                WidthSpecification = FrameWidth
            };

            categoryList.Add(itemA);
            categoryList.Add(itemB);
            categoryList.Add(itemC);
            categoryList.Add(itemD);
        }

        private void PopulateContentStore()
        {
            contentStore = new List<List<View>>();
            Size2D thumbnailSize = new Size2D((FrameWidth/ThumbnailSizeRatio), (FrameHeight/ThumbnailSizeRatio));
            Size2D contentImageSize = new Size2D((int)Math.Ceiling(FrameWidth*ItemImageRatioToFrameWidth),
                                                 (int)Math.Ceiling(FrameHeight*ItemImageRatioToFrameHeight));

            foreach (string category in titleList)
            {
                List<View> categoryContent = new List<View>();
                string []files = Directory.GetFiles(resources + "content/" + category, "*.*", SearchOption.AllDirectories);
                foreach ( string file in files)
                {
                    Console.WriteLine(" :{0}", file);
                    ImageView imageView = CreateImageViewFromUrl(file, contentImageSize);
                    categoryContent.Add(imageView);
                }

                for(var i =0; i<=1; i++)
                {
                    categoryContent.Add(CreateReviewEntry(files[0], contentImageSize.Width, thumbnailSize));
                }

                contentStore.Add(categoryContent);

            }
        }

        private void SetCatergoryStackTransition(View stack, AlphaFunction.BuiltinFunctions alphaFunction, int delay, int duration)
        {
            TransitionComponents shrinkAndGrow = new TransitionComponents();
            shrinkAndGrow.AlphaFunction = new AlphaFunction(alphaFunction);
            shrinkAndGrow.Delay = delay;
            shrinkAndGrow.Duration = duration;

            stack.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                           AnimatableProperties.Position,
                                                           0.0,
                                                           shrinkAndGrow );

            stack.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                           AnimatableProperties.Size,
                                                           0.0f,
                                                           shrinkAndGrow );
        }

        private void InitializeCategoryStack()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            categoryStack = new View()
            {
                Name = "CategoryStack",
                Layout = layout,
                WidthSpecification = FrameWidth,
                HeightSpecification = FrameHeight,
            };
            listView = true;

            SetCatergoryStackTransition(categoryStack, AlphaFunction.BuiltinFunctions.EaseIn, 0, 480);

            foreach( View category in categoryList)
            {
                categoryStack.Add(category);
                category.Weight = 1.0f;
            }
        }

        private void InitializeTitleStack()
        {
            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            layout.CellPadding = new Size2D(400,0);

            titleStack = new View()
            {
                Layout = layout,
                WidthSpecification = FrameWidth*titleList.Count,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Name = "TitleStack",
            };

            TransitionComponents easeIn = new TransitionComponents();
            easeIn.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn);
            easeIn.Delay = 0;
            easeIn.Duration = 480;

            titleStack.LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                                AnimatableProperties.Position,
                                                                0.0,
                                                                easeIn );

            foreach( string name in titleList)
            {
                TextLabel label = new TextLabel(name)
                {
                    WidthSpecification = FrameWidth/TitleWidthToFrameWidthRatio,
                    HeightSpecification = FrameHeight/titleList.Count,
                    PointSize = TitlePointSize,
                    TextColor = Color.White,
                    Shadow = new PropertyMap().Add("color", new PropertyValue(new Vector4(0.7f,0.7f,0.7f,1.0f))).
                                               Add("offset", new PropertyValue(new Vector2(4.0f,4.0f))),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                titleStack.Add(label);
            }
        }

        private void InitializeContent()
        {
            var layout = new LinearLayout();
            contentArea = new View()
            {
                Layout = layout,
                Name = "ContentArea",
                WidthSpecification = FrameWidth * categoryList.Count,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Padding = new Extents(1,1,20,10),
            };

            PopulateContentStore();

            foreach( List<View> subList in contentStore )
            {
                LinearLayout contentItemLayout = new LinearLayout();
                contentItemLayout.LinearOrientation = LinearLayout.Orientation.Vertical;;
                contentItemLayout.LinearAlignment =  LinearLayout.Alignment.Center;
                View contentCategory = new View()
                {
                    Layout = contentItemLayout,
                    WidthSpecification = FrameWidth,
                    HeightSpecification = LayoutParamPolicies.WrapContent,
                    Name = "ContentCategory",
                };

                foreach (View item in subList)
                {
                    contentCategory.Add(item);
                }

                contentArea.Add(contentCategory);
            }

        }

        private void InitializeScrollingContainer(ref ScrollingContainer scrollingContainer, string viewName )
        {
            TransitionComponents easeIn = new TransitionComponents();
            easeIn.AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInSquare);
            easeIn.Delay = 0;
            easeIn.Duration = 256;

            scrollingContainer = new ScrollingContainer()
            {
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                PageWidth = FrameWidth,
                Name = viewName,
                LayoutTransition = new LayoutTransition( TransitionCondition.LayoutChanged,
                                                         AnimatableProperties.Position,
                                                         0.0,
                                                         easeIn ),
                BackgroundColor = Color.Green,
            };
        }

        private void InitializeMainScreen()
        {
            FrameWidth = Window.Instance.WindowSize.Width;
            FrameHeight = Window.Instance.WindowSize.Height;

            var layout = new LinearLayout();
            layout.LinearOrientation = LinearLayout.Orientation.Vertical;
            View mainScreen = new View()
            {
                Layout = layout,
                Name = "MainScreen",
                WidthSpecification = FrameWidth,//LayoutParamPolicies.MatchParent,
                HeightSpecification = FrameHeight,//LayoutParamPolicies.MatchParent,
                Background = CreateGradientVisual(new Vector4(0.0f, 0.0f, 1.0f, 1.0f)).OutputVisualMap,
            };

            Window.Instance.Add(mainScreen);
            Window.Instance.KeyEvent += WindowKeyEvent;

            SetupCategoryItems();
            InitializeCategoryStack();
            InitializeScrollingContainer(ref categoryScrollingContainer, "CategoryScrollingContainer" );
            InitializeTitleStack();
            InitializeContent();
            InitializeScrollingContainer(ref contentScrollingContainer, "ContentScrollingContainer");

            categoryScrollingContainer.Add(categoryStack);
            contentScrollingContainer.Add(contentArea);
            Window.Instance.Add(contentScrollingContainer);
            contentScrollingContainer.PositionY = FrameHeight;
            Window.Instance.Add(categoryScrollingContainer);
            Window.Instance.Add(titleStack);

            scrollAnimation = new Animation();

            tapGestureDetector = new TapGestureDetector();
            tapGestureDetector.Attach(mainScreen);
            tapGestureDetector.Detected += OnTapGestureDetected;

            panGestureDetector = new PanGestureDetector();
            panGestureDetector.Attach(mainScreen);
            panGestureDetector.Detected += OnPanGestureDetected;
        }

        private void AnimationFinished(object sender, EventArgs e)
        {
            //Animating = false;
        }

        private void EventAction(Action action)
        {
            Animation animationController = Window.Instance.LayoutController.GetCoreAnimation();
            switch(action)
            {
                case Action.ScrollBackward:
                {
                    Console.WriteLine("Action ScrollBackward listView{0} currentView{1}", listView, currentView);
                    if (!listView && currentView <3 /*&& !Animating*/)
                    {
                        // Set animation for scrolling, different to changing layouts
                        SetCatergoryStackTransition(categoryStack, AlphaFunction.BuiltinFunctions.EaseInSquare, 0, 256);

                        categoryScrollingContainer.ScrollBackward();
                        contentScrollingContainer.ScrollBackward();
                        animationController.EndAction = Animation.EndActions.StopFinal;

                        currentView++;

                        // scrollAnimation.AnimateTo(titleStack, "PositionX", titleStack.PositionX - ((FrameWidth/TitleWidthToFrameWidthRatio)+400),
                        // 0,
                        // 256,
                        // new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInSquare) );

                    }
                }
                break;

                case Action.ScrollForward:
                {
                    Console.WriteLine("Action ScrollForward listView{0} currentView{1}", listView, currentView);
                    if (!listView && currentView > 0 /*&& !Animating*/)
                    {
                        // Set animation for scrolling, different to changing layouts
                        SetCatergoryStackTransition(categoryStack, AlphaFunction.BuiltinFunctions.EaseInSquare, 0, 256);

                        categoryScrollingContainer.ScrollForward();
                        contentScrollingContainer.ScrollForward();
                        animationController.EndAction = Animation.EndActions.StopFinal;

                        currentView--;

                        // scrollAnimation.AnimateTo(titleStack, "PositionX", titleStack.PositionX + ((FrameWidth/TitleWidthToFrameWidthRatio)+400),
                        // 0,
                        // 256,
                        // new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInSquare) );

                    }
                }
                break;

                case Action.ShowContent:
                {
                    Console.WriteLine("Action ShowContent listView{0}", listView);
                    if(listView)
                    {
                        // Use changing layout transition
                        SetCatergoryStackTransition(categoryStack, AlphaFunction.BuiltinFunctions.EaseIn, 0, 480);

                        // Get existing Linear layout and change orientation
                        LinearLayout layout = categoryStack.Layout as LinearLayout;
                        layout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                        categoryStack.HeightSpecification = CategoryStackHeight;
                        categoryStack.WidthSpecification = FrameWidth*categoryList.Count;

                        LinearLayout titlelayout = titleStack.Layout as LinearLayout;
                        titlelayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
                        titleStack.HeightSpecification = CategoryStackHeight;
                        titleStack.WidthSpecification = LayoutParamPolicies.WrapContent;

                        Animation otherAnimation = Window.Instance.LayoutController.GetCoreAnimation();

                        // otherAnimation.AnimateTo(titleStack, "PositionX", 120.0f,
                        // 0,
                        // 256,
                        // new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn) );

                        //titleStack.Margin = new Extents(0,0,0,0);

                        //Animate up the content area to meet category area.

                        // scrollAnimation.AnimateTo(contentScrollingContainer, "PositionY", CategoryStackHeight,
                        // 0,
                        // 256,
                        // new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseIn) );

                        currentView = 0;
                        //scrollAnimation.Finished += AnimationFinished;
                        listView = false;
                        Animating = true;
                    }
                }
                break;

                case Action.ShowList:
                {
                    Console.WriteLine("Action ShowList listView{0}", listView);
                    if (!listView)
                    {
                        // Use changing layout transition
                        SetCatergoryStackTransition(categoryStack, AlphaFunction.BuiltinFunctions.EaseIn, 0, 480);

                        // Get existing Linear layout and change orientation
                        LinearLayout layout = categoryStack.Layout as LinearLayout;
                        layout.LinearOrientation = LinearLayout.Orientation.Vertical;
                        categoryStack.HeightSpecification = LayoutParamPolicies.MatchParent;
                        categoryStack.WidthSpecification = LayoutParamPolicies.MatchParent;

                        LinearLayout titleLayout = titleStack.Layout as LinearLayout;
                        titleLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
                        titleStack.HeightSpecification = LayoutParamPolicies.MatchParent;
                        titleStack.WidthSpecification = LayoutParamPolicies.MatchParent;

                        Animation otherAnimation = Window.Instance.LayoutController.GetCoreAnimation();

                        if (!otherAnimation)
                        {
                            otherAnimation = new Animation();
                        }

                        // otherAnimation.AnimateTo(categoryStack, "PositionX", 0.0f,
                        // 0,
                        // 240,
                        // new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear) );

                        // otherAnimation.AnimateTo(titleStack, "PositionX", 0.0f,
                        // 0,
                        // 240,
                        // new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear) );

                        //titleStack.Margin = new Extents(60,0,0,0);
                        //Animate the content off the screen

                        otherAnimation.AnimateTo(contentScrollingContainer, "PositionY", FrameHeight,
                        0,
                        256,
                        new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut) );

                        currentView = 0;
                        //scrollAnimation.Finished += AnimationFinished;
                        listView = true;
                        //Animating = true;
                    }
                }
                break;
            }
            scrollAnimation.Play();
        }

        private void WindowKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                Animation animationController = Window.Instance.LayoutController.GetCoreAnimation();
                switch( e.Key.KeyPressedName )
                {
                    case "Left" :
                    {
                        EventAction(Action.ScrollForward);
                        animationController.Play();
                    }
                    break;
                    case "Right" :
                    {
                        EventAction(Action.ScrollBackward);
                        animationController.Play();
                    }
                    break;
                    case "Up" :
                    {
                        EventAction(Action.ShowContent);
                        animationController.Play();
                        //scrollAnimation.Play();
                    }
                    break;
                    case "Down" :
                    {
                        EventAction(Action.ShowList);
                        animationController.Play();
                        //scrollAnimation.Play();
                    }
                    break;
                }
            }
        }

        private void OnTapGestureDetected(object source, TapGestureDetector.DetectedEventArgs e)
        {
            if(listView)
            {
                EventAction(Action.ShowContent);
            }
        }

        private void OnPanGestureDetected(object source, PanGestureDetector.DetectedEventArgs e)
        {
            Animation animationController = Window.Instance.LayoutController.GetCoreAnimation();
            Console.WriteLine("OnPanGestureDetected animation null:{0}", animationController == null );
            switch(e.PanGesture.State)
            {
                case Gesture.StateType.Finished :
                {
                    if (AxisYLock)
                    {
                        Console.WriteLine("panned:{0} animationProgress:{1} scroll:{2} contentPos:{3} size:{4},{5}", PanGestureDisplacementX, animationController.CurrentProgress,
                                           scrollAnimation.CurrentProgress, contentScrollingContainer.CurrentPosition.X, contentScrollingContainer.Size2D.Width, contentScrollingContainer.Size2D.Height);

                        if (animationController.CurrentProgress > RatioToScreenDimensionToCompleteScroll)
                        {
                            // Panned enough to allow auto completion of animation.
                            scrollAnimation.SpeedFactor = 1;
                            scrollAnimation.EndAction = Animation.EndActions.StopFinal;
                            //scrollAnimation.Play();

                            animationController.SpeedFactor = 1;
                            animationController.EndAction = Animation.EndActions.StopFinal;
                            animationController.Play();
                        }
                        else
                        {
                            // Reverse animation as not panned enough to warrant completion.
                            scrollAnimation.SpeedFactor = -1;
                            scrollAnimation.EndAction = Animation.EndActions.Cancel;
                            //scrollAnimation.Play();

                            animationController.SpeedFactor = -1;
                            animationController.EndAction = Animation.EndActions.Cancel;
                            animationController.Play();
                        }
                        Window.Instance.LayoutController.OverrideCoreAnimation = false;
                        AxisYLock = false;
                    }
                    else
                    {
                        Console.WriteLine("panned:{0} animationProgress:{1} scroll:{2} contentPos:{3} size:{4},{5}", PanGestureDisplacementY, animationController.CurrentProgress,
                        animationController.CurrentProgress, contentScrollingContainer.CurrentPosition.Y, contentScrollingContainer.Size2D.Width, contentScrollingContainer.Size2D.Height);

                        if (e.PanGesture.Velocity.X > 0 )
                        {
                            Console.WriteLine("panned flick forward detected");
                            EventAction(Action.ScrollForward);
                        }
                        else if (e.PanGesture.Velocity.X < 0)
                        {
                            Console.WriteLine("panned flick back detected");
                            EventAction(Action.ScrollBackward);
                        }

                        if ( !listView && currentView<3 && currentView >0)
                        {
                            //scrollAnimation.Play();
                        }

                        if (animationController.CurrentProgress > RatioToScreenDimensionToCompleteScroll)
                        {
                            // Panned enough to allow auto completion of animation.
                            animationController.SpeedFactor = 1;
                            animationController.EndAction = Animation.EndActions.StopFinal;
                            animationController.Play();                        }
                        else
                        {
                            // Reverse animation as not panned enough to warrant completion.
                            animationController.SpeedFactor = -1;
                            animationController.EndAction = Animation.EndActions.Cancel;
                            animationController.Play();
                        }
                    }

                }
                break;
                case Gesture.StateType.Continuing :
                {
                    if (AxisYLock)
                    {
                        PanGestureDisplacementY += e.PanGesture.ScreenDisplacement.Y;
                        float progress = Math.Abs(PanGestureDisplacementY/(FrameHeight*RatioToScreenDimensionToCompleteScroll));
                        Console.WriteLine("panning:{0} progress{1} animationProgress:{2} scroll:{3} contentPos:{4} size:{5},{6}", PanGestureDisplacementY,progress, animationController.CurrentProgress,
                                           scrollAnimation.CurrentProgress, contentScrollingContainer.CurrentPosition.Y, contentScrollingContainer.Size2D.Width, contentScrollingContainer.Size2D.Height);
                        //scrollAnimation.CurrentProgress = progress;
                        animationController.CurrentProgress = progress;
                    }
                    else
                    {
                        PanGestureDisplacementX += e.PanGesture.ScreenDisplacement.X;
                        float progress = Math.Abs(PanGestureDisplacementX/(FrameWidth*RatioToScreenDimensionToCompleteScroll));
                        animationController.CurrentProgress = progress;
                        Console.WriteLine("panning:{0} progress{1} animationProgress:{2} scroll:{3} contentPos:{4} size:{5},{6}", PanGestureDisplacementX,progress, animationController.CurrentProgress,
                                           scrollAnimation.CurrentProgress, contentScrollingContainer.CurrentPosition.X, contentScrollingContainer.Size2D.Width, contentScrollingContainer.Size2D.Height);
                    }
                }
                break;
                case Gesture.StateType.Started :
                {
                    Console.WriteLine("Started Displacement.Y:{0} Displacement.X{1}", e.PanGesture.Displacement.Y, e.PanGesture.Displacement.X);
                    Window.Instance.LayoutController.OverrideCoreAnimation = true;

                    if ( Math.Abs(e.PanGesture.Displacement.Y) > Math.Abs(e.PanGesture.Displacement.X))
                    {
                        AxisYLock = true;

                        if( e.PanGesture.Displacement.Y < 0 )
                        {
                            Console.WriteLine("started: progress{0}", animationController.CurrentProgress);
                            //scrollAnimation.EndAction = Animation.EndActions.Discard;
                            EventAction(Action.ShowContent);
                            //scrollAnimation.Play();
                            //scrollAnimation.Pause();

                            animationController.EndAction = Animation.EndActions.Discard;
                            animationController.Play();
                            animationController.Pause();
                        }
                        else
                        {
                          if ( !listView)
                          {
                              Console.WriteLine("started: progress{0}", animationController.CurrentProgress);
                              //scrollAnimation.EndAction = Animation.EndActions.Discard;
                              EventAction(Action.ShowList);
                              //scrollAnimation.Play();
                              //scrollAnimation.Pause();

                              animationController.EndAction = Animation.EndActions.Discard;
                              animationController.Play();
                              animationController.Pause();
                          }
                        }
                        PanGestureDisplacementY = 0;

                    }
                    else
                    {
                        AxisYLock = false;
                        if( e.PanGesture.Displacement.X < 0 )
                        {
                            animationController.EndAction = Animation.EndActions.Discard;
                            EventAction(Action.ScrollBackward);
                        }
                        else
                        {
                            animationController.EndAction = Animation.EndActions.Discard;
                            EventAction(Action.ScrollForward);
                        }
                        animationController.Play();
                        animationController.Pause();
                        PanGestureDisplacementX = 0;
                    }
                }
                break;
            }
        }

        static private GradientVisual CreateGradientVisual(Vector4 targetcolor)
        {
            GradientVisual gradientVisualMap = new GradientVisual();
            PropertyArray stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(new Vector4(1.0f, 1.0f, 1.0f, 1.0f)));
            stopColor.Add(new PropertyValue(targetcolor));
            gradientVisualMap.StopColor = stopColor;
            gradientVisualMap.StartPosition = new Vector2(0.0f, 1.0f);
            gradientVisualMap.EndPosition = new Vector2(-1.0f, -0.5f);
            gradientVisualMap.PositionPolicy = VisualTransformPolicyType.Relative;
            gradientVisualMap.SizePolicy = VisualTransformPolicyType.Relative;
            return gradientVisualMap;
        }

        static void Main(string[] args)
        {
            // Do not remove this print out - helps with the TizenFX stub sync issue
            Console.WriteLine("Running Example...");
            Demo silkDemo = new Demo();
            silkDemo.Run(args);
        }
    } // Class Demo
} // namespace Silk