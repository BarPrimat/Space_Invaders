using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace GameSprites
{
    public class LifeManager
    {
        private readonly Vector2 r_StartPosition;
        private int m_CounterOfLife;
        private readonly List<Sprite> r_LifeArray;
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly ContentManager r_ContentManager;
        private readonly string r_TexturePath;
        private Game m_Game;

        public LifeManager(Game i_Game, string i_TexturePath, int i_CounterOfLife)
        {
            m_CounterOfLife = i_CounterOfLife;
            r_StartPosition = new Vector2(StartLifePositionWidth - LifeSize, StartLifePositionHeight);
            r_LifeArray = new List<Sprite>();
            r_TexturePath = i_TexturePath;
            m_Game = i_Game;
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < m_CounterOfLife; i++)
            {
                r_LifeArray.Add(new Life(m_Game, r_TexturePath));
            }

            InitPosition();
        }

        public void InitPosition()
        {
            float currentXPosition = r_StartPosition.X;

            foreach (Life life in r_LifeArray)
            {
                life.Position = new Vector2(currentXPosition, life.Position.Y);
                currentXPosition -= SpaceBetweenLife;
            }
        }

        public void RemoveOneLife()
        {
            r_LifeArray.RemoveAt(m_CounterOfLife);
            m_CounterOfLife--;
        }

        public void AddOneLife()
        {
            r_LifeArray.Add(new Life(m_Game, r_TexturePath));
            m_CounterOfLife++;
        }
    }
}
