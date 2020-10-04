using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders;
using static SpaceInvaders.GameDefinitions;

namespace Screens.MainMenuScreens
{
    public class ScreenSettingsMenu : GameScreen
    {
        private readonly Background r_Background;
        private ItemListManager m_ItemListManager;

        public ScreenSettingsMenu(Game i_Game) : base(i_Game)
        {
            this.IsModal = true;
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ItemListManager = new ItemListManager(this.Game, GameDefinitions.XOffsetOfMenuText, GameDefinitions.YOffsetOfMenuText, this.CenterOfViewPort, GameDefinitions.SoundNameForMenuMove);
            initMenuItems();
        }

        private void initMenuItems()
        {
            m_ItemListManager.AddNewItem(new ToggleItem("Allow Window Resizing: ", this, ToggleItem.eToggleItemType.OnOffItem, SettingsCollection.AllowUserResizing));
            m_ItemListManager.LastInsertItem().ItemIsClicked += allowWindowResizing_ItemIsClicked;
            m_ItemListManager.AddNewItem(new ToggleItem("Full Screen Mode: ", this, ToggleItem.eToggleItemType.OnOffItem, SettingsCollection.ToggleFullScreen));
            m_ItemListManager.LastInsertItem().ItemIsClicked += fullScreenMode_ItemIsClicked;
            m_ItemListManager.AddNewItem(new ToggleItem("Mouse Visability: ", this, ToggleItem.eToggleItemType.VisibleInvisibleItem, SettingsCollection.IsMouseVisible));
            m_ItemListManager.LastInsertItem().ItemIsClicked += mouseVisability_ItemIsClicked;
            m_ItemListManager.AddNewItem(new MenuItem("Done", this));
            m_ItemListManager.LastInsertItem().ItemIsClicked += done_ItemIsClicked;
        }

        public override void Update(GameTime i_GameTime)
        {
            m_ItemListManager.Update(i_GameTime);
            base.Update(i_GameTime);
        }

        private void allowWindowResizing_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            this.Game.Window.AllowUserResizing = !this.Game.Window.AllowUserResizing;
            SettingsCollection.AllowUserResizing = this.Game.Window.AllowUserResizing;
        }

        private void fullScreenMode_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            GraphicsDeviceManager graphicsDeviceManager = this.Game.Services.GetService(typeof(IGraphicsDeviceManager)) as GraphicsDeviceManager;

            graphicsDeviceManager.ToggleFullScreen();
            SettingsCollection.ToggleFullScreen = !SettingsCollection.ToggleFullScreen;
        }

        private void mouseVisability_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            this.Game.IsMouseVisible = !this.Game.IsMouseVisible;
            SettingsCollection.IsMouseVisible = this.Game.IsMouseVisible;
        }

        private void done_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            this.ExitScreen();
        }
    }
}
