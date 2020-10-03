using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders;

namespace Screens
{
    public class LevelTransitionScreen : GameScreen
    {
        private readonly Background r_Background;
        private int m_Level;
        private float m_TimeUntilNextLevelInSec = 3f;
        private TextService m_NextLevelTextService;
        private TextService m_TimeUntilNextLevelTextService;

        public LevelTransitionScreen(Game i_Game) : base(i_Game)
        {
            m_Level = SettingsCollection.CurrentLevel;
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
        }

        public override void Initialize()
        {
            base.Initialize();
            initText();
        }

        private void initText()
        {
            Vector2 nextLevelTextPosition = new Vector2(this.CenterOfViewPort.X - this.CenterOfViewPort.X / 4, this.CenterOfViewPort.Y);
            Vector2 timeUntilNextLevelTextPosition = new Vector2(this.CenterOfViewPort.X, this.CenterOfViewPort.Y + (this.CenterOfViewPort.Y / 4));
            string nextLevelText = "Level " + m_Level;
            m_NextLevelTextService = new TextService(nextLevelText, this, nextLevelTextPosition, Color.LightBlue);
            m_NextLevelTextService.Scales *= 2;

            m_TimeUntilNextLevelTextService = new TextService(SpritesDefinition.TextFont, this, timeUntilNextLevelTextPosition, Color.LightBlue);
            m_TimeUntilNextLevelTextService.TextToPrint = m_TimeUntilNextLevelInSec.ToString();
            m_TimeUntilNextLevelTextService.Scales *= 2;
        }

        public override void Update(GameTime i_GameTime)
        {
            int lastTimeUntilNextLevel = (int) Math.Ceiling(m_TimeUntilNextLevelInSec);

            m_TimeUntilNextLevelInSec -= (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            int currentTimeUntilNextLevel = (int) Math.Ceiling(m_TimeUntilNextLevelInSec);
            if(m_TimeUntilNextLevelInSec <= 0)
            {
                this.ExitScreen();
                ScreensManager.SetCurrentScreen(new PlayScreen(this.Game, m_Level));
            }

            if (lastTimeUntilNextLevel > currentTimeUntilNextLevel)
            {
                m_TimeUntilNextLevelTextService.TextToPrint = currentTimeUntilNextLevel.ToString();
            }

            base.Update(i_GameTime);
        }
    }
}
