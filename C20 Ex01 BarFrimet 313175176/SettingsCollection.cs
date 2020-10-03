using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public static class SettingsCollection
    {
        private static int s_NumberOfPlayers = GameDefinitions.NumberOfPlayers;
        private static int s_CurrentLevel = 1;
        private static int s_BackgroundMusicVolume = GameDefinitions.StartBackgroundMusicVolume;
        private static int s_SoundsEffectsVolume = GameDefinitions.StartSoundsEffectsVolume;
        private static bool s_AllowUserResizing = false;
        private static bool s_ToggleFullScreen = false;
        private static bool s_IsMouseVisible = true;

        public static int NumberOfPlayers
        {
            get => s_NumberOfPlayers;
            set => s_NumberOfPlayers = (value % 3) + (value % 3 == 0 ? 1 : 0);
        }
        public static int CurrentLevel
        {
            get => s_CurrentLevel;
            set => s_CurrentLevel = value;
        }
        public static bool AllowUserResizing
        {
            get => s_AllowUserResizing;
            set => s_AllowUserResizing = value;
        }
        public static bool ToggleFullScreen
        {
            get => s_ToggleFullScreen;
            set => s_ToggleFullScreen = value;
        }
        public static bool IsMouseVisible
        {
            get => s_IsMouseVisible;
            set => s_IsMouseVisible = value;
        }

        public static bool NumberOfPlayersIsOne => s_NumberOfPlayers == 1 ? true : false;

        public static int BackgroundMusicVolume
        {
            get => s_BackgroundMusicVolume;
            set => s_BackgroundMusicVolume = value;
        }
        public static int SoundsEffectsVolume
        {
            get => s_SoundsEffectsVolume;
            set => s_SoundsEffectsVolume = value;
        }
    }
}
