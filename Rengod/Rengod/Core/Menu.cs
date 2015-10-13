using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Rengod.Core
{
    internal class MenuX : Model
    {
        public static string[] prio = {"E", "Q", "W"};
        public static Slider ComboPrio, HarassPrio, SkinHax;

        public static void getMenu()
        {
            /*
            Main Menu
            */
            Rengar = MainMenu.AddMenu("Rengod", "Rengod");
            Rengar.AddGroupLabel("RenGOD ~.^");
            Rengar.AddSeparator();
            Rengar.AddLabel("" + G_name);

            /*
            Combo Menu
            */
            ComboMenu = Rengar.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo Options");
            ComboMenu.AddSeparator();
            ComboMenu.Add("ComboQ", new CheckBox("Use Q on Combo"));
            ComboMenu.Add("ComboW", new CheckBox("Use W on Combo"));
            ComboMenu.Add("ComboE", new CheckBox("Use E on Combo"));
            ComboPrio = ComboMenu.Add("cPrio", new Slider("Prioritize: ", 0, 0, 2));
            ComboPrio.OnValueChange +=
                delegate { ComboPrio.DisplayName = "Prioritize: " + prio[ComboPrio.CurrentValue]; };
            ComboPrio.DisplayName = "Prioritize: " + prio[ComboPrio.CurrentValue];
            ComboMenu.AddSeparator();
            ComboMenu.Add("useIG", new CheckBox("Use Ignite on Combo"));
            ComboMenu.Add("useSmite", new CheckBox("Use Smite on Combo"));

            /*
            Harass Menu
            */
            HarassMenu = Rengar.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass Options");
            HarassMenu.AddSeparator();
            HarassMenu.Add("HarassQ", new CheckBox("Use Q on Harass"));
            HarassMenu.Add("HarassW", new CheckBox("Use W on Harass"));
            HarassMenu.Add("HarassE", new CheckBox("Use E on Harass"));
            HarassPrio = HarassMenu.Add("hPrio", new Slider("Prioritize: ", 0, 0, 1));
            HarassPrio.OnValueChange +=
                delegate { HarassPrio.DisplayName = "Prioritize: " + prio[HarassPrio.CurrentValue]; };
            HarassPrio.DisplayName = "Prioritize: " + prio[HarassPrio.CurrentValue];

            /*
            LaneClear Menu
            */
            LaneMenu = Rengar.AddSubMenu("LaneClear", "LaneClear");
            LaneMenu.AddGroupLabel("LaneClear Options");
            LaneMenu.AddSeparator();
            LaneMenu.Add("LaneQ", new CheckBox("Use Q on LaneClear"));
            LaneMenu.Add("LaneW", new CheckBox("Use W on LaneClear"));
            LaneMenu.Add("LaneE", new CheckBox("Use E on LaneClear"));

            /*
            JungleClear Menu
            */
            JungleMenu = Rengar.AddSubMenu("Jungle", "Jungle");
            JungleMenu.AddGroupLabel("Jungle Options");
            JungleMenu.AddSeparator();
            JungleMenu.Add("JungleQ", new CheckBox("Use Q on Jungle"));
            JungleMenu.Add("JungleW", new CheckBox("Use W on Jungle"));
            JungleMenu.Add("JungleE", new CheckBox("Use E on Jungle"));

            /*
            Items Menu
            */
            ItemsMenu = Rengar.AddSubMenu("Items", "Items");
            ItemsMenu.AddGroupLabel("Items Options");
            ItemsMenu.Add("useYoumu", new CheckBox("Use Youmuus"));
            ItemsMenu.Add("useHydra", new CheckBox("Use Hydra"));
            ItemsMenu.AddSeparator();
            ItemsMenu.Add("useBTRK", new CheckBox("Use BTRK"));
            ItemsMenu.Add("myHP", new Slider("My Hp <% to use", 60));
            ItemsMenu.Add("enemyHP", new Slider("Enemy HP <% to use", 60));
            ItemsMenu.AddSeparator();
            ItemsMenu.Add("usePOT", new CheckBox("Use Potions"));

            /*
            Misc Menu
            */
            MiscMenu = Rengar.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc Options");
            MiscMenu.AddSeparator();
            MiscMenu.Add("useHeal", new CheckBox("Auto Heal with W"));
            MiscMenu.Add("hpHeal", new Slider("HP % to Heal", 25));
            MiscMenu.AddSeparator();
            SkinHax = MiscMenu.Add("skinHax", new Slider("Choose you Skin [number]", 2, 0, 2));
            SkinHax.OnValueChange += delegate { _Player.SetSkinId(SkinHax.CurrentValue); };

            /*
            Drawing Menu
            */
            DrawingMenu = Rengar.AddSubMenu("Drawing", "Drawing");
            DrawingMenu.AddGroupLabel("Drawing Options");
            DrawingMenu.AddSeparator();
            DrawingMenu.Add("drawQ", new CheckBox("Draw Q Range"));
            DrawingMenu.Add("drawW", new CheckBox("Draw W Range"));
            DrawingMenu.Add("drawE", new CheckBox("Draw E Range"));
            DrawingMenu.Add("drawR", new CheckBox("Draw R Range"));
            DrawingMenu.Add("drawC", new CheckBox("Draw Current Combo Prioritize"));
            DrawingMenu.Add("drawK", new CheckBox("Draw H on Killable Enemies"));
        }
    }
}