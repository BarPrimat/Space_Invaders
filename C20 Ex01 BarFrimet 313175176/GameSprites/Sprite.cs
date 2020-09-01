﻿using System;
using System.Collections.Generic;
using System.Text;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSprites
{
    public abstract class Sprite : DrawableGameComponent
    {
        protected Texture2D m_Texture;
        protected Vector2 m_Position;
        protected Color m_Tint;
        protected readonly string r_TexturePath;
        // Needed to loading the SpriteBatch just one time
        protected static SpriteBatch s_SpriteBatch; 
        private bool m_FirstTimeLoad = true;

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
            get => s_SpriteBatch;
            set => s_SpriteBatch = value;
        }

        public string TexturePath
        {
            get => r_TexturePath;
        }

        protected override void LoadContent()
        {
            // Needed to loading the SpriteBatch just one time
            if (m_FirstTimeLoad)
            {
                s_SpriteBatch = new SpriteBatch(GraphicsDevice);
                m_FirstTimeLoad = !m_FirstTimeLoad;
            }

            m_Texture = this.Game.Content.Load<Texture2D>(r_TexturePath);
            this.InitPosition();
        }

        public override void Draw(GameTime i_GameTime)
        {
            s_SpriteBatch.Begin();
            if (this.Visible)
            {
                s_SpriteBatch.Draw(m_Texture, m_Position, m_Tint);
            }

            s_SpriteBatch.End();
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            Game.Components.Remove(this);
        }

        public abstract void InitPosition();
    }
}
