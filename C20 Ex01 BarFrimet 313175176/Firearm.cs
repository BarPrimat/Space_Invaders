using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace C20_Ex01_BarFrimet_313175176
{
    public class Firearm : DrawableGameComponent
    {
        private int m_MaximumOfBullet;
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly ContentManager r_ContentManager;
        private readonly Color r_Tint;
        private readonly Bullet.eBulletType r_eBulletType;

        public Firearm(Game i_Game, int i_MaximumOfBullet, Bullet.eBulletType i_eBulletType) : base(i_Game)
        {
            m_MaximumOfBullet = i_MaximumOfBullet;
            r_eBulletType = i_eBulletType;
            r_Tint = i_eBulletType == Bullet.eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
            i_Game.Components.Add(this);
        }

        public void CreateNewBullet(Vector2 i_Position)
        {
            if (GameManager.SpaceShipBulletInTheAir < m_MaximumOfBullet)
            {
                Bullet bullet = new Bullet(Game, GameSprites.SpritesDefinition.BulletAsset, r_Tint, i_Position, r_eBulletType);
                GameManager.ListBullets.Add(bullet);
                GameManager.SpaceShipBulletInTheAir++;
            }
        }

        public void RemoveBulletFormListAndComponent(Bullet i_Bullet)
        {
            GameManager.ListBullets.Remove(i_Bullet);
            i_Bullet.RemoveComponent();
        }
    }
}
