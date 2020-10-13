using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public static class SettingsCollection
    {
        private static int s_NumberOfPlayers = GameDefinitions.NumberOfPlayers;
        private static bool s_AllowUserResizing = false;
        private static bool s_ToggleFullScreen = false;
        private static bool s_IsMouseVisible = true;

        public static int NumberOfPlayers
        {
            get => s_NumberOfPlayers;
            set => s_NumberOfPlayers = (value % 3) + (value % 3 == 0 ? 1 : 0);
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
    }
}