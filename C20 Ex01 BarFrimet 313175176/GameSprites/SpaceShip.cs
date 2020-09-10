﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SpaceInvaders.GameDefinitions;
using static SpaceInvaders.Enum;


namespace GameSprites
{
    public class Spaceship : Sprite
    {
        private readonly InputManager r_InputManager;
        private readonly Firearm r_Firearm;
        private float m_SpaceshipSpeed;
        private int m_CounterOfSpaceShipBulletInAir = 0;
        private int m_NumberOfTheSpaceship;

        public Spaceship(Game i_Game, string i_TexturePath, int i_NumberOfTheSpaceship) : base (i_Game, i_TexturePath, GameDefinitions.SpaceshipTint)
        {
            // this.TintColor = GameDefinitions.SpaceshipTint;
            r_InputManager = new InputManager();
            r_Firearm = new Firearm(i_Game, SpaceshipMaxOfBullet, eBulletType.SpaceShipBullet);
            m_SpaceshipSpeed = GameDefinitions.SpaceshipSpeed;
            m_NumberOfTheSpaceship = i_NumberOfTheSpaceship;
            SpaceInvadersGame.ListOfSprites.Add(this);
        }

        public override void InitPosition()
        {
            // Init the ship position
            float x = m_NumberOfTheSpaceship * this.Texture.Width;
            float y = (float) PreferredBackBufferHeight;

            // Offset:
            y -= this.Texture.Height;

            // Put it a little bit higher:
            y -= 10;
            this.Position = new Vector2(x, y);
        }

        public override void Update(GameTime i_GameTime)
        {
            moveSpaceship(i_GameTime);
            if (r_InputManager.IsUserClickToShoot())
            {
                tryToShoot(i_GameTime);
            }
        }

        private void tryToShoot(GameTime i_GameTime)
        {
            r_Firearm.CreateNewBullet(new Vector2(this.Position.X + Texture.Width / 2, this.Position.Y), ref m_CounterOfSpaceShipBulletInAir);
        }

        private void moveSpaceship(GameTime i_GameTime)
        {
            float maxBoundaryWithoutOffset = GraphicsDevice.Viewport.Width - Texture.Width;
            float keyboardNewXPosition = r_InputManager.UserTryToMoveWithKeyboard(i_GameTime, this.Position.X, m_SpaceshipSpeed);

            setupNewPosition(keyboardNewXPosition, maxBoundaryWithoutOffset);
            float mouseNewXPosition = this.Position.X + r_InputManager.GetMousePositionDelta().X;
            setupNewPosition(mouseNewXPosition, maxBoundaryWithoutOffset);
        }

        private void setupNewPosition(float i_NewXPosition, float i_MaxBoundaryWithoutOffset)
        {
            i_NewXPosition = MathHelper.Clamp(i_NewXPosition, 0, i_MaxBoundaryWithoutOffset);
            this.Position = new Vector2(i_NewXPosition, Position.Y);
        }

        public float SpaceshipSpeed => m_SpaceshipSpeed;

        public int CounterOfSpaceShipBulletInAir
        {
            get => m_CounterOfSpaceShipBulletInAir;
            set => m_CounterOfSpaceShipBulletInAir = value;
        }
        public int NumberOfTheSpaceship
        {
            get => m_NumberOfTheSpaceship;
            set => m_NumberOfTheSpaceship = value;
        }
    }
}