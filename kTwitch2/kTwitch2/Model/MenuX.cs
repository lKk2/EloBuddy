using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace kTwitch2.Model
{
    internal class MenuX : Model
    {
        public static Slider HarassModeSlider, SkinHackSlider;
        public static string[] hModes = {"E on 6 stacks", "E on Killable"};

        public static void Init()
        {
            /* Main */
            TwitchMenu = MainMenu.AddMenu("kTwitch2", "kTwitch2");
            TwitchMenu.AddGroupLabel("kTwitch2");
            TwitchMenu.AddSeparator();
            TwitchMenu.AddLabel("Made by Kk2");

            /* Combo */
            ComboMenu = TwitchMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Options");
            ComboMenu.AddSeparator();
            ComboMenu.Add("comboW", new CheckBox("Use W on Combo"));
            ComboMenu.Add("comboE", new CheckBox("Use E on Combo (Only when Killable)"));
            ComboMenu.Add("comboR", new CheckBox("Use R on Combo"));
            ComboMenu.Add("comboMinR", new Slider("Min Enemies to R", 1, 1, 5));

            /* Harass */
            HarassMenu = TwitchMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Options");
            HarassMenu.AddSeparator();
            HarassMenu.Add("harassW", new CheckBox("Use W on Harass"));
            HarassMenu.Add("harassE", new CheckBox("Use E on Harass"));
            HarassModeSlider = HarassMenu.Add("hMode", new Slider("Use E Only: ", 0, 0, 1));
            HarassModeSlider.OnValueChange += delegate
            {
                HarassModeSlider.DisplayName = "Use E Only: " + hModes[HarassModeSlider.CurrentValue];
            };
            HarassModeSlider.DisplayName = "Use E Only: " + hModes[HarassModeSlider.CurrentValue];
            HarassMenu.AddSeparator();
            HarassMenu.Add("harassMana", new Slider("Min Mana %> to Harass", 20));

            /* LaneClear */
            LaneClearMenu = TwitchMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.AddGroupLabel("LaneClear Options");
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("luseW", new CheckBox("Use W on LaneClear"));
            LaneClearMenu.Add("luseE", new CheckBox("Use E on LaneClear"));
            LaneClearMenu.Add("lminE", new Slider("Min Minions to Kill to use E", 2, 1, 5));
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("lMana", new Slider("Min Mana %> to LaneClear", 20));

            /* JungleClear */
            JungleClearMenu = TwitchMenu.AddSubMenu("JungleClear", "JungleClear");
            JungleClearMenu.AddGroupLabel("JungleClear Options");
            JungleClearMenu.AddSeparator();
            JungleClearMenu.Add("juseW", new CheckBox("Use W on JungleClear"));
            JungleClearMenu.Add("juseE", new CheckBox("use E on JungleClear"));
            JungleClearMenu.Add("jMana", new Slider("Min Mana %> to JungleClear"));

            /* Items */
            ItemsMenu = TwitchMenu.AddSubMenu("Items", "Items");
            ItemsMenu.AddGroupLabel("Items Menu");
            ItemsMenu.AddSeparator();
            ItemsMenu.Add("usePOT", new CheckBox("Use Potions"));
            ItemsMenu.Add("useYoumu", new CheckBox("Use Youmuus Ghostblade"));
            ItemsMenu.AddSeparator();
            ItemsMenu.Add("useBTRK", new CheckBox("Use Blade of the Ruined King"));
            ItemsMenu.Add("myHP", new Slider("My HP <% to Use BTRK", 80));
            ItemsMenu.Add("enemyHP", new Slider("Enemy HP <% to Use BTRK", 80));
            

            /* Draws */
            DrawingsMenu = TwitchMenu.AddSubMenu("Drawings", "Drawings");
            DrawingsMenu.AddGroupLabel("Drawings Options");
            DrawingsMenu.AddSeparator();
            DrawingsMenu.Add("drawW", new CheckBox("Draw W Range"));
            DrawingsMenu.Add("drawR", new CheckBox("Draw R Range"));
            DrawingsMenu.AddSeparator();
            DrawingsMenu.Add("drawTimer", new CheckBox("Draw Remaining Time of Q"));
            DrawingsMenu.Add("drawE", new CheckBox("Draw E Damage on Enemy HPBAR"));

            /* Misc */
            MiscMenu = TwitchMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc Options");
            MiscMenu.AddSeparator();
            MiscMenu.Add("stealM", new CheckBox("Steal Jungle/Dragon/Baron with E"));
            MiscMenu.AddSeparator();
            SkinHackSlider = MiscMenu.Add("skinHack", new Slider("Choose your Skin [number]", 0, 0, 7));
            SkinHackSlider.OnValueChange += delegate
            {
                _Player.SetSkinId(SkinHackSlider.CurrentValue);
            };
            _Player.SetSkinId(SkinHackSlider.CurrentValue);
        }
    }
}
