using System;
using System.Collections.Generic;
using System.Text;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static SpaceInvaders.GameDefinitions;
using static SpaceInvaders.Enum;


namespace GameSprites
{
    public class EnemyArmy : DrawableGameComponent
    {
        private readonly Enemy[,] r_EnemyArray;
        private readonly Firearm r_Firearm;
        private readonly Random r_Random;
        private float m_CurrentTopLeftX;
        private float m_CurrentTopLeftY;
        private float m_TimeDeltaCounterToShoot;
        private float m_EnemyNextTimeToShoot;
        private bool m_FirstTimeSetup = true;
        private bool m_MoveStepDown = false;
        private eDirectionMove m_eDirectionMove;
        private static float s_CurrentSpeed = 1;
        private static int s_CounterOfEnemyBulletInAir = 0;
        private static int s_EnemyKilledCounter = 0;

        public EnemyArmy(Game i_Game) : base(i_Game)
        {
            r_EnemyArray = new Enemy[GameDefinitions.NumberOfEnemyInRow, GameDefinitions.NumberOfEnemyInColumn];
            m_eDirectionMove = eDirectionMove.Right;
            r_Firearm = new Firearm(i_Game, EnemyArmyMaxOfBullet, eBulletType.EnemyBullet);
            r_Random = new Random();
            m_EnemyNextTimeToShoot = r_Random.Next((int) EnemyMaxTimeForShoot);
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
        }

        private void initEnemyArmy(int i_Row, int i_Column)
        {
            string texturePath = null;
            Color colorAsset = Color.White;

            // The number represent the line number
            switch (i_Row)
            {
                case 0:
                    texturePath = SpritesDefinition.PinkEnemyAsset;
                    colorAsset = GameDefinitions.PinkEnemyTint;
                    break;
                case 1:
                case 2:
                    texturePath = SpritesDefinition.LightBlueEnemyAsset;
                    colorAsset = GameDefinitions.LightBlueEnemyTint;
                    break;
                case 3:
                case 4:
                    texturePath = SpritesDefinition.YellowEnemyAsset;
                    colorAsset = GameDefinitions.YellowEnemyTint;
                    break;
            }

            r_EnemyArray[i_Row, i_Column] = new Enemy(Game, texturePath, colorAsset);
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounterToShoot += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            enemyArmyMove(i_GameTime);
            enemyArmyTryToShoot();
            checkAndChangeMoveDirection();
            InitPosition();
            if(isEnemyHitFloorOrSpaceShip())
            {
                GameManager.ShowScoreAndEndGame(Game);
            }
        }

        private void enemyArmyMove(GameTime i_GameTime)
        {
            if (m_MoveStepDown)
            {
                m_CurrentTopLeftY += EnemySizeHeight / 2;
                s_CurrentSpeed += EnemyIncreaseSpeedGoingDown;
                m_MoveStepDown = false;
            }
            else
            {
                float newMoveToAdd = (float)i_GameTime.ElapsedGameTime.TotalSeconds * EnemyStartSpeedInSec * s_CurrentSpeed;
                float rightGroupBorder = getRightGroupBorder();
                float leftGroupBorder = getLeftGroupBorder();

                m_MoveStepDown = (PreferredBackBufferWidth < rightGroupBorder + EnemySizeWidth && m_eDirectionMove == eDirectionMove.Right)
                                 || (leftGroupBorder - EnemySizeHeight < 0 && m_eDirectionMove == eDirectionMove.Left);

                m_CurrentTopLeftX += m_eDirectionMove == eDirectionMove.Right ? newMoveToAdd * 1 : newMoveToAdd * -1;
            }
        }

        private void enemyArmyTryToShoot()
        {
            if(m_EnemyNextTimeToShoot <= m_TimeDeltaCounterToShoot)
            {
                int randomizeEnemyRow = r_Random.Next(NumberOfEnemyInRow);
                int randomizeEnemyColumn = r_Random.Next(NumberOfEnemyInColumn);
                if (r_EnemyArray[randomizeEnemyRow, randomizeEnemyColumn].Visible)
                {
                    r_Firearm.CreateNewBullet(r_EnemyArray[randomizeEnemyRow, randomizeEnemyColumn].Position);
                }

                m_EnemyNextTimeToShoot = r_Random.Next((int)GameDefinitions.EnemyMaxTimeForShoot);
                m_TimeDeltaCounterToShoot -= m_TimeDeltaCounterToShoot;
            }
        }

        private void checkAndChangeMoveDirection()
        {
            if (GraphicsDevice.Viewport.Width <= getRightGroupBorder() + r_EnemyArray[0, 0].Texture.Width)
            {
                m_eDirectionMove = eDirectionMove.Left;
            }
            else if (0 >= getLeftGroupBorder() - r_EnemyArray[0, 0].Texture.Width)
            {
                m_eDirectionMove = eDirectionMove.Right;
            }
        }

        private float getRightGroupBorder()
        {
            float leftBorderXPosition = 0;
            bool isFound = false;

            for (int column = NumberOfEnemyInColumn - 1; column >= 0 && !isFound; column--)
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
                        leftBorderXPosition = r_EnemyArray[row, column].Position.X + r_EnemyArray[row, column].Texture.Width;
                        isFound = true;
                    }
                }
            }

            return leftBorderXPosition;
        }

        private bool isEnemyHitFloorOrSpaceShip()
        {
            bool isEnemyHitWasSomething = false;
            Rectangle spaceShipRectangle = default;
            float spaceShipYPosition = 0;

            // Find Spaceship sprite her for better efficiency
            foreach (Sprite sprite in SpaceInvadersGame.ListOfSprites)
            {
                if (sprite is Spaceship)
                {
                    spaceShipRectangle = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, sprite.Texture.Width, sprite.Texture.Height);
                    spaceShipYPosition = sprite.Position.Y;
                }
            }

            if (m_CurrentTopLeftY + NumberOfEnemyInColumn * NumberOfEnemyInRow + spaceShipYPosition >= GraphicsDevice.Viewport.Height)
            {
                bool findHit = false;
                for (int row = NumberOfEnemyInRow - 1; row >= 0 && !findHit; row--)
                {
                    for(int column = 0; column < NumberOfEnemyInColumn; column++)
                    {
                        if(isEnemyHitWasSomething = isEnemyHitSomething(row, column, spaceShipRectangle))
                        {
                            findHit = !findHit;
                            break;
                        }
                    }
                }
            }

            return isEnemyHitWasSomething;
        }

        private bool isEnemyHitSomething(int i_Row, int i_Column, Rectangle i_SpaceShipRectangle)
        {
            bool isEnemyHitSomething = false;

            if (r_EnemyArray[i_Row, i_Column].Visible){
                if (r_EnemyArray[i_Row, i_Column].Position.Y + r_EnemyArray[i_Row, i_Column].Texture.Height >= GraphicsDevice.Viewport.Height)
                {
                    isEnemyHitSomething = !isEnemyHitSomething;
                }
                else
                {
                    Rectangle enemyRectangle = new Rectangle((int)r_EnemyArray[i_Row, i_Column].Position.X, (int)r_EnemyArray[i_Row, i_Column].Position.Y, r_EnemyArray[i_Row, i_Column].Texture.Width, r_EnemyArray[i_Row, i_Column].Texture.Height);

                    if (i_SpaceShipRectangle.Intersects(enemyRectangle))
                    {
                        isEnemyHitSomething = !isEnemyHitSomething;
                    }
                }
            }

            return isEnemyHitSomething;
        }

        private static void increaseSpeedIfEnemyKilled()
        {
            if(s_EnemyKilledCounter % NumberOfEnemyKilledToIncreaseSpeed == 0)
            {
                s_CurrentSpeed += EnemyIncreaseSpeedInEach5Dead;
            }
        }

        public static void AddEnemyKilledByOne()
        {
            s_EnemyKilledCounter++;
            increaseSpeedIfEnemyKilled();
        }

        public static int CounterOfEnemyBulletInAir
        {
            get => s_CounterOfEnemyBulletInAir;
            set => s_CounterOfEnemyBulletInAir = value;
        }
    }
}
