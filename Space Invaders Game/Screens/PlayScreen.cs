using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;
using Microsoft.Xna.Framework.Graphics;
using Screens.MainMenuScreens;


namespace Screens
{
    public class PlayScreen : GameScreen
    {
        private const bool k_PlaySoundOfNextLevel = true;
        private readonly GameManager r_GameManager;
        private readonly PauseScreen r_PauseScreenScreen;
        private readonly ISoundManager r_SoundManager;
        private TextService m_PauseText;
        private bool m_PlaySoundOfNextLevel = true;
        private bool m_IsFirstTimeRun = true;

        public PlayScreen(Game i_Game) : base(i_Game)
        {
            if (Game.Services.GetService<ICollisionsManager>() != null)
            {
                i_Game.Services.RemoveService(typeof(ICollisionsManager));
            }

            new CollisionsManager(i_Game);
            r_GameManager = new GameManager(this, SettingsCollection.NumberOfPlayers);
            r_GameManager.GameIsOver += finishGame_GameIsOver;
            r_GameManager.GoToNextLevel += loadingAndRunNextLevel_GoToNextLevel;
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
            base.Initialize();
            initText();
        }

        private void initText()
        {
            int xOffset = (int) (this.CenterOfViewPort.X / 5);
            int yOffset = (int) (this.CenterOfViewPort.X / 1.5f);
            Vector2 position = new Vector2(this.CenterOfViewPort.X - xOffset, this.CenterOfViewPort.Y - yOffset);

            m_PauseText = new TextService(GameDefinitions.PauseGameText, this, position, Color.LightBlue);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (m_IsFirstTimeRun)
            {
                initLevelTransitionScreen();
                m_IsFirstTimeRun = !m_IsFirstTimeRun;
            }

            if (InputManager.KeyPressed(Keys.P))
            {
                this.ScreensManager.SetCurrentScreen(r_PauseScreenScreen);
            }
            // Implemented of shortcut to go next level
            /*
            else if(InputManager.KeyPressed(Keys.L))
            {
                r_GameManager.GoToNextLevel_AllEnemyAreDead();
            }
            */
        }

        private void loadingAndRunNextLevel_GoToNextLevel()
        {
            if(m_PlaySoundOfNextLevel)
            {
                r_SoundManager.PlaySoundEffect(GameDefinitions.SoundNameForLevelWin);
            }

            m_PlaySoundOfNextLevel = true;
            initLevelTransitionScreen();
            r_GameManager.InitForNextLevel();
        }

        private void finishGame_GameIsOver()
        {
            GameOverScreen gameOverScreen = new GameOverScreen(this.Game);

            gameOverScreen.UserWantNewGame += resetNewGame_UserWantNewGame;
            gameOverScreen.UserWantLeaveGame += exitScreen_UserWantLeaveGame;
            r_SoundManager.PlaySoundEffect(GameDefinitions.SoundNameForGameOver);
            this.ScreensManager.SetCurrentScreen(gameOverScreen);
        }

        private void exitScreen_UserWantLeaveGame()
        {
            this.ExitScreen();
            ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
        }

        private void resetNewGame_UserWantNewGame()
        {
            m_PlaySoundOfNextLevel = !k_PlaySoundOfNextLevel;
            loadingAndRunNextLevel_GoToNextLevel();
        }
    }
}