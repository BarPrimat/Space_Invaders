using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;
using Microsoft.Xna.Framework.Graphics;


namespace Screens
{
    public class PlayScreen : GameScreen
    {
        private readonly GameManager r_GameManager;
        private readonly PauseScreen r_PauseScreenScreen;
        private TextService m_PauseText;

        public PlayScreen(Game i_Game, int i_Level) : base(i_Game)
        {
            r_GameManager = new GameManager(this, SettingsCollection.NumberOfPlayers, i_Level);
            r_PauseScreenScreen = new PauseScreen(i_Game);
            this.BlendState = BlendState.NonPremultiplied;
        }

        public override void Initialize()
        {
            base.Initialize();
            initText();
        }

        private void initText()
        {
            int xOffset = (int)(this.CenterOfViewPort.X / 5);
            int yOffset = (int)(this.CenterOfViewPort.X / 1.5f);
            Vector2 position = new Vector2(this.CenterOfViewPort.X - xOffset, this.CenterOfViewPort.Y - yOffset);

            m_PauseText = new TextService(SpritesDefinition.TextFont, this, position, Color.LightBlue);
            m_PauseText.TextToPrint = "P - To Resume Game";
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(InputManager.KeyPressed(Keys.P))
            {
                this.ScreensManager.SetCurrentScreen(this.r_PauseScreenScreen);
            }
        }
    }
}
