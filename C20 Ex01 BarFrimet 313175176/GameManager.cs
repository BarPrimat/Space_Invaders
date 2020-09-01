using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using static C20_Ex01_BarFrimet_313175176.Enum;


namespace C20_Ex01_BarFrimet_313175176
{
    public class GameManager : DrawableGameComponent
    {
        private static LifeManager s_LifeManager;
        private static int s_CurrentScore = 0;
        private static int s_EnemyThatLeftToFinishGame;
        private static readonly List<Bullet> sr_ListOfBullets = new List<Bullet>();

        public GameManager(Game i_Game) : base(i_Game)
        {
            s_LifeManager = new LifeManager(i_Game, SpritesDefinition.LifeAsset, GameDefinitions.NumberOfLifeToStart);
            s_EnemyThatLeftToFinishGame = GameDefinitions.NumberOfEnemyInColumn * GameDefinitions.NumberOfEnemyInRow;
            i_Game.Components.Add(this);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (s_EnemyThatLeftToFinishGame == 0 || s_LifeManager.IsNoMoreLifeRemains())
            {
                ShowScoreAndEndGame(Game);
            }
        }

        public static void ShowScoreAndEndGame(Game i_Game)
        {
            System.Windows.Forms.MessageBox.Show(GameDefinitions.EndGameText + s_CurrentScore, GameDefinitions.EndGameCaption);
            i_Game.Exit();
        }

        public static void UpdateScore(Sprite i_Sprite)
        {
            if (i_Sprite is Spaceship)
            {
                s_CurrentScore += (int) eScoreValue.LoseLife;
                if(s_CurrentScore < 0)
                {
                    s_CurrentScore = 0;
                }

                s_LifeManager.RemoveOneLife();
            }
            else if (i_Sprite is Enemy)
            {
                identifiesEnemyAndUpdateScore(i_Sprite);
                s_EnemyThatLeftToFinishGame--;
            }
            else if (i_Sprite is MotherShip)
            {
                s_CurrentScore += (int) eScoreValue.MotherShip;
            }
        }

        private static void identifiesEnemyAndUpdateScore(Sprite i_Sprite)
        {
            if (i_Sprite is Enemy)
            {
                if (i_Sprite.Tint == Color.Pink)
                {
                    s_CurrentScore += (int) eScoreValue.PinkEnemy;
                } 
                else if (i_Sprite.Tint == Color.LightBlue)
                {
                    s_CurrentScore += (int) eScoreValue.LightBlueEnemy;
                }
                else if (i_Sprite.Tint == Color.Yellow)
                {
                    s_CurrentScore += (int) eScoreValue.YellowEnemy;
                }
            }
        }

        public static List<Bullet> ListOfBullets => sr_ListOfBullets;
    }
}
