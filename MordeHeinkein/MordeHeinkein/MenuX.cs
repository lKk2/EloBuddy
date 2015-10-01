using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace MordeHeinkein
{
    internal class MenuX
    {
        public static Menu Mordekaiser, Combo, Harass, LaneClear, JungleClear, Drawing, Misc;
        public static Slider skinSelect;
        // Init
        public static void GetMenu()
        {
            Mordekaiser = MainMenu.AddMenu("Mordekaiser", "Mordekaiser");

            Combo = Mordekaiser.AddSubMenu("Combo", "Combo");
            Combo.AddGroupLabel("Combo Options");
            Combo.AddSeparator();
            Combo.Add("useQC", new CheckBox("Use Q"));
            Combo.Add("useEC", new CheckBox("Use E"));
            Combo.Add("useWC", new CheckBox("Use W"));
            Combo.Add("useRC", new CheckBox("Use R"));

            Harass = Mordekaiser.AddSubMenu("Harass", "Harass");
            Harass.AddGroupLabel("Harass Options");
            Harass.AddSeparator();
            Harass.Add("useEH", new CheckBox("use E"));
            Harass.Add("useQH", new CheckBox("use Q"));
            Harass.Add("useWH", new CheckBox("use W"));
            Harass.Add("HPSliderH", new Slider("HP % > for Harass", 20));

            LaneClear = Mordekaiser.AddSubMenu("LaneClear", "LaneClear");
            LaneClear.AddGroupLabel("Lane Clear Options");
            LaneClear.AddSeparator();
            LaneClear.Add("UseEL", new CheckBox("Use E"));
            LaneClear.Add("UseQL", new CheckBox("Use Q"));

            JungleClear = Mordekaiser.AddSubMenu("JungleClear", "JungleClear");
            JungleClear.AddGroupLabel("Jungle Clear Options");
            JungleClear.AddSeparator();
            JungleClear.Add("UseEJ", new CheckBox("Use E"));
            JungleClear.Add("UseWJ", new CheckBox("Use W"));
            JungleClear.Add("UseQJ", new CheckBox("Use Q"));

            Misc = Mordekaiser.AddSubMenu("Misc", "misc");
            Misc.AddGroupLabel("Misc Options");
            Misc.AddSeparator();
            Misc.Add("UsePot", new CheckBox("Use Potions"));
            Misc.Add("AutoPilot", new CheckBox("AutoPilot Ult Ghosts"));
            Misc.AddSeparator();
            skinSelect = Misc.Add("ChangeSkin", new Slider("Change Skin [Number]", 2, 0, 5));

            Drawing = Mordekaiser.AddSubMenu("Drawings", "Drawings");
            Drawing.AddGroupLabel("Drawing Options");
            Drawing.AddSeparator();
            Drawing.Add("drawQ", new CheckBox("Draw Q"));
            Drawing.Add("drawW", new CheckBox("Draw W"));
            Drawing.Add("drawE", new CheckBox("Draw E"));
            Drawing.Add("drawR", new CheckBox("Draw R"));
        }
    }
}