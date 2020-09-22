﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Text;
using FarseerPhysics.Dynamics;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders;
using Enum = SpaceInvaders.Enum;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameSprites
{
    public class Barrier : Infrastructure.ObjectModel.Sprite, ICollidable2D
    {
        private Color[] m_Pixels;
        private Vector2 m_StartPosition;
        private static bool s_TextureUsed = false;

        public Barrier(Game i_Game, string i_AssetName, Enum.eDirectionMove i_eDirectionMove) : base(i_AssetName, i_Game)
        {
            float moveOnXAxis = i_eDirectionMove == Enum.eDirectionMove.Right ? GameDefinitions.BarrierSpeed : -1 * GameDefinitions.BarrierSpeed;
            this.Velocity = new Vector2(moveOnXAxis, 0);
            // this.Scales = new Vector2(2, 2);
        }

        protected override void InitOrigins()
        {
            this.m_PositionOrigin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);
            base.InitOrigins();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            m_Pixels = new Color[Texture.Width * Texture.Height];
            this.Texture.GetData<Color>(m_Pixels);
            if (s_TextureUsed)
            {
                this.Texture = new Texture2D(Game.GraphicsDevice, Texture.Width, Texture.Height);
                this.Texture.SetData<Color>(m_Pixels);
            }
            else
            {
                s_TextureUsed = true;
            }
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
            Enemy enemy = i_Collidable as Enemy;
            List<Vector2> collidedPoints;
            bool v_AllowedToClear = true;

            if (enemy != null)
            {
                IsPixelsIntersects(enemy, out collidedPoints, v_AllowedToClear);
            }

            if (bullet != null && IsPixelsIntersects(bullet, out collidedPoints, !v_AllowedToClear))
            {
                ClearPixelsInBulletCollision(collidedPoints, bullet, (int) Math.Round(bullet.Height * GameDefinitions.BarrierPercentageThatBallEats));
                bullet.DisableBullet();
            }
        }

        public bool IsPixelsIntersects(Sprite i_CollisionSprite, out List<Vector2> o_PointsThatCollided, bool i_AllowedToClear)
        {
            bool isCollided = false;
            o_PointsThatCollided = new List<Vector2>();
            int topBoarder = Math.Max(this.Bounds.Top, i_CollisionSprite.Bounds.Top);
            int bottomBoarder = Math.Min(this.Bounds.Bottom, i_CollisionSprite.Bounds.Bottom);
            int leftBoarder = Math.Max(this.Bounds.Left, i_CollisionSprite.Bounds.Left);
            int rightBoarder = Math.Min(this.Bounds.Right, i_CollisionSprite.Bounds.Right);
            Color[] collisionSpriteData = new Color[i_CollisionSprite.Texture.Width * i_CollisionSprite.Texture.Height];

            i_CollisionSprite.Texture.GetData(collisionSpriteData);
            for (int y = topBoarder; y < bottomBoarder; y++)
            {
                int yPositionOfThisSprite = (y - this.Bounds.Top) * this.Bounds.Width;
                int yPositionOfCollisionSprite = (y - i_CollisionSprite.Bounds.Top) * i_CollisionSprite.Bounds.Width;
                for (int x = leftBoarder; x < rightBoarder; x++)
                {
                    int indexOfThisSprite = x - this.Bounds.Left + yPositionOfThisSprite;
                    int indexCollisionSprite = x - i_CollisionSprite.Bounds.Left + yPositionOfCollisionSprite;
                    if (m_Pixels[indexOfThisSprite].A != 0 && collisionSpriteData[indexCollisionSprite].A != 0)
                    {
                        isCollided = true;
                        o_PointsThatCollided.Add(new Vector2(x, y));
                        if (i_AllowedToClear)
                        {
                            m_Pixels[indexOfThisSprite] = new Color(0, 0, 0, 0);
                        }
                    }
                }
            }

            if (isCollided && i_AllowedToClear)
            {
                this.Texture.SetData(m_Pixels);
            }

            return isCollided;
        }

        public void ClearPixelsInBulletCollision(List<Vector2> i_StartPixelPositions, Bullet i_Bullet, int i_Length)
        {
            // The intersection must be between the maximum of the left and the minimum of the right
            int leftBoarder = Math.Max(this.Bounds.Left, i_Bullet.Bounds.Left);
            int rightBorder = Math.Min(this.Bounds.Right, i_Bullet.Bounds.Right);

            // To handle and clear the pixels that between the bullet and the barrier right now
            clearPixelsCollision(i_StartPixelPositions, i_Length);
            for (int i = leftBoarder; i < rightBorder; i++)
            {
                int xPositionInArray = i - this.Bounds.Left;

                for (int k = 0; k < i_Length; k++)
                {
                    // i_Bullet.Bounds.Bottom is always Plus 1 than this.Bounds.Top so the intersection is in -1 (consider offset)
                    int index = i_Bullet.eBulletType == Enum.eBulletType.SpaceShipBullet ? xPositionInArray + ((i_Bullet.Bounds.Top - this.Bounds.Top - k) * (this.Bounds.Width)) 
                                    : xPositionInArray + ((i_Bullet.Bounds.Bottom - 1 - this.Bounds.Top + k) * (this.Bounds.Width));

                    if(index < 0 || index >= m_Pixels.Length)
                    {
                        // The intersection in this X position and the in the future Y is finish
                        break;
                    }

                    m_Pixels[index] = new Color(0, 0, 0, 0);
                }
            }

            this.Texture.SetData(m_Pixels);
        }

        private void clearPixelsCollision(List<Vector2> i_StartPixelPositions, int i_Length)
        {
            foreach (Vector2 position in i_StartPixelPositions)
            {
                int index = (int)((position.X - this.Bounds.Left) + ((position.Y - this.Bounds.Top) * this.Bounds.Width));
                if (i_Length > 0)
                {
                    m_Pixels[index] = new Color(0, 0, 0, 0);
                }
            }
        }

        public void SetFirstPosition(Vector2 i_Vector2)
        {
            this.Position = i_Vector2;
            m_StartPosition = i_Vector2;
        }
    }
}
