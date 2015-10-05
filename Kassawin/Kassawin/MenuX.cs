using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Kassawin
{
    internal class MenuX
    {
        public static Menu Kassawin, Combo, Harass, LaneClear, JungleClear, Misc;
        public static Slider SkinSelect;

        public static void GetMenu()
        {
            Kassawin = MainMenu.AddMenu("KassaWIN", "KassaWIN");
            Kassawin.AddGroupLabel("Free Win with Kassadin");
            Kassawin.AddSeparator();
            Kassawin.AddLabel("Made by Kk2");

            Combo = Kassawin.AddSubMenu("Combo", "Combo");
            Combo.AddGroupLabel("Combo Options");
            Combo.AddSeparator();
            Combo.Add("ComboQ", new CheckBox("Use Q on Combo"));
            Combo.Add("ComboW", new CheckBox("Use W on Combo"));
            Combo.Add("ComboE", new CheckBox("Use E on Combo"));
            Combo.Add("ComboR", new CheckBox("Use R on Combo"));
            Combo.Add("IgniteToKill", new CheckBox("Use Ignite on Combo to Kill"));
            Combo.Add("sliderR", new Slider("Max Heroes Around to Cast R", 3, 1, 5));

            Harass = Kassawin.AddSubMenu("Harass", "Harass");
            Harass.AddGroupLabel("Harass Options");
            Harass.AddSeparator();
            Harass.Add("HarassQ", new CheckBox("Use Q on Harass"));
            Harass.Add("HarassW", new CheckBox("Use W on Harass"));
            Harass.Add("HarassE", new CheckBox("Use E on Harass"));
            Harass.Add("manaPCTH", new Slider("Min Mana % to Harass", 20));

            LaneClear = Kassawin.AddSubMenu("LaneClear", "LaneClear");
            LaneClear.AddGroupLabel("LaneClear Options");
            LaneClear.AddSeparator();
            LaneClear.Add("LaneQ", new CheckBox("Use Q on LaneClear"));
            LaneClear.Add("LaneW", new CheckBox("Use W on LaneClear"));
            LaneClear.Add("LaneE", new CheckBox("Use E on LaneClear"));
            LaneClear.Add("manaPCTL", new Slider("Min Mana % to LaneClear", 20));

            JungleClear = Kassawin.AddSubMenu("Jungle", "Jungle");
            JungleClear.AddGroupLabel("Jungle Options");
            JungleClear.AddSeparator();
            JungleClear.Add("JungleQ", new CheckBox("Use Q on Jungle"));
            JungleClear.Add("JungleW", new CheckBox("Use W on Jungle"));
            JungleClear.Add("JungleE", new CheckBox("Use E on Jungle"));
            JungleClear.Add("manaPCTJ", new Slider("Min Mana % to Jungle Clear", 20));

            Misc = Kassawin.AddSubMenu("Misc", "Misc");
            Misc.AddGroupLabel("Misc Options");
            Misc.AddSeparator();
            Misc.Add("usePot", new CheckBox("Use Potions"));
            Misc.AddSeparator();
            Misc.Add("drawQ", new CheckBox("Draw Q Range"));
            Misc.Add("drawW", new CheckBox("Draw W Range"));
            Misc.Add("drawE", new CheckBox("Draw E Range"));
            Misc.Add("drawR", new CheckBox("Draw R Range"));
            Misc.AddSeparator();
            SkinSelect = Misc.Add("skinSelect", new Slider("Choose you Skin [number]", 0, 0, 6));
        }
    }
}