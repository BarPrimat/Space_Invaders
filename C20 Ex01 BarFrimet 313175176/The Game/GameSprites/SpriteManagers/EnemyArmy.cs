using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static SpaceInvaders.GameDefinitions;
using static SpaceInvaders.Enum;

namespace GameSprites
{
    public class EnemyArmy : GameComponent
    {
        private readonly List<List<Enemy>> r_EnemyArray;
        private readonly int r_NumberOfRow;
        private int m_NumberOfColumn;
        private int m_EnemyThatLeft;
        private float m_CurrentSpeed;
        private float m_CurrentTopLeftX;
        private float m_CurrentTopLeftY;
        private float m_TimeDeltaCounterToShoot;
        private float m_EnemyNextTimeToShoot;
        private float m_TimeDeltaCounterToMove;
        private bool m_FirstTimeSetup;
        private bool m_MoveStepDown = false;
        private eDirectionMove m_eDirectionMove;
        private readonly Random r_Random;
        private readonly GameScreen r_GameScreen;
        private static float s_TimeBetweenJumpsInSec;
        private static bool s_IsTimeBetweenJumpsChanged;
        public event Action EnemyReachSpaceShip;
        public event Action AllEnemyAreDead;

        public EnemyArmy(GameScreen i_GameScreen) : base(i_GameScreen.Game)
        {
            r_GameScreen = i_GameScreen;
            r_NumberOfRow = GameDefinitions.NumberOfEnemyInRow;
            r_EnemyArray = new List<List<Enemy>>();
            r_Random = new Random();
            m_NumberOfColumn = GameDefinitions.NumberOfEnemyInColumn;
            for (int i = 0; i < r_NumberOfRow; i++)
            {
                r_EnemyArray.Add(new List<Enemy>());
            }

            setupNewArmy();
            i_GameScreen.Add(this);
        }

        private void setupNewArmy()
        {
            int newNumberOfColumnInNewLevel = GameDefinitions.NumberOfEnemyInColumn + ((GameManager.CurrentLevel - 1) % GameDefinitions.MaxOfDifficultyLevel);

            if (m_NumberOfColumn > newNumberOfColumnInNewLevel)
            {
                for (int i = 0; i < r_NumberOfRow; i++)
                {
                    for (int j = m_NumberOfColumn - 1; j > newNumberOfColumnInNewLevel - 1; j--)
                    {
                        r_EnemyArray[i][j].RemoveComponent();
                        r_EnemyArray[i].RemoveAt(j);
                    }
                }
            }

            m_NumberOfColumn = newNumberOfColumnInNewLevel;
            m_EnemyThatLeft = r_NumberOfRow * m_NumberOfColumn;
            m_eDirectionMove = eDirectionMove.Right;
            m_EnemyNextTimeToShoot = r_Random.Next((int)EnemyMaxTimeForShoot);
            s_TimeBetweenJumpsInSec = GameDefinitions.StartTimeBetweenJumpsInSec;
            s_IsTimeBetweenJumpsChanged = true;
            m_MoveStepDown = false;
            m_TimeDeltaCounterToMove = 0;

            m_CurrentTopLeftX = 0;
            m_CurrentSpeed = 1;
            m_CurrentTopLeftY = GameDefinitions.VerticalSpaceBetweenEnemyAndTopEdge;


            m_FirstTimeSetup = true;
            InitPosition();
            m_FirstTimeSetup = false;
        }

        public void InitPosition()
        {
            float tempCurrentXPosition = m_CurrentTopLeftX;
            float xPosition = m_CurrentTopLeftX;
            float yPosition = m_CurrentTopLeftY;

            int newNumberOfColumnInNewLevel = GameDefinitions.NumberOfEnemyInColumn + ((GameManager.CurrentLevel - 1) % GameDefinitions.MaxOfDifficultyLevel);

            for (int i = 0; i < r_NumberOfRow; i++)
            {
                for (int j = 0; j < m_NumberOfColumn; j++)
                {
                    Vector2 position = new Vector2(xPosition, yPosition);

                    if (m_FirstTimeSetup)
                    {
                        if (j > r_EnemyArray[i].Count - 1 && j <= newNumberOfColumnInNewLevel)
                        {
                            initEnemyArmy(i, j);
                        }
                        else
                        {
                            r_EnemyArray[i][j].BackToLifeIfNeeded();
                        }

                        /*
                        if(r_EnemyArray[i].Count > )
                        if (m_NumberOfColumn > newNumberOfColumnInNewLevel)
                        {
                            removeColumns();
                        }
                        else if (m_NumberOfColumn < newNumberOfColumnInNewLevel)
                        {
                            addNewColumn();
                        }

                        */
                        // First time creating the Army
                        // initEnemyArmy(i, j);
                    }

                    r_EnemyArray[i][j].Position = position;
                    xPosition += HorizontalSpaceBetweenEnemy + EnemySizeWidth;
                }

                yPosition += VerticalSpaceBetweenEnemy + EnemySizeHeight;
                xPosition = tempCurrentXPosition;
            }

            if (s_IsTimeBetweenJumpsChanged)
            {
                s_IsTimeBetweenJumpsChanged = false;
            }
        }

        public void InitForNextLevel()
        {
            setupNewArmy();
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

            if (i_Row % 2 == 1)
            {
                columnIndexInPicture = 1;
            }

            Enemy newEnemy = new Enemy(r_GameScreen, texturePath, colorAsset, rowIndexInPicture, columnIndexInPicture, GameDefinitions.EnemyNumberOfAssetChangesInSec, GameDefinitions.EnemyNumberOfAssetInRow);
            r_EnemyArray[i_Row].Add(newEnemy);
            r_EnemyArray[i_Row][i_Column].EnemyIsKilled += OneEnemyIsDead_EnemyIsKilled;
        }

        private void OneEnemyIsDead_EnemyIsKilled(object? i_Sender, EventArgs i_E)
        {
            m_EnemyThatLeft--;
            increaseSpeedIfEnemyKilled();
            if (m_EnemyThatLeft == 0)
            {
                AllEnemyAreDead?.Invoke();
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounterToMove += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_TimeDeltaCounterToShoot += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            enemyArmyMove(i_GameTime);
            InitPosition();
            enemyArmyTryToShoot();
            if (isEnemyReachSpaceShipHeight())
            {
                EnemyReachSpaceShip?.Invoke();
            }
        }

        private void enemyArmyMove(GameTime i_GameTime)
        {
            if (m_TimeDeltaCounterToMove >= s_TimeBetweenJumpsInSec)
            {
                if (m_MoveStepDown)
                {
                    m_CurrentTopLeftY += EnemyStepDown;
                    m_CurrentSpeed += EnemyIncreaseSpeedGoingDown;
                    s_TimeBetweenJumpsInSec -= s_TimeBetweenJumpsInSec * IncreaseTimeBetweenJumpsInSec;
                    m_MoveStepDown = false;
                    s_IsTimeBetweenJumpsChanged = true;
                    m_TimeDeltaCounterToMove = 0;
                }
                else
                {
                    float newMoveToAdd = EnemyStartSpeedInSec * s_TimeBetweenJumpsInSec * m_CurrentSpeed;
                    float rightGroupBorder = getRightGroupBorder();
                    float leftGroupBorder = getLeftGroupBorder();

                    if (!m_MoveStepDown && (this.Game.GraphicsDevice.Viewport.Width <= rightGroupBorder + Math.Max(EnemySizeWidth, newMoveToAdd)) && m_eDirectionMove == eDirectionMove.Right)
                    {
                        newMoveToAdd = this.Game.GraphicsDevice.Viewport.Width - rightGroupBorder;
                    }
                    else if (!m_MoveStepDown && (leftGroupBorder - Math.Max(EnemySizeWidth, newMoveToAdd) <= 0) && m_eDirectionMove == eDirectionMove.Left)
                    {
                        newMoveToAdd = leftGroupBorder;
                    }

                    m_MoveStepDown = (this.Game.GraphicsDevice.Viewport.Width < rightGroupBorder + EnemySizeWidth && m_eDirectionMove == eDirectionMove.Right)
                                     || (leftGroupBorder - EnemySizeHeight < 0 && m_eDirectionMove == eDirectionMove.Left);

                    m_CurrentTopLeftX += m_eDirectionMove == eDirectionMove.Right ? newMoveToAdd * 1 : newMoveToAdd * -1;
                    checkAndChangeMoveDirection();
                    m_TimeDeltaCounterToMove -= s_TimeBetweenJumpsInSec;
                }
            }
        }

        private void enemyArmyTryToShoot()
        {
            if (m_EnemyNextTimeToShoot <= m_TimeDeltaCounterToShoot)
            {
                int randomizeEnemyRow = r_Random.Next(r_NumberOfRow);
                int randomizeEnemyColumn = r_Random.Next(m_NumberOfColumn);
                if (r_EnemyArray[randomizeEnemyRow][randomizeEnemyColumn].Visible && !r_EnemyArray[randomizeEnemyRow][randomizeEnemyColumn].IsDying)
                {
                    r_EnemyArray[randomizeEnemyRow][randomizeEnemyColumn].Shoot();
                }

                m_EnemyNextTimeToShoot = r_Random.Next((int)GameDefinitions.EnemyMaxTimeForShoot);
                m_TimeDeltaCounterToShoot -= m_TimeDeltaCounterToShoot;
            }
        }

        private void checkAndChangeMoveDirection()
        {
            if (this.Game.GraphicsDevice.Viewport.Width <= getRightGroupBorder() + r_EnemyArray[0][0].WidthBeforeScale)
            {
                m_eDirectionMove = eDirectionMove.Left;
            }
            else if (0 >= getLeftGroupBorder() - r_EnemyArray[0][0].WidthBeforeScale)
            {
                m_eDirectionMove = eDirectionMove.Right;
            }
        }

        private float getRightGroupBorder()
        {
            float rightBorderXPosition = 0;
            bool isFound = false;

            for (int column = m_NumberOfColumn - 1; column >= 0 && !isFound; column--)
            {
                for (int row = 0; row < r_NumberOfRow; row++)
                {
                    if (r_EnemyArray[row][column].Visible)
                    {
                        rightBorderXPosition = r_EnemyArray[row][column].Position.X + r_EnemyArray[row][column].WidthBeforeScale;
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

            for (int column = 0; column < m_NumberOfColumn && !isFound; column++)
            {
                for (int row = 0; row < r_NumberOfRow; row++)
                {
                    if (r_EnemyArray[row][column].Visible)
                    {
                        leftBorderXPosition = r_EnemyArray[row][column].Position.X;
                        isFound = true;
                    }
                }
            }

            return leftBorderXPosition;
        }

        private bool isEnemyReachSpaceShipHeight()
        {
            float spaceShipYPosition = this.Game.GraphicsDevice.Viewport.Height - GameDefinitions.SpaceshipYOffsetStartPosition;
            bool findHit = false;

            // No need to enter the loop is the enemy army can not hit nothing
            if (m_CurrentTopLeftY + m_NumberOfColumn * r_NumberOfRow + spaceShipYPosition >= this.Game.GraphicsDevice.Viewport.Height)
            {
                for (int row = r_NumberOfRow - 1; row >= 0 && !findHit; row--)
                {
                    for (int column = 0; column < m_NumberOfColumn && !findHit; column++)
                    {
                        if (r_EnemyArray[row][column].Visible && !r_EnemyArray[row][column].IsDying && (r_EnemyArray[row][column].Position.Y + r_EnemyArray[row][column].Height >= spaceShipYPosition))
                        {
                            findHit = !findHit;
                        }
                    }
                }
            }

            return findHit;
        }

        private void increaseSpeedIfEnemyKilled()
        {
            if ((r_EnemyArray.Count + r_EnemyArray[0].Count - m_EnemyThatLeft) % NumberOfEnemyKilledToIncreaseSpeed == 0)
            {
                m_CurrentSpeed += EnemyIncreaseSpeedEveryXDead;
            }
        }

        public int EnemyThatLeft
        {
            get => m_EnemyThatLeft;
            set => m_EnemyThatLeft = value;
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