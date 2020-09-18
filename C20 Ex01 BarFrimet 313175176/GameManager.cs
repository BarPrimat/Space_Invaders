using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using GameSprites;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static SpaceInvaders.Enum;


namespace SpaceInvaders
{
    public class GameManager : GameComponent
    {
        private int m_EnemyThatLeftToFinishGame;
        private static readonly List<Player> sr_PlayersList = new List<Player>();
        // It is not necessary to save the elements game but they may be used in the future
        private readonly Background r_Background;
        private readonly MotherShip r_MotherShip;
        private readonly EnemyArmy r_EnemyArmy;
        private readonly BarriersGroup r_BarriersGroup;

        public GameManager(Game i_Game, int i_NumberOfPlayers) : base(i_Game)
        {
            m_EnemyThatLeftToFinishGame = GameDefinitions.NumberOfEnemyInColumn * GameDefinitions.NumberOfEnemyInRow;
            // It is not necessary to save the elements game but they may be used in the future
            r_Background = new Background(i_Game, SpritesDefinition.BackgroundAsset);
            r_MotherShip = new MotherShip(i_Game, SpritesDefinition.MotherSpaceShipAsset, Color.Red);
            r_EnemyArmy = new EnemyArmy(i_Game);
            r_BarriersGroup = new BarriersGroup(i_Game, SpritesDefinition.BarrierAsset, GameDefinitions.NumberOfBarrier);
            new CollisionsManager(i_Game);

            for (int i = 0; i < i_NumberOfPlayers; i++)
            {
                string assetPath = i == 0 ? SpritesDefinition.SpaceshipUser1Asset : SpritesDefinition.SpaceshipUser2Asset;
                Keys leftKey = i == 0 ? GameDefinitions.FirstPlayerKeyToLeft : GameDefinitions.SecondPlayerKeyToLeft;
                Keys rightKey = i == 0 ? GameDefinitions.FirstPlayerKeyToRight : GameDefinitions.SecondPlayerKeyToRight;
                Keys shootKey = i == 0 ? GameDefinitions.FirstPlayerKeyToShoot : GameDefinitions.SecondPlayerKeyToShoot;
                sr_PlayersList.Add(new Player(i_Game, "P " + (i + 1), assetPath, i, rightKey, leftKey, shootKey));
            }

            i_Game.Components.Add(this);
        }

        public override void Update(GameTime i_GameTime)
        {

            if (EnemyArmy.EnemyThatLeft == 0)
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

        public static void UpdateScore(Sprite i_Sprite, int i_NumberOfPlayer)
        {
            if(i_NumberOfPlayer != -1)
            {
                if (i_Sprite is Spaceship)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.LoseLife;
                    if (sr_PlayersList[i_NumberOfPlayer].CurrentScore < 0)
                    {
                        sr_PlayersList[i_NumberOfPlayer].CurrentScore = 0;
                    }
                }
                else if (i_Sprite is Enemy)
                {
                    identifiesEnemyAndUpdateScore(i_Sprite, i_NumberOfPlayer);
                }
                else if (i_Sprite is MotherShip)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.MotherShip;
                }
            }
        }

        private static void identifiesEnemyAndUpdateScore(Sprite i_Sprite, int i_NumberOfPlayer)
        {
            if (i_Sprite is Enemy)
            {
                if (i_Sprite.TintColor == Color.Pink)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.PinkEnemy;
                }
                else if (i_Sprite.TintColor == Color.LightBlue)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.LightBlueEnemy;
                }
                else if (i_Sprite.TintColor == Color.Yellow)
                {
                    sr_PlayersList[i_NumberOfPlayer].CurrentScore += (int)Enum.eScoreValue.YellowEnemy;
                }
            }
        }

        public static void ShowScoreAndEndGame(Game i_Game)
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
{1} with the Score: {2}", winnerText, player.Name , player.CurrentScore);

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

        public static List<Player> PlayersList => sr_PlayersList;
    }
}
