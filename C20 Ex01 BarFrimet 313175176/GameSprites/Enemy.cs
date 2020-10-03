using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Enum = SpaceInvaders.Enum;

namespace GameSprites
{
    public class Enemy : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private CellAnimator m_EnemyCellAnimation;
        private bool m_IsInitialize = false;
        private bool m_IsDying;
        private readonly Firearm r_Firearm;
        private readonly TimeSpan r_TimeUntilNextAssetChangesInSec;
        private readonly int r_RowIndexInPicture;
        private readonly int r_NumberOfAssetChange;
        private readonly int r_ColumnIndexInPicture;
        private const bool k_ThereIsDummyPixel = true;

        public Enemy(GameScreen i_GameScreen, string i_TexturePath, Color i_Tint, int i_RowIndexInPicture, int i_ColumnIndexInPicture, float i_TimeUntilNextAssetChangesInSec, int i_NumberOfAssetChange) 
            : base(i_TexturePath, i_GameScreen)
        {
            this.TintColor = i_Tint;
            m_IsDying = false;
            r_Firearm = new Firearm(i_GameScreen, GameDefinitions.EnemyMaxOfBullet, Enum.eBulletType.EnemyBullet);
            this.r_TimeUntilNextAssetChangesInSec = TimeSpan.FromSeconds(i_TimeUntilNextAssetChangesInSec);
            r_NumberOfAssetChange = i_NumberOfAssetChange;
            r_RowIndexInPicture = i_RowIndexInPicture;
            r_ColumnIndexInPicture = i_ColumnIndexInPicture;
        }

        public bool IsDying
        {
            get => m_IsDying;
            set => m_IsDying = value;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.initAnimations();
            m_IsInitialize = true;
        }

        protected override void InitOrigins()
        {
            this.m_WidthBeforeScale = (int) GameDefinitions.EnemySize;
            this.m_HeightBeforeScale = (int) GameDefinitions.EnemySize;
            this.m_RotationOrigin = new Vector2(this.Width / 2, this.Height / 2);
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            // The pulse 1 is to handle that in the texture there is 1 dummy pixel between every 2 enemies (that on purpose because of the crop defect)
            this.SourceRectangle = new Rectangle(0, (int)(r_RowIndexInPicture * (GameDefinitions.EnemySize + 1)), (int) GameDefinitions.EnemySize, (int) GameDefinitions.EnemySize);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if(bullet != null && this.Enabled && !m_IsDying)
            {
                if(bullet.eBulletType != Enum.eBulletType.EnemyBullet)
                {
                    GameManager.UpdateScore(this, bullet.FirearmSerialNumber);
                    this.Animations["DyingEnemy"].Restart();
                    m_IsDying = true;
                    bullet.DisableBullet();
                }
            }
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            this.Enabled = false;
            this.Game.Components.Remove(this);
        }

        protected override void OnPositionChanged()
        {
            if(EnemyArmy.IsTimeBetweenJumpsChanged && m_IsInitialize)
            {
                m_EnemyCellAnimation.CellTime = TimeSpan.FromSeconds(EnemyArmy.TimeBetweenJumpsInSec);
            }
        }

        private void initAnimations()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(GameDefinitions.EnemyAnimationLengthInSec);
            RotateAnimator rotateAnimator = new RotateAnimator("RotateAnimator", timeSpan, GameDefinitions.EnemyNumberOfRotateInSec, RotateAnimator.eDirectionMove.Right);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator("ShrinkAnimator", timeSpan);
            CompositeAnimator dyingEnemy = new CompositeAnimator("DyingEnemy", timeSpan, this, rotateAnimator, shrinkAnimator);
            m_EnemyCellAnimation = new CellAnimator(r_TimeUntilNextAssetChangesInSec, r_NumberOfAssetChange, TimeSpan.Zero,r_ColumnIndexInPicture, k_ThereIsDummyPixel);

            this.Animations.Add(m_EnemyCellAnimation);
            this.Animations.Add(dyingEnemy);
            this.Animations.Enabled = true;
            this.Animations["DyingEnemy"].Pause();
            dyingEnemy.Finished += animations_Finished;
        }

        private void animations_Finished(object i_Sender, EventArgs i_EventArgs)
        {
            RemoveComponent();
            EnemyArmy.SubtractionEnemyByOne();
        }

        public void Shoot()
        {
            if(!m_IsDying)
            {
                Vector2 shootingPosition = new Vector2((this.Position.X + this.Width / 2), this.Position.Y + this.Height);
                r_Firearm.Shoot(shootingPosition);
            }
        }
    }
}
