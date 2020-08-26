using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace GameSprites
{
    public class EnemyArmy
    {
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
        private float m_CurrentSpeed = 1;
        private eDirectionMove m_eDirectionMove;
        private bool m_FirstTimeSetup = true;
        private bool m_MoveStepDown = false;
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly ContentManager r_ContentManager;

        public EnemyArmy(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath) 
        {
            r_EnemyArray = new Enemy[NumberOfEnemyInRow, NumberOfEnemyInColumn];
            r_TexturePath = i_TexturePath;
            m_eDirectionMove = eDirectionMove.Right;
            r_Graphics = i_Graphics;
            r_ContentManager = i_Content;
        }

        public void Initialize()
        {
            m_CurrentTopLeftX = 0;
            m_CurrentTopLeftY = VerticalSpaceBetweenEnemyAndTopEdge + EnemySize * NumberOfEnemyInColumn;
            InitPosition();
            m_FirstTimeSetup = false;
        }

        public void InitPosition()
        {
            float tempCurrentXPosition = m_CurrentTopLeftX;
            float xPosition = m_CurrentTopLeftX;
            float yPosition = m_CurrentTopLeftY;

            for (int i = 0; i < NumberOfEnemyInRow; i++)
            {
                for (int j = 0; j < NumberOfEnemyInColumn; j++)
                {
                    Vector2 position = new Vector2(xPosition, yPosition);

                    if (m_FirstTimeSetup)
                    {
                        r_EnemyArray[i, j] = new Enemy(r_Graphics, r_ContentManager, r_TexturePath, sr_EnemyArmyTint, position);
                        r_EnemyArray[i, j].Initialize();
                    }

                    r_EnemyArray[i, j].Position = position;
                    xPosition += HorizontalSpaceBetweenEnemy + EnemySize;
                }

                yPosition -= VerticalSpaceBetweenEnemy + EnemySize;
                xPosition = tempCurrentXPosition;
            }
        }

        public void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounter += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeDeltaCounter >= TimeUntilNextStepInSec)
            {
                if (m_MoveStepDown)
                {
                    m_CurrentTopLeftY += EnemySize / 2;
                    m_CurrentSpeed += EnemyIncreaseSpeedGoingDown;
                    m_MoveStepDown = false;
                }
                else
                {
                    float newMoveToAdd = EnemyStartSpeedInSec * TimeUntilNextStepInSec * m_CurrentSpeed;
                    float rightGroupBorder = getRightGroupBorder();
                    float leftGroupBorder = 0;

                    if (PreferredBackBufferWidth < rightGroupBorder + EnemySize && m_eDirectionMove == eDirectionMove.Right)
                    {
                        newMoveToAdd = PreferredBackBufferWidth - rightGroupBorder;
                        m_MoveStepDown = true;
                    }
                    else if (((leftGroupBorder = getLeftGroupBorder()) - EnemySize < 0 ) && m_eDirectionMove == eDirectionMove.Left)
                    {
                        newMoveToAdd = leftGroupBorder;
                        m_MoveStepDown = true;
                    }

                    m_CurrentTopLeftX += m_eDirectionMove == eDirectionMove.Right ? newMoveToAdd * 1 : newMoveToAdd * -1;
                }

                m_TimeDeltaCounter -= TimeUntilNextStepInSec;
                checkAndChangeMoveDirection();
                InitPosition();
            }
        }

        private void checkAndChangeMoveDirection()
        {
            if(PreferredBackBufferWidth <= getRightGroupBorder() + EnemySize)
            {
                m_eDirectionMove = eDirectionMove.Left;
            }
            else if(0 >= getLeftGroupBorder() - EnemySize)
            {
                m_eDirectionMove = eDirectionMove.Right;
            }
        }


        private float getRightGroupBorder() 
        { 
            float rightBorderX = 0; 
            bool isFound = false;

            for(int column = NumberOfEnemyInColumn - 1; column >= 0 && !isFound; column--)
            { 
                for(int row = 0; row < NumberOfEnemyInRow; row++) 
                { 
                    if(r_EnemyArray[row, column].Visible) 
                    { 
                        rightBorderX = r_EnemyArray[row, column].Position.X + r_EnemyArray[row, column].Texture.Width; 
                        isFound = true;
                    }
                }
            }
            
            return rightBorderX;
        }

        private float getLeftGroupBorder()
        { 
            float leftBorderX = 0; 
            bool isFound = false;

            for (int column = 0; column < NumberOfEnemyInColumn && !isFound; column++) 
            { 
                for (int row = 0; row < NumberOfEnemyInRow; row++) 
                { 
                    if (this.r_EnemyArray[row, column].Visible) 
                    { 
                        leftBorderX = r_EnemyArray[row, column].Position.X; 
                        isFound = true;
                    }
                }
            }
            
            return leftBorderX;
        }

        public void Draw(GameTime i_GameTime, SpriteBatch i_SpriteBatch)
        {
            for(int i = 0; i < NumberOfEnemyInRow; i++)
            {
                for(int j = 0; j < NumberOfEnemyInColumn; j++)
                { 
                    r_EnemyArray[i,j].Draw(i_GameTime, i_SpriteBatch);
                }
            }
        }
    }
}
