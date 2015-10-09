using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace SwagZilean
{
    internal class MenuX
    {
        public static Menu Zilean, Combo, Harass, Misc, UltMenu, LaneClear, Draw;
        public static Slider SkinSelect, ComboSlider, PredictionSlider;
        public static string[] CombosZileans = {"Fast Zilean", "Slow Zilean", "Focus AD Zilean"};
        public static string[] PredicOptions = {"Low", "Medium", "High"};

        public static void getMenu()
        {
            Zilean = MainMenu.AddMenu("SwagZilean", "SwagZilean");
            Zilean.AddGroupLabel("Swaaaaaaaaaaaaaaaaaaaaaaag Zilean");
            Zilean.AddSeparator();
            Zilean.AddLabel("Requested by yorik100");
            Zilean.AddLabel("Made by Kk2");

            /*
            Combo Menu
            */
            Combo = Zilean.AddSubMenu("Combo", "Combo");
            Combo.AddGroupLabel("Combo Options");
            Combo.AddSeparator();
            Combo.Add("comboQ", new CheckBox("Use Q on Combo"));
            Combo.Add("comboW", new CheckBox("Use W on Combo"));
            Combo.Add("comboE", new CheckBox("Use E on Combo"));
            Combo.AddSeparator();
            ComboSlider = Combo.Add("whatcombo", new Slider("Choose your Combo: ", 0, 0, 2));
            ComboSlider.OnValueChange +=
                delegate { ComboSlider.DisplayName = "Choose your Combo: " + CombosZileans[ComboSlider.CurrentValue]; };
            ComboSlider.DisplayName = "Choose your Combo: " + CombosZileans[ComboSlider.CurrentValue];
            Combo.AddSeparator();
            PredictionSlider = Combo.Add("dPrediction", new Slider("Spell Prediction: ", 2, 0, 2));
            PredictionSlider.OnValueChange +=
                delegate
                {
                    PredictionSlider.DisplayName = "Spell Prediction: " + PredicOptions[PredictionSlider.CurrentValue];
                };
            PredictionSlider.DisplayName = "Spell Prediction: " + PredicOptions[PredictionSlider.CurrentValue];

            /*
            Harass Menu
            */
            Harass = Zilean.AddSubMenu("Harass", "Harass");
            Harass.AddGroupLabel("Harass Options");
            Harass.AddSeparator();
            Harass.Add("harassQ", new CheckBox("Use Q on Harass"));
            Harass.Add("harrasW", new CheckBox("Use W on Harass"));
            Harass.Add("harrasE", new CheckBox("Use E on Harass"));
            Harass.AddSeparator();
            Harass.Add("hManaSlider", new Slider("Mana % > to Harass", 20));

            /*
            LaneClear Menu
            */
            LaneClear = Zilean.AddSubMenu("LaneClear", "LaneClear");
            LaneClear.AddGroupLabel("LaneClear Options");
            LaneClear.AddSeparator();
            LaneClear.Add("laneQ", new CheckBox("Use Q on LaneClear"));
            LaneClear.Add("laneW", new CheckBox("Use W on LaneClear"));
            LaneClear.AddSeparator();
            LaneClear.Add("lManaSlider", new Slider("Mana % > to LaneClear", 20));

            /*
            Ult Menu
            */
            UltMenu = Zilean.AddSubMenu("UltMenu", "UltMenu");
            UltMenu.AddGroupLabel("Ultimate Options");
            UltMenu.AddSeparator();
            foreach (var h in EntityManager.Heroes.Allies)
            {
                UltMenu.AddSeparator();
                UltMenu.Add("r" + h.ChampionName, new CheckBox("Ult ON " + h.ChampionName));
                UltMenu.AddSeparator();
                UltMenu.Add("rpct" + h.ChampionName, new Slider("Health % " + h.ChampionName, 10));
            }

            /*
            Misc Menu
            */
            Misc = Zilean.AddSubMenu("Misc", "Misc");
            Misc.AddGroupLabel("Misc Options");
            Misc.AddSeparator();
            Misc.Add("Support", new CheckBox("Support Mode"));
            Misc.Add("gapCloser", new CheckBox("Use E on GapCloser"));
            Misc.Add("Interrupt", new CheckBox("Try to Interrupt with double Q"));
            Misc.AddSeparator();
            SkinSelect = Misc.Add("skinX", new Slider("Choose your Skin [number]:", 5, 0, 5));

            /*
            Drawings Menu
            */
            Draw = Zilean.AddSubMenu("Drawings", "Drawings");
            Draw.AddGroupLabel("Drawings Options");
            Draw.AddSeparator();
            Draw.Add("drawQ", new CheckBox("Draw Q Range"));
            Draw.Add("drawE", new CheckBox("Draw E Range"));
            Draw.Add("drawR", new CheckBox("Draw R Range"));
            Draw.Add("cMode", new CheckBox("Draw Current Combo Mode"));
        }
    }
}