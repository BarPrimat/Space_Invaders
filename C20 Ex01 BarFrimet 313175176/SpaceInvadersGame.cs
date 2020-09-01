using System.Collections.Generic;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SpaceInvaders.GameDefinitions;

namespace SpaceInvaders
{
    public class SpaceInvadersGame : Game
    {
        private readonly GraphicsDeviceManager r_Graphics;

        // It is not necessary to save the elements game but they may be used in the future
        private readonly Background r_Background;
        private readonly Spaceship r_Spaceship;
        private readonly EnemyArmy r_EnemyArmy;
        private readonly MotherShip r_MotherShip;
        private readonly GameManager r_GameManager;
        private static readonly List<Sprite> sr_ListOfSprites = new List<Sprite>();

        public SpaceInvadersGame()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            // It is not necessary to save the elements game but they may be used in the future
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
            r_Spaceship = new Spaceship(this, SpritesDefinition.SpaceshipAsset);
            r_MotherShip = new MotherShip(this, SpritesDefinition.MotherSpaceShipAsset, Color.Red);
            r_EnemyArmy = new EnemyArmy(this);
            r_GameManager = new GameManager(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.r_Graphics.PreferredBackBufferWidth = GameDefinitions.PreferredBackBufferWidth;
            this.r_Graphics.PreferredBackBufferHeight = GameDefinitions.PreferredBackBufferHeight;
            this.r_Graphics.ApplyChanges();
            Mouse.SetPosition((int)this.r_Spaceship.Position.X, GraphicsDevice.Viewport.Height);
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

        public static List<Sprite> ListOfSprites => sr_ListOfSprites;
    }
}
