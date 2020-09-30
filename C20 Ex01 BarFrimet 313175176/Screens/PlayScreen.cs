using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders;

namespace Screens
{
    public class PlayScreen : GameScreen
    {
        private GameManager r_GameManager;

        public PlayScreen(Game i_Game, int i_Level) : base(i_Game)
        {
            r_GameManager = new GameManager(this, GameDefinitions.NumberOfPlayers, i_Level);

            this.Add(r_GameManager);
        }
    }
}
