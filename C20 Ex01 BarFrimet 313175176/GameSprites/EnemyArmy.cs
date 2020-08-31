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
        private const float k_TimeUntilNextStepInSec = 0.5f;
        // private const float k_HorizontalJumpInEachStep = ;
        private const float k_VerticalJumpInEachStep = k_EnemySize / 2;
        private const float k_EnemyStartSpeedInSec = 60;
        private const float k_EnemyIncreaseSpeedInEach5Dead = 0.03f;
        private const float k_EnemyIncreaseSpeedGoingDown = 0.06f;

        private readonly Enemy[,] r_EnemyArray;
        private readonly string r_TexturePath;
        private float m_TimeDeltaCounterToMove = 0;
        private float m_CurrentTopLeftX;
        private float m_CurrentTopLeftY;
        private float m_CurrentSpeed = 1;
        private Enum.eDirectionMove m_eDirectionMove;
        private bool m_FirstTimeSetup = true;
        private bool m_MoveStepDown = false;
        private readonly Firearm r_Firearm;
        private float m_TimeDeltaCounterToShoot;
        private readonly Random r_Random;
        private float m_EnemyNextTimeToShoot;

        public EnemyArmy(Game i_Game, string i_TexturePath) : base(i_Game)
        {
            r_EnemyArray = new Enemy[k_NumberOfEnemyInRow, k_NumberOfEnemyInColumn];
            r_TexturePath = i_TexturePath;
            m_eDirectionMove = Enum.eDirectionMove.Right;
            r_Firearm = new Firearm(i_Game, SpaceshipMaxOfBullet, Bullet.eBulletType.EnemyBullet);
            r_Random = new Random();
            m_EnemyNextTimeToShoot = r_Random.Next((int)GameDefinitions.EnemyMaxTimeForShoot);
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
            m_TimeDeltaCounterToMove += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            m_TimeDeltaCounterToShoot += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeDeltaCounterToMove >= k_TimeUntilNextStepInSec)
            {
                enemyArmyMove(i_GameTime);
                enemyArmyTryToShoot();
                m_TimeDeltaCounterToMove = 0;
                checkAndChangeMoveDirection();
                InitPosition();
                if(isEnemyHitFloorOrSpaceShip())
                {
                    GameManager.ShowScoreAndEndGame(Game);
                }

                m_TimeDeltaCounterToMove -= k_TimeUntilNextStepInSec;
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
                m_CurrentSpeed += k_EnemyIncreaseSpeedGoingDown;
                m_MoveStepDown = false;
            }
            else
            {
                float newMoveToAdd = k_EnemyStartSpeedInSec * k_TimeUntilNextStepInSec * m_CurrentSpeed;
                float rightGroupBorder = getRightGroupBorder();
                float leftGroupBorder = 0;

                if (PreferredBackBufferWidth < rightGroupBorder + k_EnemySize && m_eDirectionMove == Enum.eDirectionMove.Right)
                {
                    newMoveToAdd = PreferredBackBufferWidth - rightGroupBorder;
                    m_MoveStepDown = true;
                }
                else if (((leftGroupBorder = getLeftGroupBorder()) - k_EnemySize < 0) && m_eDirectionMove == Enum.eDirectionMove.Left)
                {
                    newMoveToAdd = leftGroupBorder;
                    m_MoveStepDown = true;
                }

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
                        leftBorderXPosition = r_EnemyArray[row, column].Position.X + r_EnemyArray[row, column].Texture.Width;
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
                        leftBorderXPosition = r_EnemyArray[row, column].Position.X;
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

            if (m_CurrentTopLeftY + k_NumberOfEnemyInColumn * k_NumberOfEnemyInRow >= GraphicsDevice.Viewport.Height)
            {
                foreach(Sprite sprite in SpaceInvaders.ListOfSprites)
                {
                    if(sprite is Spaceship)
                    {
                        spaceShipRectangle = new Rectangle((int)sprite.Position.X - sprite.Texture.Width, (int)sprite.Position.Y - sprite.Texture.Height, sprite.Texture.Width, sprite.Texture.Height);
                    }
                }

                for (int row = k_NumberOfEnemyInRow - 1; row >= 0; row--)
                {
                    for(int column = 0; column < k_NumberOfEnemyInColumn; column++)
                    {
                        isEnemyHitWasSomething = isEnemyHitSomething(r_EnemyArray[row, column], spaceShipRectangle);
                    }
                }
            }

            return isEnemyHitWasSomething;
        }

        private bool isEnemyHitSomething(Enemy i_Enemy, Rectangle i_SpaceShipRectangle)
        {
            bool isEnemyHitSomething = false;

            if (i_Enemy.Visible){
                if(i_Enemy.Position.Y + i_Enemy.Texture.Height >= GraphicsDevice.Viewport.Height)
                {
                    isEnemyHitSomething = !isEnemyHitSomething;
                }
                else
                {
                    Rectangle enemyRectangle = new Rectangle((int)i_Enemy.Position.X, (int)i_Enemy.Position.Y, i_Enemy.Texture.Width, i_Enemy.Texture.Height);

                    if (i_SpaceShipRectangle.Intersects(enemyRectangle))
                    {
                        isEnemyHitSomething = !isEnemyHitSomething;
                    }
                }
            }

            return isEnemyHitSomething;
        }
    }
}
