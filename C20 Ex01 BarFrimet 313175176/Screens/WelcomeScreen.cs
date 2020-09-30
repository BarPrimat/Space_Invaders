using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Screens.MainMenuScreens;
using SpaceInvaders;

namespace Screens
{
    public class WelcomeScreen : GameScreen
    {
        private const int k_LevelOne = 1;
        private readonly Background r_Background;
        private readonly Sprite r_SpaceInvadersTitle;
        private IInputManager m_InputManager;

        public WelcomeScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
            r_SpaceInvadersTitle = new Sprite(SpritesDefinition.SpaceInvadersTitle, this);
        }

        public override void Initialize()
        {
            base.Initialize();
            r_SpaceInvadersTitle.PositionOrigin = r_SpaceInvadersTitle.SourceRectangleCenter;
            r_SpaceInvadersTitle.Position = this.CenterOfViewPort;
            initText();
            if (m_InputManager == null)
            {
                m_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            }
        }

        private void initText()
        {
            Vector2 position = new Vector2(this.CenterOfViewPort.X - (this.CenterOfViewPort.X / 2), this.CenterOfViewPort.Y + (this.CenterOfViewPort.Y / 2));
            
            TextService textMenu = new TextService(SpritesDefinition.TextFont, this, position, Color.DarkBlue);
            string textToPrint = string.Format(@"
press Enter for a new game
press M for main menu
press Esc to exit");

            textMenu.TextToPrint = textToPrint;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(m_InputManager.KeyPressed(Keys.Enter))
            {
                ScreensManager.SetCurrentScreen(new LevelTransitionScreen(this.Game, k_LevelOne));
            }
            else if (m_InputManager.KeyPressed(Keys.Escape))
            {
                this.Game.Exit();
            }
            else if(m_InputManager.KeyPressed(Keys.M))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }
    }
}
