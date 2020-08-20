﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace GameSprites
{
    public class Background : Sprite
    {
        private static readonly Vector2 sr_BackgroundPosition = Vector2.Zero;
        private static readonly Color sr_BackgroundTint = Color.White;

        public Background(Game i_Game, string i_TexturePath) 
            : base(i_Game, i_TexturePath, sr_BackgroundTint)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void InitPosition()
        {
            this.Position = sr_BackgroundPosition;
        }
    }
}
