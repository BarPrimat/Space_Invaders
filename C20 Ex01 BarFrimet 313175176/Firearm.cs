using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static SpaceInvaders.GameDefinitions;
using static SpaceInvaders.Enum;



namespace SpaceInvaders
{
    public class Firearm
    {
        private readonly int r_MaximumOfBullet;
        private readonly Enum.eBulletType r_eBulletType;
        private readonly List<Bullet> r_ListOfBullets = new List<Bullet>();
        private readonly int r_FirearmSerialNumber;
        private readonly Color r_TintColor;
        private readonly GameScreen r_GameScreen;
        private const int k_DefaultSerialNumber = -1;

        public Firearm(GameScreen i_GameScreen, int i_MaximumOfBullet, Enum.eBulletType i_eBulletType, int i_FirearmSerialNumber)
        {
            r_FirearmSerialNumber = i_FirearmSerialNumber;
            r_MaximumOfBullet = i_MaximumOfBullet;
            r_eBulletType = i_eBulletType;
            r_TintColor = i_eBulletType == Enum.eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
            r_GameScreen = i_GameScreen;
        }

        public Firearm(GameScreen i_GameScreen, int i_EnemyMaxOfBullet, eBulletType i_EBulletType) 
            : this(i_GameScreen, i_EnemyMaxOfBullet, i_EBulletType, k_DefaultSerialNumber)
        {
        }

        public void Shoot(Vector2 i_Position)
        {
            bool isExistBulletHasBeenSet = setExistDisableBullet(i_Position);

            if(!isExistBulletHasBeenSet)
            {
                if(r_ListOfBullets.Count < r_MaximumOfBullet)
                {
                    Bullet bullet = new Bullet(r_GameScreen, GameSprites.SpritesDefinition.BulletAsset, r_TintColor, i_Position, r_eBulletType, r_FirearmSerialNumber);
                    r_ListOfBullets.Add(bullet);
                }
            }
        }

        private bool setExistDisableBullet(Vector2 i_Position)
        {
            bool isExistBulletHasBeenSet = false;

            foreach (Bullet bullet in r_ListOfBullets)
            {
                if (!bullet.Visible)
                {
                    isExistBulletHasBeenSet = true;
                    bullet.EnableBullet(i_Position);
                    break;
                }
            }

            return isExistBulletHasBeenSet;
        }

        public int FirearmSerialNumber => r_FirearmSerialNumber;
    }
}