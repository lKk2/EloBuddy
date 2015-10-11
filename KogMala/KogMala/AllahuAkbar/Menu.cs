using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KogMala.AllahuAkbar
{
    internal class MenuX : Model
    {
        public static void getMenu()
        {
            MenuKog = MainMenu.AddMenu("KogMala", "KogMala");
            MenuKog.AddGroupLabel("O cuspidao nervoso!");
            MenuKog.AddSeparator();
            MenuKog.AddLabel("Version: " + G_version);
            MenuKog.AddLabel("Made without love by Kk2");

            ComboMenu = MenuKog.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Options");
            ComboMenu.AddSeparator();
            ComboMenu.Add("comboQ", new CheckBox("Use Q on Combo"));
            ComboMenu.Add("comboW", new CheckBox("Use W on Combo"));
            ComboMenu.Add("comboE", new CheckBox("Use E on Combo"));
            ComboMenu.Add("comboR", new CheckBox("Use R on Combo"));

            HarassMenu = MenuKog.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Options");
            HarassMenu.AddSeparator();
            HarassMenu.Add("harassQ", new CheckBox("Use Q on Harass"));
            HarassMenu.Add("harassW", new CheckBox("Use W on Harass"));
            HarassMenu.Add("harassE", new CheckBox("Use E on Harass"));
            HarassMenu.Add("harassR", new CheckBox("Use R on Harass"));

            LaneClearMenu = MenuKog.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.AddGroupLabel("LaneClear Options");
            LaneClearMenu.AddSeparator();
            LaneClearMenu.Add("clearW", new CheckBox("use W on LaneClear"));

            JungleMenu = MenuKog.AddSubMenu("JungleClear", "JungleClear");
            JungleMenu.AddGroupLabel("JungleClear Options");
            JungleMenu.AddSeparator();
            JungleMenu.Add("jungleW", new CheckBox("use W on JungleClear"));
            JungleMenu.Add("jungleR", new CheckBox("use R on JungleClear"));
            JungleMenu.Add("jungleS", new Slider("Mana % > to JungleClear", 20));

            MiscMenu = MenuKog.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc Options");
            MiscMenu.AddSeparator();
            MiscMenu.Add("autoPilot", new CheckBox("AutoPilot Passive of Kog"));
            MiscMenu.AddSeparator();
            SkinSelect = MiscMenu.Add("skinSelect", new Slider("Choose your Skin [number]", 5, 0, 8));
            SkinSelect.OnValueChange +=
                delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs aargs)
                {
                    _Player.SetSkin(_Player.ChampionName, aargs.NewValue);
                };

            ItemsMenu = MenuKog.AddSubMenu("Items", "Items");
            ItemsMenu.AddGroupLabel("Items Options");
            ItemsMenu.AddSeparator();
            ItemsMenu.Add("usePOT", new CheckBox("Use Health Potions"));
            ItemsMenu.AddSeparator();
            ItemsMenu.Add("useBTRK", new CheckBox("Use BTRK"));
            ItemsMenu.Add("myHP", new Slider("Use If my HP <", 20));
            ItemsMenu.Add("enemyHP", new Slider("Use If enemy HP <", 20));
            ItemsMenu.AddSeparator();

            DrawsMenu = MenuKog.AddSubMenu("Drawings", "Drawings");
            DrawsMenu.AddGroupLabel("Drawing Options");
            DrawsMenu.AddSeparator();
            DrawsMenu.Add("drawQ", new CheckBox("Draw Q Range"));
            DrawsMenu.Add("drawW", new CheckBox("Draw W Range"));
            DrawsMenu.Add("drawE", new CheckBox("Draw E Range"));
            DrawsMenu.Add("drawR", new CheckBox("Draw R Range"));
        }
    }
}