using System.Collections.Generic;
using GameSprites;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SpaceInvaders.GameDefinitions;
using Infrastructure.Managers;
//using Sprite = Infrastructure.ObjectModel.Sprite;

namespace SpaceInvaders
{
    public class SpaceInvadersGame : Game
    {
        private readonly GraphicsDeviceManager r_Graphics;

        // It is not necessary to save the elements game but they may be used in the future
        private readonly Background r_Background;
        private readonly EnemyArmy r_EnemyArmy;
        private readonly MotherShip r_MotherShip;
        private readonly GameManager r_GameManager;
        private readonly InputManager r_InputManager;
        private readonly CollisionsManager r_CollisionsManager;

        public SpaceInvadersGame()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            r_GameManager = new GameManager(this, 2);
            r_InputManager = new InputManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.r_Graphics.PreferredBackBufferWidth = GameDefinitions.PreferredBackBufferWidth;
            this.r_Graphics.PreferredBackBufferHeight = GameDefinitions.PreferredBackBufferHeight;
            this.r_Graphics.ApplyChanges();
            Mouse.SetPosition(0, GraphicsDevice.Viewport.Height);
            this.Window.Title = GameName;
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime i_GameTime)
        {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
               || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(i_GameTime);
        }

        protected override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(i_GameTime);
        }
    }
}
