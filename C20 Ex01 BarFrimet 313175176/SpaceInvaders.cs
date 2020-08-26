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
        private readonly LifeManager r_LifeManager;

        private readonly List<Sprite> r_SpritesList = new List<Sprite>();

        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            r_SpritesList.Add(new Background(m_Graphics, this.Content, SpritesDefinition.BackgroundAsset));
            r_SpritesList.Add(new Spaceship(m_Graphics, this.Content, SpritesDefinition.SpaceshipAsset));
            r_SpritesList.Add(new MotherShip(m_Graphics, this.Content, SpritesDefinition.MotherSpaceShipAsset, Color.Red));
            r_LifeManager = new LifeManager(m_Graphics, this.Content, SpritesDefinition.LifeAsset, 3);

            r_EnemyArmy = new EnemyArmy(m_Graphics, this.Content, SpritesDefinition.Enemy0101Asset);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            m_Graphics.PreferredBackBufferWidth = PreferredBackBufferWidth;
            m_Graphics.PreferredBackBufferHeight = PreferredBackBufferHeight;
            m_Graphics.ApplyChanges();
            foreach(Sprite sprite in r_SpritesList)
            {
                sprite.Initialize();
            }

            r_LifeManager.Initialize();
            r_EnemyArmy.Initialize();
            Mouse.SetPosition(PreferredBackBufferWidth, GraphicsDevice.Viewport.Height);

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

            foreach (Sprite sprite in r_SpritesList)
            {
                sprite.Update(i_GameTime);
            }

            r_EnemyArmy.Update(i_GameTime);

            // TODO: Add your update logic here
            base.Update(i_GameTime);
        }

        protected override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            m_SpriteBatch.Begin();

            foreach (Sprite sprite in r_SpritesList)
            {
                sprite.Draw(i_GameTime, m_SpriteBatch);
            }

            r_LifeManager.Draw(i_GameTime, m_SpriteBatch);
            r_EnemyArmy.Draw(i_GameTime, m_SpriteBatch);

            m_SpriteBatch.End();

           // m_SpriteBatch.Draw(i_GameTime);

            base.Draw(i_GameTime);
        }
    }
}
