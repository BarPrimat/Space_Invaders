using System.Collections.Generic;
using GameSprites;
using Infrastructure;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Screens;

namespace SpaceInvaders
{
    public class SpaceInvadersGame : Game
    {
        private readonly GraphicsDeviceManager r_Graphics;
        // It is not necessary to save the elements game but they may be used in the future
        private readonly GameManager r_GameManager;
        private readonly InputManager r_InputManager;
        private readonly ScreensMananger r_ScreensMananger;

        public SpaceInvadersGame()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            r_InputManager = new InputManager(this);
            r_ScreensMananger = new ScreensMananger(this);
            // new Background(this, SpritesDefinition.BackgroundAsset);
            WelcomeScreen welcomeScreen = new WelcomeScreen(this);
            r_ScreensMananger.SetCurrentScreen(welcomeScreen);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.r_Graphics.PreferredBackBufferWidth = GameDefinitions.PreferredBackBufferWidth;
            this.r_Graphics.PreferredBackBufferHeight = GameDefinitions.PreferredBackBufferHeight;
            this.r_Graphics.ApplyChanges();
            Mouse.SetPosition(0, GraphicsDevice.Viewport.Height);
            this.Window.Title = GameDefinitions.GameName;
        }

        protected override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
        }

        protected override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(i_GameTime);
        }
    }
}
