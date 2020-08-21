using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameSprites
{
    public class Enemy : Sprite
    {
        private Vector2 m_Position;

        public Enemy(Game i_Game, string i_TexturePath, Color i_Tint, Vector2 i_Position) : base(i_Game, i_TexturePath, i_Tint)
        {
            m_Position = i_Position;
        }

        public override void Initialize()
        {
            base.Initialize();
            InitPosition();
        }

        public override void InitPosition()
        {
            this.Position = m_Position;
        }
    }
}
