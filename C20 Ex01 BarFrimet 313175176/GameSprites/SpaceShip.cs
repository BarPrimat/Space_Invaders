﻿using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace GameSprites
{
    public class Spaceship : Sprite
    {
        private static readonly Color sr_SpaceshipTint = Color.White;
        private const float k_SpaceshipSpeed = 130;
        private readonly InputManager r_InputManager;
        private Firearm r_Firearm;

        public Spaceship(Game i_Game, string i_TexturePath) 
            : base (i_Game, i_TexturePath, sr_SpaceshipTint)
        {
            r_InputManager = new InputManager();
            r_Firearm = new Firearm(i_Game, SpaceshipMaxOfBullet, Bullet.eBulletType.SpaceShipBullet);
            SpaceInvaders.ListOfSprites.Add(this);
        }

        public static float SpaceshipSpeed => k_SpaceshipSpeed;

        public override void InitPosition()
        {
            // Init the ship position
            float x = (float) PreferredBackBufferWidth;
            float y = (float) PreferredBackBufferHeight;

            // Offset:
            x -= this.Texture.Width;
            y -= this.Texture.Height;

            // Put it a little bit higher:
            y -= 10;
            this.Position = new Vector2(x, y);
        }

        public override void Update(GameTime i_GameTime)
        {
            moveSpaceship(i_GameTime);
            if(r_InputManager.IsUserClickToShoot())
            {
                tryToShoot(i_GameTime);
            }
        }

        private void tryToShoot(GameTime i_GameTime)
        {
            r_Firearm.CreateNewBullet(new Vector2(this.Position.X + Texture.Width / 2, this.Position.Y));
            r_Firearm.Update(i_GameTime);
            //r_Firearm.Draw(i_GameTime, i_SpriteBatch);
        }

        private void moveSpaceship(GameTime i_GameTime)
        {
            float maxBoundaryWithoutOffset = GraphicsDevice.Viewport.Width - Texture.Width;
            float keyboardNewXPosition = r_InputManager.UserTryToMoveWithKeyboard(i_GameTime, this.Position.X);

            setupNewPosition(keyboardNewXPosition, maxBoundaryWithoutOffset);
            float mouseNewXPosition = this.Position.X + r_InputManager.GetMousePositionDelta().X;
            setupNewPosition(mouseNewXPosition, maxBoundaryWithoutOffset);
        }

        private void setupNewPosition(float i_NewXPosition, float i_MaxBoundaryWithoutOffset)
        {
            i_NewXPosition = MathHelper.Clamp(i_NewXPosition, 0, i_MaxBoundaryWithoutOffset);
            this.Position = new Vector2(i_NewXPosition, Position.Y);
        }
    }
}