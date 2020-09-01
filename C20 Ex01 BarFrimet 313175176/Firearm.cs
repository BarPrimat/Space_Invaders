using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;
using static C20_Ex01_BarFrimet_313175176.Enum;



namespace C20_Ex01_BarFrimet_313175176
{
    public class Firearm
    {
        private readonly int r_MaximumOfBullet;
        private readonly eBulletType r_eBulletType;
        private readonly Color r_Tint;
        private readonly Game r_Game;

        public Firearm(Game i_Game, int i_MaximumOfBullet, eBulletType i_eBulletType)
        {
            r_MaximumOfBullet = i_MaximumOfBullet;
            r_eBulletType = i_eBulletType;
            r_Tint = i_eBulletType == eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
            r_Game = i_Game;
        }

        public void CreateNewBullet(Vector2 i_Position)
        {
            if (r_eBulletType == eBulletType.SpaceShipBullet && Spaceship.CounterOfSpaceShipBulletInAir < r_MaximumOfBullet)
            {
                createBulletAndAddToList(i_Position);
                Spaceship.CounterOfSpaceShipBulletInAir++;
            }
            else if (r_eBulletType == eBulletType.EnemyBullet && EnemyArmy.CounterOfEnemyBulletInAir < r_MaximumOfBullet)
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
