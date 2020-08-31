﻿using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameSprites
{
    public class Enemy : Sprite
    {
        private Vector2 m_Position;

        public Enemy(Game i_Game, string i_TexturePath, Color i_Tint, Vector2 i_Position) 
            : base(i_Game, i_TexturePath, i_Tint)
        {
            m_Position = i_Position;
            SpaceInvaders.ListOfSprites.Add(this);
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