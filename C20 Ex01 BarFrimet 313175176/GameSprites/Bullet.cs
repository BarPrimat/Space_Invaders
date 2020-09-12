using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static SpaceInvaders.GameDefinitions;
using static SpaceInvaders.Enum;


namespace GameSprites
{
    public class Bullet : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private readonly eBulletType r_eBulletType;
        private readonly int r_FirearmSerialNumber;
        private static readonly Random sr_Random = new Random();

        public Bullet(Game i_Game, string i_TexturePath, Color i_Tint, Vector2 i_CurrentPosition, eBulletType i_eBulletType, int i_FirearmSerialNumber) : base(i_TexturePath, i_Game)
        {
            r_FirearmSerialNumber = i_FirearmSerialNumber;
            r_eBulletType = i_eBulletType;
            this.TintColor = i_Tint;
            this.Position = i_CurrentPosition;
            float velocityAxisY = BulletStartSpeedInSec * (i_eBulletType == eBulletType.EnemyBullet ? 1 : -1);
            this.Velocity = new Vector2(0, velocityAxisY);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if (this.Visible && isBulletHitBorder())
            {
                DisableBullet();
            }
        }

        protected override void InitOrigins()
        {
            this.m_PositionOrigin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
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
                    else if (sr_Random.NextDouble() < ChanceBallDeleteWithHittingAnotherBall)
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
        }

        public void DisableBullet()
        {
            this.Visible = false;
        }

        public eBulletType eBulletType => r_eBulletType;

        public int FirearmSerialNumber => r_FirearmSerialNumber;
    }
}