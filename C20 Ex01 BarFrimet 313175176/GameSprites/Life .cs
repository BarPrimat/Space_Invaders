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
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly ContentManager r_ContentManager;
        private static readonly Color sr_LifeTint = Color.White;


        public Life(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath)
            : base(i_Graphics, i_Content, i_TexturePath, sr_LifeTint)
        {
            r_Graphics = i_Graphics;
            r_ContentManager = i_Content;
        }

        public override void InitPosition()
        {
        }

        public override void Update(GameTime i_GameTime)
        {
        }
    }
}