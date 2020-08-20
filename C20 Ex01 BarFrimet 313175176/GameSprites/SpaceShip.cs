using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameSprites
{
    public class SpaceShip : Sprite
    {
        private static readonly Color sr_SpaceShipTint = Color.White;
        private static InputManager m_InputManager;
        private float m_ShipDirection = 1f;

        public SpaceShip(Game i_Game, string i_TexturePath)
            : base(i_Game, i_TexturePath, sr_SpaceShipTint)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            m_InputManager = new InputManager();
            InitPosition();
        }

        public override void InitPosition()
        {
            // 1. init the ship position
            // Get the bottom and center:
            float x = (float)GraphicsDevice.Viewport.Width;
            float y = (float)GraphicsDevice.Viewport.Height;

            // Offset:
            x -= this.Texture.Width;
            y -= this.Texture.Height;

            // Put it a little bit higher:
            y -= 10;
            this.m_Position = new Vector2(x, y);
        }

        MouseState m_PrevMouseState;
        public override void Update(GameTime i_GameTime)
        {
            /*
            // get the current input devices state:
            GamePadState currGamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState currKeyboardState = Keyboard.GetState();

            // move the ship using the GamePad left thumb stick and set viberation according to movement:
            Vector2 position = this.Position;

            position.X += currGamePadState.ThumbSticks.Left.X * 120 * (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            GamePad.SetVibration(PlayerIndex.One, 0, Math.Abs(currGamePadState.ThumbSticks.Left.X));

            // move the ship using the mouse:
            position.X = m_InputManager.GetMousePositionDelta().X;

            // clam the position between screen boundries:
            position.X = MathHelper.Clamp(position.X, 0, this.GraphicsDevice.Viewport.Width - this.Texture.Width);

            // if we hit the wall, lets change direction:
            if (position.X == 0 || position.X == this.GraphicsDevice.Viewport.Width - this.Texture.Width)
            {
                m_ShipDirection *= -1f;
            }

            this.Position = position;
            base.Update(i_GameTime);
            //  this.moveUsingKeyboard(i_GameTime);
            //  this.moveUsingMouse();
            // this.Position = new Vector2(MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Texture.Width), Position.Y);
            */
            moveSpaceShip(i_GameTime);
        }

        private void moveSpaceShip(GameTime i_GameTime)
        {
            moveUsingKeyboard(i_GameTime);
            float newPositionWithBoundary = MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Texture.Width);
            this.Position = new Vector2(newPositionWithBoundary, Position.Y);
        }

        private void moveUsingMouse()
        {
            this.Position = new Vector2(this.Position.X + m_InputManager.GetMousePositionDelta().X, Position.Y);
        }
        private void moveUsingKeyboard(GameTime i_GameTime)
        {
            if (SpaceInvaders.s_GameUtils.InputOutputManager.IsUserAskedToMoveLeft())
            {
                this.Position = new Vector2(this.Position.X - (k_KeyboardVelocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds), this.Position.Y);
            }
            else if (SpaceInvaders.s_GameUtils.InputOutputManager.IsUserAskedToMoveRight())
            {
                this.Position = new Vector2(this.Position.X + (k_KeyboardVelocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds), this.Position.Y);
            }
        }


    }
}
