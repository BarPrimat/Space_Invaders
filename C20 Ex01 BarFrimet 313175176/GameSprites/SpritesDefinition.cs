using System;
using System.Collections.Generic;
using System.Text;

namespace GameSprites
{
    class SpritesDefinition
    {
        private const string k_MotherSpaceShipAsset = @"Sprites\MotherShip_32x120";
        private const string k_SpaceshipUser1Asset = @"Sprites\Ship01_32x32";
        private const string k_SpaceshipUser2Asset = @"Sprites\Ship02_32x32";
        private const string k_BackgroundAsset = @"Sprites\BG_Space01_1024x768";
        private const string k_PinkEnemyAsset = @"Sprites\EnemyCollection_65x98";
        private const string k_LightBlueEnemyAsset = k_PinkEnemyAsset;
        private const string k_YellowEnemyAsset = k_PinkEnemyAsset;
        private const string k_LifeUser1Asset = @"Sprites\Ship01_32x32";
        private const string k_LifeUser2Asset = @"Sprites\Ship02_32x32";
        private const string k_BulletAsset = @"Sprites\Bullet";
        private const string k_BarrierAsset = @"Sprites\Barrier_44x32";
        private const string k_SpaceInvadersTitle = @"Screens\SpaceInvadersTitle";
        private const string k_TextBoardScoreFont = @"Fonts\Consolas";
        private const string k_TextFont = k_TextBoardScoreFont;

        public static string SpaceshipUser1Asset => k_SpaceshipUser1Asset;

        public static string BackgroundAsset => k_BackgroundAsset;

        public static string MotherSpaceShipAsset => k_MotherSpaceShipAsset;

        public static string PinkEnemyAsset => k_PinkEnemyAsset;

        public static string LifeUser1Asset => k_LifeUser1Asset;

        public static string BulletAsset => k_BulletAsset;

        public static string LightBlueEnemyAsset => k_LightBlueEnemyAsset;

        public static string YellowEnemyAsset => k_YellowEnemyAsset;

        public static string SpaceshipUser2Asset => k_SpaceshipUser2Asset;

        public static string LifeUser2Asset => k_LifeUser2Asset;

        public static string BarrierAsset => k_BarrierAsset;

        public static string TextBoardScoreFont => k_TextBoardScoreFont;

        public static string SpaceInvadersTitle => k_SpaceInvadersTitle;

        public static string TextFont => k_TextFont;
    }
}
