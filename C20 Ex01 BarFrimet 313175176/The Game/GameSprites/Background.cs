using Infrastructure.ObjectModel.Screens;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace GameSprites
{
    public class Background : Infrastructure.ObjectModel.Sprite
    {
        public Background(GameScreen i_GameScreen, string i_TexturePath) : base(i_TexturePath, i_GameScreen)
        {
            this.TintColor = GameDefinitions.BackgroundTint;
        }
    }
}
