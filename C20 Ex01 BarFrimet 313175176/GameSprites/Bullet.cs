using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;
using static C20_Ex01_BarFrimet_313175176.Enum;


namespace GameSprites
{
    public class Bullet : Sprite
    {
        private Vector2 m_CurrentPosition;
        private readonly eBulletType r_eBulletType;
        private readonly Color r_Tint;

        public Bullet(Game i_Game, string i_TexturePath, Color i_Tint, Vector2 i_CurrentPosition, eBulletType i_eBulletType)
            : base(i_Game, i_TexturePath, i_Tint)
        {
            m_CurrentPosition = i_CurrentPosition;
            r_eBulletType = i_eBulletType;
            this.Position = i_CurrentPosition;
            r_Tint = i_eBulletType == eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
            SpaceInvaders.ListOfSprites.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void InitPosition()
        {
        }

        public override void Update(GameTime i_GameTime)
        { 
            isBulletHitSomething(i_GameTime);
        }

        private void isBulletHitSomething(GameTime i_GameTime)
        {
            bool bulletHitSomething = false;
            Sprite spriteThatHit = getSpriteCollision();

            bulletHitSomething = deleteOrMoveBullet(i_GameTime, spriteThatHit);
            if (bulletHitSomething){
                switch (r_eBulletType)
                {
                    case eBulletType.SpaceShipBullet:
                        Spaceship.CounterOfSpaceShipBulletInAir--;
                        break;
                    case eBulletType.EnemyBullet:
                        EnemyArmy.CounterOfEnemyBulletInAir--;
                        break;
                }
            }
        }

        private Sprite getSpriteCollision()
        {
            Sprite spriteThatHit = null;
            Rectangle bulletRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);

            foreach (Sprite sprite in SpaceInvaders.ListOfSprites)
            {
                // Check if spaceship is hit
                bool isRival = (sprite is Spaceship && r_eBulletType == eBulletType.EnemyBullet);
                // Check if some enemy is hit
                isRival = isRival || ((sprite is Enemy || sprite is MotherShip) && (r_eBulletType == eBulletType.SpaceShipBullet));

                if (isRival && sprite.Visible)
                {
                    Rectangle spriteRectangle = new Rectangle((int)(sprite).Position.X, (int)(sprite).Position.Y, (int)(sprite).Texture.Width, (int)(sprite).Texture.Height);

                    if (bulletRectangle.Intersects(spriteRectangle))
                    {
                        spriteThatHit = sprite;
                    }
                }
            }

            return spriteThatHit;
        }

        private bool deleteOrMoveBullet(GameTime i_GameTime, Sprite i_SpriteThatCollision)
        {
            bool bulletHitSomething = false;

            if (isBulletHitBorder())
            {
                this.RemoveComponent();
                bulletHitSomething = !bulletHitSomething;
            }

            if (i_SpriteThatCollision != null)
            {
                killElementOnHit(i_SpriteThatCollision);
                bulletHitSomething = !bulletHitSomething;
            }
            else
            {
                moveBullet(i_GameTime);
            }

            return bulletHitSomething;
        }

        private void killElementOnHit(Sprite i_SpriteWosHit)
        {
            if (i_SpriteWosHit is Spaceship)
            {
                i_SpriteWosHit.InitPosition();
            }
            else
            {
                // Some enemy was hit
                i_SpriteWosHit.RemoveComponent();
                EnemyArmy.AddEnemyKilledByOne();
            }

            GameManager.UpdateScore(i_SpriteWosHit);
            this.RemoveComponent();
        }

        private void moveBullet(GameTime i_GameTime)
        {
            float newYPosition = BulletStartSpeedInSec * (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            m_CurrentPosition.Y += r_eBulletType == eBulletType.EnemyBullet ? 1 * newYPosition : -1 * newYPosition;
            this.Position = m_CurrentPosition;
        }

        private bool isBulletHitBorder()
        {
            return m_Position.Y >= Game.GraphicsDevice.Viewport.Height || 0 >= m_Position.Y;
        }
    }
}