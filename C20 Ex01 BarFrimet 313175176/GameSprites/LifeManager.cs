using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace GameSprites
{
    public class LifeManager : DrawableGameComponent
    {
        private readonly Vector2 r_StartPosition;
        private int m_CounterOfLife;
        private readonly List<Sprite> r_LifeArray;
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly ContentManager r_ContentManager;
        private readonly string r_TexturePath;

        public LifeManager(Game i_Game, string i_TexturePath, int i_CounterOfLife) : base(i_Game)
        {
            m_CounterOfLife = i_CounterOfLife;
            r_StartPosition = new Vector2(StartLifePositionWidth - LifeSize, StartLifePositionHeight);
            r_LifeArray = new List<Sprite>();
            r_TexturePath = i_TexturePath;
            i_Game.Components.Add(this);
        }

        public void Initialize()
        {
            for (int i = 0; i < m_CounterOfLife; i++)
            {
                r_LifeArray.Add(new Life(Game, r_TexturePath));
                r_LifeArray[i].Initialize();
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
            r_LifeArray.Add(new Life(Game, r_TexturePath));
            m_CounterOfLife++;
        }
    }
}
