using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Screens
{
    public class ToggleItem : MenuItem
    {
        public enum eToggleItemType
        {
            OnOffItem,
            VisibleInvisibleItem,
            OneTwoPlayersItem
        }

        private const bool k_FirstSetOption = true;
        private readonly eToggleItemType r_eToggleItemType;
        private readonly string[] r_BooleanOption = new string[2];
        private bool m_BooleanOption = true;
        // r_BooleanOption[0] is true and r_BooleanOption[1] is false

        public ToggleItem(string i_MenuText, GameScreen i_GameScreen, Vector2 i_Position, eToggleItemType i_eToggleItemType, bool i_FirstSetOption)
            : base(i_MenuText, i_GameScreen, i_Position)
        {
            r_eToggleItemType = i_eToggleItemType;
            switch (r_eToggleItemType)
            {
                case eToggleItemType.OnOffItem:
                    r_BooleanOption[0] = "On";
                    r_BooleanOption[1] = "Off";
                    break;
                case eToggleItemType.VisibleInvisibleItem:
                    r_BooleanOption[0] = "Visible";
                    r_BooleanOption[1] = "Invisible";
                    break;
                case eToggleItemType.OneTwoPlayersItem:
                    r_BooleanOption[0] = "One";
                    r_BooleanOption[1] = "Two";
                    break;
            }

            InitOption(i_FirstSetOption);
        }

        public ToggleItem(string i_MenuText, GameScreen i_GameScreen, eToggleItemType i_eToggleItemType, bool i_FirstSetOption)
            : this(i_MenuText, i_GameScreen, Vector2.Zero, i_eToggleItemType, i_FirstSetOption)
        {
        }

        public ToggleItem(string i_MenuText, GameScreen i_GameScreen, eToggleItemType i_eToggleItemType)
            : this(i_MenuText, i_GameScreen, Vector2.Zero, i_eToggleItemType, k_FirstSetOption)
        {
        }

        public bool BooleanOption
        {
            get => m_BooleanOption;
            set => m_BooleanOption = value;
        }

        public override void ItemWasClick()
        {
            base.ItemWasClick();
            if(m_BooleanOption)
            {
                this.TextToPrint = this.r_MenuText + r_BooleanOption[1];
            }
            else
            {
                this.TextToPrint = this.r_MenuText + r_BooleanOption[0];
            }

            m_BooleanOption = !m_BooleanOption;
        }

        public void InitOption(bool i_BooleanOption)
        {
            m_BooleanOption = i_BooleanOption;
            if(m_BooleanOption)
            {
                this.TextToPrint = this.r_MenuText + r_BooleanOption[0];
            }
            else
            {
                this.TextToPrint = this.r_MenuText + r_BooleanOption[1];
            }
        }
    }
}
