using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace TooFatGragas.Helpers
{
    internal class Config
    {
        private const string MenuName = "Gragas";
        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("TooFatGragas ^.^");
            Menu.AddSeparator();
            Menu.AddLabel("Made by Kk2 (:");
            Combo.Initialize();
            Harass.Initialize();
            LaneClear.Initialize();
            JungleClear.Initialize();
            Insec.Initialize();
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
            private static readonly CheckBox _useE;
            private static readonly CheckBox _useR;

            static Combo()
            {
                Menu = Config.Menu.AddSubMenu("Combo");
                _useQ = Menu.Add("useQCombo", new CheckBox("Use Q"));
                _useW = Menu.Add("useWCombo", new CheckBox("Use W"));
                _useE = Menu.Add("useECombo", new CheckBox("Use E"));
                _useR = Menu.Add("useRCombo", new CheckBox("Use R to Kill"));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static bool UseE
            {
                get { return _useE.CurrentValue; }
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
            private static readonly CheckBox _useE;
            private static readonly Slider _minMana;

            static Harass()
            {
                Menu = Config.Menu.AddSubMenu("Harass");
                _useQ = Menu.Add("useQHarass", new CheckBox("Use Q"));
                _useE = Menu.Add("useEHarass", new CheckBox("Use E"));
                _minMana = Menu.Add("minManaHarass", new Slider("Mana %> to Harass", 40));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseE
            {
                get { return _useE.CurrentValue; }
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
            private static readonly CheckBox _useE;
            private static readonly Slider _minMana;

            static LaneClear()
            {
                Menu = Config.Menu.AddSubMenu("LaneClear");
                _useQ = Menu.Add("useQLane", new CheckBox("Use Q"));
                _useE = Menu.Add("useELane", new CheckBox("Use E"));
                _minMana = Menu.Add("minManaLane", new Slider("Mana %> to Harass", 40));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseE
            {
                get { return _useE.CurrentValue; }
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
            private static readonly CheckBox _useE;
            private static readonly Slider _minMana;

            static JungleClear()
            {
                Menu = Config.Menu.AddSubMenu("JungleClear");
                _useQ = Menu.Add("useQJungle", new CheckBox("Use Q"));
                _useW = Menu.Add("useWJungle", new CheckBox("Use W"));
                _useE = Menu.Add("useEJungle", new CheckBox("Use E"));
                _minMana = Menu.Add("minManaJungle", new Slider("Mana %> to Harass", 40));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static bool UseE
            {
                get { return _useE.CurrentValue; }
            }

            public static int MinMana
            {
                get { return _minMana.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Insec
        {
            private static readonly KeyBind _insec;
            private static readonly Menu Menu;
            private static readonly CheckBox _draw;
            private static readonly CheckBox _eonGap;

            static Insec()
            {
                Menu = Config.Menu.AddSubMenu("InSec");
                _insec = Menu.Add("insecKey",
                    new KeyBind("Choose your insec Key", false, KeyBind.BindTypes.HoldActive, 'N'));
                _draw = Menu.Add("drawInsec", new CheckBox("Draw Insec Position"));
                _eonGap = Menu.Add("eonGap", new CheckBox("Use E on GapCloser"));
            }

            public static bool getInsec
            {
                get { return _insec.CurrentValue; }
            }

            public static bool getDraw
            {
                get { return _draw.CurrentValue; }
            }

            public static bool getGap
            {
                get { return _eonGap.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Drawings
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;
            private static readonly CheckBox _healthbar;
            private static readonly CheckBox _percent;

            static Drawings()
            {
                Menu = Config.Menu.AddSubMenu("Drawings");
                _drawQ = Menu.Add("DrawQ", new CheckBox("Draw Q Range"));
                _drawE = Menu.Add("DrawE", new CheckBox("Draw E Range"));
                _drawR = Menu.Add("DrawR", new CheckBox("Draw R Range"));
                Menu.AddGroupLabel("Damage indicators");
                _healthbar = Menu.Add("healthbar", new CheckBox("Healthbar overlay"));
                _percent = Menu.Add("percent", new CheckBox("Damage percent info"));
            }

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }

            public static bool DrawE
            {
                get { return _drawE.CurrentValue; }
            }

            public static bool DrawR
            {
                get { return _drawR.CurrentValue; }
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