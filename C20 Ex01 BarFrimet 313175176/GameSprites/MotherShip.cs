using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using static SpaceInvaders.GameDefinitions;
using Enum = SpaceInvaders.Enum;

namespace GameSprites
{
    public class MotherShip : Infrastructure.ObjectModel.Sprite, ICollidable
    {
        private int m_RandomTimeToNextAppears;
        private readonly Random r_Random;
        private float m_TimeDeltaCounter = 0;

        public MotherShip(Game i_Game, string i_TexturePath, Color i_Tint) : base(i_TexturePath, i_Game)
        {
            this.TintColor = i_Tint;
            r_Random = new Random();
            m_RandomTimeToNextAppears = r_Random.Next(0, MotherShipMaxTimeToNextAppearsInSec);
        }

        public override void Initialize()
        {
            base.Initialize();
            initPosition();
        }

        private void initPosition()
        { 
            this.Position = new Vector2(-this.Texture.Width, this.Texture.Height);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            m_TimeDeltaCounter += (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            if (m_TimeDeltaCounter >= m_RandomTimeToNextAppears)
            {
                if (this.Position.X >= GraphicsDevice.Viewport.Width || this.Position.X < 0)
                {
                    initPosition();
                    this.Velocity = new Vector2(MotherShipSpeed, 0);
                    setupTimeDeltaAndRandomTime();
                }
            }
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if (bullet != null)
            {
                bullet.DisableBullet();
                this.Velocity = Vector2.Zero;
                setupTimeDeltaAndRandomTime();
                initPosition();
            }
        }

        private void setupTimeDeltaAndRandomTime()
        {
            m_TimeDeltaCounter = 0;
            m_RandomTimeToNextAppears = r_Random.Next(0, MotherShipMaxTimeToNextAppearsInSec);
        }
    }
}