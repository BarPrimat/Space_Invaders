using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;

namespace GameSprites
{
    public class Life : Sprite
    {
        public Life(Game i_Game, string i_TexturePath) : base(i_Game, i_TexturePath, LifeTint)
        {
        }

        public override void InitPosition()
        {
        }

        public override void Update(GameTime i_GameTime)
        {
        }
    }
}