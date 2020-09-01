using System;
using System.Collections.Generic;
using System.Text;

namespace GameSprites
{
    class SpritesDefinition
    {
        private const string k_MotherSpaceShipAsset = @"Sprites\MotherShip_32x120";
        private const string k_SpaceshipAsset = @"Sprites\Ship01_32x32";
        private const string k_BackgroundAsset = @"Sprites\BG_Space01_1024x768";
        private const string k_PinkEnemyAsset = @"Sprites\Enemy0101_32x32";
        private const string k_LightBlueEnemyAsset = @"Sprites\Enemy0201_32x32";
        private const string k_YellowEnemyAsset = @"Sprites\Enemy0301_32x32";
        private const string k_LifeAsset = @"Sprites\Ship01_32x32";
        private const string k_BulletAsset = @"Sprites\Bullet";

        public static string SpaceshipAsset => k_SpaceshipAsset;

        public static string BackgroundAsset => k_BackgroundAsset;

        public static string MotherSpaceShipAsset => k_MotherSpaceShipAsset;

        public static string PinkEnemyAsset => k_PinkEnemyAsset;

        public static string LifeAsset => k_LifeAsset;

        public static string BulletAsset => k_BulletAsset;

        public static string LightBlueEnemyAsset => k_LightBlueEnemyAsset;

        public static string YellowEnemyAsset => k_YellowEnemyAsset;
    }
}
