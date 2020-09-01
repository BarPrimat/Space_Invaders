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
        // CCan make problems with the order of operations therefore needed 2 pointers
        private MouseState m_PrevMouseStateToMove;
        private MouseState m_PrevMouseStateToShoot;
        private KeyboardState m_PrevKeyboardState;
        private KeyboardState m_PrevKeyboardStateToShoot;


        public Vector2 GetMousePositionDelta()
        {
            Vector2 retVal = Vector2.Zero;
            MouseState currentState = Mouse.GetState();

            retVal.X = currentState.X - m_PrevMouseStateToMove.X;
            retVal.Y = currentState.Y - m_PrevMouseStateToMove.Y;
            m_PrevMouseStateToMove = currentState;

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
            MouseState currentMouseState = Mouse.GetState();
            KeyboardState currentKeyboardState = Keyboard.GetState();
            bool isUserClickToShoot = ((m_PrevKeyboardStateToShoot.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyUp(Keys.Enter)) 
                                       || (m_PrevMouseStateToShoot.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed));
            
            m_PrevMouseStateToShoot = currentMouseState;
            m_PrevKeyboardStateToShoot = currentKeyboardState;

            return isUserClickToShoot;
        }
    }
}
