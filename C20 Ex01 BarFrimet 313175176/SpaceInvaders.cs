using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex01_BarFrimet_313175176
{
    public class SpaceInvaders : Game
    {
        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;

        private const string k_GameName = "Space Invaders";
        private const int k_PreferredBackBufferWidth = 1024;
        private const int k_PreferredBackBufferHeight = 704;


        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Background background = new Background(this, SpritesDefinition.Background);
            Spaceship spaceInvaders = new Spaceship(this, SpritesDefinition.Spaceship);
            EnemyArmy enemyArmy = new EnemyArmy(this, SpritesDefinition.Enemy0101);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            this.m_Graphics.PreferredBackBufferWidth = k_PreferredBackBufferWidth;
            this.m_Graphics.PreferredBackBufferHeight = k_PreferredBackBufferHeight;
            this.m_Graphics.ApplyChanges();
            this.Window.Title = k_GameName;
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
