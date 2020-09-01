using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace GameSprites
{
    public class Background : Sprite
    {
        public Background(Game i_Game, string i_TexturePath) : base(i_Game, i_TexturePath, GameDefinitions.BackgroundTint)
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
