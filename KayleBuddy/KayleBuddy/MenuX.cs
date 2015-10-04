using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KayleBuddy
{
    class MenuX
    {
        public static Menu KayleMenu, ComboMenu, HarassMenu, JungleMenu, FarmMenu, HealingMenu, UltMenu, MiscMenu;
        public static Slider skinSelect;

        public static void getMenu()
        {
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
            
            HarassMenu = KayleMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Settings");
            HarassMenu.AddSeparator();
            HarassMenu.Add("useQHarass", new CheckBox("Use Q"));
            HarassMenu.Add("useEHarass", new CheckBox("Use E"));
            HarassMenu.Add("manaHarass", new Slider("Mana % > to Use Spells", 20));

            FarmMenu = KayleMenu.AddSubMenu("Farm", "Farm");
            FarmMenu.AddGroupLabel("Farming Settings");
            FarmMenu.AddSeparator();
            FarmMenu.Add("useEWave", new CheckBox("Use E for Waveclear"));
            FarmMenu.Add("useQFarm", new CheckBox("Use Q To LastHit"));
            FarmMenu.Add("manaFarm", new Slider("Mana % > to Use Spells", 20));

            JungleMenu = KayleMenu.AddSubMenu("Jungle", "Jungle");
            JungleMenu.AddGroupLabel("Jungle Settings");
            JungleMenu.AddSeparator();
            JungleMenu.Add("useEJungle", new CheckBox("Use E"));
            JungleMenu.Add("useQJungle", new CheckBox("Use Q"));
            JungleMenu.Add("manaJungle", new Slider("Mana % > to Use Spells", 20));

            HealingMenu = KayleMenu.AddSubMenu("Healing", "Healing");
            HealingMenu.AddGroupLabel("Healing Menu");
            HealingMenu.AddSeparator();
            foreach (var h in HeroManager.Allies)
            {
                HealingMenu.Add("useW" + h.ChampionName, new CheckBox("Heal " + h.ChampionName));
                HealingMenu.Add("minHPW" + h.ChampionName, new Slider("HP % to Heal " + h.ChampionName, 20));
                HealingMenu.AddSeparator();
            }

            UltMenu = KayleMenu.AddSubMenu("Ultimate Manager", "Ult");
            UltMenu.AddGroupLabel("Ultimate Settings");
            UltMenu.AddSeparator();
            foreach (var h in HeroManager.Allies)
            {
                UltMenu.Add("UseR" + h.ChampionName, new CheckBox("Ult on " + h.ChampionName));
                UltMenu.Add("minHPR" + h.ChampionName, new Slider("HP % to Ult " + h.ChampionName, 20));
                UltMenu.AddSeparator();
            }

            MiscMenu = KayleMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc Settings");
            MiscMenu.Add("usePot", new CheckBox("Use Potions"));
            MiscMenu.Add("drawQ", new CheckBox("Draw Q Range"));
            MiscMenu.Add("drawH", new CheckBox("Draw an H on HPbar of Needed Healing Allies"));
            skinSelect = MiscMenu.Add("ChangeSkin", new Slider("Change Skin [Number]", 7, 0, 7));

        }
    }
}
