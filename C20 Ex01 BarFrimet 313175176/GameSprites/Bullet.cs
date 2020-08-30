using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace GameSprites
{
    public class Bullet : Sprite
    {
        private readonly Vector2 r_StartPosition;
        private eBulletType m_eBulletType;

        public enum eBulletType
        {
            SpaceShipBullet = -1,
            EnemyBullet = 1
        }


        public Bullet(GraphicsDeviceManager i_Graphics, ContentManager i_Content, string i_TexturePath, Color i_Tint, Vector2 i_StartPosition, eBulletType i_eBulletType)
            : base(i_Graphics, i_Content, i_TexturePath, i_Tint)
        {
            r_StartPosition = i_StartPosition;
            m_eBulletType = i_eBulletType;
        }

        public override void InitPosition()
        {
            this.Position = r_StartPosition;
        }

        public override void Update(GameTime i_GameTime)
        {
            float newYPosition = BulletStartSpeedInSec * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            newYPosition = m_eBulletType == eBulletType.EnemyBullet ? newYPosition + r_StartPosition.Y : newYPosition - r_StartPosition.Y;

            this.Position = new Vector2(r_StartPosition.X, newYPosition);
        }
    }
}
