using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KayleBuddy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        /** 
          Variables specific for my Hero~
        **/
        public static Spell.Targeted Q;
        public static Spell.Targeted W;
        public static Spell.Active E;
        public static Spell.Targeted R;
        public static Menu KayleMenu, ComboMenu, HarassMenu, FarmMenu, UltMenu;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            // init stufferino
            Bootstrap.Init(null);
            Chat.Print("KayleBuddy LOADED");

            Q = new Spell.Targeted(SpellSlot.Q, 650);
            W = new Spell.Targeted(SpellSlot.W, 900);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Targeted(SpellSlot.R, 900);

            /**
               All Menus Below
             **/

            KayleMenu = MainMenu.AddMenu("KayleBuddy", "kaylebuddy");
            KayleMenu.AddGroupLabel("KayleBuddy");
            KayleMenu.AddSeparator();
            KayleMenu.AddLabel("Made by Kk2");

            ComboMenu = KayleMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.AddSeparator();
            ComboMenu.Add("useQCombo", new CheckBox("Use Q"));
            ComboMenu.Add("useWCombo", new CheckBox("Use Smart W"));
            ComboMenu.Add("useECombo", new CheckBox("Use E"));

            UltMenu = KayleMenu.AddSubMenu("Ultimate Manager", "Ult");
            UltMenu.AddGroupLabel("Ultimate Settings");
            UltMenu.AddSeparator();
            UltMenu.Add("useRSelf", new CheckBox("SELF ULT"));
          // not yet !  UltMenu.Add("UseRAlly", new CheckBox("Use ULT ON ALLY"));
            UltMenu.Add("MinHPR", new Slider("HP % to use Ult", 30));

            HarassMenu = KayleMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.AddSeparator();
            HarassMenu.Add("useQHarass", new CheckBox("Use Q"));
            HarassMenu.Add("useEHarass", new CheckBox("Use E"));

            FarmMenu = KayleMenu.AddSubMenu("Farm", "Farm");
            FarmMenu.AddGroupLabel("Farming Settings");
            FarmMenu.AddSeparator();
            FarmMenu.Add("useEWave", new CheckBox("Use E for Waveclear"));
            FarmMenu.Add("useQFarm", new CheckBox("Use Q To LastHit"));


            // Time for the magic~
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            Brain.UltManager();

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Brain.Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Brain.Harass();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                Brain.WaveClear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                Brain.LastHit();
            }
        }
    }
}