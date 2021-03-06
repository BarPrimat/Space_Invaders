﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static SpaceInvaders.Enum;


namespace GameSprites
{
    public class Bullet : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private readonly eBulletType r_eBulletType;
        private readonly int r_FirearmSerialNumber;
        private static readonly Random sr_Random = new Random();

        public Bullet(GameScreen i_GameScreen, string i_TexturePath, Color i_Tint, Vector2 i_CurrentPosition, eBulletType i_eBulletType, int i_FirearmSerialNumber)
            : base(i_TexturePath, i_GameScreen)
        {
            r_FirearmSerialNumber = i_FirearmSerialNumber;
            r_eBulletType = i_eBulletType;
            this.TintColor = i_Tint;
            this.Position = i_CurrentPosition;
            float velocityAxisY = GameDefinitions.BulletStartSpeedInSec * (i_eBulletType == eBulletType.EnemyBullet ? 1 : -1);
            this.Velocity = new Vector2(0, velocityAxisY);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (this.Enabled && isBulletHitBorder())
            {
                DisableBullet();
            }
        }

        protected override void InitOrigins()
        {
            this.PositionOrigin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
            base.InitOrigins();
        }

        private bool isBulletHitBorder()
        {
            return this.Position.Y > Game.GraphicsDevice.Viewport.Height || 0 > (this.Position.Y + this.Texture.Height);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if (bullet != null)
            {
                if(this.r_eBulletType != bullet.eBulletType)
                {
                    if(this.eBulletType == eBulletType.SpaceShipBullet)
                    {
                        this.DisableBullet();
                    }
                    else if (sr_Random.NextDouble() < GameDefinitions.ChanceBallDeleteWithHittingAnotherBall)
                    {
                        this.DisableBullet();
                    }
                }
            }
        }

        public void EnableBullet(Vector2 i_Position)
        {
            this.Position = i_Position;
            this.Visible = true;
            this.Enabled = true;
        }

        public void DisableBullet()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        public void RemoveComponent()
        {
            DisableBullet();
            this.RemoveComponentInGameAndGameScreen();
        }

        public eBulletType eBulletType => r_eBulletType;

        public int FirearmSerialNumber => r_FirearmSerialNumber;
    }
}