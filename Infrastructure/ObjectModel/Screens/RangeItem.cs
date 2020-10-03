using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Screens
{
    public class RangeItem : MenuItem
    {
        private readonly int r_MaxValue = 100;
        private readonly int r_MinValue = 0;
        private int m_ItemValue;

        public RangeItem(string i_MenuText, GameScreen i_GameScreen, Vector2 i_Position, int i_ItemValue)
            : base(i_MenuText, i_GameScreen, i_Position)
        {
            m_ItemValue = i_ItemValue;
        }


        public RangeItem(string i_MenuText, GameScreen i_GameScreen, int i_ItemValue)
            : this(i_MenuText, i_GameScreen, Vector2.Zero, i_ItemValue)
        {
        }

        public int ItemValue
        {
            get => m_ItemValue;
            set
            {
                m_ItemValue = MathHelper.Clamp(value, r_MinValue, r_MaxValue);
                this.TextToPrint = this.r_MenuText + m_ItemValue;
            }
        }
    }
}
