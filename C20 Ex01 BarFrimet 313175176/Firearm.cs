using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace C20_Ex01_BarFrimet_313175176
{
    public class Firearm
    {
        private int m_MaximumOfBullet;
        private readonly Color r_Tint;
        private readonly Enum.eBulletType r_eBulletType;
        private readonly Game m_Game;

        public Firearm(Game i_Game, int i_MaximumOfBullet, Enum.eBulletType i_eBulletType)
        {
            m_MaximumOfBullet = i_MaximumOfBullet;
            r_eBulletType = i_eBulletType;
            r_Tint = i_eBulletType == Enum.eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
            m_Game = i_Game;
        }

        public void CreateNewBullet(Vector2 i_Position)
        {
            if (r_eBulletType == Enum.eBulletType.SpaceShipBullet && Spaceship.CounterOfSpaceShipBulletInAir < m_MaximumOfBullet)
            {
                createBulletAndAddToList(i_Position);
                Spaceship.CounterOfSpaceShipBulletInAir++;
            }
            else if (r_eBulletType == Enum.eBulletType.EnemyBullet && EnemyArmy.CounterOfEnemyBulletInAir < m_MaximumOfBullet)
            {
                createBulletAndAddToList(i_Position);
                EnemyArmy.CounterOfEnemyBulletInAir++;
            }
        }

        private void createBulletAndAddToList(Vector2 i_Position)
        {
            Bullet bullet = new Bullet(m_Game, GameSprites.SpritesDefinition.BulletAsset, r_Tint, i_Position, r_eBulletType);
            GameManager.ListBullets.Add(bullet);
        }
    }
}
