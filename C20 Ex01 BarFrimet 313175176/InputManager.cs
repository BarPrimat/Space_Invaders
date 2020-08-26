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
        
        public Vector2 GetMousePositionDelta()
        {
            Vector2 retVal = Vector2.Zero;
            MouseState currentState = Mouse.GetState();

            retVal.X = currentState.X - m_PrevMouseState.X;
            retVal.Y = currentState.Y - m_PrevMouseState.Y;

            m_PrevMouseState = currentState;

            return retVal;
        }

        public float KeyboardXPositionToMove(GameTime i_GameTime, float i_PositionX)
        {
            float newXPosition = i_PositionX;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                newXPosition -= Spaceship.SpaceshipSpeed * (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                newXPosition += Spaceship.SpaceshipSpeed * (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            }

            return newXPosition;
        }
    }
}
