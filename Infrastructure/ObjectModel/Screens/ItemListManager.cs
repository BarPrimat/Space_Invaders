using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Managers;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Infrastructure.ObjectModel.Screens
{
    public class ItemListManager
    {
        private const bool k_SelectIsGoingDown = true;
        private readonly List<MenuItem> r_MenuItemsList = new List<MenuItem>();
        private readonly IInputManager r_InputManager;
        private readonly int r_YOffsetForNewItem;
        private static double s_LastTimeThatUpdate = 0;
        private int m_NumberOfItemThatSelect = 0;
        private Vector2 m_NextPositionToAdd;
        private bool m_IsFirstInsertItem = true;

        public ItemListManager(Game i_Game, int i_XOffset, int i_YOffset, Vector2 i_CenterOfViewPort)
        {
            r_YOffsetForNewItem = i_YOffset;
            m_NextPositionToAdd = new Vector2(i_XOffset, i_CenterOfViewPort.Y + i_YOffset);
            if (r_InputManager == null)
            {
                r_InputManager = i_Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            }
        }

        public void Update(GameTime i_GameTime)
        {
            // Only need to run 1 time in for each GameTime update
            bool isRunAlreadyInThisTime = s_LastTimeThatUpdate == i_GameTime.TotalGameTime.TotalSeconds;
            bool isSpecialItem = r_MenuItemsList[m_NumberOfItemThatSelect] is ToggleItem || r_MenuItemsList[m_NumberOfItemThatSelect] is RangeItem;

            if (isSpecialItem && (r_InputManager.KeyPressed(Keys.PageUp) || r_InputManager.KeyPressed(Keys.PageDown)))
            {
                r_MenuItemsList[m_NumberOfItemThatSelect].ItemWasClick();
            }
            else if ((r_InputManager.KeyPressed(Keys.Down)))
            {
                selectGoingDownOrUp(k_SelectIsGoingDown);
            }
            else if ((r_InputManager.KeyPressed(Keys.Up)))
            {
                selectGoingDownOrUp(!k_SelectIsGoingDown);
            }
            else if (!isSpecialItem && r_InputManager.KeyPressed(Keys.Enter) && !isRunAlreadyInThisTime)
            {
                r_MenuItemsList[m_NumberOfItemThatSelect].ItemWasClick();
            }

            s_LastTimeThatUpdate = i_GameTime.TotalGameTime.TotalSeconds;
        }

        private void selectGoingDownOrUp(bool i_IsGoingDown)
        {
            r_MenuItemsList[m_NumberOfItemThatSelect].ItemIsInactive();
            if (i_IsGoingDown)
            {
                m_NumberOfItemThatSelect++;
                m_NumberOfItemThatSelect = m_NumberOfItemThatSelect % r_MenuItemsList.Count;
            }
            else
            {
                m_NumberOfItemThatSelect--;
                if (m_NumberOfItemThatSelect < 0)
                {
                    m_NumberOfItemThatSelect = r_MenuItemsList.Count - 1;
                }
            }

            r_MenuItemsList[m_NumberOfItemThatSelect].ItemIsSelect();
        }

        public void AddNewItem(MenuItem i_MenuItem)
        {
            if(i_MenuItem != null)
            {
                i_MenuItem.Position = m_NextPositionToAdd;
                r_MenuItemsList.Add(i_MenuItem);
                m_NextPositionToAdd = new Vector2(m_NextPositionToAdd.X, m_NextPositionToAdd.Y + r_YOffsetForNewItem);
                if (m_IsFirstInsertItem)
                {
                    this.MenuItemsList[0].ItemIsSelect();
                    m_IsFirstInsertItem = !m_IsFirstInsertItem;
                }
            }
        }

        public MenuItem LastInsertItem()
        {
            return r_MenuItemsList[r_MenuItemsList.Count - 1];
        }

        public Vector2 CalculatorPosition(int i_XOffset, int i_YOffset, Vector2 i_CenterOfViewPort)
        {
            return new Vector2(i_XOffset, i_CenterOfViewPort.Y + (this.MenuItemsList.Count * i_YOffset) + i_YOffset);
        }

        public List<MenuItem> MenuItemsList => r_MenuItemsList;

        public int NumberOfItemThatSelect
        {
            get => m_NumberOfItemThatSelect;
            set => m_NumberOfItemThatSelect = value;
        }
    }
}
