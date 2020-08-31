using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;



namespace GameSprites
{
    public class Bullet : Sprite
    {
        private  Vector2 m_CurrentPosition;
        private eBulletType m_eBulletType;
        private readonly Color r_Tint;

        public enum eBulletType
        {
            SpaceShipBullet = -1,
            EnemyBullet = 1
        }


        public Bullet(Game i_Game, string i_TexturePath, Color i_Tint, Vector2 i_CurrentPosition, eBulletType i_eBulletType)
            : base(i_Game, i_TexturePath, i_Tint)
        {
            m_CurrentPosition = i_CurrentPosition;
            m_eBulletType = i_eBulletType;
            this.Position = i_CurrentPosition;
            r_Tint = i_eBulletType == Bullet.eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
        }

        public override void Initialize()
        {
            base.Initialize();
           // InitPosition();
        }

        public override void InitPosition()
        {
        }

        public override void Update(GameTime i_GameTime)
        {
            isBulletHitSomething(i_GameTime);
            // bulletMove(i_GameTime);
        }

        public void RemoveBulletFormListAndComponent(Bullet i_Bullet)
        {
            GameManager.ListBullets.Remove(i_Bullet);
            i_Bullet.RemoveComponent();
        }

        private void isBulletHitSomething(GameTime i_GameTime)
        {
            bool bulletHitSomething = false;

            if (IsBulletHitBorder())
            {
                this.RemoveComponent();
                bulletHitSomething = !bulletHitSomething;
            }

            Sprite hittenSprite = getHitSprite();

            if (hittenSprite != null)
            {
                OnHit(hittenSprite);
                bulletHitSomething = !bulletHitSomething;
            }
            else
            {
                bulletMove(i_GameTime);
            }

            if(bulletHitSomething)
            {
                switch (m_eBulletType)
                {
                    case eBulletType.SpaceShipBullet:
                        GameManager.SpaceShipBulletInTheAir--;
                        break;
                    case eBulletType.EnemyBullet:
                        GameManager.EnemyBulletInTheAir--;
                        break;
                }
            }
        }

        public void OnHit(Sprite i_SpriteWosHit)
        {
            if (!(i_SpriteWosHit is Spaceship))
            {
                // Some enemy was hit
                i_SpriteWosHit.RemoveComponent();
            }

            GameManager.UpdateScore(i_SpriteWosHit);
            this.RemoveComponent();
        }

        private Sprite getHitSprite()
        {
            Sprite hittenSprite = null;
            Rectangle bulletRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            foreach (Sprite sprite in SpaceInvaders.ListOfSprites)
            {
                if (isShootableComponent(sprite) && isOpponents(sprite) && sprite.Visible)
                {
                    Rectangle spriteRectangle = new Rectangle((int)(sprite).Position.X, (int)(sprite).Position.Y, (int)(sprite).Texture.Width, (int)(sprite).Texture.Height);

                    if (bulletRectangle.Intersects(spriteRectangle))
                    {
                        hittenSprite = sprite;
                    }
                }
            }

            return hittenSprite;
        }

        private bool isShootableComponent(Sprite i_Sprite)
        {
            bool isShootableComponent = i_Sprite is Enemy || i_Sprite is Bullet || i_Sprite is Spaceship || i_Sprite is MotherShip;

            return isShootableComponent;
        }

        private bool isOpponents(Sprite i_Sprite)
        {
            bool isOpponent;

            isOpponent = (m_eBulletType == Bullet.eBulletType.EnemyBullet && (i_Sprite is Spaceship || (i_Sprite is Bullet && m_eBulletType == Bullet.eBulletType.SpaceShipBullet)))
                         || (m_eBulletType == Bullet.eBulletType.SpaceShipBullet && (i_Sprite is Enemy || i_Sprite is MotherShip));

            return isOpponent;
        }

        private void bulletMove(GameTime i_GameTime)
        {
            float newYPosition = BulletStartSpeedInSec * (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            m_CurrentPosition.Y += m_eBulletType == eBulletType.EnemyBullet ? 1 * newYPosition : -1 * newYPosition;
            this.Position = m_CurrentPosition;
        }

        public bool IsBulletHitBorder()
        {
            return m_Position.Y >= Game.GraphicsDevice.Viewport.Height || 0 >= m_Position.Y;
        }
    }
}