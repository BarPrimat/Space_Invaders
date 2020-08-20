using System.Net.Mime;
using System.Threading;
using SharpDX.Direct3D9;


namespace C20_Ex01_BarFrimet_313175176
{
    public static class SpritesFactory
    {

        public enum eSpriteType
        {
            EnemyPink,
            EnemyYellow,
            EnemyLightBlue,
            SpaceShip,
            MotherShip,
            Bullet,
            SpaceBackground
        }

        private const string k_EnemysAsset = @"Sprites\Enemies";
        private const string k_MotherSpaceShipAsset = @"Sprites\MotherSpaceShip";
        private const string k_GreenSpaceShipAsset = @"Sprites\SpaceShip2";
        private const string k_BlueSpaceShipAsset = @"Sprites\SpaceShip";
        private const string k_SpaceBGAsset = @"Sprites\SpaceBg";
        private const string k_BulletAsset = @"Sprites\Bullet";
        private const string k_BarrierAsset = @"Sprites\Barrier";
        private const string k_MouseAsset = @"Sprites\Mouse";
        private const string k_CalibriSmallFontAsset = @"Fonts\CalibriSmallFont";
        private const string k_CalibriBigFontAsset = @"Fonts\CalibriBigFont";
        private const string k_CalibriMediumFontAsset = @"Fonts\CalibriMediumFont";
        /*

        public static Sprite CreateSprite(eSpriteType i_SpriteType)
        {
            Sprite sprite = null;

            switch (i_SpriteType)
            {
                case eSpriteType.Bullet:
                    sprite = new Bullet(i_GameScreen, k_BulletAsset);
                    break;
                case eSpriteType.EnemyLightBlue:
                    sprite = new Enemy(i_GameScreen, Color.LightBlue, 2, 4, k_EnemysAsset);
                    break;
                case eSpriteType.EnemyPink:
                    sprite = new Enemy(i_GameScreen, Color.Pink, 0, 2, k_EnemysAsset);
                    break;

                case eSpriteType.EnemyYellow:
                    sprite = new Enemy(i_GameScreen, Color.Yellow, 4, 6, k_EnemysAsset);
                    break;

                case eSpriteType.MotherShip:
                    sprite = new MotherShip(i_GameScreen, k_MotherSpaceShipAsset);
                    break;

                case eSpriteType.SpaceBackground:
                    sprite = new SpaceBackground(i_GameScreen, k_SpaceBGAsset);
                    break;

                case eSpriteType.BlueSpaceShip:
                    sprite = new SpaceShip(i_GameScreen, k_BlueSpaceShipAsset);
                    break;

                case eSpriteType.GreenSpaceShip:
                    sprite = new SpaceShip(i_GameScreen, k_GreenSpaceShipAsset);
                    break;

                case eSpriteType.Barrier:
                    sprite = new Barrier(i_GameScreen, k_BarrierAsset);
                    break;

                case eSpriteType.SmallText:
                    sprite = new MediaTypeNames.Text(i_GameScreen, k_CalibriSmallFontAsset);
                    break;
                case eSpriteType.MediumText:
                    sprite = new MediaTypeNames.Text(i_GameScreen, k_CalibriMediumFontAsset);
                    break;
                case eSpriteType.BigText:
                    sprite = new MediaTypeNames.Text(i_GameScreen, k_CalibriBigFontAsset);
                    break;
                case eSpriteType.LifeBlueSpaceShip:
                    sprite = new Life(i_GameScreen, k_BlueSpaceShipAsset);
                    break;

                case eSpriteType.LifeGreenSpaceShip:
                    sprite = new Life(i_GameScreen, k_GreenSpaceShipAsset);
                    break;

                case eSpriteType.Mouse:
                    sprite = new MouseSprite(i_GameScreen, k_MouseAsset);
                    break;
            }

            sprite.Initialize();
            i_GameScreen.Add(sprite);
            return sprite;
        }
        */
    }

}