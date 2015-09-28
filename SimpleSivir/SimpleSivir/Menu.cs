using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace SimpleSivir
{
    internal class MenuX
    {
        public static Menu Sivir, ComboMenu, HarassMenu, ShieldMenu, KsMenu, MiscMenu;

        public static void CallMeNiga()
        {
            Sivir = MainMenu.AddMenu("Sivir", "Sivir");
            Sivir.AddGroupLabel("Sivir add0n :)");
            Sivir.AddSeparator();
            Sivir.AddLabel("Made by Kk2");

            ComboMenu = Sivir.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Options");
            ComboMenu.AddSeparator();
            ComboMenu.Add("useQc", new CheckBox("Use Q"));
            ComboMenu.Add("useWc", new CheckBox("Use W"));
            ComboMenu.Add("autoR", new CheckBox("Auto R"));

            HarassMenu = Sivir.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Options");
            HarassMenu.AddSeparator();
            HarassMenu.Add("useQh", new CheckBox("Use Q"));
            HarassMenu.Add("useWh", new CheckBox("Use W"));
            HarassMenu.Add("mppc", new Slider("Mana % for Harras", 20));

            ShieldMenu = Sivir.AddSubMenu("Shield", "Eshield");
            ShieldMenu.AddGroupLabel("Shield Options");
            ShieldMenu.AddSeparator();
            ShieldMenu.Add("autoE", new CheckBox("Auto E"));
            ShieldMenu.Add("AntiGap", new CheckBox("Anti GapCloser E"));

            KsMenu = Sivir.AddSubMenu("KillSteal", "ks");
            KsMenu.AddGroupLabel("KillSteal Options");
            KsMenu.AddSeparator();
            KsMenu.Add("useQks", new CheckBox("Use Q on KS"));
            KsMenu.Add("useWks", new CheckBox("Use W on KS"));

            MiscMenu = Sivir.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc Options");
            MiscMenu.AddSeparator();
            MiscMenu.Add("drawQ", new CheckBox("Draw Q"));
            MiscMenu.Add("drawAA", new CheckBox("Draw AACircle on Minions Killable"));
            MiscMenu.Add("drawRTimer", new CheckBox("Draw R Timer"));
            MiscMenu.Add("usePot", new CheckBox("Use Life Potions"));
            MiscMenu.Add("autoQ", new CheckBox("Autocast Q On Immobile Targets"));
        }
    }
}