using System;
using System.Collections.Generic;
using System.Text;

namespace GameSprites
{
    class SpritesDefinition
    {
        private const string k_MotherSpaceShipAsset = @"Sprites\MotherShip_32x120";
        private const string k_Spaceship = @"Sprites\Ship01_32x32";
        private const string k_Background = @"Sprites\BG_Space01_1024x768";
        private const string k_Enemy0101 = @"Sprites\Enemy0101_32x32";


        public static string Spaceship => k_Spaceship;

        public static string Background => k_Background;

        public static string MotherSpaceShipAsset => k_MotherSpaceShipAsset;

        public static string Enemy0101 => k_Enemy0101;
    }
}
