using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    public class TextService : Infrastructure.ObjectModel.Sprite
    {
        private SpriteFont m_Font;
        private string m_TextToPrint;
        private bool m_IsShareSpriteBatch = true;

        public TextService(string i_AssetName, GameScreen i_GameScreen, Vector2 i_Position, Color i_ColorOfText)
            : base(i_AssetName, i_GameScreen)
        {
            m_TextToPrint = i_AssetName;
            this.TintColor = i_ColorOfText;
            this.Position = i_Position;
        }

        public TextService(string i_AssetName, GameScreen i_GameScreen, Vector2 i_Position)
            : this(i_AssetName, i_GameScreen, i_Position, Color.White)
        {
        }

        public TextService(string i_AssetName, GameScreen i_GameScreen)
            : this(i_AssetName, i_GameScreen, Vector2.Zero, Color.White)
        {
        }

        protected override void LoadContent()
        {
            m_Font = Game.Content.Load<SpriteFont>(@"Fonts\Consolas");
            if (m_SpriteBatch == null)
            {
                m_SpriteBatch = this.Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
                }
            }
            
        }

        protected override void InitBounds()
        {
        }

        public override void Draw(GameTime i_GameTime)
        {
            if(!m_IsShareSpriteBatch)
            {
                this.m_SpriteBatch.Begin();
            }

            this.m_SpriteBatch.DrawString(m_Font, m_TextToPrint, this.Position, TintColor, this.Rotation, this.PositionOrigin, this.Scales, this.SpriteEffects, this.LayerDepth);
            if(!m_IsShareSpriteBatch)
            {
                this.m_SpriteBatch.End();
            }
        }

        public string TextToPrint
        {
            get => m_TextToPrint;
            set => m_TextToPrint = value;
        }
    }
}