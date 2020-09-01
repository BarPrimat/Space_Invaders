using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
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
        private readonly Color r_Tint;
        private readonly Game r_Game;

        public Firearm(Game i_Game, int i_MaximumOfBullet, Enum.eBulletType i_eBulletType)
        {
            r_MaximumOfBullet = i_MaximumOfBullet;
            r_eBulletType = i_eBulletType;
            r_Tint = i_eBulletType == Enum.eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
            r_Game = i_Game;
        }

        public void CreateNewBullet(Vector2 i_Position)
        {
            if (r_eBulletType == Enum.eBulletType.SpaceShipBullet && Spaceship.CounterOfSpaceShipBulletInAir < r_MaximumOfBullet)
            {
                createBulletAndAddToList(i_Position);
                Spaceship.CounterOfSpaceShipBulletInAir++;
            }
            else if (r_eBulletType == Enum.eBulletType.EnemyBullet && EnemyArmy.CounterOfEnemyBulletInAir < r_MaximumOfBullet)
            {
                createBulletAndAddToList(i_Position);
                EnemyArmy.CounterOfEnemyBulletInAir++;
            }
        }

        private void createBulletAndAddToList(Vector2 i_Position)
        {
            Bullet bullet = new Bullet(r_Game, GameSprites.SpritesDefinition.BulletAsset, r_Tint, i_Position, r_eBulletType);
            GameManager.ListOfBullets.Add(bullet);
        }
    }
}
