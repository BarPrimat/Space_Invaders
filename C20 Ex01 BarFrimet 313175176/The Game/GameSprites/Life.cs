using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders;

namespace GameSprites
{
    public class Life : Infrastructure.ObjectModel.Sprite
    {
        private readonly Vector2 r_StartPosition;
        private readonly ISoundManager r_SoundManager;

        public Life(GameScreen i_GameScreen, string i_TexturePath, Vector2 i_Position) : base(i_TexturePath, i_GameScreen)
        {
            r_StartPosition = i_Position; 
            this.BlendState = BlendState.NonPremultiplied;
            this.Opacity = GameDefinitions.LifeStartOpacity;
            this.Scales = new Vector2(GameDefinitions.LifeScales, GameDefinitions.LifeScales);
            r_SoundManager = i_GameScreen.Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
        }

        protected override void InitOrigins()
        {
            this.Position = r_StartPosition;
            base.InitOrigins(); 
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            if (r_SoundManager != null)
            {
                r_SoundManager.PlaySoundEffect(GameDefinitions.SoundNameForLifeDie);
            }

            Game.Components.Remove(this);
        }
    }
}