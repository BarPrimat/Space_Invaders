﻿using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;

namespace GameSprites
{
    public class MotherShip : Sprite
    {
        private int m_RandomTimeToNextAppears;
        private readonly Random r_Random;
        private float m_TimeDeltaCounter = 0;

        public MotherShip(Game i_Game, string i_TexturePath, Color i_Tint) : base(i_Game, i_TexturePath, i_Tint)
        {
            r_Random = new Random();
            m_RandomTimeToNextAppears = r_Random.Next(0, MotherShipMaxTimeToNextAppearsInSec);
            SpaceInvaders.ListOfSprites.Add(this);
        }

        public override void InitPosition()
        {
            this.Position = new Vector2(-this.Texture.Width, this.Texture.Height);
        }

        public override void Update(GameTime i_GameTime)
        {
            m_TimeDeltaCounter += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeDeltaCounter >= m_RandomTimeToNextAppears)
            {
                this.m_Position.X += MotherShipSpeed * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                if (m_Position.X >= GraphicsDevice.Viewport.Width)
                {
                    this.InitPosition();
                    m_TimeDeltaCounter = 0;
                    m_RandomTimeToNextAppears = r_Random.Next(0, MotherShipMaxTimeToNextAppearsInSec);
                }
            }
        }
    }
}