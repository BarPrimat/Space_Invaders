using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders;
using Microsoft.Xna.Framework;

namespace GameSprites
{
    public class LifeManager
    {
        private readonly List<Life> r_LifeList;
        private readonly string r_TexturePath;
        private readonly GameScreen r_GameScreen;
        private readonly int r_StartYPosition;
        private readonly int r_StartNumberOfLife;
        private int m_CounterOfLife;
        private Vector2 m_CurrentPosition;

        public LifeManager(GameScreen i_GameScreen, string i_TexturePath, int i_CounterOfLife, int i_YPosition)
        {
            m_CounterOfLife = 0;
            r_StartNumberOfLife = i_CounterOfLife;
            r_StartYPosition = i_YPosition;
            r_LifeList = new List<Life>();
            r_TexturePath = i_TexturePath;
            r_GameScreen = i_GameScreen;
            m_CurrentPosition = new Vector2(this.r_GameScreen.GraphicsDevice.Viewport.Width - GameDefinitions.LifeSize, r_StartYPosition);
            Initialize();
        }

        public void Initialize()
        {
            float currentXPosition = m_CurrentPosition.X;

            for (int i = m_CounterOfLife; i < r_StartNumberOfLife; i++)
            {
                Life life = new Life(r_GameScreen, r_TexturePath, m_CurrentPosition);
                life.Position = m_CurrentPosition;
                currentXPosition -= GameDefinitions.SpaceBetweenLife;
                m_CurrentPosition = new Vector2(currentXPosition, m_CurrentPosition.Y);
                m_CounterOfLife++;
                r_LifeList.Add(life);
            }
        }

        public bool IsNoMoreLifeRemains()
        {
            return r_LifeList.Count == 0;
        }

        public void RemoveOneLife()
        {
            r_LifeList[r_LifeList.Count - 1].RemoveComponent();
            r_LifeList.RemoveAt(r_LifeList.Count - 1);
            m_CurrentPosition = new Vector2(m_CurrentPosition.X + GameDefinitions.SpaceBetweenLife, m_CurrentPosition.Y);
            m_CounterOfLife--;
        }

        /**
         * Maybe in the future, we will want to add new life during the game
         */
        public void AddOneLife()
        {
            r_LifeList.Add(new Life(r_GameScreen, r_TexturePath, m_CurrentPosition));
            m_CounterOfLife++;
            float currentXPosition = m_CurrentPosition.X - GameDefinitions.SpaceBetweenLife;
            m_CurrentPosition = new Vector2(currentXPosition, m_CurrentPosition.Y);
        }

        public void InitForNextLevel()
        {
            Initialize();
        }
    }
}
