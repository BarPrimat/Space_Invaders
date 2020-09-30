using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpaceInvaders;

namespace GameSprites
{
    public class Life : Infrastructure.ObjectModel.Sprite
    {
        private readonly Vector2 r_StartPosition;

        public Life(GameScreen i_GameScreen, string i_TexturePath, Vector2 i_Position) : base(i_TexturePath, i_GameScreen)
        {
            r_StartPosition = i_Position;
            this.Opacity = GameDefinitions.LifeStartOpacity;
            this.Scales = new Vector2(GameDefinitions.LifeScales, GameDefinitions.LifeScales);
        }

        protected override void InitOrigins()
        {
            this.Position = r_StartPosition;
            base.InitOrigins(); 
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            Game.Components.Remove(this);
        }
    }
}