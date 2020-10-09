using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using GameSprites;
using Infrastructure;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static SpaceInvaders.Enum;


namespace SpaceInvaders
{
    public class GameManager : GameComponent
    {
        private const int k_StartLevel = 1;
        // It is not necessary to save the elements game but they may be used in the future
        private readonly Background r_Background;
        private readonly MotherShip r_MotherShip;
        private readonly EnemyArmy r_EnemyArmy;
        private readonly BarriersGroup r_BarriersGroup;
        private int m_EnemyThatLeftToFinishGame;
        private int m_NumberOfAlivePlayers;
        private static List<Player> s_PlayersList;
        private static int s_Level;
        private readonly GameScreen r_GameScreen;

        public event Action GameIsOver;
        public event Action GoToNextLevel;

        public GameManager(GameScreen i_GameScreen, int i_NumberOfPlayers) : base(i_GameScreen.Game)
        {
            new CollisionsManager(i_GameScreen.Game);
            s_Level = k_StartLevel;
            r_GameScreen = i_GameScreen;
            // It is not necessary to save the elements game but they may be used in the future
            r_Background = new Background(i_GameScreen, SpritesDefinition.BackgroundAsset);
            r_MotherShip = new MotherShip(i_GameScreen, SpritesDefinition.MotherSpaceShipAsset, Color.Red);
            r_EnemyArmy = new EnemyArmy(i_GameScreen);
            r_EnemyArmy.AllEnemyAreDead += GoToNextLevel_AllEnemyAreDead;
            r_EnemyArmy.EnemyReachSpaceShip += gameIsOver_EnemyReachSpaceShip;
            r_BarriersGroup = new BarriersGroup(i_GameScreen, SpritesDefinition.BarrierAsset, GameDefinitions.NumberOfBarrier);
            s_PlayersList = new List<Player>();

            for (int i = 0; i < i_NumberOfPlayers; i++)
            {
                string assetPath = i == 0 ? SpritesDefinition.SpaceshipUser1Asset : SpritesDefinition.SpaceshipUser2Asset;
                Keys leftKey = i == 0 ? GameDefinitions.FirstPlayerKeyToLeft : GameDefinitions.SecondPlayerKeyToLeft;
                Keys rightKey = i == 0 ? GameDefinitions.FirstPlayerKeyToRight : GameDefinitions.SecondPlayerKeyToRight;
                Keys shootKey = i == 0 ? GameDefinitions.FirstPlayerKeyToShoot : GameDefinitions.SecondPlayerKeyToShoot;
                Player newPlayer =  new Player(i_GameScreen, "P " + (i + 1), assetPath, i, rightKey, leftKey, shootKey);
                newPlayer.Spaceship.SpaceShipIsDead += oneSpaceshipIsDead_SpaceShipIsDead;
                s_PlayersList.Add(newPlayer);
            }

            m_NumberOfAlivePlayers = s_PlayersList.Count;
            i_GameScreen.Add(this);
        }

        public void DeleteAllEnemyAndGoToNextLevel()
        {
            // r_EnemyArmy.DeleteAllEnemyAndGoToNextLevel();
        }

        public void GoToNextLevel_AllEnemyAreDead()
        {
            s_Level++;
            GoToNextLevel?.Invoke();
        }

        public void InitForNextLevel()
        {
            foreach (Player player in s_PlayersList)
            {
                player.InitForNextLevel();
            }

            m_NumberOfAlivePlayers = s_PlayersList.Count;
            r_EnemyArmy.InitForNextLevel();
            r_BarriersGroup.InitForNextLevel();
            r_MotherShip.InitForNextLevel();
        }

        private void oneSpaceshipIsDead_SpaceShipIsDead()
        {
            m_NumberOfAlivePlayers--;

            if (m_NumberOfAlivePlayers == 0)
            {
                GameIsOverAndRestGame();
            }
        }

        private void gameIsOver_EnemyReachSpaceShip()
        {
            GameIsOverAndRestGame();
        }

        public void GameIsOverAndRestGame()
        {
            s_Level = 1;
            foreach (Player player in PlayersList)
            {
                player.CurrentScore = 0;
            }

            GameIsOver?.Invoke();
        }

        public static void UpdateScore(Sprite i_Sprite, int i_NumberOfPlayer)
        {
            if(i_NumberOfPlayer != -1)
            {
                if (i_Sprite is Spaceship)
                {
                    s_PlayersList[i_NumberOfPlayer].CurrentScore += (int) Enum.eScoreValue.LoseLife;
                    if (s_PlayersList[i_NumberOfPlayer].CurrentScore < 0)
                    {
                        s_PlayersList[i_NumberOfPlayer].CurrentScore = 0;
                    }
                }
                else if (i_Sprite is Enemy)
                {
                    identifiesEnemyAndUpdateScore(i_Sprite, i_NumberOfPlayer);
                }
                else if (i_Sprite is MotherShip)
                {
                    s_PlayersList[i_NumberOfPlayer].CurrentScore += (int) Enum.eScoreValue.MotherShip;
                }

                s_PlayersList[i_NumberOfPlayer].UpdateScoreBoardText();
            }
        }

        private static void identifiesEnemyAndUpdateScore(Sprite i_Sprite, int i_NumberOfPlayer)
        {
            int increaseScoreInEachLevel = GameDefinitions.NumberOfIncreaseScoreInEachLevel * ((s_Level - 1) % GameDefinitions.MaxOfDifficultyLevel);

            if (i_Sprite is Enemy)
            {
                s_PlayersList[i_NumberOfPlayer].CurrentScore += increaseScoreInEachLevel;
                if (i_Sprite.TintColor == Color.Pink)
                {
                    s_PlayersList[i_NumberOfPlayer].CurrentScore += (int) Enum.eScoreValue.PinkEnemy;
                }
                else if (i_Sprite.TintColor == Color.LightBlue)
                {
                    s_PlayersList[i_NumberOfPlayer].CurrentScore += (int) Enum.eScoreValue.LightBlueEnemy;
                }
                else if (i_Sprite.TintColor == Color.Yellow)
                {
                    s_PlayersList[i_NumberOfPlayer].CurrentScore += (int) Enum.eScoreValue.YellowEnemy;
                }
            }
        }

        public static void ShowScoreAndEndGame(Game i_Game)
        {
            string endGameText = s_PlayersList.Count < 2 ? GameDefinitions.EndGameText1Player : GameDefinitions.EndGameTextMoreThen1Player;
            string winnerText = "And the winner is:";
            string winnerNameText = String.Empty;
            int maxScore = -1; // There is no negative score
            bool isDraw = false;

            endGameText = getStringUsersScore(endGameText);
            if (s_PlayersList.Count > 1)
            {
                foreach (Player player in s_PlayersList)
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

        private static string getStringUsersScore(string i_EndGameText)
        {
            StringBuilder stringBuilder = new StringBuilder(i_EndGameText);

            foreach (Player player in s_PlayersList)
            {
                stringBuilder.Append(String.Format(@"
    {0}", player.ToString()));
            }

            return stringBuilder.ToString();
        }

        public static List<Player> PlayersList => s_PlayersList;

        public static int CurrentLevel
        {
            get => s_Level;
            set => s_Level = value;
        }
    }
}