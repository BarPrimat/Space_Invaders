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

        public LifeManager(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath, int i_CounterOfLife)
        {
            m_CounterOfLife = i_CounterOfLife;
            r_StartPosition = new Vector2(StartLifePositionWidth - LifeSize, StartLifePositionHeight);
            r_LifeArray = new List<Sprite>();
            r_Graphics = i_Graphics;
            r_ContentManager = i_Content;
            r_TexturePath = i_TexturePath;
        }

        public void Initialize()
        {
            for (int i = 0; i < m_CounterOfLife; i++)
            {
                r_LifeArray.Add(new Life(r_Graphics, r_ContentManager, r_TexturePath));
                r_LifeArray[i].Initialize();
            }

            InitPosition();
        }

        public void InitPosition()
        {
            float currentXPosition = r_StartPosition.X;

            foreach (Life life in r_LifeArray)
            {
                life.SetXPosition(currentXPosition);
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
            r_LifeArray.Add(new Life(r_Graphics, r_ContentManager, r_TexturePath));
            m_CounterOfLife++;
        }

        public void Draw(GameTime i_GameTime, SpriteBatch i_SpriteBatch)
        {
            foreach (Life life in r_LifeArray)
            {
                life.Draw(i_GameTime, i_SpriteBatch);
            }
        }
    }
}
