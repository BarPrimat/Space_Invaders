using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;
using Enum = C20_Ex01_BarFrimet_313175176.Enum;


namespace GameSprites
{
    public class EnemyArmy : DrawableGameComponent
    {
        private const float k_HorizontalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemy = 32 * 0.6f;
        private const float k_VerticalSpaceBetweenEnemyAndTopEdge = 32 * 3f;
        private const float k_EnemySize = 32;
        private const int k_NumberOfEnemyInRow = 5;
        private const int k_NumberOfEnemyInColumn = 9;
        private static readonly Color sr_EnemyArmyTint = Color.LightBlue;
        private const float k_EnemyStartSpeedInSec = 60;
        private const float k_NumberOfEnemyKilledToIncreaseSpeed = 5;
        private const float k_EnemyIncreaseSpeedInEach5Dead = 0.03f;
        private const float k_EnemyIncreaseSpeedGoingDown = 0.06f;

        private readonly Enemy[,] r_EnemyArray;
        private float m_CurrentTopLeftX;
        private float m_CurrentTopLeftY;
        private static float s_CurrentSpeed = 1;
        private Enum.eDirectionMove m_eDirectionMove;
        private bool m_FirstTimeSetup = true;
        private bool m_MoveStepDown = false;
        private readonly Firearm r_Firearm;
        private float m_TimeDeltaCounterToShoot;
        private readonly Random r_Random;
        private float m_EnemyNextTimeToShoot;
        private static int s_CounterOfEnemyBulletInAir = 0;
        private static int s_EnemyKilledCounter = 0;

        public EnemyArmy(Game i_Game) : base(i_Game)
        {
            r_EnemyArray = new Enemy[k_NumberOfEnemyInRow, k_NumberOfEnemyInColumn];
            m_eDirectionMove = Enum.eDirectionMove.Right;
            r_Firearm = new Firearm(i_Game, EnemyArmyMaxOfBullet, Enum.eBulletType.EnemyBullet);
            r_Random = new Random();
            m_EnemyNextTimeToShoot = r_Random.Next((int)GameDefinitions.EnemyMaxTimeForShoot);
            i_Game.Components.Add(this);
        }

        public static int CounterOfEnemyBulletInAir
        {
            get => s_CounterOfEnemyBulletInAir;
            set => s_CounterOfEnemyBulletInAir = value;
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
                        // First time creating the Army
                        initEnemyArmy(i, j);
                    }

                    r_EnemyArray[i, j].Position = position;
                    xPosition += k_HorizontalSpaceBetweenEnemy + k_EnemySize;
                }

                yPosition -= k_VerticalSpaceBetweenEnemy + k_EnemySize;
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
                case 1:
                    texturePath = SpritesDefinition.YellowEnemyAsset;
                    colorAsset = GameDefinitions.EnemyYellowTint;
                    break;
                case 2:
                case 3:
                    texturePath = SpritesDefinition.BlueLightEnemyAsset;
                    colorAsset = GameDefinitions.EnemyLightBlueTint;
                    break;
                case 4:
                    texturePath = SpritesDefinition.PinkEnemyAsset;
                    colorAsset = GameDefinitions.EnemyPinkTint;
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

        private void enemyArmyTryToShoot()
        {
            if(m_EnemyNextTimeToShoot <= m_TimeDeltaCounterToShoot)
            {
                int randomizeEnemyRow = r_Random.Next(k_NumberOfEnemyInRow - 1);
                int randomizeEnemyColumn = r_Random.Next(k_NumberOfEnemyInColumn - 1);
                if (r_EnemyArray[randomizeEnemyRow, randomizeEnemyColumn].Visible)
                {
                    r_Firearm.CreateNewBullet(r_EnemyArray[randomizeEnemyRow, randomizeEnemyColumn].Position);
                }

                m_EnemyNextTimeToShoot = r_Random.Next((int)GameDefinitions.EnemyMaxTimeForShoot);
                m_TimeDeltaCounterToShoot -= m_TimeDeltaCounterToShoot;
            }
        }

        private void enemyArmyMove(GameTime i_GameTime)
        {
            if (m_MoveStepDown)
            {
                m_CurrentTopLeftY += k_EnemySize / 2;
                s_CurrentSpeed += k_EnemyIncreaseSpeedGoingDown;
                m_MoveStepDown = false;
            }
            else
            {
                float newMoveToAdd = (float)i_GameTime.ElapsedGameTime.TotalSeconds * k_EnemyStartSpeedInSec * s_CurrentSpeed;

                float rightGroupBorder = getRightGroupBorder();
                float leftGroupBorder = getLeftGroupBorder();

                m_MoveStepDown = (PreferredBackBufferWidth < rightGroupBorder + k_EnemySize && m_eDirectionMove == Enum.eDirectionMove.Right)
                                 || (leftGroupBorder - k_EnemySize < 0 && m_eDirectionMove == Enum.eDirectionMove.Left);

                m_CurrentTopLeftX += m_eDirectionMove == Enum.eDirectionMove.Right ? newMoveToAdd * 1 : newMoveToAdd * -1;
            }
        }

        private void checkAndChangeMoveDirection()
        {
            if (PreferredBackBufferWidth <= getRightGroupBorder() + k_EnemySize)
            {
                m_eDirectionMove = Enum.eDirectionMove.Left;
            }
            else if (0 >= getLeftGroupBorder() - k_EnemySize)
            {
                m_eDirectionMove = Enum.eDirectionMove.Right;
            }
        }


        private float getRightGroupBorder()
        {
            float leftBorderXPosition = 0;
            bool isFound = false;

            for (int column = k_NumberOfEnemyInColumn - 1; column >= 0 && !isFound; column--)
            {
                for (int row = 0; row < k_NumberOfEnemyInRow; row++)
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

            for (int column = 0; column < k_NumberOfEnemyInColumn && !isFound; column++)
            {
                for (int row = 0; row < k_NumberOfEnemyInRow; row++)
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

            foreach (Sprite sprite in SpaceInvaders.ListOfSprites)
            {
                if (sprite is Spaceship)
                {
                    spaceShipRectangle = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, sprite.Texture.Width, sprite.Texture.Height);
                    spaceShipYPosition = sprite.Position.Y;
                }
            }

            if (m_CurrentTopLeftY + k_NumberOfEnemyInColumn * k_NumberOfEnemyInRow + spaceShipYPosition >= GraphicsDevice.Viewport.Height)
            {
                bool findHit = false;
                for (int row = k_NumberOfEnemyInRow - 1; row >= 0 && !findHit; row--)
                {
                    for(int column = 0; column < k_NumberOfEnemyInColumn; column++)
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
            if(s_EnemyKilledCounter % k_NumberOfEnemyKilledToIncreaseSpeed == 0)
            {
                s_CurrentSpeed += k_EnemyIncreaseSpeedInEach5Dead;
            }
        }

        public static void AddEnemyKilledByOne()
        {
            s_EnemyKilledCounter++;
            increaseSpeedIfEnemyKilled();
        }
    }
}
