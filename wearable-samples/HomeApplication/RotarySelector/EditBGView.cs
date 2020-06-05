using NUIWHome.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIWHome
{
    public class EditBGView : ImageView
    {
        private View bgView;
        private ImageView editEfView;
        private ImageView editRightSelectView;
        private ImageView editLeftSelectView;

        private int currentPage;
        private int lastPage;

        private bool isAleadyScaled = false;

        public delegate void PageMoveDelegate(bool isRight);
        public PageMoveDelegate OnMovePageEditMode;

        private Timer checkingEnterTimer;

        public EditBGView(int currentPage, int lastPage)
        {
            bgView = new View();
            bgView.HeightResizePolicy = ResizePolicyType.FillToParent;
            bgView.WidthResizePolicy = ResizePolicyType.FillToParent;
            bgView.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            bgView.PivotPoint = Tizen.NUI.PivotPoint.Center;
            bgView.PositionUsesPivotPoint = true;
            bgView.BackgroundColor = Color.Black;
            bgView.Scale = new Vector3(0.9f, 0.9f, 1.0f);
            bgView.CornerRadius = 180.0f;
            this.Add(bgView);

            //this.BackgroundColor = Color.Black;
            this.currentPage = currentPage;
            this.lastPage = lastPage;

            int screen_size = ApplicationConstants.SCREEN_SIZE_RADIUS * 2;
            Size = new Size(screen_size, screen_size);

            editEfView = new ImageView();
            editEfView.BackgroundImage = CommonResource.GetResourcePath() + "/b_home_screen_edit_mode_ef.png";
            editEfView.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            editEfView.PivotPoint = Tizen.NUI.PivotPoint.Center;
            editEfView.PositionUsesPivotPoint = true;
            this.Add(editEfView);

            editRightSelectView = new ImageView();
            editRightSelectView.Color = new Color(0.1f, 0.5f, 0.85f, 1.0f);
            editRightSelectView.BackgroundImage = CommonResource.GetResourcePath() + "/b_home_screen_edit_mode_ef_r.png";
            editRightSelectView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
            editRightSelectView.PivotPoint = Tizen.NUI.PivotPoint.Center;
            editRightSelectView.PositionUsesPivotPoint = true;
            editRightSelectView.Position = new Position(20, 0);
            editRightSelectView.TouchEvent += EditRightSelectView_TouchEvent;

            this.Add(editRightSelectView);

            editLeftSelectView = new ImageView();
            editLeftSelectView.Color = new Color(0.1f, 0.5f, 0.85f, 1.0f);
            editLeftSelectView.BackgroundImage = CommonResource.GetResourcePath() + "/b_home_screen_edit_mode_ef_l.png";
            editLeftSelectView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            editLeftSelectView.PivotPoint = Tizen.NUI.PivotPoint.Center;
            editLeftSelectView.PositionUsesPivotPoint = true;
            editLeftSelectView.Position = new Position(-20, 0);
            editLeftSelectView.TouchEvent += EditLeftSelectView_TouchEvent;
            this.Add(editLeftSelectView);

            Animation ani = new Animation(333);
            ani.AnimateBy(editRightSelectView, "Position", new Position(-20.0f, 0.0f), new AlphaFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 1.0f)));
            ani.AnimateBy(editLeftSelectView, "Position", new Position(20.0f, 0.0f), new AlphaFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 1.0f)));
            ani.Play();

            CheckNextPrevPage(currentPage, lastPage);


            checkingEnterTimer = new Timer(500);
            checkingEnterTimer.Tick += Timer_Tick;
        }

        public void CheckNextPrevPage(int currentPage, int lastPage)
        {
            Tizen.Log.Error("MYLOG", "c : " + currentPage + ", l:" + lastPage);
            if (currentPage == 0)
            {
                editRightSelectView.Hide();
            }
            else
            {
                editRightSelectView.Show();
            }

            if (currentPage == lastPage - 1)
            {
                editLeftSelectView.Hide();
            }
            else
            {
                editLeftSelectView.Show();
            }
            this.currentPage = currentPage;
            this.lastPage = lastPage;
        }

        public void AnimateBG(bool isRight)
        {
            //editRightSelectView.Hide();
            //editLeftSelectView.Hide();

            Animation ani = new Animation(370);
            ani.AnimateTo(this, "Position", new Position(isRight ? 15 : -15, 0), 0, 200, new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.2f, 1.0f)));
            ani.AnimateTo(this, "Opacity", 0.0f, 0, 200, new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.2f, 1.0f)));

            ani.AnimateTo(this, "Position", new Position(isRight ? -20 : 20, 0), 200, 201, new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.2f, 1.0f)));
            ani.AnimateTo(this, "Position", new Position(isRight ? 0 : 0, 0), 201, 370, new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.2f, 1.0f)));
            ani.AnimateTo(this, "Opacity", 1.0f, 202, 370, new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.2f, 1.0f)));

            ani.Play();
            ani.Finished += Ani_Finished;
        }

        internal void AnimateRightCue()
        {
            if (!isAleadyScaled)
            {
                AlphaFunction alpha = new AlphaFunction(new Vector2(0.26f, 0.46f), new Vector2(0.45f, 1.0f));

                Animation ani = new Animation(333);
                ani.AnimateTo(this.editRightSelectView, "Position", new Position(-03, 0), alpha);
                ani.Play();
                isAleadyScaled = true;


                isRight = false;
                checkingEnterTimer.Start();
            }
        }

        bool isRight = true;

        internal void AnimateLeftCue()
        {
            if (!isAleadyScaled)
            {
                AlphaFunction alpha = new AlphaFunction(new Vector2(0.26f, 0.46f), new Vector2(0.45f, 1.0f));

                Animation ani = new Animation(333);
                ani.AnimateTo(this.editLeftSelectView, "Position", new Position(10, 0), alpha);
                ani.Play();
                isAleadyScaled = true;

                isRight = true;
                checkingEnterTimer.Start();
            }
        }

        internal void AnimateReturn()
        {
            if (!isAleadyScaled)
            {
                return;
            }

            AlphaFunction alpha = new AlphaFunction(new Vector2(0.26f, 0.46f), new Vector2(0.45f, 1.0f));
            //if (this.editRightSelectView.Visibility)
            {
                Animation ani = new Animation(333);
                ani.AnimateTo(this.editLeftSelectView, "Position", new Position(0, 0), alpha);
                ani.AnimateTo(this.editRightSelectView, "Position", new Position(0, 0), alpha);
                ani.Play();

                checkingEnterTimer.Stop();
            }


            isAleadyScaled = false;
        }

        private bool Timer_Tick(object source, Timer.TickEventArgs e)
        {
            AnimateReturn();
            OnMovePageEditMode(isRight);
            return false;
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            CheckNextPrevPage(currentPage, lastPage);
        }

        private bool EditRightSelectView_TouchEvent(object source, TouchEventArgs e)
        {
            return false;
        }

        private bool EditLeftSelectView_TouchEvent(object source, TouchEventArgs e)
        {
            return false;
        }

        public void DisposeChild()
        {
            editLeftSelectView.TouchEvent -= EditRightSelectView_TouchEvent;
            editRightSelectView.TouchEvent -= EditRightSelectView_TouchEvent;

            editRightSelectView.Unparent();
            editLeftSelectView.Unparent();

            editRightSelectView.Dispose();
            editLeftSelectView.Dispose();

            editRightSelectView = null;
            editLeftSelectView = null;
        }


    }
}
