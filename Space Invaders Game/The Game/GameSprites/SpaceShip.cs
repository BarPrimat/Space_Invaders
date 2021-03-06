﻿using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static SpaceInvaders.GameDefinitions;
using static SpaceInvaders.Enum;


namespace GameSprites
{
    public class Spaceship : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private readonly Firearm r_Firearm;
        private readonly LifeManager r_LifeManager;
        private readonly float r_SpaceshipSpeed;
        private readonly int r_NumberOfTheSpaceship;
        private bool m_IsDying = false;
        public event Action SpaceShipIsDead;

        public Spaceship(GameScreen i_GameScreen, string i_TexturePath, int i_NumberOfSpaceship, LifeManager i_LifeManager) : base (i_TexturePath, i_GameScreen)
        {
            this.TintColor = GameDefinitions.SpaceshipTint;
            r_Firearm = new Firearm(i_GameScreen, SpaceshipMaxOfBullet, eBulletType.SpaceShipBullet, i_NumberOfSpaceship);
            r_SpaceshipSpeed = GameDefinitions.SpaceshipSpeed;
            r_NumberOfTheSpaceship = i_NumberOfSpaceship;
            r_LifeManager = i_LifeManager;
            this.BlendState  = BlendState.NonPremultiplied;
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
            base.InitOrigins();
        }

        private void initPosition()
        {
            // Init the ship position
            float x = r_NumberOfTheSpaceship * this.Texture.Width;

            this.Position = new Vector2(x, this.Game.GraphicsDevice.Viewport.Height - GameDefinitions.SpaceshipYOffsetStartPosition);
        }

        public void InitForNextLevel()
        {
            r_Firearm.InitForNextLevel();
            if (!r_LifeManager.IsNoMoreLifeRemains())
            {
                this.Animations["DyingSpaceship"].Reset();
                this.Animations["DyingSpaceship"].Pause();
                this.Animations["HittingSpaceship"].Reset();
                this.Animations["HittingSpaceship"].Pause();
                this.Enabled = true;
                m_IsDying = false;
                initPosition();
            }
        }

        public void Shoot()
        {
            if(this.Visible && !m_IsDying)
            {
                r_Firearm.Shoot(new Vector2(this.Position.X + this.Texture.Width / 2, this.Position.Y));
            }
        }

        public void SetupNewPosition(float i_NewXPosition, float i_MaxBoundaryWithoutOffset)
        {
            i_NewXPosition = MathHelper.Clamp(i_NewXPosition, 0, i_MaxBoundaryWithoutOffset);
            this.Position = new Vector2(i_NewXPosition, Position.Y);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if(bullet != null && this.Visible && !m_IsDying)
            {
                if(bullet.eBulletType != eBulletType.SpaceShipBullet)
                {
                    GameManager.UpdateScore(this, r_Firearm.FirearmSerialNumber);
                    r_LifeManager.RemoveOneLife();
                    m_IsDying = true;
                    if (r_LifeManager.IsNoMoreLifeRemains())
                    {
                        this.Animations["DyingSpaceship"].Restart();
                    }
                    else
                    {
                        this.Animations["HittingSpaceship"].Restart();
                    }

                    bullet.DisableBullet();
                }
            }
        }

        private void initAnimations()
        {
            // Animator when space ship dying
            TimeSpan timeSpanDyingSpaceship = TimeSpan.FromSeconds(GameDefinitions.SpaceshipAnimationLengthInSec);
            RotateAnimator rotateAnimator = new RotateAnimator("RotateAnimator", timeSpanDyingSpaceship, GameDefinitions.SpaceshipNumberOfRotateInSec, RotateAnimator.eDirectionMove.Right);
            FadeoutAnimator fadeoutAnimator = new FadeoutAnimator("ShrinkAnimator", timeSpanDyingSpaceship);
            CompositeAnimator dyingSpaceship = new CompositeAnimator("DyingSpaceship", timeSpanDyingSpaceship, this, rotateAnimator, fadeoutAnimator);

            // Animator when space ship hit by a bullet
            TimeSpan timeSpanHittingSpaceship = TimeSpan.FromSeconds(GameDefinitions.SpaceshipAnimationLengthWhenBulletHitInSec);
            BlinkAnimator blinkBulletHit = new BlinkAnimator("BlinkBulletHit", TimeSpan.FromSeconds(GameDefinitions.SpaceshipNumberOfBlinkingWhenBulletHitInSec), timeSpanHittingSpaceship);
            // Not necessary need it but maybe in the further, we may need it
            CompositeAnimator hittingSpaceship = new CompositeAnimator("HittingSpaceship", timeSpanHittingSpaceship, this, blinkBulletHit);

            this.Animations.Add(dyingSpaceship);
            this.Animations.Add(hittingSpaceship);
            this.Animations.Enabled = true;
            this.Animations["DyingSpaceship"].Pause();
            this.Animations["HittingSpaceship"].Pause();
            dyingSpaceship.Finished += animationsDying_Finished;
            hittingSpaceship.Finished += animationsHit_Finished;
        }

        private void animationsDying_Finished(object i_Sender, EventArgs i_EventArgs)
        {
            this.Visible = false;
            this.Enabled = false;
            m_IsDying = false;
            SpaceShipIsDead?.Invoke();
        }

        private void animationsHit_Finished(object i_Sender, EventArgs i_EventArgs)
        {
            initPosition();
            m_IsDying = false;
        }

        public float SpaceshipSpeed => r_SpaceshipSpeed;
    }
}