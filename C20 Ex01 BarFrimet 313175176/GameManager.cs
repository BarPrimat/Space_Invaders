using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using static SpaceInvaders.Enum;


namespace SpaceInvaders
{
    public class GameManager : GameComponent
    {
        private int m_EnemyThatLeftToFinishGame;
        private static readonly List<Player> sr_PlayersList = new List<Player>();
        private static readonly List<Bullet> sr_ListOfBullets = new List<Bullet>();

        public GameManager(Game i_Game, int i_NumberOfPlayers) : base(i_Game)
        {
            m_EnemyThatLeftToFinishGame = GameDefinitions.NumberOfEnemyInColumn * GameDefinitions.NumberOfEnemyInRow;
            for(int i = 0; i < i_NumberOfPlayers; i++)
            {
                string assetPath = i == 0 ? SpritesDefinition.SpaceshipUser1Asset : SpritesDefinition.SpaceshipUser2Asset;
                sr_PlayersList.Add(new Player(i_Game, "P " + i, assetPath, i));
            }

            i_Game.Components.Add(this);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_EnemyThatLeftToFinishGame == 0)
            {
                ShowScoreAndEndGame(Game);
            } 
            else
            {
                int numberOfAlivePlayers = sr_PlayersList.Count;
                foreach (Player player in sr_PlayersList)
                {
                    if(player.LifeManager.IsNoMoreLifeRemains())
                    {
                        numberOfAlivePlayers--;
                    }
                }

                if(numberOfAlivePlayers == 0)
                {
                    ShowScoreAndEndGame(Game);
                }
            }
        }


        public void UpdateScore(Sprite i_Sprite, int i_NumberOfPlayer)
        {
            if (i_Sprite is Spaceship)
            {
                sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.LoseLife;
                if (sr_PlayersList[i_NumberOfPlayer].CurrentScore < 0)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore = 0;
                }

                sr_PlayersList[i_NumberOfPlayer].LifeManager.RemoveOneLife();
            }
            else if (i_Sprite is Enemy)
            {
                identifiesEnemyAndUpdateScore(i_Sprite, i_NumberOfPlayer);
                m_EnemyThatLeftToFinishGame--;
            }
            else if (i_Sprite is MotherShip)
            {
                sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.MotherShip;
            }
        }

        private void identifiesEnemyAndUpdateScore(Sprite i_Sprite, int i_NumberOfPlayer)
        {
            if (i_Sprite is Enemy)
            {
                if (i_Sprite.Tint == Color.Pink)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.PinkEnemy;
                }
                else if (i_Sprite.Tint == Color.LightBlue)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.LightBlueEnemy;
                }
                else if (i_Sprite.Tint == Color.Yellow)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.YellowEnemy;
                }
            }
        }

        public void ShowScoreAndEndGame(Game i_Game)
        {
            string endGameText = sr_PlayersList.Count < 2 ? string.Format("{0}{1}{2}: {3}", GameDefinitions.EndGameText1Player, sr_PlayersList[0].Name, sr_PlayersList[0].CurrentScore) 
                                     : GameDefinitions.EndGameTextMoreThen1Player;
            string winnerText = "And the winner is:";
            string winnerNameText = String.Empty;
            int maxScore = -1; // There is no negative score
            bool isDraw = false;

            if (sr_PlayersList.Count > 1)
            {
                foreach (Player player in sr_PlayersList)
                {
                    if (maxScore < player.CurrentScore)
                    {
                        maxScore = player.CurrentScore;
                        winnerNameText = string.Format(@"
{0}
{1}", winnerText, player.Name);

                    }
                    else if (maxScore == player.CurrentScore)
                    {
                        winnerNameText = "There is a draw maybe next time someone will win";
                        isDraw = !isDraw;
                    }
                }

                endGameText = isDraw ? winnerNameText : endGameText + winnerNameText;
            }

            System.Windows.Forms.MessageBox.Show(endGameText, GameDefinitions.EndGameCaption);
            i_Game.Exit();
        }

        public static List<Bullet> ListOfBullets => sr_ListOfBullets;

        public static List<Player> PlayersList => sr_PlayersList;
    }
}
