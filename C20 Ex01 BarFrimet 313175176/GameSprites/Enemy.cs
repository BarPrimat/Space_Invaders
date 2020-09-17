using System;
using System.Collections.Generic;
using System.Text;
using Animators;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using SpaceInvaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Enum = SpaceInvaders.Enum;

namespace GameSprites
{
    public class Enemy : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private readonly Firearm r_Firearm;
        private bool m_IsDying;

        public Enemy(Game i_Game, string i_TexturePath, Color i_Tint) : base(i_TexturePath, i_Game)
        {
            this.TintColor = i_Tint;
            m_IsDying = false;
            r_Firearm = new Firearm(i_Game, GameDefinitions.EnemyMaxOfBullet, Enum.eBulletType.EnemyBullet);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.initAnimations();
        }

        protected override void InitOrigins()
        {
            this.m_RotationOrigin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if(bullet != null && this.Enabled && !m_IsDying)
            {
                if(bullet.eBulletType != Enum.eBulletType.EnemyBullet)
                {
                    GameManager.UpdateScore(this, bullet.FirearmSerialNumber);
                    EnemyArmy.SubtractionEnemyByOne();

                    this.Animations.Enabled = true;


                    /*
                    this.Animations["FadeoutAnimator"].Reset();
                    this.Animations["FadeoutAnimator"].Resume();

                    this.Animations["RotateAnimator"].Reset();
                    this.Animations["RotateAnimator"].Resume();

                    this.Animations["ShrinkAnimator"].Reset();
                    this.Animations["ShrinkAnimator"].Resume();
                    */
                    m_IsDying = true;
                    bullet.DisableBullet();
                }
            }
        }

        public void RemoveComponent()
        {
            this.Visible = false;
            this.Enabled = false;
            Game.Components.Remove(this);
        }

        private void initAnimations()
        {
            /*
            RotateAnimator rotateAnimator = new RotateAnimator("RotateAnimator", TimeSpan.FromSeconds(5), 1f, RotateAnimator.eDirectionMove.Right);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator("ShrinkAnimator", TimeSpan.FromSeconds(5));
            FadeoutAnimator fadeoutAnimator = new FadeoutAnimator("FadeoutAnimator", TimeSpan.FromSeconds(3));
            this.Animations.Add(shrinkAnimator);
            this.Animations.Add(rotateAnimator);
            this.Animations.Add(fadeoutAnimator);

            this.Animations.Enabled = true;
            shrinkAnimator.Finished += animations_Finished;
            this.Animations["FadeoutAnimator"].Pause();
            this.Animations["RotateAnimator"].Pause();
            this.Animations["ShrinkAnimator"].Pause();
            */

            RotateAnimator rotateAnimator = new RotateAnimator("RotateAnimator", TimeSpan.FromSeconds(5), 1f, RotateAnimator.eDirectionMove.Right);
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator("ShrinkAnimator", TimeSpan.FromSeconds(5));
            FadeoutAnimator fadeoutAnimator = new FadeoutAnimator("FadeoutAnimator", TimeSpan.FromSeconds(5));

            CompositeAnimator dyingEnemy = new CompositeAnimator("dyingEnemy", TimeSpan.FromSeconds(5), this, rotateAnimator, shrinkAnimator, fadeoutAnimator);
          //  CellAnimator enemyCellAnimation = new CellAnimator(this.m_TimeUntilNextStepInSec, 2, TimeSpan.Zero, this.m_StartSqureIndex, true, this.m_Toggeler);

           // this.Animations.Add(enemyCellAnimation);
            this.Animations.Add(dyingEnemy);


            dyingEnemy.Finished += animations_Finished;
        }

        private void animations_Finished(object sender, EventArgs e)
        {
            RemoveComponent();
        }

        public void Shoot(Vector2 i_Position)
        {
            if(!m_IsDying)
            {
                r_Firearm.Shoot(i_Position);
            }
        }
    }
}
