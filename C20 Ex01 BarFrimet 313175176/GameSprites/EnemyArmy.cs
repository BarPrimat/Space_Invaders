using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSprites
{
    public class EnemyArmy : DrawableGameComponent
    {
        private const float k_HorizontalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemyAndTopEdge = 32 * 3f;
        private const float k_EnemySize= 32;
        private const int k_NumberOfEnemyInRow = 5;
        private const int k_NumberOfEnemyInColumn = 9;
        private static readonly Color sr_EnemyArmyTint = Color.White;
        private const float k_TimeUntilNextStepInSec = 0.5f;
        // private const float k_HorizontalJumpInEachStep = ;
        private const float k_VerticalJumpInEachStep = k_EnemySize / 2;
        private const float k_EnemyStartSpeedInSec = 60;
        private const float k_EnemyIncreaseSpeedInSec = 60;

        public enum eDirectionMove
        {
            Left = -1,
            Right = 1
        }






        private readonly Enemy[,] r_EnemyArray;
        private readonly string r_TexturePath;
        private float m_TimeDeltaCounter = 0;
        private float m_CurrentTopLeftX;
        private float m_CurrentTopLeftY;
        private bool m_FirstTimeSetup = true;

        public EnemyArmy(Game i_Game, string i_TexturePath)
            : base(i_Game)
        {
            r_EnemyArray = new Enemy[k_NumberOfEnemyInRow, k_NumberOfEnemyInColumn];
            r_TexturePath = i_TexturePath;
            i_Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_CurrentTopLeftX = 0;
            m_CurrentTopLeftY = (float)GraphicsDevice.Viewport.Height - k_VerticalSpaceBetweenEnemyAndTopEdge - k_EnemySize;
            InitPosition();
            m_FirstTimeSetup = false;
        }

        public void InitPosition()
        {
            float xPosition = m_CurrentTopLeftX;
            float yPosition = m_CurrentTopLeftY;

            for (int i = 0; i < k_NumberOfEnemyInRow; i++)
            {
                for (int j = 0; j < k_NumberOfEnemyInColumn; j++)
                {
                    Vector2 position = new Vector2(xPosition, yPosition);
                    if(m_FirstTimeSetup)
                    {
                        r_EnemyArray[i, j] = new Enemy(Game, r_TexturePath, sr_EnemyArmyTint, position);
                    }

                    r_EnemyArray[i, j].Position = position;
                    xPosition += k_HorizontalSpaceBetweenEnemy + k_EnemySize;
                }

                yPosition -= (k_VerticalSpaceBetweenEnemy + k_EnemySize);
                xPosition = m_CurrentTopLeftX;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounter += (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            if (m_TimeDeltaCounter >= k_TimeUntilNextStepInSec)
            {
                //if(rightBoundry || leftBoundry)
               // {

                    m_CurrentTopLeftX += k_EnemyStartSpeedInSec * k_TimeUntilNextStepInSec;
                    m_TimeDeltaCounter = 0;
               // }
               InitPosition();
            }
        }
    }
}
