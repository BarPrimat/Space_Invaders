using System;
using System.Collections.Generic;
using System.Text;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSprites
{
    public class LifeManager
    {
        private readonly Vector2 r_StartPosition;
        private readonly List<Life> r_LifeArray;
        private readonly string r_TexturePath;
        private readonly Game r_Game;
        private int m_CounterOfLife;

        public LifeManager(Game i_Game, string i_TexturePath, int i_CounterOfLife)
        {
            m_CounterOfLife = i_CounterOfLife;
            r_StartPosition = new Vector2(GameDefinitions.StartLifePositionWidth - GameDefinitions.LifeSize, GameDefinitions.StartLifePositionHeight);
            r_LifeArray = new List<Life>();
            r_TexturePath = i_TexturePath;
            r_Game = i_Game;
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < m_CounterOfLife; i++)
            {
                r_LifeArray.Add(new Life(r_Game, r_TexturePath));
            }

            InitPosition();
        }

        public void InitPosition()
        {
            float currentXPosition = r_StartPosition.X;

            foreach (Life life in r_LifeArray)
            {
                life.Position = new Vector2(currentXPosition, life.Position.Y);
                currentXPosition -= GameDefinitions.SpaceBetweenLife;
            }
        }

        public bool IsNoMoreLifeRemains()
        {
            return r_LifeArray.Count == 0;
        }


        public void RemoveOneLife()
        {
            r_LifeArray[m_CounterOfLife - 1].RemoveComponent();
            r_LifeArray.RemoveAt(m_CounterOfLife - 1);
            m_CounterOfLife--;
        }

        /**
         * Maybe in the future, we will want to add new life during the game
         */
        public void AddOneLife()
        {
            r_LifeArray.Add(new Life(r_Game, r_TexturePath));
            m_CounterOfLife++;
        }
    }
}
