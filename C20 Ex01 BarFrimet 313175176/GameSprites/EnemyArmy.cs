using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


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
        private const float k_EnemyIncreaseSpeedInEach5Dead = 0.03f;
        private const float k_EnemyIncreaseSpeedGoingDown = 0.06f;


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

        public EnemyArmy(Game i_Game, string i_TexturePath)
            : base(i_Game)
        {
            r_EnemyArray = new Enemy[k_NumberOfEnemyInRow, k_NumberOfEnemyInColumn];
            r_TexturePath = i_TexturePath;
            m_eDirectionMove = eDirectionMove.Right;
            i_Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_CurrentTopLeftX = 0;
            m_CurrentTopLeftY = k_VerticalSpaceBetweenEnemyAndTopEdge + k_EnemySize * k_NumberOfEnemyInColumn;
            InitPosition();
            m_FirstTimeSetup = false;
        }

        public void InitPosition()
        {
            float tempCurrentXPosition = m_CurrentTopLeftX;
            float xPosition = m_CurrentTopLeftX;
            float yPosition = m_CurrentTopLeftY;

            for (int i = 0; i < k_NumberOfEnemyInRow; i++)
            {
                for (int j = 0; j < k_NumberOfEnemyInColumn; j++)
                {
                    Vector2 position = new Vector2(xPosition, yPosition);

                    if (m_FirstTimeSetup)
                    {
                        r_EnemyArray[i, j] = new Enemy(Game, r_TexturePath, sr_EnemyArmyTint, position);
                    }

                    r_EnemyArray[i, j].Position = position;
                    xPosition += k_HorizontalSpaceBetweenEnemy + k_EnemySize;
                }

                yPosition -= k_VerticalSpaceBetweenEnemy + k_EnemySize;
                xPosition = tempCurrentXPosition;
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounter += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeDeltaCounter >= k_TimeUntilNextStepInSec)
            {
                if (m_MoveStepDown)
                {
                    m_CurrentTopLeftY += k_EnemySize / 2;
                    m_CurrentSpeed += k_EnemyIncreaseSpeedGoingDown;
                    m_MoveStepDown = false;
                }
                else
                {
                    float newMoveToAdd = k_EnemyStartSpeedInSec * k_TimeUntilNextStepInSec * m_CurrentSpeed;
                    float rightGroupBorder = getRightGroupBorder();
                    float leftGroupBorder = 0;

                    if (PreferredBackBufferWidth < rightGroupBorder + k_EnemySize && m_eDirectionMove == eDirectionMove.Right)
                    {
                        newMoveToAdd = PreferredBackBufferWidth - rightGroupBorder;
                        m_MoveStepDown = true;
                    }
                    else if (((leftGroupBorder = getLeftGroupBorder()) - k_EnemySize < 0 ) && m_eDirectionMove == eDirectionMove.Left)
                    {
                        newMoveToAdd = leftGroupBorder;
                        m_MoveStepDown = true;
                    }

                    m_CurrentTopLeftX += m_eDirectionMove == eDirectionMove.Right ? newMoveToAdd * 1 : newMoveToAdd * -1;
                }

                m_TimeDeltaCounter = 0;
                checkAndChangeMoveDirection();
                InitPosition();
            }
        }

        private void checkAndChangeMoveDirection()
        {
            if(PreferredBackBufferWidth <= getRightGroupBorder() + k_EnemySize)
            {
                m_eDirectionMove = eDirectionMove.Left;
            }
            else if(0 >= getLeftGroupBorder() - k_EnemySize)
            {
                m_eDirectionMove = eDirectionMove.Right;
            }
        }


        private float getRightGroupBorder() 
        { 
            float rightBorderX = 0; 
            bool isFound = false;

            for(int column = k_NumberOfEnemyInColumn - 1; column >= 0 && !isFound; column--)
            { 
                for(int row = 0; row < k_NumberOfEnemyInRow; row++) 
                { 
                    if(this.r_EnemyArray[row, column].Visible) 
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

            for (int column = 0; column < k_NumberOfEnemyInColumn && !isFound; column++) 
            { 
                for (int row = 0; row < k_NumberOfEnemyInRow; row++) 
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
    }
}
