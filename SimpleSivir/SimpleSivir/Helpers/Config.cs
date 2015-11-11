using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace SimpleSivir.Helpers
{
    internal class Config
    {
        private const string MenuName = "Sivir";
        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("SimpleSivir Rework");
            Menu.AddSeparator();
            Menu.AddLabel("Made by Kk2");
            Menu.AddLabel("Helped by MrArticuno!");
            Combo.Initialize();
            Harass.Initialize();
            LaneClear.Initialize();
            JungleClear.Initialize();
            Misc.Initialize();
            Drawings.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Combo
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly CheckBox _useR;

            static Combo()
            {
                Menu = Config.Menu.AddSubMenu("Combo");
                _useQ = Menu.Add("useQCombo", new CheckBox("Use Q"));
                _useW = Menu.Add("useWCombo", new CheckBox("Use W"));
                _useR = Menu.Add("useRCombo", new CheckBox("Use R"));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static bool UseR
            {
                get { return _useR.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Harass
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly Slider _minMana;

            static Harass()
            {
                Menu = Config.Menu.AddSubMenu("Harass");
                _useQ = Menu.Add("useQHarass", new CheckBox("Use Q"));
                _useW = Menu.Add("useWHarass", new CheckBox("Use W"));
                _minMana = Menu.Add("minManaHarass", new Slider("Mana % for Harras", 40));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static int MinMana
            {
                get { return _minMana.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class LaneClear
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly Slider _minMana;

            static LaneClear()
            {
                Menu = Config.Menu.AddSubMenu("LaneClear");
                _useQ = Menu.Add("useQLane", new CheckBox("Use Q"));
                _useW = Menu.Add("useWLane", new CheckBox("Use W"));
                _minMana = Menu.Add("minManaLane", new Slider("Mana %>", 40));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static int MinMana
            {
                get { return _minMana.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class JungleClear
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly Slider _minMana;

            static JungleClear()
            {
                Menu = Config.Menu.AddSubMenu("JungleClear");
                _useQ = Menu.Add("useQJungle", new CheckBox("Use Q"));
                _useW = Menu.Add("useWJungle", new CheckBox("Use W"));
                _minMana = Menu.Add("minManaJungle", new Slider("Mana %>", 40));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static int MinMana
            {
                get { return _minMana.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Misc
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useE;
            private static readonly CheckBox _autoQ;
            private static readonly Slider _skinHax;

            static Misc()
            {
                Menu = Config.Menu.AddSubMenu("Misc");
                _useE = Menu.Add("useEOP", new CheckBox("Auto use E (PLX YES)"));
                _autoQ = Menu.Add("autoQop", new CheckBox("Auto Use Q on Immobile/Dashing Targets"));
                Menu.AddSeparator();
                _skinHax = Menu.Add("skinhax", new Slider("Choose your Skin [number]", 0, 0, 7));
                _skinHax.OnValueChange += delegate { ObjectManager.Player.SetSkinId(_skinHax.CurrentValue); };
                ObjectManager.Player.SetSkinId(_skinHax.CurrentValue);
            }

            public static bool UseE
            {
                get { return _useE.CurrentValue; }
            }

            public static bool AutoQ
            {
                get { return _autoQ.CurrentValue; }
            }

            public static int SkinHax
            {
                get { return _skinHax.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Drawings
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawRTimer;
            private static readonly CheckBox _healthbar;
            private static readonly CheckBox _percent;

            static Drawings()
            {
                Menu = Config.Menu.AddSubMenu("Drawings");
                _drawQ = Menu.Add("drawQ", new CheckBox("Draw Q Range"));
                _drawRTimer = Menu.Add("drawRTimer", new CheckBox("Draw R Timer"));
                Menu.AddGroupLabel("Damage indicators");
                _healthbar = Menu.Add("healthbar", new CheckBox("Healthbar overlay"));
                _percent = Menu.Add("percent", new CheckBox("Damage percent info"));
            }

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }

            public static bool DrawR
            {
                get { return _drawRTimer.CurrentValue; }
            }

            public static bool IndicatorHealthbar
            {
                get { return _healthbar.CurrentValue; }
            }

            public static bool IndicatorPercent
            {
                get { return _percent.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }
    }
}