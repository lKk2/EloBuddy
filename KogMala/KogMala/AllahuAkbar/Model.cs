using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KogMala.AllahuAkbar
{
    internal abstract class Model
    {
        #region Global Vars

        /*
        Config
        */

        public static string G_version = "1.0.0";

        /*
        Spells
        */

        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static float Qmana, Wmana, Emana, Rmana;
        public static float Qdmg, Wdmg, Edmg, Rdmg;
        public static Item BTRK, BILGE, Potion;

        /*
        Menus
        */

        public static Menu MenuKog,
            ComboMenu,
            HarassMenu,
            LaneClearMenu,
            JungleMenu,
            MiscMenu,
            DrawsMenu,
            ItemsMenu;

        public static Slider SkinSelect;

        /*
        Misc
        */

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        #endregion
    }
}