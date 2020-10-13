using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders;

namespace Screens
{
    public class LevelTransitionScreen : GameScreen
    {
        private const float k_ScalePulseForTitle = 2.25f;
        private const float k_PulsePerSecForTitle = 1f;
        private const float k_TimeOfPulseInSec = 2.5f;
        private readonly Background r_Background;
        private readonly int r_Level;
        private float m_TimeUntilNextLevelInSec = 3f;
        private TextService m_NextLevelTextService;
        private TextService m_TimeUntilNextLevelTextService;

        public LevelTransitionScreen(Game i_Game) : base(i_Game)
        {
            this.IsModal = true;
            r_Level = GameManager.CurrentLevel;
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
        }

        public override void Initialize()
        {
            base.Initialize();
            initText();
            initAnimations();
        }

        private void initText()
        {
            Vector2 nextLevelTextPosition = new Vector2(this.CenterOfViewPort.X - this.CenterOfViewPort.X / 4, this.CenterOfViewPort.Y);
            Vector2 timeUntilNextLevelTextPosition = new Vector2(this.CenterOfViewPort.X, this.CenterOfViewPort.Y + (this.CenterOfViewPort.Y / 4));
            string nextLevelText = "Level " + r_Level;

            m_NextLevelTextService = new TextService(nextLevelText, this, nextLevelTextPosition, Color.LightBlue);
            m_NextLevelTextService.Scales *= 2;
            m_TimeUntilNextLevelTextService = new TextService(m_TimeUntilNextLevelInSec.ToString(), this, timeUntilNextLevelTextPosition, Color.LightBlue);
            m_TimeUntilNextLevelTextService.Scales *= 2;
        }

        private void initAnimations()
        {
            PulseAnimator pulseAnimator = new PulseAnimator("PulseAnimator", TimeSpan.FromSeconds(k_TimeOfPulseInSec), k_ScalePulseForTitle, k_PulsePerSecForTitle);

            m_TimeUntilNextLevelTextService.PositionOrigin = m_NextLevelTextService.SourceRectangleCenter;
            m_TimeUntilNextLevelTextService.RotationOrigin = m_NextLevelTextService.SourceRectangleCenter;
            m_TimeUntilNextLevelTextService.Animations.Add(pulseAnimator);
            m_TimeUntilNextLevelTextService.Animations.Restart();
        }

        public override void Update(GameTime i_GameTime)
        {
            int lastTimeUntilNextLevel = (int) Math.Ceiling(m_TimeUntilNextLevelInSec);
            m_TimeUntilNextLevelInSec -= (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            int currentTimeUntilNextLevel = (int) Math.Ceiling(m_TimeUntilNextLevelInSec);

            if(m_TimeUntilNextLevelInSec <= 0)
            {
                this.ExitScreen();
            }

            if (lastTimeUntilNextLevel > currentTimeUntilNextLevel)
            {
                m_TimeUntilNextLevelTextService.TextToPrint = currentTimeUntilNextLevel.ToString();
            }

            base.Update(i_GameTime);
        }
    }
}