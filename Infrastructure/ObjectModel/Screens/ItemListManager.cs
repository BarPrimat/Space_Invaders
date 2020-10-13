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
        private readonly ISoundManager r_SoundManager;
        private readonly int r_YOffsetForNewItem;
        private readonly string r_SoundPath;
        private readonly Game r_Game;
        private static double s_LastTimeThatUpdate = 0;
        private int m_NumberOfItemThatSelect = 0;
        private Vector2 m_NextPositionToAdd;
        private bool m_IsFirstInsertItem = true;
        private Vector2 m_LastMousePosition = Vector2.Zero;

        public ItemListManager(Game i_Game, int i_XOffset, int i_YOffset, Vector2 i_CenterOfViewPort, string i_SoundPath)
        {
            r_YOffsetForNewItem = i_YOffset;
            float firstOffsetInYAxis = i_CenterOfViewPort.Y / 2;
            m_NextPositionToAdd = new Vector2(i_XOffset, (i_CenterOfViewPort.Y - firstOffsetInYAxis) + i_YOffset);
            r_SoundPath = i_SoundPath;
            r_Game = i_Game;
            r_SoundManager = i_Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
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

            moveWithMouse();
            if(isSpecialItem && (r_InputManager.KeyPressed(Keys.PageUp) || r_InputManager.KeyPressed(Keys.PageDown) || isUserMouseWantChangeItem()))
            {
                someOfSpecialItemIsActive();
            }
            else if(r_InputManager.KeyPressed(Keys.Down))
            {
                selectGoingDownOrUp(k_SelectIsGoingDown);
            }
            else if(r_InputManager.KeyPressed(Keys.Up))
            {
                selectGoingDownOrUp(!k_SelectIsGoingDown);
            }
            else if(!isSpecialItem && !isRunAlreadyInThisTime && isUserWantEnterItem())
            {
                r_MenuItemsList[m_NumberOfItemThatSelect].OnClick();
            }

            s_LastTimeThatUpdate = i_GameTime.TotalGameTime.TotalSeconds;
        }

        private void moveWithMouse()
        {
            Vector2 mousePosition = new Vector2(r_InputManager.MouseState.X, r_InputManager.MouseState.Y);
            int newItemToSelect = 0;

            foreach (MenuItem menuItem in r_MenuItemsList)
            {
                if(menuItem.Bounds.Contains(mousePosition) && !menuItem.IsActive && r_Game.IsMouseVisible && mousePosition != m_LastMousePosition)
                {
                    r_MenuItemsList[m_NumberOfItemThatSelect].ItemIsInactive();
                    m_NumberOfItemThatSelect = newItemToSelect;
                    r_MenuItemsList[m_NumberOfItemThatSelect].ItemIsActive();
                }

                newItemToSelect++;
            }

            m_LastMousePosition = mousePosition;
        }

        private void someOfSpecialItemIsActive()
        {
            RangeItem rangeItem = r_MenuItemsList[m_NumberOfItemThatSelect] as RangeItem;
            
            if (rangeItem != null)
            {
                if (r_InputManager.KeyPressed(Keys.PageUp) || isMouseScrollWheelUp() || r_InputManager.ButtonPressed(eInputButtons.Right))
                {
                    rangeItem.ItemValue += rangeItem.ValueToDecreaseOrIncrease;
                }
                else if (r_InputManager.KeyPressed(Keys.PageDown) || isMouseScrollWheelDown())
                {
                    rangeItem.ItemValue -= rangeItem.ValueToDecreaseOrIncrease;
                }
            }

            r_MenuItemsList[m_NumberOfItemThatSelect].OnClick();
        }

        private bool isUserWantEnterItem()
        {
            return r_InputManager.KeyPressed(Keys.Enter) || (r_InputManager.ButtonPressed(eInputButtons.Left) && r_Game.IsMouseVisible);
        }

        private bool isUserMouseWantChangeItem()
        {
            return (r_Game.IsMouseVisible && r_InputManager.ButtonPressed(eInputButtons.Right)) || isMouseScrollWheelDown() || isMouseScrollWheelUp();
        }

        private bool isMouseScrollWheelDown()
        {
            return r_InputManager.ScrollWheelDelta < 0;
        }

        private bool isMouseScrollWheelUp()
        {
            return r_InputManager.ScrollWheelDelta > 0;
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

            r_MenuItemsList[m_NumberOfItemThatSelect].ItemIsActive();
        }

        public void AddNewItem(MenuItem i_MenuItem)
        {
            if (i_MenuItem != null)
            {
                i_MenuItem.Position = m_NextPositionToAdd;
                r_MenuItemsList.Add(i_MenuItem);
                m_NextPositionToAdd = new Vector2(m_NextPositionToAdd.X, m_NextPositionToAdd.Y + r_YOffsetForNewItem);
                if (m_IsFirstInsertItem)
                {
                    this.MenuItemsList[0].ItemIsActive();
                    m_IsFirstInsertItem = !m_IsFirstInsertItem;
                }

                if (r_SoundManager != null)
                {
                    i_MenuItem.SoundPath = r_SoundPath;
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