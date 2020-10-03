﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Microsoft.Xna.Framework;
using SpaceInvaders;

namespace Infrastructure.ObjectModel.Screens
{
    public class MenuItem : TextService
    {
        private const float k_ScalePulseForItem = 1.1f;
        private const float k_PulsePerSecForItem = 1f;
        private readonly Color r_ActiveColor = Color.Blue;
        private readonly Color r_InactiveColor = Color.LightBlue;
        protected readonly string r_MenuText;
        public event EventHandler ItemIsClicked;

        public MenuItem(string i_MenuText, GameScreen i_GameScreen, Vector2 i_Position)
            : base(i_MenuText, i_GameScreen, i_Position)
        {
            r_MenuText = i_MenuText;
            this.TintColor = r_InactiveColor;
        }

        public MenuItem(string i_MenuText, GameScreen i_GameScreen)
            : this(i_MenuText, i_GameScreen, Vector2.Zero)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            initAnimations();
        }

        private void initAnimations()
        {
            PulseAnimator pulseAnimator = new PulseAnimator("PulseAnimator", TimeSpan.Zero, k_ScalePulseForItem, k_PulsePerSecForItem);
            this.Animations.Add(pulseAnimator);
        }

        public virtual void ItemWasClick()
        {
            if(ItemIsClicked != null)
            {
                ItemIsClicked.Invoke(this, EventArgs.Empty);
            }
        }

        public void ItemIsSelect()
        {
            this.Animations.Restart();
            this.TintColor = r_ActiveColor;
        }

        public void ItemIsInactive()
        {
            this.Animations.Pause();
            this.TintColor = r_InactiveColor;
        }
    }
}