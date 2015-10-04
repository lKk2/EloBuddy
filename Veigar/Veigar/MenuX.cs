using System;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Veigar
{
    internal class MenuX
    {
        public static Menu Veigar, Combo, Harass, KillSteal, LaneClear, Misc, LastHit, Drawing;
        public static Slider skinSelect;
        public static string[] PredictValues = {"Low", "Medium", "High"};

        public static void CallMenu()
        {
            Veigar = MainMenu.AddMenu("Veigar", "Veigar");
            Veigar.AddGroupLabel("Veigar the badASS");
            Veigar.AddSeparator();
            Veigar.AddLabel("Made by Kk2");

            // Combo
            Combo = Veigar.AddSubMenu("Combo", "Combo");
            Combo.AddGroupLabel("Combo Options");
            Combo.AddSeparator();
            Combo.Add("useQCombo", new CheckBox("Use Q"));
            Combo.Add("useWCombo", new CheckBox("Use W"));
            Combo.Add("useWComboS", new CheckBox("Use W Only on Stunned Targets"));
            Combo.Add("useECombo", new CheckBox("Use E"));
            Combo.Add("useRCombo", new CheckBox("Use R"));
            Combo.Add("useIGCombo", new CheckBox("Use Ignite to Kill"));
            Combo.AddSeparator();
            var SliderComboPredict = Combo.Add("SliderCPredict", new Slider("Spell Prediction: ", 1, 0, 2));
            SliderComboPredict.OnValueChange += delegate
            {
                SliderComboPredict.DisplayName = "Spell Prediction: " + PredictValues[SliderComboPredict.CurrentValue];
            };
            SliderComboPredict.DisplayName = "Spell Prediction: " + PredictValues[SliderComboPredict.CurrentValue];

            //Harass
            Harass = Veigar.AddSubMenu("Harass", "Harass");
            Harass.AddGroupLabel("Harass Options");
            Harass.AddSeparator();
            Harass.Add("useQH", new CheckBox("Use Q"));
            Harass.Add("useWH", new CheckBox("Use W"));
            Harass.Add("useWHS", new CheckBox("Use W Only on Stunned Targets"));
            Harass.Add("useEH", new CheckBox("Use E"));
            Harass.Add("minManaH", new Slider("Min Mana % for Harass", 20));
            Harass.AddSeparator();
            var sliderHarassPredict = Harass.Add("SliderHPredict", new Slider("Spell Prediction: ", 1, 0, 2));
            sliderHarassPredict.OnValueChange += delegate
            {
                sliderHarassPredict.DisplayName = "Spell Prediction: " + PredictValues[sliderHarassPredict.CurrentValue];
            };
            sliderHarassPredict.DisplayName = "Spell Prediction: " + PredictValues[sliderHarassPredict.CurrentValue];

            //LastHit
            LastHit = Veigar.AddSubMenu("LastHit", "LastHit");
            LastHit.AddGroupLabel("LastHit Options");
            LastHit.AddSeparator();
            LastHit.Add("farmQActive", new KeyBind("Activate Auto Farm", true, KeyBind.BindTypes.PressToggle, 'N'));
            LastHit.Add("farmSlider", new Slider("Min Mana % to Farm with Q", 40));

            //LaneClear
            LaneClear = Veigar.AddSubMenu("LaneClear", "LaneClear");
            LaneClear.AddGroupLabel("LaneClear Options");
            LaneClear.AddSeparator();
            LaneClear.Add("useQL", new CheckBox("Use Q"));
            LaneClear.Add("useWL", new CheckBox("Use W"));
            LaneClear.Add("minML", new Slider("Min Mana % to LaneClear", 20));

            //KillSteal
            KillSteal = Veigar.AddSubMenu("KillSteal", "KillSteal");
            KillSteal.AddGroupLabel("KillSteal Options");
            KillSteal.AddSeparator();
            KillSteal.Add("ksQ", new CheckBox("KS with Q"));
            KillSteal.Add("ksW", new CheckBox("KS with W"));
            KillSteal.Add("ksR", new CheckBox("KS with R"));
            
            //Misc
            Misc = Veigar.AddSubMenu("Misc", "Misc");
            Misc.AddGroupLabel("Misc Options");
            Misc.AddSeparator();
            Misc.Add("EInterrupt", new CheckBox("Use E To Interrupt!"));
            Misc.Add("EGap", new CheckBox("Use E on GapCloser"));
            Misc.Add("EDash", new CheckBox("Use E on Dashing Heroes"));
            Misc.Add("useEFlee", new CheckBox("Use E on Flee Mode"));
            Misc.Add("AutoPot", new CheckBox("Use Potions"));
            Misc.AddSeparator();
            skinSelect = Misc.Add("ChangeSkin", new Slider("Change Skin [Number]", 8, 0, 8));
            Misc.AddSeparator();
            Misc.Add("extension", new Slider("Extension to E Cast", 375, 0, 500));
            
            //Drawing
            Drawing = Veigar.AddSubMenu("Drawing", "Drawing");
            Drawing.AddGroupLabel("Drawing Options");
            Drawing.AddSeparator();
            Drawing.Add("drawQ", new CheckBox("Draw Q"));
            Drawing.Add("drawW", new CheckBox("Draw W"));
            Drawing.Add("drawE", new CheckBox("Draw E"));
            Drawing.Add("writeKillable", new CheckBox("Write Killable Targets with Combo"));

        }
    }
}