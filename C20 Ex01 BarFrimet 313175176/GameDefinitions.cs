using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_BarFrimet_313175176
{
    public class GameDefinitions
    {
        private const string k_GameName = "Space Invaders";
        private const int k_PreferredBackBufferWidth = 1024;
        private const int k_PreferredBackBufferHeight = 704;

        public static string GameName => k_GameName;

        public static int PreferredBackBufferWidth => k_PreferredBackBufferWidth;

        public static int PreferredBackBufferHeight => k_PreferredBackBufferHeight;
    }
}
