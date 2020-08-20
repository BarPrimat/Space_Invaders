using System;

namespace C20_Ex01_BarFrimet_313175176
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SpaceInvaders())
                game.Run();
        }
    }
}