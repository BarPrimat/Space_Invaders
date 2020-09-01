using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameSprites
{
    public class Enemy : Sprite
    {
        public Enemy(Game i_Game, string i_TexturePath, Color i_Tint) : base(i_Game, i_TexturePath, i_Tint)
        {
            SpaceInvaders.ListOfSprites.Add(this);
        }

        public override void InitPosition()
        {
        }

        public override void Update(GameTime i_GameTime)
        {
        }
    }
}
