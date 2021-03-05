/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
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
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;
using Tizen.NUI.Components;

class MultipleWindowExample : NUIApplication
{
    private FocusManager _focusManager;

    class TextButton
    {
        public TextButton(int id, string text, string textToggled, Vector4 stopColor1, Vector4 stopColor2)
        {
            _id = id;
            _text = text;
            _textToggled = textToggled;
            _stopColor1 = stopColor1;
            _stopColor2 = stopColor2;
            _button = null;
            _window = null;
        }

        public int _id;
        public TextLabel _button;
        public Window _window;
        public string _text;
        public string _textToggled;
        public Vector4 _stopColor1;
        public Vector4 _stopColor2;
    };

    private TextButton[] _textButton = new TextButton[]
    {
        new TextButton(1, "Create Window", "Delete Window", new Vector4(0.31f, 0.31f, 0.83f, 1.0f), new Vector4(0.57f, 0.91f, 0.89f, 1.0f)),
        new TextButton(2, "Resize / Move", "Resize / Move", new Vector4(0.41f, 0.31f, 0.64f, 1.0f), new Vector4(0.92f, 0.69f, 0.78f, 1.0f)),
        new TextButton(3, "Raise Window", "Lower Window", new Vector4(0.43f, 0.38f, 0.15f, 1.0f), new Vector4(0.83f, 0.81f, 0.72f, 1.0f)),
        new TextButton(4, "Show Window", "Hide Window", new Vector4(0.12f, 0.25f, 0.22f, 1.0f), new Vector4(0.61f, 0.95f, 0.78f, 1.0f))
    };

    private GradientVisual CreateGradientVisual( Vector4 stopColor1, Vector4 stopColor2 )
    {
        PropertyArray stopColor = new PropertyArray();
        stopColor.Add(new PropertyValue(stopColor1));
        stopColor.Add(new PropertyValue(stopColor2));

        // Create gradient visual that can be set as a background
        GradientVisual gradientVisualMap = new GradientVisual()
        {
            StopColor = stopColor,
            StartPosition = new Vector2(-0.5f, -0.5f),
            EndPosition = new Vector2(0.5f, 0.5f),
            PositionPolicy = VisualTransformPolicyType.Relative,
            SizePolicy = VisualTransformPolicyType.Relative,
        };

        return gradientVisualMap;
    }

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        // Get the main window instance, change background color & respond to key events
        Window mainWindow = Window.Instance;
        mainWindow.BackgroundColor = Color.Red;
        mainWindow.KeyEvent += OnKeyEvent;

        var layout = new LinearLayout()
        {
            LinearOrientation = LinearLayout.Orientation.Horizontal,
            Padding = new Extents(30, 0, 30, 0),
            CellPadding = new Size2D(30, 0),
        };

        View menuView = new View()
        {
            Layout = layout,
            WidthSpecification = LayoutParamPolicies.MatchParent,
            HeightSpecification = LayoutParamPolicies.MatchParent,
            BackgroundColor = new Color(0.61f, 0.59f, 0.64f, 1.0f),
        };

        mainWindow.Add(menuView);

        for (int i = 0; i < _textButton.Length; i++)
        {
            PropertyMap shadow = new PropertyMap();
            shadow.Add("offset", new PropertyValue(new Vector2(2.0f, 2.0f)));
            shadow.Add("color", new PropertyValue(Color.Black));
            shadow.Add("blurRadius", new PropertyValue(5.0f));

            // Create a simple TextLabel
            _textButton[i]._button = new TextLabel(_textButton[i]._text)
            {
                Name = "" + _textButton[i]._id,
                WidthSpecification = 285,
                HeightSpecification = 130,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Focusable = true,
                Background = CreateGradientVisual(_textButton[i]._stopColor1, _textButton[i]._stopColor2).OutputVisualMap,
                TextColor = new Color(0.61f, 0.59f, 0.64f, 1.0f),
                PointSize = 16,
                Shadow = shadow,
            };

            // Add the text to the window
            menuView.Add(_textButton[i]._button);

            _textButton[i]._button.TouchEvent += OnButtonTouched;
        }

        _focusManager = FocusManager.Instance;
        _focusManager.PreFocusChange += OnPreFocusChange;
        _focusManager.FocusChanged += OnFocusChanged;
        _focusManager.FocusedViewActivated += OnFocusedViewEnterKey;

        _focusManager.SetAsFocusGroup(menuView, true);
        _focusManager.FocusIndicator = new View();
    }

    /// <summary>
    /// Called when any key event is received.
    /// Will use this to exit the application if the Back or Escape key is pressed
    /// </summary>
    private void OnKeyEvent( object sender, Window.KeyEventArgs eventArgs )
    {
        if( eventArgs.Key.State == Key.StateType.Down )
        {
            switch( eventArgs.Key.KeyPressedName )
            {
                case "Escape":
                case "Back":
                {
                    Exit();
                }
                break;
            }
        }
    }

    private View OnPreFocusChange(object source, FocusManager.PreFocusChangeEventArgs e)
    {
        if (e.CurrentView != null && !e.ProposedView)
        {
            int index = Int32.Parse(e.CurrentView.Name);
            if (e.Direction == View.FocusDirection.Left)
            {
                index--;
            }
            if (e.Direction == View.FocusDirection.Right)
            {
                index++;
            }

            if (index < 1) index = _textButton.Length;
            if (index > _textButton.Length) index = 1;

            e.ProposedView = _textButton[index - 1]._button;
        }

        if (!e.ProposedView && !e.CurrentView)
        {
            e.ProposedView = _textButton[0]._button;
        }

        return e.ProposedView;
    }

    private void OnFocusChanged(object source, FocusManager.FocusChangedEventArgs e)
    {
        Console.WriteLine("Focus Changed");

        Animation scaleAnimation = new Animation();
        scaleAnimation.Duration = 300;
        if (e.CurrentView)
        {
            TextLabel currentFocusedButton = (TextLabel)(e.CurrentView);
            scaleAnimation.AnimateTo(currentFocusedButton, "Scale", new Size(1.0f, 1.0f, 1.0f));
            scaleAnimation.AnimateTo(currentFocusedButton, "TextColor", new Color(0.61f, 0.59f, 0.64f, 1.0f));
        }
        if (e.NextView)
        {
            TextLabel nextFocusedButton = (TextLabel)(e.NextView);
            scaleAnimation.AnimateTo(nextFocusedButton, "Scale", new Size(1.12f, 1.12f, 1.12f));
            scaleAnimation.AnimateTo(nextFocusedButton, "TextColor", Color.White );
        }
        scaleAnimation.Play();
    }

    private void OnFocusedViewEnterKey(object source, FocusManager.FocusedViewActivatedEventArgs e)
    {
      int index = Int32.Parse(e.View.Name) - 1;
      switch (index)
      {
          case 0:
              _focusManager.SetCurrentFocusView(_textButton[0]._button);
              CreateDeleteWindowTest();
              break;
          case 1:
              _focusManager.SetCurrentFocusView(_textButton[1]._button);
              ResizeMoveWindowTest();
              break;
          case 2:
              _focusManager.SetCurrentFocusView(_textButton[2]._button);
              RaiseLowerWindowTest();
              break;
          case 3:
              _focusManager.SetCurrentFocusView(_textButton[3]._button);
              ShowHideWindowTest();
              break;
          default:
              break;
      }
    }

    public bool OnButtonTouched(object sender, View.TouchEventArgs e)
    {
        if (e.Touch.GetState(0) == PointStateType.Up)
        {
            _focusManager.SetCurrentFocusView((View)sender);

            if (sender == _textButton[0]._button)
            {
                CreateDeleteWindowTest();
            }
            else if (sender == _textButton[1]._button)
            {
                ResizeMoveWindowTest();
            }
            else if (sender == _textButton[2]._button)
            {
                RaiseLowerWindowTest();
            }
            else if (sender == _textButton[3]._button)
            {
                ShowHideWindowTest();
            }

            return true;
        }

        return false;
    }

    public void CreateDeleteWindowTest()
    {
        if (!_textButton[0]._window)
        {
            _textButton[0]._window = new Window(new Rectangle(0, 0, 720, 1280))
            {
                BackgroundColor = Color.Cyan,
                Title = "new window 1",
            };

            _textButton[0]._button.Text = _textButton[0]._textToggled;

            var layout = new FlexLayout()
            {
                Direction = FlexLayout.FlexDirection.Column,
                Justification = FlexLayout.FlexJustification.SpaceAround,
                ItemsAlignment = FlexLayout.AlignmentType.Center,
            };

            var view = new ImageView("./res/images/background-1.jpg")
            {
                Layout = layout,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            _textButton[0]._window.Add(view);

            // Create a simple TextLabel
            TextLabel title = new TextLabel("Click the same button to\ndelete this window")
            {
                WidthSpecification =  LayoutParamPolicies.MatchParent,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextColor = Color.Cyan,
                MultiLine = true,
            };

            // Add the text to the window
            view.Add(title);

            TextEditor textEditor = new TextEditor()
            {
                BackgroundColor = Color.Cyan,
                Text = " Input your text ",
                PointSize = 20,
                TextColor = Color.Black,
                LineWrapMode = LineWrapMode.Character,
            };

            view.Add(textEditor);
        }
        else
        {
            _textButton[0]._window.Destroy();
            _textButton[0]._button.Text = _textButton[0]._text;
        }
    }

    public void ResizeMoveWindowTest()
    {
      if (!_textButton[1]._window)
      {
          _textButton[1]._window = new Window(new Rectangle(0, 0, 720, 1280))
          {
              BackgroundColor = Color.Cyan,
              Title = "new window 2",
          };
          _textButton[1]._button.Text = _textButton[1]._text;

          var layout = new FlexLayout()
          {
              Direction = FlexLayout.FlexDirection.Column,
              Justification = FlexLayout.FlexJustification.SpaceAround,
              ItemsAlignment = FlexLayout.AlignmentType.Center,
          };

          var view = new ImageView("./res/images/background-2.jpg")
          {
              Layout = layout,
              WidthSpecification = LayoutParamPolicies.MatchParent,
              HeightSpecification = LayoutParamPolicies.MatchParent,
          };

          _textButton[1]._window.Add(view);

          Button resizeButton = new Button()
          {
              Name = "resize-button",
              Text = "Click me to resize this window",
          };

          resizeButton.Clicked += (sender, e) =>
          {
              Size2D windowSize = _textButton[1]._window.WindowSize;
              if (windowSize.Width > windowSize.Height)
              {
                  windowSize.Width /= 2;
              }
              else
              {
                  windowSize.Width *= 2;
              }
              _textButton[1]._window.WindowSize = windowSize;
          };

          view.Add(resizeButton);

          Button moveButton = new Button()
          {
              Name = "move-button",
              Text = "Click me to move this window right",
          };

          moveButton.Clicked += (sender, e) =>
          {
              Position2D windowPosition = _textButton[1]._window.WindowPosition;
              windowPosition.X += 100;
              _textButton[1]._window.WindowPosition = windowPosition;
          };

            view.Add(moveButton);
        }
        else
        {
            _textButton[1]._window.Raise();
        }
    }

    public void RaiseLowerWindowTest()
    {
        if (!_textButton[2]._window)
        {
            _textButton[2]._window = new Window(new Rectangle(0, 0, 720, 1280))
            {
                BackgroundColor = Color.Cyan,
                Title = "new window 3",
            };
            _textButton[2]._button.Text = _textButton[2]._textToggled;

            var layout = new GridLayout()
            {
                Columns = 2,
            };

            var view = new ImageView("./res/images/background-3.jpg")
            {
                Layout = layout,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            _textButton[2]._window.Add(view);

            for (int i = 1; i < 9; i++)
            {
                var image = new ImageView("./res/images/gallery-" + i + ".jpg");
                image.Margin = new Extents(20, 20, 20, 20);
                view.Add(image);
            }
        }
        else
        {
            if (_textButton[2]._button.Text == _textButton[2]._text)
            {
                _textButton[2]._window.Raise();
                _textButton[2]._button.Text = _textButton[2]._textToggled;
            }
            else
            {
                _textButton[2]._window.Lower();
                _textButton[2]._button.Text = _textButton[2]._text;
            }
        }
    }

    public void ShowHideWindowTest()
    {
        if (!_textButton[3]._window)
        {
            _textButton[3]._window = new Window(new Rectangle(0, 0, 720, 1280))
            {
                BackgroundColor = Color.Cyan,
                Title = "new window 4",
            };
            _textButton[3]._button.Text = _textButton[3]._textToggled;

            var layout = new FlexLayout()
            {
                Direction = FlexLayout.FlexDirection.Column,
                Justification = FlexLayout.FlexJustification.Center,
                ItemsAlignment = FlexLayout.AlignmentType.Center,
            };

            var view = new ImageView("./res/images/background-4.jpg")
            {
                Layout = layout,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            _textButton[3]._window.Add(view);

            var image = new ImageView("./res/images/gallery-5.jpg");
            view.Add(image);

            Animation rotateAnimation = new Animation()
            {
                Duration = 4000,
                Looping = true,
                EndAction = Animation.EndActions.Discard,
            };
            rotateAnimation.AnimateTo(image, "Orientation", new Rotation(new Radian(new Degree(180.0f)), PositionAxis.Z), 0, 2000);
            rotateAnimation.AnimateTo(image, "Orientation", new Rotation(new Radian(new Degree(360.0f)), PositionAxis.Z), 2000, 4000);
            rotateAnimation.Play();
        }
        else
        {
            if (_textButton[3]._button.Text == _textButton[3]._text)
            {
                _textButton[3]._window.Show();
                _textButton[3]._button.Text = _textButton[3]._textToggled;
            }
            else
            {
                _textButton[3]._window.Hide();
                _textButton[3]._button.Text = _textButton[3]._text;
            }
        }
    }

    public MultipleWindowExample(Size2D windowSize, Position2D windowPosition) : base(windowSize, windowPosition)
    {
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        MultipleWindowExample example = new MultipleWindowExample(new Size2D(1290, 200), new Position2D(0, 0));
        example.Run(args);
    }
}

