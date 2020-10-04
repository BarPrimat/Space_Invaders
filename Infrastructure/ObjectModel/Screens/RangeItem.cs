using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Screens
{
    public class RangeItem : MenuItem
    {
        private const int k_MaxValue = 100;
        private const int k_MinValue = 0;
        private const int k_ValueToDecreaseOrIncrease = 10;
        private int m_ItemValue;
        private int m_ValueToDecreaseOrIncrease;

        public RangeItem(string i_MenuText, GameScreen i_GameScreen, Vector2 i_Position, int i_ItemValue, int i_ValueToDecreaseOrIncrease)
            : base(i_MenuText, i_GameScreen, i_Position)
        {
            ItemValue = i_ItemValue;
            m_ValueToDecreaseOrIncrease = i_ValueToDecreaseOrIncrease;
        }


        public RangeItem(string i_MenuText, GameScreen i_GameScreen, int i_ItemValue)
            : this(i_MenuText, i_GameScreen, Vector2.Zero, i_ItemValue, k_ValueToDecreaseOrIncrease)
        {
        }

        public int ItemValue
        {
            get => m_ItemValue;
            set
            {
                if(value > k_MaxValue)
                {
                    value = k_MinValue;
                }
                else if(value < k_MinValue)
                {
                    value = k_MaxValue;
                }

                m_ItemValue = value;
                this.TextToPrint = this.r_MenuText + m_ItemValue;
            }
        }
        public int ValueToDecreaseOrIncrease
        {
            get => m_ValueToDecreaseOrIncrease;
            set => m_ValueToDecreaseOrIncrease = value;
        }
    }
}
