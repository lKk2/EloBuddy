using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Veigar
{
    class MenuX
    {
        public static Menu Veigar, Combo, Harass, KillSteal, LaneClear, Misc, LastHit, Drawing;

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

            //Harass
            Harass = Veigar.AddSubMenu("Harass", "Harass");
            Harass.AddGroupLabel("Harass Options");
            Harass.AddSeparator();
            Harass.Add("useQH", new CheckBox("Use Q"));
            Harass.Add("useWH", new CheckBox("Use W"));
            Harass.Add("useWHS", new CheckBox("Use W Only on Stunned Targets"));
            Harass.Add("useEH", new CheckBox("Use E"));
            Harass.Add("minManaH", new Slider("Min Mana % for Harass", 20));

            //LastHit
            LastHit = Veigar.AddSubMenu("LastHit", "LastHit");
            LastHit.AddGroupLabel("LastHit Options");
            LastHit.AddSeparator();
            LastHit.Add("farmQ", new CheckBox("Farm with Q"));
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
            KillSteal.Add("ksIG", new CheckBox("KS with Ignite"));

            //Misc
            Misc = Veigar.AddSubMenu("Misc", "Misc");
            Misc.AddGroupLabel("Misc Options");
            Misc.AddSeparator();
            Misc.Add("autoE", new CheckBox("Auto E"));
            Misc.Add("useEFlee", new CheckBox("Use E on Flee Mode"));
            Misc.Add("AutoPot", new CheckBox("Use Potions"));

            //Drawing
            Drawing = Veigar.AddSubMenu("Drawing", "Drawing");
            Drawing.AddGroupLabel("Drawing Menu");
            Drawing.AddSeparator();
            Drawing.Add("drawQ", new CheckBox("Draw Q"));
            Drawing.Add("drawW", new CheckBox("Draw W"));
            Drawing.Add("drawE", new CheckBox("Draw E"));
            Drawing.Add("writeKillable", new CheckBox("Write Killable Targets with Combo"));
        }
    }
}
