using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders;

namespace Screens.MainMenuScreens
{
    public class SoundSettingsMenu : GameScreen
    {
        private ItemListManager m_ItemListManager;
        private readonly Background r_Background;

        public SoundSettingsMenu(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ItemListManager = new ItemListManager(this.Game, GameDefinitions.XOffsetOfMenuText, GameDefinitions.YOffsetOfMenuText, this.CenterOfViewPort);
            initMenuItems();
        }

        private void initMenuItems()
        {
            m_ItemListManager.AddNewItem(new ToggleItem("Toggle Sound: ", this, ToggleItem.eToggleItemType.OnOffItem));
            m_ItemListManager.LastInsertItem().ItemIsClicked += toggleSound_ItemIsClicked;
            m_ItemListManager.AddNewItem(new RangeItem("Background Music Volume: ", this,  SettingsCollection.BackgroundMusicVolume));
            m_ItemListManager.LastInsertItem().ItemIsClicked += backgroundMusic_ItemIsClicked;
            m_ItemListManager.AddNewItem(new RangeItem("Sounds Effects Volume: ", this, SettingsCollection.SoundsEffectsVolume));
            m_ItemListManager.LastInsertItem().ItemIsClicked += SoundsEffects_ItemIsClicked;
            m_ItemListManager.AddNewItem(new MenuItem("Done", this));
            m_ItemListManager.LastInsertItem().ItemIsClicked += done_ItemIsClicked;
        }

        private void toggleSound_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            throw new NotImplementedException();
        }

        private void backgroundMusic_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            throw new NotImplementedException();
        }

        private void SoundsEffects_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            throw new NotImplementedException();
        }

        private void done_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
