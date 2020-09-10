using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpaceInvaders;

namespace GameSprites
{
    public class Life : Infrastructure.ObjectModel.Sprite
    {
        private Vector2 m_StartPosition;

        public Life(Game i_Game, string i_TexturePath, Vector2 i_Position) : base(i_TexturePath, i_Game)
        {
            m_StartPosition = i_Position;
            this.Opacity = GameDefinitions.Opacity;
            this.Scales = new Vector2(0.5f, 0.5f);
        }

        protected override void InitOrigins()
        {
            this.Position = m_StartPosition;
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            Game.Components.Remove(this);
        }
    }
}