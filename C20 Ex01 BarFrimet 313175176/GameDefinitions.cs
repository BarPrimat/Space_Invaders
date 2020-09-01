using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace C20_Ex01_BarFrimet_313175176
{
    public class GameDefinitions
    {
        // Game setup definitions
        private const string k_GameName = "Space Invaders";
        private const string k_EndGameText = "Your score is: ";
        private const string k_EndGameCaption = "Game Over";
        private const int k_PreferredBackBufferWidth = 700;
        private const int k_PreferredBackBufferHeight = 500;

        // EnemyArmy definitions
        private const float k_HorizontalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemyAndTopEdge = 32 * 3f;
        private const int k_NumberOfEnemyInRow = 5;
        private const int k_NumberOfEnemyInColumn = 9;
        private const float k_EnemyStartSpeedInSec = 60;
        private const float k_EnemyIncreaseSpeedInEach5Dead = 0.03f;
        private const float k_EnemyIncreaseSpeedGoingDown = 0.06f;
        private const float k_EnemyMaxTimeForShoot = 3;
        private const int k_EnemyArmyMaxOfBullet = 5;
        private const int k_NumberOfEnemyKilledToIncreaseSpeed = 5;

        // Enemy definitions
        private static readonly Color sr_PinkEnemyTint = Color.Pink;
        private static readonly Color sr_LightBlueEnemyTint = Color.LightBlue;
        private static readonly Color sr_YellowEnemyTint = Color.Yellow;
        private const float k_EnemySize = 32;
        private const float k_EnemySizeWidth = k_EnemySize;
        private const float k_EnemySizeHeight = k_EnemySize;

        // Life definitions
        private const int k_SpaceBetweenLife = 32;
        private const int k_StartLifePositionWidth = k_PreferredBackBufferWidth;
        private const int k_StartLifePositionHeight = 32;
        private const int k_LifeSize = 32;
        private const int k_NumberOfLifeToStart = 3;
        private static readonly Color sr_LifeTint = Color.White;

        // Bullet definitions
        private const int k_BulletStartSpeedInSec = 140;
        public static readonly Color sr_EnemyBulletTint = Color.Blue;
        public static readonly Color sr_SpaceshipBulletTint = Color.Red;

        // SpaceShip definitions
        private const int k_SpaceshipMaxOfBullet = 100;
        private static readonly Color sr_SpaceshipTint = Color.White;
        private const float k_SpaceshipSpeed = 130;

        // Background definitions
        private static readonly Color sr_BackgroundTint = Color.White;

        // MotherShip definitions
        private const float k_MotherShipSpeed = 95;
        private const int k_MotherShipMaxTimeToNextAppearsInSec = 15;


        public static string GameName => k_GameName;

        public static int PreferredBackBufferWidth => k_PreferredBackBufferWidth;

        public static int PreferredBackBufferHeight => k_PreferredBackBufferHeight;

        public static int SpaceBetweenLife => k_SpaceBetweenLife;

        public static float HorizontalSpaceBetweenEnemy => k_HorizontalSpaceBetweenEnemy;

        public static float VerticalSpaceBetweenEnemy => k_VerticalSpaceBetweenEnemy;

        public static float VerticalSpaceBetweenEnemyAndTopEdge => k_VerticalSpaceBetweenEnemyAndTopEdge;

        public static int NumberOfEnemyInRow => k_NumberOfEnemyInRow;

        public static int NumberOfEnemyInColumn => k_NumberOfEnemyInColumn;

        public static float EnemyStartSpeedInSec => k_EnemyStartSpeedInSec;

        public static float EnemyIncreaseSpeedInEach5Dead => k_EnemyIncreaseSpeedInEach5Dead;

        public static float EnemyIncreaseSpeedGoingDown => k_EnemyIncreaseSpeedGoingDown;

        public static int StartLifePositionWidth => k_StartLifePositionWidth;

        public static int StartLifePositionHeight => k_StartLifePositionHeight;

        public static float EnemySize => k_EnemySize;

        public static int LifeSize => k_LifeSize;

        public static Color LifeTint => sr_LifeTint;

        public static int BulletStartSpeedInSec => k_BulletStartSpeedInSec;

        public static Color EnemyBulletTint => sr_EnemyBulletTint;

        public static Color SpaceshipBulletTint => sr_SpaceshipBulletTint;

        public static int SpaceshipMaxOfBullet => k_SpaceshipMaxOfBullet;

        public static int NumberOfLifeToStart => k_NumberOfLifeToStart;

        public static float EnemyMaxTimeForShoot => k_EnemyMaxTimeForShoot;

        public static int EnemyArmyMaxOfBullet => k_EnemyArmyMaxOfBullet;

        public static float SpaceshipSpeed => k_SpaceshipSpeed;

        public static Color SpaceshipTint => sr_SpaceshipTint;

        public static Color PinkEnemyTint => sr_PinkEnemyTint;

        public static Color LightBlueEnemyTint => sr_LightBlueEnemyTint;

        public static Color YellowEnemyTint => sr_YellowEnemyTint;

        public static Color BackgroundTint => sr_BackgroundTint;

        public static int NumberOfEnemyKilledToIncreaseSpeed => k_NumberOfEnemyKilledToIncreaseSpeed;

        public static float EnemySizeWidth => k_EnemySizeWidth;

        public static float EnemySizeHeight => k_EnemySizeHeight;

        public static float MotherShipSpeed => k_MotherShipSpeed;

        public static int MotherShipMaxTimeToNextAppearsInSec => k_MotherShipMaxTimeToNextAppearsInSec;

        public static string EndGameText => k_EndGameText;

        public static string EndGameCaption => k_EndGameCaption;
    }
}
