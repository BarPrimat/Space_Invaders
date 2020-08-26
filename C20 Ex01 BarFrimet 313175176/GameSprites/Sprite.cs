using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSprites
{
    public abstract class Sprite 
    {
        private Texture2D m_Texture;
        private readonly string r_TexturePath;
        private Vector2 m_Position;
        private Color m_Tint;
        private SpriteBatch m_SpriteBatch;
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly ContentManager r_ContentManager;

        private bool m_Visible = true;

        public Sprite(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath, Color i_Tint)
        {
            r_TexturePath = i_TexturePath;
            m_Tint = i_Tint;
            r_Graphics = i_Graphics;
            r_ContentManager = i_Content;
            m_Position = Vector2.Zero;
        }

        public void Initialize()
        {
            LoadContent();
            InitPosition();
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

        public void SetXPosition(float i_NewXPosition)
        {
            m_Position.X = i_NewXPosition;
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
        public bool Visible
        {
            get => m_Visible;
            set => m_Visible = value;
        }

        public void LoadContent()
        {
            // m_SpriteBatch = new SpriteBatch(r_Graphics.GraphicsDevice);
            m_Texture = r_ContentManager.Load<Texture2D>(r_TexturePath);
        }

        public void Draw(GameTime i_GameTime, SpriteBatch i_SpriteBatch)
        {
            if (m_Visible)
            {
                i_SpriteBatch.Draw(m_Texture, m_Position, m_Tint);
            }
        }

        public abstract void InitPosition();
        public abstract void Update(GameTime i_GameTime);

    }
}
