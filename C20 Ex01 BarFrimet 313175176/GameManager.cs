using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;

namespace C20_Ex01_BarFrimet_313175176
{
    public class GameManager : DrawableGameComponent
    {
        private static readonly List<Bullet> r_ListBullets = new List<Bullet>();
        private static int m_SpaceShipBulletInTheAir = 0;
        private static int m_EnemyBulletInTheAir = 0;
        private static int m_CurrentScore = 0;
        private static int m_EnemyThatLeftToFinishGame;

        private static LifeManager r_LifeManager;
        private bool m_GameIsFinish = false;

        public GameManager(Game i_Game) : base(i_Game)
        {
            r_LifeManager = new LifeManager(i_Game, SpritesDefinition.LifeAsset, GameDefinitions.NumberOfLifeToStart);
            m_EnemyThatLeftToFinishGame = GameDefinitions.NumberOfEnemyInColumn * GameDefinitions.NumberOfEnemyInRow;
            i_Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_EnemyThatLeftToFinishGame == 0 || r_LifeManager.IsNoMoreLifeRemains())
            {
                ShowScoreAndEndGame(Game);
            }
        }

        public static void UpdateScore(Sprite sprite)
        {
            if(sprite is Spaceship)
            {
                m_CurrentScore += (int) Enum.eScoreValue.LoseLife;
                r_LifeManager.RemoveOneLife();
            }
            else if(sprite is Enemy)
            {
                identifiesEnemyAndUpdateScore(sprite);
                m_EnemyThatLeftToFinishGame--;
            }
            else if (sprite is MotherShip)
            {
                m_CurrentScore += (int)Enum.eScoreValue.MotherShip;
            }
        }

        public static void ShowScoreAndEndGame(Game i_Game)
        {
            System.Windows.Forms.MessageBox.Show(string.Format(@"
Game Over
Youre score is: {0}", m_CurrentScore));
            i_Game.Exit();
        }

        private static void identifiesEnemyAndUpdateScore(Sprite i_Sprite)
        {
            if(i_Sprite is Enemy)
            {
                if(i_Sprite.Tint == Color.Pink)
                {
                    m_CurrentScore += (int)Enum.eScoreValue.PinkEnemy;
                } 
                else if(i_Sprite.Tint == Color.LightBlue)
                {
                    m_CurrentScore += (int)Enum.eScoreValue.LightBlueEnemy;
                }
                else if(i_Sprite.Tint == Color.Yellow)
                {
                    m_CurrentScore += (int)Enum.eScoreValue.YellowEnemy;
                }
            }
        }

        public static List<Bullet> ListBullets => r_ListBullets;

        public static int SpaceShipBulletInTheAir
        {
            get => m_SpaceShipBulletInTheAir;
            set => m_SpaceShipBulletInTheAir = value;
        }
        public static int EnemyBulletInTheAir
        {
            get => m_EnemyBulletInTheAir;
            set => m_EnemyBulletInTheAir = value;
        }
    }
}
