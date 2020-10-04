using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;
using static SpaceInvaders.GameDefinitions;


namespace Screens.MainMenuScreens
{
    public class MainMenuScreen : GameScreen
    {
        private ItemListManager m_ItemListManager;
        private readonly Background r_Background;

        public MainMenuScreen(Game i_Game) : base(i_Game)
        {
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
            m_ItemListManager.AddNewItem(new MenuItem("Screen Settings", this));
            m_ItemListManager.LastInsertItem().ItemIsClicked += screenSettings_ItemIsClicked;
            m_ItemListManager.AddNewItem(new ToggleItem("Players: ", this, ToggleItem.eToggleItemType.OneTwoPlayersItem, SettingsCollection.NumberOfPlayersIsOne));
            m_ItemListManager.LastInsertItem().ItemIsClicked += updateNumberOfPlayers_ItemIsClicked;
            m_ItemListManager.AddNewItem(new MenuItem("Sound Settings", this));
            m_ItemListManager.LastInsertItem().ItemIsClicked += soundSettings_ItemIsClicked;
            m_ItemListManager.AddNewItem(new MenuItem("Play", this));
            m_ItemListManager.LastInsertItem().ItemIsClicked += play_ItemIsClicked;
            m_ItemListManager.AddNewItem(new MenuItem("Quit", this));
            m_ItemListManager.LastInsertItem().ItemIsClicked += quit_ItemIsClicked;
        }

        public override void Update(GameTime i_GameTime)
        {
            m_ItemListManager.Update(i_GameTime);
            base.Update(i_GameTime);
        }

        private void screenSettings_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            ScreensManager.SetCurrentScreen(new ScreenSettingsMenu(this.Game));
        }

        private void updateNumberOfPlayers_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            SettingsCollection.NumberOfPlayers++;
        }

        private void soundSettings_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            ScreensManager.SetCurrentScreen(new SoundSettingsMenu(this.Game));
        }

        private void play_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(this.Game));
        }

        private void quit_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            this.Game.Exit();
        }
    }
}
