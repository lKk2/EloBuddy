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

namespace kTwitch
{
    class MenuX
    {
        public static Menu TwitchMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu FarmMenu;
        public static Menu MiscMenu;

        public static void CallMeNigga()
        {
            TwitchMenu = MainMenu.AddMenu("kTwitch", "kTwitch");
            TwitchMenu.AddGroupLabel("kTwitch d(0.o)b");
            TwitchMenu.AddSeparator();
            TwitchMenu.AddLabel("Made by Kk2");

            ComboMenu = TwitchMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Settings");
            ComboMenu.AddSeparator();
            ComboMenu.Add("useWCombo", new CheckBox("Use W"));
            ComboMenu.Add("useRCombo", new CheckBox("Use R"));
            ComboMenu.Add("minRCombo", new Slider("Minimun to R", 2, 1, 5));

            HarassMenu = TwitchMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.AddSeparator();
            HarassMenu.Add("useWHarass", new CheckBox("Use W"));
            HarassMenu.Add("minWManaH", new Slider("Minimun Mana to W", 20));
            HarassMenu.AddSeparator();
            HarassMenu.Add("autoEHarass", new CheckBox("Auto E on Stacks"));
            HarassMenu.Add("minEHarras", new Slider("How many Stacks", 4, 1, 5));

            FarmMenu = TwitchMenu.AddSubMenu("Farming", "Farming");
            FarmMenu.AddGroupLabel("LaneClear Settings");
            FarmMenu.AddSeparator();
            FarmMenu.AddLabel("Soon Kappa!");
            //FarmMenu.Add("useWFarm", new CheckBox("Use W"));
            //FarmMenu.Add("useEFarm", new CheckBox("Use E on LaneClear"));
            //FarmMenu.Add("useEAmmo", new Slider("How many minions with E", 3, 1, 5));


            MiscMenu = TwitchMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc Settings");
            MiscMenu.AddSeparator();
            MiscMenu.Add("EKillsteal", new CheckBox("Killsteal with E"));
            MiscMenu.AddSeparator();
            MiscMenu.AddGroupLabel("Items Settings");
            MiscMenu.AddSeparator();
            MiscMenu.Add("useHP", new CheckBox("Use Health Potion"));
            MiscMenu.Add("useBTRK", new CheckBox("Use BTRK"));
            MiscMenu.Add("useYOMU", new CheckBox("Use YOMUMUMUMUMUMUMUMU!"));

        }
    }
}
