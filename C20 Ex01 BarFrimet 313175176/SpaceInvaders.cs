using System.Collections.Generic;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;

namespace C20_Ex01_BarFrimet_313175176
{
    public class SpaceInvaders : Game
    {
        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;

        private readonly Background r_Background;
        private readonly Spaceship r_Spaceship;
        private readonly EnemyArmy r_EnemyArmy;
        private readonly MotherShip r_MotherShip;
        private static GameManager s_GameManager;
        private static readonly List<Sprite> sr_ListOfSprites = new List<Sprite>();



        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
            r_Spaceship = new Spaceship(this, SpritesDefinition.SpaceshipAsset);
            r_EnemyArmy = new EnemyArmy(this);
            r_MotherShip = new MotherShip(this, SpritesDefinition.MotherSpaceShipAsset, Color.Red);
            s_GameManager = new GameManager(this);
        }

        public static List<Sprite> ListOfSprites => sr_ListOfSprites;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            this.m_Graphics.PreferredBackBufferWidth = PreferredBackBufferWidth;
            this.m_Graphics.PreferredBackBufferHeight = PreferredBackBufferHeight;
            this.m_Graphics.ApplyChanges();
            Mouse.SetPosition((int)this.r_Spaceship.Position.X, GraphicsDevice.Viewport.Height);

            this.Window.Title = GameName;
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime i_GameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(i_GameTime);
        }

        protected override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            m_SpriteBatch.Begin();

            m_SpriteBatch.End();

            base.Draw(i_GameTime);
        }
    }
}
