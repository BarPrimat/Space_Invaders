using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSprites
{
    public class MotherShip : Sprite
    {
        private const float k_MotherShipSpeed = 95;
        private int m_RandomToNextAppears;
        private Random r_Random;
        private float m_TimeDeltaCounter = 0;
        private readonly GraphicsDeviceManager r_Graphics;

        public MotherShip(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath, Color i_Tint) : base(i_Graphics, i_Content, i_TexturePath, i_Tint)
        {
            r_Random = new Random();
            m_RandomToNextAppears = r_Random.Next(0, 30);
            r_Graphics = i_Graphics;
        }

        public override void InitPosition()
        {
            this.Position = new Vector2(-this.Texture.Width, this.Texture.Height);
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounter += (float) i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeDeltaCounter >= m_RandomToNextAppears)
            {
                float newXPosition = this.Position.X;

                newXPosition += k_MotherShipSpeed * (float) i_GameTime.ElapsedGameTime.TotalSeconds;
                SetXPosition(newXPosition);
                if (this.Position.X >= r_Graphics.GraphicsDevice.Viewport.Width)
                {
                    this.InitPosition();
                    m_TimeDeltaCounter = 0;
                    m_RandomToNextAppears = r_Random.Next(0, 30);
                }
            }
        }
    }
}
