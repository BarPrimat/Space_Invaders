using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public class Enum
    {
        public enum eDirectionMove
        {
            Left,
            Right
        }

        public enum eBulletType
        {
            SpaceShipBullet = -1,
            EnemyBullet = 1
        }

        public enum eScoreValue
        {
            PinkEnemy = 300,
            LightBlueEnemy = 200,
            YellowEnemy = 100,
            MotherShip = 600,
            LoseLife = -600
        }
    }
}
