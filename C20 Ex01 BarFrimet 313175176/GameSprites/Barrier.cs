using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders;
using Enum = SpaceInvaders.Enum;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameSprites
{
    public class Barrier : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private Color[] m_Pixels;
        private Vector2 m_StartPosition;

        public Barrier(Game i_Game, string i_AssetName, Enum.eDirectionMove i_eDirectionMove) : base(i_AssetName, i_Game)
        {
            float moveOnXAxis = i_eDirectionMove == Enum.eDirectionMove.Right ? GameDefinitions.BarrierSpeed : -1 * GameDefinitions.BarrierSpeed;
            this.Velocity = new Vector2(moveOnXAxis, 0);
        }

        protected override void InitOrigins()
        {
            this.m_PositionOrigin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
            base.InitOrigins();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Pixels = new Color[this.Texture.Width * this.Texture.Height];
            this.Texture.GetData<Color>(m_Pixels);

        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            moveWall();
        }

        private void moveWall()
        {
            float maxOfHorizontalMove = this.Texture.Width / 2;

            if ((this.Position.X >= maxOfHorizontalMove + this.m_StartPosition.X) || (this.Position.X <= this.m_StartPosition.X - maxOfHorizontalMove))
            {
                this.Velocity *= -1;
            }
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;

            if (bullet != null && this.Visible)
            {
                bullet.DisableBullet();
                this.Game.Components.Remove(this);
            }
        }

        public void SetFirstPosition(Vector2 i_Vector2)
        {
            this.Position = i_Vector2;
            m_StartPosition = i_Vector2;
        }
    }
}
