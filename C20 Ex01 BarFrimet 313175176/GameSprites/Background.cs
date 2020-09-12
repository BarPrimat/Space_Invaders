using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace GameSprites
{
    public class Background : Infrastructure.ObjectModel.Sprite
    {
        public Background(Game i_Game, string i_TexturePath) : base(i_TexturePath, i_Game)
        {
            this.TintColor = GameDefinitions.BackgroundTint;
        }
    }
}
