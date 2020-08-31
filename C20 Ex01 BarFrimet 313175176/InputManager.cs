using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace C20_Ex01_BarFrimet_313175176
{
    public class InputManager
    {
        private MouseState m_PrevMouseState;
        private KeyboardState m_PrevKeyboardState;

        public Vector2 GetMousePositionDelta()
        {
            Vector2 retVal = Vector2.Zero;
            MouseState currentState = Mouse.GetState();

            retVal.X = currentState.X - m_PrevMouseState.X;
            retVal.Y = currentState.Y - m_PrevMouseState.Y;
            m_PrevMouseState = currentState;

            return retVal;
        }

        public float UserTryToMoveWithKeyboard(GameTime i_GameTime, float i_PositionX)
        {
            float newXPosition = i_PositionX;
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                newXPosition -= Spaceship.SpaceshipSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                newXPosition += Spaceship.SpaceshipSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }

            m_PrevKeyboardState = currentKeyboardState;

            return newXPosition;
        }

        public bool IsUserClickToShoot()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();
            bool isUserClickToShoot = false;
         //   bool isUserClickToShoot = ((m_PrevKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))
         //     || (m_PrevMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed));

            if ((m_PrevKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))
               || (m_PrevMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed))
            {
                isUserClickToShoot = !isUserClickToShoot;
            }

           m_PrevMouseState = currentMouseState;

            return isUserClickToShoot;
        }
    }
}
