using System;
using System.Collections.Generic;
using System.Text;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static SpaceInvaders.GameDefinitions;
using static SpaceInvaders.Enum;
//using Sprite = Infrastructure.ObjectModel.Sprite;

namespace GameSprites
{
    public class EnemyArmy : GameComponent
    {
        private readonly Enemy[,] r_EnemyArray;
        private readonly Random r_Random;
        private float m_CurrentTopLeftX;
        private float m_CurrentTopLeftY;
        private float m_TimeDeltaCounterToShoot;
        private float m_EnemyNextTimeToShoot;
        private float m_TimeDeltaCounterToMove;
        private bool m_FirstTimeSetup = true;
        private bool m_MoveStepDown = false;
        private eDirectionMove m_eDirectionMove;
        private static float s_CurrentSpeed = 1;
        private static float s_TimeBetweenJumpsInSec;
        private static int s_EnemyThatLeft = 0;
        private static bool s_IsTimeBetweenJumpsChanged = false;

        public EnemyArmy(Game i_Game) : base(i_Game)
        {
            r_EnemyArray = new Enemy[GameDefinitions.NumberOfEnemyInRow, GameDefinitions.NumberOfEnemyInColumn];
            s_EnemyThatLeft = GameDefinitions.NumberOfEnemyInRow * GameDefinitions.NumberOfEnemyInColumn;
            m_eDirectionMove = eDirectionMove.Right;
            r_Random = new Random();
            m_EnemyNextTimeToShoot = r_Random.Next((int) EnemyMaxTimeForShoot);
            s_TimeBetweenJumpsInSec = GameDefinitions.StartTimeBetweenJumpsInSec;
            i_Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_CurrentTopLeftX = 0;
            m_CurrentTopLeftY = VerticalSpaceBetweenEnemyAndTopEdge;
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
                        // First time creating the Army
                        initEnemyArmy(i, j);
                    }

                    r_EnemyArray[i, j].Position = position;
                    xPosition += HorizontalSpaceBetweenEnemy + EnemySizeWidth;
                }

                yPosition += VerticalSpaceBetweenEnemy + EnemySizeHeight;
                xPosition = tempCurrentXPosition;
            }

            if(s_IsTimeBetweenJumpsChanged)
            {
                s_IsTimeBetweenJumpsChanged = false;
            }
        }

        private void initEnemyArmy(int i_Row, int i_Column)
        {
            string texturePath = null;
            Color colorAsset = Color.White;
            int rowIndexInPicture = 0;
            int columnIndexInPicture = 0;

            // The number represent the line number
            switch (i_Row)
            {
                case 0:
                    texturePath = SpritesDefinition.PinkEnemyAsset;
                    colorAsset = GameDefinitions.PinkEnemyTint;
                    rowIndexInPicture = 0;
                    break;
                case 1:
                case 2:
                    texturePath = SpritesDefinition.LightBlueEnemyAsset;
                    colorAsset = GameDefinitions.LightBlueEnemyTint;
                    rowIndexInPicture = 1;
                    break;
                case 3:
                case 4:
                    texturePath = SpritesDefinition.YellowEnemyAsset;
                    colorAsset = GameDefinitions.YellowEnemyTint;
                    rowIndexInPicture = 2;
                    break;
            }

            if(i_Row % 2 == 1)
            {
                columnIndexInPicture = 1;
            }

            r_EnemyArray[i_Row, i_Column] = new Enemy(Game, texturePath, colorAsset, rowIndexInPicture, columnIndexInPicture, GameDefinitions.EnemyNumberOfAssetChangesInSec, GameDefinitions.EnemyNumberOfAssetInRow);
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounterToMove += (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            m_TimeDeltaCounterToShoot += (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            enemyArmyMove(i_GameTime);
            InitPosition();
            enemyArmyTryToShoot();
            if (isEnemyReachSpaceShipHeight())
            {
                GameManager.ShowScoreAndEndGame(this.Game);
            }
        }


        private void enemyArmyMove(GameTime i_GameTime)
        {
            if (m_TimeDeltaCounterToMove >= s_TimeBetweenJumpsInSec)
            {
                if (m_MoveStepDown)
                {
                    m_CurrentTopLeftY += EnemyStepDown;
                    s_CurrentSpeed += EnemyIncreaseSpeedGoingDown;
                    s_TimeBetweenJumpsInSec -= s_TimeBetweenJumpsInSec * IncreaseTimeBetweenJumpsInSec;
                    m_MoveStepDown = false;
                    s_IsTimeBetweenJumpsChanged = true;
                }
                else
                {
                    float newMoveToAdd = EnemyStartSpeedInSec * s_TimeBetweenJumpsInSec * s_CurrentSpeed;
                    float rightGroupBorder = getRightGroupBorder();
                    float leftGroupBorder = getLeftGroupBorder();

                    if (!m_MoveStepDown && (PreferredBackBufferWidth <= rightGroupBorder + Math.Max(EnemySizeWidth, newMoveToAdd)) && m_eDirectionMove == eDirectionMove.Right)
                    {
                        newMoveToAdd = PreferredBackBufferWidth - rightGroupBorder;
                    }
                    else if (!m_MoveStepDown && (leftGroupBorder - Math.Max(EnemySizeWidth, newMoveToAdd) <= 0) && m_eDirectionMove == eDirectionMove.Left)
                    {
                        newMoveToAdd = leftGroupBorder;
                    }

                    m_MoveStepDown = (PreferredBackBufferWidth < rightGroupBorder + EnemySizeWidth && m_eDirectionMove == eDirectionMove.Right)
                                     || (leftGroupBorder - EnemySizeHeight < 0 && m_eDirectionMove == eDirectionMove.Left);

                    m_CurrentTopLeftX += m_eDirectionMove == eDirectionMove.Right ? newMoveToAdd * 1 : newMoveToAdd * -1;
                    checkAndChangeMoveDirection();
                }

                m_TimeDeltaCounterToMove -= StartTimeBetweenJumpsInSec;
            }
        }

        private void enemyArmyTryToShoot()
        {
            if(m_EnemyNextTimeToShoot <= m_TimeDeltaCounterToShoot)
            {
                int randomizeEnemyRow = r_Random.Next(NumberOfEnemyInRow);
                int randomizeEnemyColumn = r_Random.Next(NumberOfEnemyInColumn);
                if (r_EnemyArray[randomizeEnemyRow, randomizeEnemyColumn].Visible && !r_EnemyArray[randomizeEnemyRow, randomizeEnemyColumn].IsDying)
                {
                    r_EnemyArray[randomizeEnemyRow, randomizeEnemyColumn].Shoot();
                }

                m_EnemyNextTimeToShoot = r_Random.Next((int) GameDefinitions.EnemyMaxTimeForShoot);
                m_TimeDeltaCounterToShoot -= m_TimeDeltaCounterToShoot;
            }
        }

        private void checkAndChangeMoveDirection()
        {
            if (this.Game.GraphicsDevice.Viewport.Width <= getRightGroupBorder() + r_EnemyArray[0, 0].WidthBeforeScale)
            {
                m_eDirectionMove = eDirectionMove.Left;
            }
            else if (0 >= getLeftGroupBorder() - r_EnemyArray[0, 0].WidthBeforeScale)
            {
                m_eDirectionMove = eDirectionMove.Right;
            }
        }

        private float getRightGroupBorder()
        {
            float rightBorderXPosition = 0;
            bool isFound = false;

            for (int column = NumberOfEnemyInColumn - 1; column >= 0 && !isFound; column--)
            {
                for (int row = 0; row < NumberOfEnemyInRow; row++)
                {
                    if (r_EnemyArray[row, column].Visible)
                    {
                        rightBorderXPosition = r_EnemyArray[row, column].Position.X + r_EnemyArray[row, column].WidthBeforeScale;
                        isFound = true;
                    }
                }
            }

            return rightBorderXPosition;
        }

        private float getLeftGroupBorder()
        {
            float leftBorderXPosition = 0;
            bool isFound = false;

            for (int column = 0; column < NumberOfEnemyInColumn && !isFound; column++)
            {
                for (int row = 0; row < NumberOfEnemyInRow; row++)
                {
                    if (r_EnemyArray[row, column].Visible)
                    {
                        leftBorderXPosition = r_EnemyArray[row, column].Position.X;
                        isFound = true;
                    }
                }
            }

            return leftBorderXPosition;
        }

        private bool isEnemyReachSpaceShipHeight()
        {
            float spaceShipYPosition = GameDefinitions.SpaceshipYStartPosition;
            bool findHit = false;

            // No need to enter the loop is the enemy army can not hit nothing
            if (m_CurrentTopLeftY + NumberOfEnemyInColumn * NumberOfEnemyInRow + spaceShipYPosition >= this.Game.GraphicsDevice.Viewport.Height)
            {
                for (int row = NumberOfEnemyInRow - 1; row >= 0 && !findHit; row--)
                {
                    for(int column = 0; column < NumberOfEnemyInColumn && !findHit; column++)
                    {
                        if (r_EnemyArray[row, column].Visible && !r_EnemyArray[row, column].IsDying && (r_EnemyArray[row, column].Position.Y + r_EnemyArray[row, column].Height >= spaceShipYPosition))
                        {
                            findHit = !findHit;
                        }
                    }
                }
            }
            
            return findHit;
        }

        private static void increaseSpeedIfEnemyKilled()
        {
            if(s_EnemyThatLeft % NumberOfEnemyKilledToIncreaseSpeed == 0)
            {
                s_CurrentSpeed += EnemyIncreaseSpeedEveryXDead;
            }
        }

        public static void SubtractionEnemyByOne()
        {
            s_EnemyThatLeft--;
            increaseSpeedIfEnemyKilled();
        }

        public static  int EnemyThatLeft
        {
            get => s_EnemyThatLeft;
            set => s_EnemyThatLeft = value;
        }
        public static float TimeBetweenJumpsInSec
        {
            get => s_TimeBetweenJumpsInSec;
            set => s_TimeBetweenJumpsInSec = value;
        }
        public static bool IsTimeBetweenJumpsChanged
        {
            get => s_IsTimeBetweenJumpsChanged;
            set => s_IsTimeBetweenJumpsChanged = value;
        }
    }
}
