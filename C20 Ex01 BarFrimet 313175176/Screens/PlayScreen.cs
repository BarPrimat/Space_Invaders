using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
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
        private readonly ISoundManager r_SoundManager;
        private TextService m_PauseText;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            // initLevelTransitionScreen();
            r_GameManager = new GameManager(this, SettingsCollection.NumberOfPlayers);
            r_GameManager.GameIsOver += gameIsOver_GameIsOver;
            r_GameManager.GoToNextLevel += goToNextLevel_GoToNextLevel;
            r_PauseScreenScreen = new PauseScreen(i_Game);
            this.BlendState = BlendState.NonPremultiplied;
            r_SoundManager = this.Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
        }

        private void initLevelTransitionScreen()
        {
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(this.Game));
        }

        public override void Initialize()
        {
            initLevelTransitionScreen();
            base.Initialize();
            initText();
        }

        private void initText()
        {
            int xOffset = (int) (this.CenterOfViewPort.X / 5);
            int yOffset = (int) (this.CenterOfViewPort.X / 1.5f);
            Vector2 position = new Vector2(this.CenterOfViewPort.X - xOffset, this.CenterOfViewPort.Y - yOffset);

            m_PauseText = new TextService(SpritesDefinition.TextFont, this, position, Color.LightBlue);
            m_PauseText.TextToPrint = "P - To Resume Game";
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(InputManager.KeyPressed(Keys.P))
            {
                this.ScreensManager.SetCurrentScreen(r_PauseScreenScreen);
            }
            else if(InputManager.KeyPressed(Keys.L))
            { 
                r_GameManager.DeleteAllEnemyAndGoToNextLevel();
            }
        }

        private void goToNextLevel_GoToNextLevel()
        {
            r_SoundManager.PlaySoundEffect(GameDefinitions.SoundNameForLevelWin);
            initLevelTransitionScreen();
            r_GameManager.InitForNextLevel();
        }

        private void gameIsOver_GameIsOver()
        {
            List<TextService> playerScoreTextService = new List<TextService>();
            GameOverScreen gameOverScreen = new GameOverScreen(this.Game, playerScoreTextService);

            foreach (Player player in GameManager.PlayersList)
            {
                playerScoreTextService.Add(player.ScoreBoardText);
            }

            gameOverScreen.UserWantNewGame += resetNewGame_UserWantNewGame;
            gameOverScreen.UserWantLeaveGame += exitScreen_UserWantLeaveGame;
            r_SoundManager.PlaySoundEffect(GameDefinitions.SoundNameForGameOver);
            this.ScreensManager.SetCurrentScreen(gameOverScreen);
        }

        private void exitScreen_UserWantLeaveGame()
        {
            this.ExitScreen();
        }

        private void resetNewGame_UserWantNewGame()
        {
            goToNextLevel_GoToNextLevel();
        }
    }
}