using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Microsoft.Xna.Framework;

namespace C20_Ex01_BarFrimet_313175176
{
    public class GameManager : DrawableGameComponent
    {
        private static readonly List<Bullet> r_ListBullets = new List<Bullet>();
        private static int m_SpaceShipBulletInTheAir = 0;
        private static int m_EnemyBulletInTheAir = 0;
        private LifeManager m_LifeManager;

        public GameManager(Game i_Game) : base(i_Game)
        {
            m_LifeManager = new LifeManager(i_Game, SpritesDefinition.LifeAsset, GameDefinitions.NumberOfLifeToStart);
        }


        public static List<Bullet> ListBullets => r_ListBullets;

        public static int SpaceShipBulletInTheAir
        {
            get => m_SpaceShipBulletInTheAir;
            set => m_SpaceShipBulletInTheAir = value;
        }
        public static int EnemyBulletInTheAir
        {
            get => m_EnemyBulletInTheAir;
            set => m_EnemyBulletInTheAir = value;
        }
    }
}
