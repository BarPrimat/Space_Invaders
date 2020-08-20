using System;
using System.Collections.Generic;
using System.Text;

namespace GameSprites
{
    class SpritesDefinition
    {
        private const string k_MotherSpaceShipAsset = @"Sprites\MotherShip_32x120";
        private const string k_SpaceShip = @"Sprites\Ship01_32x32";
        private const string k_Background = @"Sprites\BG_Space01_1024x768";

        public static string SpaceShip => k_SpaceShip;

        public static string Background => k_Background;

        public static string MotherSpaceShipAsset => k_MotherSpaceShipAsset;
    }
}
