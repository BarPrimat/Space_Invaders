using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace C20_Ex01_BarFrimet_313175176
{
    public class Firearm 
    {
        private int m_MaximumOfBullet;
        private readonly List<Bullet> r_ListBullets = new List<Bullet>();
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly ContentManager r_ContentManager;
        private readonly Color r_Tint;
        private readonly Bullet.eBulletType r_eBulletType;

        public Firearm(GraphicsDeviceManager i_Graphics, ContentManager i_ContentManage, int i_MaximumOfBullet, Bullet.eBulletType i_eBulletType)
        {
            m_MaximumOfBullet = i_MaximumOfBullet;
            r_Graphics = i_Graphics;
            r_ContentManager = i_ContentManage;
            r_eBulletType = i_eBulletType;
            r_Tint = i_eBulletType == Bullet.eBulletType.SpaceShipBullet ? SpaceshipBulletTint : EnemyBulletTint;
        }

        public void Update(GameTime i_GameTime)
        {
            foreach(Bullet bullet in r_ListBullets)
            {
                bullet.Update(i_GameTime);
            }
        }

        public void CreateNewBullet(Vector2 i_Position)
        {
            if(r_ListBullets.Count < m_MaximumOfBullet)
            {
                r_ListBullets.Add(new Bullet(r_Graphics, r_ContentManager, GameSprites.SpritesDefinition.BulletAsset, r_Tint, i_Position, r_eBulletType));
            }
        }

        public void RemoveBulletFormList(Bullet i_Bullet)
        {
            int indexToRemove = 0;
            bool bulletInList = false;

            foreach (Bullet bullet in r_ListBullets)
            {
                if (bullet == i_Bullet)
                {
                    bulletInList = !bulletInList;
                    break;
                }

                indexToRemove++;
            }

            if(bulletInList)
            {
                r_ListBullets.RemoveAt(indexToRemove);
                i_Bullet.Visible = !i_Bullet.Visible;
            }
        }
    }
}
