using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Managers;
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
        private float m_SpaceshipSpeed;
        private int m_NumberOfTheSpaceship;

        public Spaceship(Game i_Game, string i_TexturePath, int i_NumberOfTheSpaceship, LifeManager i_LifeManager) : base (i_TexturePath, i_Game)
        {
            this.TintColor = GameDefinitions.SpaceshipTint;
            r_Firearm = new Firearm(i_Game, SpaceshipMaxOfBullet, eBulletType.SpaceShipBullet);
            m_SpaceshipSpeed = GameDefinitions.SpaceshipSpeed;
            m_NumberOfTheSpaceship = i_NumberOfTheSpaceship;
            r_LifeManager = i_LifeManager;
            // SpaceInvadersGame.ListOfSprites.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            InitPosition();
        }

        private void InitPosition()
        {
            // Init the ship position
            float x = m_NumberOfTheSpaceship * this.Texture.Width;
            float y = (float) PreferredBackBufferHeight;

            // Offset:
            y -= this.Texture.Height;

            // Put it a little bit higher:
            y -= 10;
            this.Position = new Vector2(x, y);
        }

        public void Shoot()
        {
            if(this.Visible)
            {
                r_Firearm.Shoot(new Vector2(this.Position.X + Texture.Width / 2, this.Position.Y));
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

            if(bullet != null && this.Visible)
            {
                if(bullet.eBulletType != eBulletType.SpaceShipBullet)
                {
                    r_LifeManager.RemoveOneLife();
                    if (r_LifeManager.IsNoMoreLifeRemains())
                    {
                        this.Visible = false;
                    }
                    else
                    {
                        this.InitPosition();
                    }

                    bullet.DisableBullet();
                }
            }
        }


        public float SpaceshipSpeed => m_SpaceshipSpeed;
    }
}