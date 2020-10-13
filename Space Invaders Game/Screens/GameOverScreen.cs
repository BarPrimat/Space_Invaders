using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Screens.MainMenuScreens;
using SpaceInvaders;

namespace Screens
{
    public class GameOverScreen : GameScreen
    {
        private readonly Background r_Background;
        private TextService m_TextToPrint;
        public event Action UserWantNewGame;
        public event Action UserWantLeaveGame;

        public GameOverScreen(Game i_Game)
            : base(i_Game)
        {
            this.IsModal = true;
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
        }

        public override void Initialize()
        {
            base.Initialize();
            Vector2 positionOfEndGameTextToPrint = new Vector2(this.CenterOfViewPort.X - this.CenterOfViewPort.X / 1.5f, this.CenterOfViewPort.Y - (this.CenterOfViewPort.Y / 2));
            string optionTextToPrint = string.Format(@"
{0}

press Esc to exit
press Home for a new game
press M for main menu", initText());

            m_TextToPrint = new TextService(optionTextToPrint, this, positionOfEndGameTextToPrint, GameDefinitions.TextColor);
        }

        private string initText()
        { 
            string endGameText = GameManager.PlayersList.Count < 2 ? GameDefinitions.EndGameText1Player : GameDefinitions.EndGameTextMoreThen1Player;
            string winnerText = "And the winner is:";
            string winnerNameText = String.Empty;
            int maxScore = -1; // There is no negative score
            bool isDraw = false;

            endGameText = getStringUsersScore(endGameText);
            if (GameManager.PlayersList.Count > 1)
            {
                foreach (Player player in GameManager.PlayersList)
                {
                    if (maxScore < player.LastScore)
                    {
                        maxScore = player.LastScore;
                        winnerNameText = string.Format(@"
{0}
{1} with the Score: {2}", winnerText, player.Name, player.LastScore);

                    }
                    else if (maxScore == player.LastScore)
                    {
                        winnerNameText = string.Format(@"
There is a draw 
maybe next time someone will win");
                        isDraw = !isDraw;
                    }
                }

                endGameText = isDraw ? winnerNameText : endGameText + winnerNameText;
            }

            return endGameText;
        }

        private static string getStringUsersScore(string i_EndGameText)
        {
            StringBuilder stringBuilder = new StringBuilder(i_EndGameText);

            foreach (Player player in GameManager.PlayersList)
            {
                stringBuilder.Append(String.Format(@"
    {0}", player.LastScoreString()));
            }

            return stringBuilder.ToString();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(InputManager.KeyPressed(Keys.Escape))
            {
                this.Game.Exit();
            }
            else if(InputManager.KeyPressed(Keys.Home))
            {
                this.ExitScreen();
                UserWantNewGame?.Invoke();
            }
            else if(InputManager.KeyPressed(Keys.M))
            {
                this.ExitScreen();
                UserWantLeaveGame?.Invoke();
            }
        }
    }
}