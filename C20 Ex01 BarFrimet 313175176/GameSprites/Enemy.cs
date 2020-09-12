using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Enum = SpaceInvaders.Enum;

namespace GameSprites
{
    public class Enemy : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private readonly Firearm r_Firearm;
        private static readonly int r_FirearmSerialNumber = 0;

        public Enemy(Game i_Game, string i_TexturePath, Color i_Tint) : base(i_TexturePath, i_Game)
        {
            this.TintColor = i_Tint;
            r_Firearm = new Firearm(i_Game, GameDefinitions.EnemyMaxOfBullet, Enum.eBulletType.EnemyBullet);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if(bullet != null && this.Visible)
            {
                if(bullet.eBulletType == Enum.eBulletType.SpaceShipBullet)
                {
                    GameManager.UpdateScore(this, bullet.FirearmSerialNumber);
                    RemoveComponent();
                    bullet.DisableBullet();
                }
            }
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            Game.Components.Remove(this);
        }

        public void Shoot(Vector2 i_Position)
        {
            r_Firearm.Shoot(i_Position);
        }
    }
}
