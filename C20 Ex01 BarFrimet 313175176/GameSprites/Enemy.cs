using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameSprites
{
    public class Enemy : Sprite
    {
        private Vector2 m_Position;

        public Enemy(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath, Color i_Tint, Vector2 i_Position) 
            : base(i_Graphics, i_Content, i_TexturePath, i_Tint)
        {
            m_Position = i_Position;
        }

        public override void InitPosition()
        {
            this.Position = m_Position;
        }

        public override void Update(GameTime i_GameTime)
        {
        }
    }
}
