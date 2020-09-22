using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    public class TextService : Infrastructure.ObjectModel.Sprite
    {
        private SpriteFont m_Font;
        private string m_TextToPrint;

        public TextService(string i_AssetName, Game i_Game, Vector2 i_Position, Color i_ColorOfText)
            : base(i_AssetName, i_Game)
        {
            TintColor = i_ColorOfText;
            this.Position = i_Position;
        }

        protected override void LoadContent()
        {
            m_Font = Game.Content.Load<SpriteFont>(SpritesDefinition.TextBoardScoreFont);
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
            this.m_SpriteBatch.Begin();
            this.m_SpriteBatch.DrawString(m_Font, m_TextToPrint, this.Position, TintColor);
            this.m_SpriteBatch.End();
        }

        public string TextToPrint
        {
            get => m_TextToPrint;
            set => m_TextToPrint = value;
        }
    }
}