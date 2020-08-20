using System;
using System.Collections.Generic;
using System.Text;
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
            MouseState currState = Mouse.GetState();

            if (this.m_PrevMouseState != null)
            {
                retVal.X = currState.X - m_PrevMouseState.X;
                retVal.Y = currState.Y - m_PrevMouseState.Y;
            }

            m_PrevMouseState = currState;

            return retVal;
        }

    }
}
