/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
using System.Runtime.InteropServices;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;

namespace CustomControlTest
{

    // A custom control for star rating (draggable to change the rating)
    class StarRating : CustomView
    {
        private const string resources = "/home/owner/apps_rw/NUISamples.TizenTV/res";
        private FlexContainer _container;
        private ImageView[] _images;
        private Vector3 _gestureDisplacement;
        private int _currentValue;
        private int _myRating;
        private bool _myDragEnabled;

        // Called by DALi Builder if it finds a StarRating control in a JSON file
        static CustomView CreateInstance()
        {
          return new StarRating();
        }

        // static constructor registers the control type (only runs once)
        static StarRating()
        {
          // ViewRegistry registers control type with DALi type registery
          // also uses introspection to find any properties that need to be registered with type registry
          CustomViewRegistry.Instance.Register(CreateInstance, typeof(StarRating) );
        }

        public StarRating() : base(typeof(StarRating).Name, CustomViewBehaviour.ViewBehaviourDefault)
        {
        }

        public override void OnInitialize()
        {
            // Create a container for the star images
            _container = new FlexContainer();

            _container.FlexDirection = FlexContainer.FlexDirectionType.Row;
            _container.WidthResizePolicy = ResizePolicyType.FillToParent;
            _container.HeightResizePolicy = ResizePolicyType.FillToParent;

            this.Add(_container);

            // Create the images
            _images = new ImageView[5];

            for(int i = 0; i < 5; i++)
            {
                _images[i] = new ImageView(resources+"/images/star-dim.png");
                _container.Add( _images[i] );
            }

            // Update the images according to the rating (dimmed star by default)
            _myRating = 0;
            UpdateStartImages(_myRating);

            // Enable pan gesture detection
            EnableGestureDetection(Gesture.GestureType.Pan);
            _myDragEnabled = true; // Allow dragging by default (can be disabled)
        }

        // Pan gesture handling
        public override void OnPan(PanGesture gesture)
        {System.Console.WriteLine("OnPan: ");
            // Only handle pan gesture if dragging is allowed
            if(_myDragEnabled)
            {
                switch (gesture.State)
                {
                    case Gesture.StateType.Started:
                    {
                        _gestureDisplacement = new Vector3(0.0f, 0.0f, 0.0f);
                        _currentValue = 0;
                        break;
                    }
                    case Gesture.StateType.Continuing:
                    {
                        // Calculate the rating according to pan desture displacement
                        _gestureDisplacement.X += gesture.Displacement.X;
                        int delta = (int)Math.Ceiling(_gestureDisplacement.X / 40.0f);
                        _currentValue = _myRating + delta;

                        // Clamp the rating
                        if(_currentValue < 0) _currentValue = 0;
                        if(_currentValue > 5) _currentValue = 5;

                        // Update the images according to the rating
                        UpdateStartImages(_currentValue);
                        break;
                    }
                    default:
                    {
                        _myRating = _currentValue;
                        break;
                    }
                }
            }
        }

	public override void OnStageConnection(int depth)
	{
            System.Console.WriteLine("StarRating::OnStageConnection: " + depth);
	}

	public override void OnStageDisconnection()
	{
            System.Console.WriteLine("StarRating::OnStageDisconnection");
	}

	public override void OnSceneConnection(int depth)
	{
            System.Console.WriteLine("StarRating::OnSceneConnection: " + depth);
	}

	public override void OnSceneDisconnection()
	{
            System.Console.WriteLine("StarRating::OnSceneDisconnection");
	}

        // Update the images according to the rating
        private void UpdateStartImages(int rating)
        {
            for(int i = 0; i < rating; i++)
            {
                _images[i].WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                _images[i].HeightResizePolicy = ResizePolicyType.UseNaturalSize;
                _images[i].SetImage(resources+"/images/star-highlight.png");
            }

            for(int i = rating; i < 5; i++)
            {
                _images[i].WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                _images[i].HeightResizePolicy = ResizePolicyType.UseNaturalSize;
                _images[i].SetImage(resources+"/images/star-dim.png");
            }
        }

        // Rating property of type int:
        public int Rating
        {
            get
            {
                return _myRating;
            }
            set
            {
                _myRating = value;
                UpdateStartImages(_myRating);
            }
        }

        // DragEnabled property of type bool:
        public bool DragEnabled
        {
            get
            {
                return _myDragEnabled;
            }
            set
            {
                _myDragEnabled = value;
            }
        }
    }

    class HelloWorldExample : NUIApplication
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void CallbackDelegate();
        private const string resources = "/home/owner/apps_rw/NUISamples.TizenTV/res";

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        public void Initialize()
        {
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

            // Create a container to layout the rows of image and rating vertically
            FlexContainer container = new FlexContainer();

            container.ParentOrigin = ParentOrigin.TopLeft;
            container.PivotPoint = PivotPoint.TopLeft;
            container.FlexDirection = (int)FlexContainer.FlexDirectionType.Column;
            container.WidthResizePolicy = ResizePolicyType.FillToParent;
            container.HeightResizePolicy = ResizePolicyType.FillToParent;

            window.Add(container);

            Random random = new Random();

            for(int i = 0; i < 6; i++) // 6 rows in total
            {
                // Create a container to layout the image and rating (in each row) horizontally
                FlexContainer imageRow = new FlexContainer();
                imageRow.ParentOrigin = ParentOrigin.TopLeft;
                imageRow.PivotPoint = PivotPoint.TopLeft;
                imageRow.FlexDirection = FlexContainer.FlexDirectionType.Row;
                imageRow.Flex = 1.0f;
                container.Add(imageRow);

                // Add the image view to the row
                ImageView image = new ImageView(resources+"/images/gallery-" + i + ".jpg");
                image.Size2D = new Size2D(120, 120);
                image.WidthResizePolicy = ResizePolicyType.Fixed;
                image.HeightResizePolicy = ResizePolicyType.Fixed;
                image.AlignSelf = (int)FlexContainer.Alignment.AlignCenter;
                image.Flex = 0.3f;
                image.FlexMargin = new Vector4(10.0f, 0.0f, 0.0f, 0.0f);
                imageRow.Add(image);

                // Create a rating control
                StarRating view = new StarRating();

                // Add the rating control to the row
                view.ParentOrigin = ParentOrigin.Center;
                view.PivotPoint = PivotPoint.Center;
                view.Size2D = new Size2D(200, 40);
                view.Flex = 0.7f;
                view.AlignSelf = (int)FlexContainer.Alignment.AlignCenter;
                view.FlexMargin = new Vector4(30.0f, 0.0f, 0.0f, 0.0f);
                imageRow.Add(view);

                // Set the initial rating randomly between 1 and 5
                view.Rating = random.Next(1, 6);

                view.HoverEvent += OnHover;
                view.WheelEvent += OnWheel;
            }

            Hover hover = new Hover();
            var count = hover.GetPointCount();
            if (count == 0)
            {
                System.Console.WriteLine("Hover point: PASSED!!!");
            };

            Wheel wheel = new Wheel();
            var point = wheel.Point;
            if (point.X == 0.0f && point.Y == 0.0f )
            {
                System.Console.WriteLine("Wheel point: PASSED!!!");
            };

            Key key = new Key();
            var keyCode = key.KeyCode;
            if (keyCode == 0 )
            {
                System.Console.WriteLine("Key code: PASSED!!!");
            };

            window.KeyEvent += OnKey;
        }

        private bool OnHover(object source, View.HoverEventArgs e)
        {
          System.Console.WriteLine("OnHover: count: " + e.Hover.GetPointCount());
          return true;
        }

        private bool OnWheel(object source, View.WheelEventArgs e)
        {
          System.Console.WriteLine("OnWheel: point: " + e.Wheel.Point.X + ", " + e.Wheel.Point.Y);
          return true;
        }

        private void OnKey(object source, Window.KeyEventArgs e)
        {
          System.Console.WriteLine("OnKey: pressed: " + e.Key.KeyPressed + ", code: " + e.Key.KeyCode);
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor (typeof(MyCSharpExample.StarRating).TypeHandle);

            HelloWorldExample example = new HelloWorldExample();
            example.Run(args);
        }
    }
}
