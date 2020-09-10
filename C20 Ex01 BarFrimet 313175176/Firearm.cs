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

        public void CreateNewBullet(Vector2 i_Position, ref int i_CounterOfBulletInAir)
        {
            if (r_eBulletType == Enum.eBulletType.SpaceShipBullet && i_CounterOfBulletInAir < r_MaximumOfBullet)
            {
                createBulletAndAddToList(i_Position);
                i_CounterOfBulletInAir++;
            }
            else if (r_eBulletType == Enum.eBulletType.EnemyBullet && i_CounterOfBulletInAir < r_MaximumOfBullet)
            {
                createBulletAndAddToList(i_Position);
                i_CounterOfBulletInAir++;
            }
        }

        private void createBulletAndAddToList(Vector2 i_Position)
        {
            Bullet bullet = new Bullet(r_Game, GameSprites.SpritesDefinition.BulletAsset, r_Tint, i_Position, r_eBulletType);
            GameManager.ListOfBullets.Add(bullet);
        }
    }
}
