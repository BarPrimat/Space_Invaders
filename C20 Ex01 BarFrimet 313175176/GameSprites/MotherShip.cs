using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
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
        private bool m_IsDying = false;

        public MotherShip(GameScreen i_GameScreen, string i_TexturePath, Color i_Tint) : base(i_TexturePath, i_GameScreen)
        {
            this.TintColor = i_Tint;
            r_Random = new Random();
            m_RandomTimeToNextAppears = r_Random.Next(0, MotherShipMaxTimeToNextAppearsInSec);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.initAnimations();
            initPosition();
        }

        protected override void InitOrigins()
        {
            this.m_RotationOrigin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
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
                if (this.Position.X >= GraphicsDevice.Viewport.Width || (this.Position.X + this.Width) <= 0)
                {
                    initPosition();
                    this.Velocity = new Vector2(MotherShipSpeed, 0); // Move only on X axis
                }
            }
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if (bullet != null && !m_IsDying)
            {
                GameManager.UpdateScore(this, bullet.FirearmSerialNumber);
                bullet.DisableBullet();
                m_IsDying = true;
                this.Velocity = Vector2.Zero;
                this.Animations.Restart();
            }
        }

        private void setupTimeDeltaAndRandomTime()
        {
            m_TimeDeltaCounter = 0;
            m_RandomTimeToNextAppears = r_Random.Next(0, MotherShipMaxTimeToNextAppearsInSec);
        }

        private void initAnimations()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(GameDefinitions.MotherShipAnimationLengthInSec);
            FadeoutAnimator fadeoutAnimator = new FadeoutAnimator("FadeoutAnimator", timeSpan);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator("ShrinkAnimator", timeSpan);
            BlinkAnimator blinkAnimator = new BlinkAnimator ("blinkAnimator", TimeSpan.FromSeconds(GameDefinitions.MotherShipNumberOfBlinkInAnimation), timeSpan);
            CompositeAnimator dyingMotherShip = new CompositeAnimator("DyingMotherShip", timeSpan, this, fadeoutAnimator, shrinkAnimator, blinkAnimator);

            this.Animations.Add(dyingMotherShip);
            this.Animations.Enabled = true;
            this.Animations.Pause();
            dyingMotherShip.Finished += animations_Finished;
        }

        private void animations_Finished(object i_Sender, EventArgs i_EventArgs)
        {
            setupTimeDeltaAndRandomTime();
            initPosition();
            m_IsDying = false;
        }
    }
}