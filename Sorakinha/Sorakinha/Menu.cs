using System.Linq;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Sorakinha
{
    internal static class MenuX
    {
        private static Menu Soraka;
        public static Menu Combo, Harass, Healing, Drawing, Misc;
        private static string[] PredictionSliderValues = {"Low", "Medium", "High"};
        public static Slider SkinSelect;
        /*
        Create the Menu ^.^
        */

        public static void CallMeNigga()
        {
            // Main Menu
            Soraka = MainMenu.AddMenu("Soraka", "Soraka");
            Soraka.AddGroupLabel("Sorakinha ^.^");
            Soraka.AddSeparator();
            Soraka.AddLabel("Cause some Healing is needed!");
            Soraka.AddLabel("Made by Kk2 (:");

            // Combo Menu
            Combo = Soraka.AddSubMenu("Combo", "Combo");
            Combo.AddGroupLabel("Combo Options >.<");
            Combo.AddSeparator();
            Combo.Add("useQCombo", new CheckBox("Use Q"));
            Combo.Add("useECombo", new CheckBox("Use E"));
            Combo.Add("minMcombo", new Slider("Mana %", 20));

            // Harass Menu
            Harass = Soraka.AddSubMenu("Harass", "Harass");
            Harass.AddGroupLabel("Harass Options ¬¬");
            Harass.AddSeparator();
            Harass.Add("useQHarass", new CheckBox("Use Q"));
            Harass.Add("useEHarass", new CheckBox("Use E"));
            Harass.Add("minMharass", new Slider("Mana % for Harras", 20));
            Harass.AddSeparator();
            var sliderValue = Harass.Add("predNeeded", new Slider("Prediction Hitchange: ", 0, 0, 2));
            sliderValue.OnValueChange +=
                delegate
                {
                    sliderValue.DisplayName = "Prediction Hitchange: " + PredictionSliderValues[sliderValue.CurrentValue];
                };
            sliderValue.DisplayName = "Prediction Hitchange: " + PredictionSliderValues[sliderValue.CurrentValue];

            // Healing Menu
            Healing = Soraka.AddSubMenu("Healing", "Healing");
            Healing.AddGroupLabel("W Settings ~.~");
            Healing.AddSeparator();
            Healing.Add("useW", new CheckBox("Auto W"));
            Healing.Add("dontWF", new CheckBox("Dont W in Fountain"));
            Healing.AddSeparator();

            /** 
            The Magic ~ 
            **/
            foreach (var hero in EntityManager.Heroes.Allies.Where(x => !x.IsMe))
            {
                Healing.AddSeparator();
                Healing.Add("w" + hero.ChampionName, new CheckBox("Heal " + hero.ChampionName));
                Healing.AddSeparator();
                Healing.Add("wpct" + hero.ChampionName, new Slider("Health % " + hero.ChampionName, 45));
            }
            Healing.AddSeparator();
            Healing.AddGroupLabel("R Settings ^.~");
            Healing.AddSeparator();
            Healing.Add("useR", new CheckBox("Use R"));
            Healing.Add("useRslider", new Slider("HP % to R", 10));
            /** 
            End of The Magic Kappa
            **/

            // Misc Menu
            Misc = Soraka.AddSubMenu("Misc", "Misc");
            Misc.AddGroupLabel("Misc Settings 0.o");
            Misc.AddSeparator();
            Misc.Add("useQGapCloser", new CheckBox("Q on GapCloser"));
            Misc.Add("useEGapCloser", new CheckBox("E on GapCloser"));
            Misc.Add("eInterrupt", new CheckBox("use E to Interrupt"));
            Misc.Add("AttackMinions", new CheckBox("Attack Minions"));
            SkinSelect = Misc.Add("skinSelect", new Slider("Choose you Skin [number]", 0, 0, 5));

            // Drawing Menu
            Drawing = Soraka.AddSubMenu("Drawing", "Drawing");
            Drawing.AddGroupLabel("Drawing Options :~");
            Drawing.AddSeparator();
            Drawing.Add("drawQ", new CheckBox("Draw Q"));
            Drawing.Add("drawE", new CheckBox("Draw E"));
            Drawing.AddSeparator();
            Drawing.Add("drawH", new CheckBox("Draw H on Healing Needed Heroes"));
        }
    }
}