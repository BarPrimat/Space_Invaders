using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;

namespace SpaceInvaders
{
    public class Player
    {
        private int m_CurrentScore = 0;
        private readonly Spaceship r_Spaceship;
        private readonly string r_Name;
        private readonly LifeManager r_LifeManager;
        private readonly List<Bullet> r_ListOfBullets = new List<Bullet>();

        public Player(Game i_Game, string i_Name, string i_TextureSpaceshipPath, int i_NumberOfThePlayer)
        {
            r_Name = i_Name;
            r_Spaceship = new Spaceship(i_Game, i_TextureSpaceshipPath, i_NumberOfThePlayer);
            r_LifeManager = new LifeManager(i_Game, i_TextureSpaceshipPath, GameDefinitions.NumberOfLifeToStart, i_NumberOfThePlayer * GameDefinitions.StartLifePositionHeight);
        }

        public LifeManager LifeManager => r_LifeManager;

        public int CurrentScore
        {
            get => m_CurrentScore;
            set => m_CurrentScore = value;
        }

        public string Name => r_Name;

        public Spaceship Spaceship => r_Spaceship;
    }
}
