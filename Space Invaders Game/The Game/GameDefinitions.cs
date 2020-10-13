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
        private const int k_NumberOfPlayers = 1;
        private static readonly Color sr_TextColor = Color.White;

        // Screen definitions
        private const int k_YOffsetOfMenuText = 30;
        private const int k_XOffsetOfMenuText = 20;
        private const string k_PauseGameText = "P - To Pause Game";

        // Sound start definitions
        private const int k_StartBackgroundMusicVolume = 100;
        private const int k_StartSoundsEffectsVolume = 100;

        // Sound definitions
        // Sound name for key in Dictionary
        private const string k_SoundNameForSSGunShot = "SSGunShot";
        private const string k_SoundNameForEnemyGunShot = "EnemyGunShot";
        private const string k_SoundNameForEnemyKill = "EnemyKill";
        private const string k_SoundNameForMotherShipKill = "MotherShipKill";
        private const string k_SoundNameForBarrierHit = "BarrierHit";
        private const string k_SoundNameForGameOver = "GameOver";
        private const string k_SoundNameForLevelWin = "LevelWin";
        private const string k_SoundNameForLifeDie = "LifeDie";
        private const string k_SoundNameForMenuMove = "MenuMove";
        private const string k_SoundNameForBGMusic = "BGMusic";
        // Sound Path
        private const string k_SoundMainPath = @"c:\temp\XNA_Assets\Ex03\Sounds\";
        private const string k_SoundPathForSSGunShot = k_SoundMainPath + "SSGunShot";
        private const string k_SoundPathForEnemyGunShot = k_SoundMainPath + "EnemyGunShot";
        private const string k_SoundPathForEnemyKill = k_SoundMainPath + "EnemyKill";
        private const string k_SoundPathForMotherShipKill = k_SoundMainPath + "MotherShipKill";
        private const string k_SoundPathForBarrierHit = k_SoundMainPath + "BarrierHit";
        private const string k_SoundPathForGameOver = k_SoundMainPath + "GameOver";
        private const string k_SoundPathForLevelWin = k_SoundMainPath + "LevelWin";
        private const string k_SoundPathForLifeDie = k_SoundMainPath + "LifeDie";
        private const string k_SoundPathForMenuMove = k_SoundMainPath + "MenuMove";
        private const string k_SoundPathForBGMusic = k_SoundMainPath + "BGMusic";

        // Level definitions
        private const int k_MaxOfDifficultyLevel = 4;
        private const int k_NumberOfIncreaseScoreInEachLevel = 100;

        // EnemyArmy definitions
        private const float k_HorizontalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemyAndTopEdge = 32 * 3f;
        private const float k_EnemyStepDown = k_EnemySizeHeight / 2;
        private const float k_EnemyStartSpeedInSec = 60;
        private const float k_EnemyIncreaseSpeedEveryXDead = 0.03f;
        private const float k_EnemyIncreaseSpeedGoingDown = 0.06f;
        private const float k_EnemyMaxTimeForShoot = 3f; 
        private const float k_StartEnemyMinTimeForShoot = 3f; 
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
        private const float k_LifeStartOpacity = 0.5f;
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
        private const float k_SpaceshipYOffsetStartPosition = k_SpaceshipSize + 10; // View point - (Offset + put it a little bit higher)
        private static readonly Color sr_SpaceshipTint = Color.White;

        // Background definitions
        private static readonly Color sr_BackgroundTint = Color.White;

        // MotherShip definitions
        private const float k_MotherShipSpeed = 95;
        private const int k_MotherShipMaxTimeToNextAppearsInSec = 15;
        private const float k_MotherShipAnimationLengthInSec = 3;
        private const float k_MotherShipNumberOfBlinkInAnimation = 0.3f;

        // Player definitions
        private const Keys k_FirstPlayerKeyToRight = Keys.O;
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

        public static Color TextColor => sr_TextColor;

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

        public static float EnemyStepDown => k_EnemyStepDown;

        public static float StartTimeBetweenJumpsInSec => k_StartTimeBetweenJumpsInSec;

        public static float BarrierSpeed => k_BarrierSpeed;

        public static float LifeStartOpacity => k_LifeStartOpacity;

        public static float ChanceBallDeleteWithHittingAnotherBall => k_ChanceBallDeleteWithHittingAnotherBall;

        public static float SpaceBetweenBarrier => k_SpaceBetweenBarrier;

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

        public static float BarrierPercentageThatBallEats => k_BarrierPercentageThatBallEats;

        public static float IncreaseTimeBetweenJumpsInSec => k_IncreaseTimeBetweenJumpsInSec;

        public static float LifeScales => k_LifeScales;

        public static float SpaceshipSize => k_SpaceshipSize;

        public static float SpaceshipYOffsetStartPosition => k_SpaceshipYOffsetStartPosition;

        public static float StartEnemyMinTimeForShoot => k_StartEnemyMinTimeForShoot;

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

        public static int EnemyNumberOfAssetInRow => k_EnemyNumberOfAssetInRow;

        public static int SpaceBetweenTextInScoreBoard => k_SpaceBetweenTextInScoreBoard;

        public static int SpaceBetweenLeftEdgeAndTextInScoreBoard => k_SpaceBetweenLeftEdgeAndTextInScoreBoard;

        public static int EnemyMaxOfBullet => k_EnemyMaxOfBullet;

        public static int PlayerThatAllowedToMouse => k_PlayerThatAllowedToMouse;

        public static int NumberOfBarrier => k_NumberOfBarrier;

        public static int NumberOfPlayers => k_NumberOfPlayers;

        public static int MaxOfDifficultyLevel => k_MaxOfDifficultyLevel;

        public static int NumberOfIncreaseScoreInEachLevel => k_NumberOfIncreaseScoreInEachLevel;

        public static int YOffsetOfMenuText => k_YOffsetOfMenuText;

        public static int XOffsetOfMenuText => k_XOffsetOfMenuText;

        public static int StartBackgroundMusicVolume => k_StartBackgroundMusicVolume;

        public static int StartSoundsEffectsVolume => k_StartSoundsEffectsVolume;

        public static string GameName => k_GameName;

        public static string EndGameText1Player => k_EndGameText1Player;

        public static string EndGameCaption => k_EndGameCaption;

        public static string EndGameTextMoreThen1Player => k_EndGameTextMoreThen1Player;

        public static string SoundNameForSSGunShot => k_SoundNameForSSGunShot;

        public static string SoundNameForEnemyGunShot => k_SoundNameForEnemyGunShot;

        public static string SoundNameForEnemyKill => k_SoundNameForEnemyKill;

        public static string SoundNameForMotherShipKill => k_SoundNameForMotherShipKill;

        public static string SoundNameForBarrierHit => k_SoundNameForBarrierHit;

        public static string SoundNameForGameOver => k_SoundNameForGameOver;

        public static string SoundNameForLevelWin => k_SoundNameForLevelWin;

        public static string SoundNameForLifeDie => k_SoundNameForLifeDie;

        public static string SoundNameForMenuMove => k_SoundNameForMenuMove;

        public static string SoundNameForBGMusic => k_SoundNameForBGMusic;

        public static string SoundPathForSSGunShot => k_SoundPathForSSGunShot;

        public static string SoundPathForEnemyGunShot => k_SoundPathForEnemyGunShot;

        public static string SoundPathForEnemyKill => k_SoundPathForEnemyKill;

        public static string SoundPathForMotherShipKill => k_SoundPathForMotherShipKill;

        public static string SoundPathForBarrierHit => k_SoundPathForBarrierHit;

        public static string SoundPathForGameOver => k_SoundPathForGameOver;

        public static string SoundPathForLevelWin => k_SoundPathForLevelWin;

        public static string SoundPathForLifeDie => k_SoundPathForLifeDie;

        public static string SoundPathForMenuMove => k_SoundPathForMenuMove;

        public static string SoundPathForBGMusic => k_SoundPathForBGMusic;

        public static string PauseGameText => k_PauseGameText;

        public static Keys FirstPlayerKeyToRight => k_FirstPlayerKeyToRight;

        public static Keys FirstPlayerKeyToLeft => k_FirstPlayerKeyToLeft;

        public static Keys FirstPlayerKeyToShoot => k_FirstPlayerKeyToShoot;

        public static Keys SecondPlayerKeyToRight => k_SecondPlayerKeyToRight;

        public static Keys SecondPlayerKeyToLeft => k_SecondPlayerKeyToLeft;

        public static Keys SecondPlayerKeyToShoot => k_SecondPlayerKeyToShoot;

        public static Enum.eDirectionMove BarrierStartDirectionToMove => k_BarrierStartDirectionToMove;
    }
}