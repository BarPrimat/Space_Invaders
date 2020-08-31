using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSprites
{
    public abstract class Sprite : DrawableGameComponent
    {
        protected Texture2D m_Texture;
        protected readonly string r_TexturePath;
        protected Vector2 m_Position;
        protected Color m_Tint;
        protected SpriteBatch m_SpriteBatch;

        public Sprite(Game i_Game, string i_TexturePath, Color i_Tint) : base(i_Game)
        {
            r_TexturePath = i_TexturePath;
            m_Tint = i_Tint;
            i_Game.Components.Add(this);
        }

        public Texture2D Texture
        {
            get => m_Texture;
            set => m_Texture = value;
        }

        public Vector2 Position
        {
            get => m_Position;
            set => m_Position = value;
        }

        public Color Tint
        {
            get => m_Tint;
            set => m_Tint = value;
        }

        public SpriteBatch SpriteBatch
        {
            get => m_SpriteBatch;
            set => m_SpriteBatch = value;
        }

        public string TexturePath
        {
            get => r_TexturePath;
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            m_Texture = this.Game.Content.Load<Texture2D>(r_TexturePath);
            this.InitPosition();
            base.LoadContent();
        }

        public override void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.Begin();
            if (this.Visible)
            {
                this.SpriteBatch.Draw(m_Texture, m_Position, m_Tint);
            }

            base.Draw(i_GameTime);
            m_SpriteBatch.End();
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            Game.Components.Remove(this);
        }

        public abstract void InitPosition();

    }
}
