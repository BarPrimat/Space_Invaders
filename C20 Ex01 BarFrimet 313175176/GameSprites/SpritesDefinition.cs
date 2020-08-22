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
        private const string k_Enemy0101Asset = @"Sprites\Enemy0101_32x32";



        public static string SpaceshipAsset => k_SpaceshipAsset;

        public static string BackgroundAsset => k_BackgroundAsset;

        public static string MotherSpaceShipAsset => k_MotherSpaceShipAsset;

        public static string Enemy0101Asset => k_Enemy0101Asset;
    }
}
