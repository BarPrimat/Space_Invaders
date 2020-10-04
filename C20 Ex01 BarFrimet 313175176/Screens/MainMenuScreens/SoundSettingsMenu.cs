using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders;

namespace Screens.MainMenuScreens
{
    public class SoundSettingsMenu : GameScreen
    {
        private ItemListManager m_ItemListManager;
        private readonly Background r_Background;
        private readonly ISoundManager r_SoundManager;

        public SoundSettingsMenu(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
            r_SoundManager = i_Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            if(r_SoundManager == null)
            {
                SoundManager soundManager = new SoundManager(i_Game);
                r_SoundManager = i_Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            m_ItemListManager = new ItemListManager(this.Game, GameDefinitions.XOffsetOfMenuText, GameDefinitions.YOffsetOfMenuText, this.CenterOfViewPort, GameDefinitions.SoundNameForMenuMove);
            initMenuItems();
        }

        private void initMenuItems()
        {
            m_ItemListManager.AddNewItem(new ToggleItem("Toggle Sound: ", this, ToggleItem.eToggleItemType.OnOffItem, r_SoundManager.AreAllSoundsMuted));
            m_ItemListManager.LastInsertItem().ItemIsClicked += toggleSound_ItemIsClicked;
            m_ItemListManager.AddNewItem(new RangeItem("Background Music Volume: ", this, r_SoundManager.CurrentValueOfSongs));
            m_ItemListManager.LastInsertItem().ItemIsClicked += backgroundMusic_ItemIsClicked;
            m_ItemListManager.AddNewItem(new RangeItem("Sounds Effects Volume: ", this, r_SoundManager.CurrentValueOfSoundEffects));
            m_ItemListManager.LastInsertItem().ItemIsClicked += soundsEffects_ItemIsClicked;
            m_ItemListManager.AddNewItem(new MenuItem("Done", this));
            m_ItemListManager.LastInsertItem().ItemIsClicked += done_ItemIsClicked;
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            m_ItemListManager.Update(i_GameTime);
            // Can change with some button of mute in the game (In our game is M Key button) 
            ((ToggleItem) m_ItemListManager.MenuItemsList[0]).InitOption(r_SoundManager.AreAllSoundsMuted);
        }

        private void toggleSound_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            r_SoundManager.ToggleSound();
        }

        private void backgroundMusic_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            RangeItem rangeItem = i_Sender as RangeItem;

            if(rangeItem != null)
            {
                r_SoundManager.UpdateMediaPlayerVolume(rangeItem.ItemValue);
            }
        }

        private void soundsEffects_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            RangeItem rangeItem = i_Sender as RangeItem;

            if (rangeItem != null)
            {
                r_SoundManager.UpdateSoundEffectMasterVolume(rangeItem.ItemValue);
            }
        }

        private void done_ItemIsClicked(object i_Sender, EventArgs i_EventArgs)
        {
            this.ExitScreen();
        }
    }
}
