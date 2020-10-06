using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Screens.MainMenuScreens;
using SpaceInvaders;

namespace Screens
{
    public class GameOverScreen : GameScreen
    {
        private readonly Background r_Background;
        private TextService m_TextService;
        public event Action UserWantNewGame;
        public event Action UserWantLeaveGame;

        public GameOverScreen(Game i_Game, List<TextService> i_PlayerScoreTextService) : base(i_Game)
        {
            this.IsModal = true;
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
            foreach (TextService playerScore in i_PlayerScoreTextService)
            {
                this.Add(playerScore);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            string textToPrint = string.Format(@"
press Esc to exit
press Home for a new game
press M for main menu");
            Vector2 position = new Vector2(this.CenterOfViewPort.X - (this.CenterOfViewPort.X / 2), this.CenterOfViewPort.Y);

            m_TextService = new TextService(textToPrint, this, position, GameDefinitions.TextColor);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(InputManager.KeyPressed(Keys.Escape))
            {
                this.Game.Exit();
            }
            else if(InputManager.KeyPressed(Keys.Home))
            {
                this.ExitScreen();
                UserWantNewGame?.Invoke();
            }
            else if(InputManager.KeyPressed(Keys.M))
            {
                UserWantLeaveGame?.Invoke();
                this.ExitScreen();
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }
    }
}
