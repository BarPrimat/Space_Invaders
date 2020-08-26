using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace GameSprites
{
    public class Background : Sprite
    {
        private static readonly Vector2 sr_BackgroundPosition = Vector2.Zero;
        private static readonly Color sr_BackgroundTint = Color.White;

        public Background(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath) 
            : base(i_Graphics, i_Content, i_TexturePath, sr_BackgroundTint)
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
