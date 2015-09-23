using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace SimpleSivir
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName.ToLower() != "sivir") return;
            Bootstrap.Init(null);
            Brain.runSpells();
            MenuX.CallMeNiga();
            Game.OnTick += Brain.Game_OnTick;
            Drawing.OnEndScene += Brain.Drawing_OnDraw;
            Gapcloser.OnGapcloser += Brain.AntiGapCloser_OnEnemyGapcloser;
            Obj_AI_Base.OnProcessSpellCast += Brain.OnProcessSpellCastX;
            Chat.Print("SimpleSivir Loaded <3", Color.Purple);
        }
    }
}