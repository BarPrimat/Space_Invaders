using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders
{
    public class InputManager
    {
        // Can make problems with the order of operations therefore needed 2 pointers for the 2 faction
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

        public float UserTryToMoveWithKeyboard(GameTime i_GameTime, float i_PositionX, float i_SpaceshipSpeed)
        {
            float newXPosition = i_PositionX;
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                newXPosition -= i_SpaceshipSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            } 
            else if(currentKeyboardState.IsKeyDown(Keys.Right))
            {
                newXPosition += i_SpaceshipSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            }

            m_PrevKeyboardState = currentKeyboardState;

            return newXPosition;
        }

        public bool IsUserClickToShoot()
        {
            MouseState currentMouseState = Mouse.GetState();
            KeyboardState currentKeyboardState = Keyboard.GetState();
            bool isUserClickToShoot = ((m_PrevKeyboardStateToShoot.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter)) 
                                       || (m_PrevMouseStateToShoot.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed));
            
            m_PrevMouseStateToShoot = currentMouseState;
            m_PrevKeyboardStateToShoot = currentKeyboardState;

            return isUserClickToShoot;
        }
    }
}
