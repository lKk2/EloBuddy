using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using KogMala.AllahuAkbar;

namespace KogMala
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loaded;
        }

        private static void Loaded(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName.ToLower() != "kogmaw") return;
            Brain.Init();
        }
    }
}