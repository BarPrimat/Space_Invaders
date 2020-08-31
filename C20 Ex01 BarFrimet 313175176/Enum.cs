using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_BarFrimet_313175176
{
    public class Enum
    {
        public enum eBulletType
        {
            SpaceShipBullet = -1,
            EnemyBullet = 1
        }

        public enum eDirectionMove
        {
            Left,
            Right
        }

        public enum eScoreValue
        {
            PinkEnemy = 300,
            LightBlueEnemy = 200,
            YellowEnemy = 70,
            MotherShip = 600,
            LoseLife = -600
        }
    }
}
