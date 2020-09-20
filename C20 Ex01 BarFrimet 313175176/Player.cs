using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GameSprites;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace SpaceInvaders
{
    public class Player : GameComponent
    {
        private int m_CurrentScore = 0;
        private IInputManager m_InputManager;
        private TextService m_ScoreBoardText;
        private readonly Spaceship r_Spaceship;
        private readonly bool r_MouseIsAllowed;
        private readonly string r_Name;
        private readonly LifeManager r_LifeManager;
        private readonly Keys r_RightMoveKey;
        private readonly Keys r_LeftMoveKey;
        private readonly Keys r_ShootKey;
        private readonly int r_SerialNumber;

        public Player(Game i_Game, string i_Name, string i_TextureSpaceshipPath, int i_SerialNumberOfPlayer, Keys i_RightMoveKey, Keys i_LeftMoveKey, Keys i_ShootKey)
            : base(i_Game)
        {
            r_SerialNumber = i_SerialNumberOfPlayer;
            r_Name = i_Name;
            r_LifeManager = new LifeManager(i_Game, i_TextureSpaceshipPath, GameDefinitions.NumberOfLifeToStart, r_SerialNumber * GameDefinitions.StartLifePositionHeight);
            r_Spaceship = new Spaceship(i_Game, i_TextureSpaceshipPath, r_SerialNumber, r_LifeManager);
            r_RightMoveKey = i_RightMoveKey;
            r_LeftMoveKey = i_LeftMoveKey;
            r_ShootKey = i_ShootKey;
            r_MouseIsAllowed = r_SerialNumber == GameDefinitions.PlayerThatAllowedToMouse;
            setupScoreBoardText();
            i_Game.Components.Add(this);
        }

        private void setupScoreBoardText()
        {
            Color colorOfText = r_SerialNumber == 0 ? Color.Blue : Color.Green;
            Vector2 positionOfText = new Vector2(GameDefinitions.SpaceBetweenLeftEdgeAndTextInScoreBoard, (r_SerialNumber + 1) * GameDefinitions.SpaceBetweenTextInScoreBoard);
            m_ScoreBoardText = new TextService(SpritesDefinition.TextBoardScoreFont, this.Game, positionOfText, colorOfText);
            UpdateScoreBoardText();
        }

        public override void Initialize()
        {
            if (m_InputManager == null)
            {
                m_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            }

            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            spaceshipMoveAndShoot(i_GameTime);
        }

        private void spaceshipMoveAndShoot(GameTime i_GameTime)
        {
            float maxBoundaryWithoutOffset = Game.GraphicsDevice.Viewport.Width - r_Spaceship.Texture.Width;
            float newXPosition = r_Spaceship.Position.X;

            if (m_InputManager.KeyPressed(r_ShootKey) || (r_MouseIsAllowed && m_InputManager.ButtonPressed(eInputButtons.Left)))
            {
                r_Spaceship.Shoot();
            }

            if (m_InputManager.KeyPressed(r_LeftMoveKey) || m_InputManager.KeyHeld(r_LeftMoveKey))
            {
                newXPosition -= r_Spaceship.SpaceshipSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (m_InputManager.KeyPressed(r_RightMoveKey) || m_InputManager.KeyHeld(r_RightMoveKey))
            {
                newXPosition += r_Spaceship.SpaceshipSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }

            r_Spaceship.SetupNewPosition(newXPosition, maxBoundaryWithoutOffset);
            if (r_MouseIsAllowed)
            {
                newXPosition = r_Spaceship.Position.X + m_InputManager.MousePositionDelta.X;
                r_Spaceship.SetupNewPosition(newXPosition, maxBoundaryWithoutOffset);
            }
        }

        public void UpdateScoreBoardText()
        {
            m_ScoreBoardText.TextToPrint = ToString();
        }

        public string ToString()
        {
            return string.Format("{0}: {1}", r_Name, m_CurrentScore);
        }

        public LifeManager LifeManager => r_LifeManager;

        public int CurrentScore
        {
            get => m_CurrentScore;
            set => m_CurrentScore = value;
        }

        public string Name => r_Name;

        public Spaceship Spaceship => r_Spaceship;

        public int SerialNumber => r_SerialNumber;
    }
}
