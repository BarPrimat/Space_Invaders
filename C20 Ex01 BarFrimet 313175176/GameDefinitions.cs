using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders
{
    public class GameDefinitions
    {
        // Game setup definitions
        private const string k_GameName = "Space Invaders";
        private const string k_EndGameText1Player = "Your score is: ";
        private const string k_EndGameTextMoreThen1Player = "The score is: ";
        private const string k_EndGameCaption = "Game Over";
        private const int k_PreferredBackBufferWidth = 750;
        private const int k_PreferredBackBufferHeight = 550;

        // EnemyArmy definitions
        private const float k_HorizontalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemyAndTopEdge = 32 * 3f;
        private const float k_EnemyStepDown = k_EnemySizeHeight / 2;
        private const float k_EnemyStartSpeedInSec = 60;
        private const float k_EnemyIncreaseSpeedEveryXDead = 0.03f;
        private const float k_EnemyIncreaseSpeedGoingDown = 0.06f;
        private const float k_EnemyMaxTimeForShoot = 3f; 
        private const float k_NumberOfJumpInSec = 2;
        private const float k_StartTimeBetweenJumpsInSec = 1 / k_NumberOfJumpInSec;
        private const float k_IncreaseTimeBetweenJumpsInSec = 0.05f;
        private const int k_NumberOfEnemyInRow = 5;
        private const int k_NumberOfEnemyInColumn = 9; 
        private const int k_NumberOfEnemyKilledToIncreaseSpeed = 5;

        // Enemy definitions
        private static readonly Color sr_PinkEnemyTint = Color.Pink;
        private static readonly Color sr_LightBlueEnemyTint = Color.LightBlue;
        private static readonly Color sr_YellowEnemyTint = Color.Yellow;
        private const int k_EnemyMaxOfBullet = 1; 
        private const int k_EnemyNumberOfAssetInRow = 2;
        private const float k_EnemySize = 32;
        private const float k_EnemySizeWidth = k_EnemySize;
        private const float k_EnemySizeHeight = k_EnemySize;
        private const float k_EnemyAnimationLengthInSec = 1.7f;
        private const float k_EnemyNumberOfRotateInSec = 5;
        private const float k_EnemyNumberOfAssetChangesInSec = 0.5f;

        // Life definitions
        private const int k_SpaceBetweenLife = 32;
        private const int k_StartLifePositionWidth = k_PreferredBackBufferWidth;
        private const int k_StartLifePositionHeight = 32;
        private const int k_LifeSize = 32;
        private const int k_NumberOfLifeToStart = 3;
        private const float k_Opacity = 0.5f;
        private const float k_LifeScales = 0.5f;
        private static readonly Color sr_LifeTint = Color.White;

        // Bullet definitions
        private const int k_BulletStartSpeedInSec = 140;
        private const float k_ChanceBallDeleteWithHittingAnotherBall = 0.5f;
        public static readonly Color sr_EnemyBulletTint = Color.Blue;
        public static readonly Color sr_SpaceshipBulletTint = Color.Red;

        // SpaceShip definitions
        private const int k_SpaceshipMaxOfBullet = 2;
        private const float k_SpaceshipSpeed = 140;
        private const float k_SpaceshipSize = 32;
        private const float k_SpaceshipAnimationLengthInSec = 2.6f;
        private const float k_SpaceshipNumberOfRotateInSec = 6;
        private const float k_SpaceshipAnimationLengthWhenBulletHitInSec = 2f;
        private const float k_SpaceshipNumberOfBlinkingWhenBulletHitInSec = 1f / (8 * k_SpaceshipAnimationLengthWhenBulletHitInSec);
        private const float k_SpaceshipYStartPosition = k_PreferredBackBufferHeight - k_SpaceshipSize - 10; // View point - Offset - put it a little bit higher
        private static readonly Color sr_SpaceshipTint = Color.White;

        // Background definitions
        private static readonly Color sr_BackgroundTint = Color.White;

        // MotherShip definitions
        private const float k_MotherShipSpeed = 95;
        private const int k_MotherShipMaxTimeToNextAppearsInSec = 15;
        private const float k_MotherShipAnimationLengthInSec = 3;
        private const float k_MotherShipNumberOfBlinkInAnimation = 0.3f;

        // Player definitions
        private const Keys k_FirstPlayerKeyToRight = Keys.P;
        private const Keys k_FirstPlayerKeyToLeft = Keys.I;
        private const Keys k_FirstPlayerKeyToShoot = Keys.D9;
        private const Keys k_SecondPlayerKeyToRight = Keys.R;
        private const Keys k_SecondPlayerKeyToLeft = Keys.W;
        private const Keys k_SecondPlayerKeyToShoot = Keys.D3;
        private const int k_PlayerThatAllowedToMouse = 0;
        private const int k_SpaceBetweenTextInScoreBoard = 20;
        private const int k_SpaceBetweenTopEdgeAndTextInScoreBoard = 10;
        private const int k_SpaceBetweenLeftEdgeAndTextInScoreBoard = 10;
        
        // Barrier definitions
        private const float k_BarrierSpeed = 35;
        private const float k_BarrierPercentageThatBallEats = 0.35f;
        private const Enum.eDirectionMove k_BarrierStartDirectionToMove = Enum.eDirectionMove.Right;

        // BarrierGroup definitions
        private const float k_SpaceBetweenBarrier = 1.3f;
        private const int k_NumberOfBarrier = 4;
        private const float k_VerticalSpaceBetweenSpaceshipAndBarrier = 1f;

        public static Color LifeTint => sr_LifeTint;

        public static Color EnemyBulletTint => sr_EnemyBulletTint;

        public static Color SpaceshipBulletTint => sr_SpaceshipBulletTint;

        public static Color SpaceshipTint => sr_SpaceshipTint;

        public static Color PinkEnemyTint => sr_PinkEnemyTint;

        public static Color LightBlueEnemyTint => sr_LightBlueEnemyTint;

        public static Color YellowEnemyTint => sr_YellowEnemyTint;

        public static Color BackgroundTint => sr_BackgroundTint;

        public static float HorizontalSpaceBetweenEnemy => k_HorizontalSpaceBetweenEnemy;

        public static float VerticalSpaceBetweenEnemy => k_VerticalSpaceBetweenEnemy;

        public static float VerticalSpaceBetweenEnemyAndTopEdge => k_VerticalSpaceBetweenEnemyAndTopEdge;

        public static float EnemyStartSpeedInSec => k_EnemyStartSpeedInSec;

        public static float EnemyIncreaseSpeedEveryXDead => k_EnemyIncreaseSpeedEveryXDead;

        public static float EnemyIncreaseSpeedGoingDown => k_EnemyIncreaseSpeedGoingDown;

        public static float EnemySize => k_EnemySize;

        public static float EnemyMaxTimeForShoot => k_EnemyMaxTimeForShoot;

        public static float SpaceshipSpeed => k_SpaceshipSpeed;

        public static float EnemySizeWidth => k_EnemySizeWidth;

        public static float EnemySizeHeight => k_EnemySizeHeight;

        public static float MotherShipSpeed => k_MotherShipSpeed;

        public static int PreferredBackBufferWidth => k_PreferredBackBufferWidth;

        public static int PreferredBackBufferHeight => k_PreferredBackBufferHeight;

        public static int SpaceBetweenLife => k_SpaceBetweenLife;

        public static int NumberOfEnemyInRow => k_NumberOfEnemyInRow;

        public static int NumberOfEnemyInColumn => k_NumberOfEnemyInColumn;

        public static int StartLifePositionWidth => k_StartLifePositionWidth;

        public static int StartLifePositionHeight => k_StartLifePositionHeight;

        public static int LifeSize => k_LifeSize;

        public static int BulletStartSpeedInSec => k_BulletStartSpeedInSec;

        public static int SpaceshipMaxOfBullet => k_SpaceshipMaxOfBullet;

        public static int NumberOfLifeToStart => k_NumberOfLifeToStart;

        public static int NumberOfEnemyKilledToIncreaseSpeed => k_NumberOfEnemyKilledToIncreaseSpeed;

        public static int MotherShipMaxTimeToNextAppearsInSec => k_MotherShipMaxTimeToNextAppearsInSec;

        public static string GameName => k_GameName;

        public static string EndGameText1Player => k_EndGameText1Player;

        public static string EndGameCaption => k_EndGameCaption;

        public static float EnemyStepDown => k_EnemyStepDown;

        public static float StartTimeBetweenJumpsInSec => k_StartTimeBetweenJumpsInSec;

        public static string EndGameTextMoreThen1Player => k_EndGameTextMoreThen1Player;

        public static float Opacity => k_Opacity;

        public static Keys FirstPlayerKeyToRight => k_FirstPlayerKeyToRight;

        public static Keys FirstPlayerKeyToLeft => k_FirstPlayerKeyToLeft;

        public static Keys FirstPlayerKeyToShoot => k_FirstPlayerKeyToShoot;

        public static Keys SecondPlayerKeyToRight => k_SecondPlayerKeyToRight;

        public static Keys SecondPlayerKeyToLeft => k_SecondPlayerKeyToLeft;

        public static Keys SecondPlayerKeyToShoot => k_SecondPlayerKeyToShoot;

        public static int PlayerThatAllowedToMouse => k_PlayerThatAllowedToMouse;

        public static float ChanceBallDeleteWithHittingAnotherBall => k_ChanceBallDeleteWithHittingAnotherBall;

        public static int EnemyMaxOfBullet => k_EnemyMaxOfBullet;

        public static float BarrierSpeed => k_BarrierSpeed;

        public static Enum.eDirectionMove BarrierStartDirectionToMove => k_BarrierStartDirectionToMove;

        public static float SpaceBetweenBarrier => k_SpaceBetweenBarrier;

        public static int NumberOfBarrier => k_NumberOfBarrier;

        public static float SpaceshipYStartPosition => k_SpaceshipYStartPosition;

        public static float VerticalSpaceBetweenSpaceshipAndBarrier => k_VerticalSpaceBetweenSpaceshipAndBarrier;

        public static float EnemyAnimationLengthInSec => k_EnemyAnimationLengthInSec;

        public static float EnemyNumberOfRotateInSec => k_EnemyNumberOfRotateInSec;

        public static float SpaceshipAnimationLengthInSec => k_SpaceshipAnimationLengthInSec;

        public static float SpaceshipNumberOfRotateInSec => k_SpaceshipNumberOfRotateInSec;

        public static float MotherShipAnimationLengthInSec => k_MotherShipAnimationLengthInSec;

        public static float MotherShipNumberOfBlinkInAnimation => k_MotherShipNumberOfBlinkInAnimation;

        public static float SpaceshipNumberOfBlinkingWhenBulletHitInSec => k_SpaceshipNumberOfBlinkingWhenBulletHitInSec;

        public static float SpaceshipAnimationLengthWhenBulletHitInSec => k_SpaceshipAnimationLengthWhenBulletHitInSec;

        public static float EnemyNumberOfAssetChangesInSec => k_EnemyNumberOfAssetChangesInSec;

        public static int EnemyNumberOfAssetInRow => k_EnemyNumberOfAssetInRow;

        public static int SpaceBetweenTextInScoreBoard => k_SpaceBetweenTextInScoreBoard;

        public static int SpaceBetweenLeftEdgeAndTextInScoreBoard => k_SpaceBetweenLeftEdgeAndTextInScoreBoard;

        public static float BarrierPercentageThatBallEats => k_BarrierPercentageThatBallEats;

        public static float IncreaseTimeBetweenJumpsInSec => k_IncreaseTimeBetweenJumpsInSec;

        public static float LifeScales => k_LifeScales;
    }
}
