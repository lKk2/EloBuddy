using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;

namespace Rengod.Core
{
    internal abstract class Model
    {
        #region Global Vars

        /*
        Config
        */
        public static string G_version = "1.0.0";
        public static string G_name = "Made by Kk2";

        /*
        Spells
        */
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Active R;

        /*
        Menus
        */
        public static Menu Rengar, ComboMenu, HarassMenu, LaneMenu, JungleMenu, ItemsMenu, MiscMenu, DrawingMenu;

        /*
        Summoners
        */
        public static SpellSlot Ignite;
        public static SpellSlot Smite;
        private static readonly int[] BlueSmite = {3706, 3710, 3709, 3708, 3707};
        private static readonly int[] RedSmite = {3715, 3718, 3717, 3716, 3714};

        /*
        Items
        */
        public static Item Youmu;
        public static Item Btrk;
        public static Item Cutl;
        public static Item Tiamat;
        public static Item Hydra;
        public static Item Potion;

        /*
        Misc
        */

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static int Ferocity
        {
            get { return (int) ObjectManager.Player.Mana; }
        }

        public static bool Passive()
        {
            foreach (
                var buff in
                    _Player.Buffs.Where(o => o.IsValid).Where(buff => buff.DisplayName.Contains("RengarPassiveBuff"))) 
            {
                return true;
            }
            return false;
        }

        public static bool RengarR
        {
            get { return _Player.Buffs.Any(x => x.Name.Contains("RengarR")); }
        }

        protected static void InitSmite()
        {
            if (BlueSmite.Any(id => Item.HasItem(id)))
            {
                Smite = _Player.GetSpellSlotFromName("s5_summonersmiteplayerganker");
                return;
            }

            if (RedSmite.Any(id => Item.HasItem(id)))
            {
                Smite = _Player.GetSpellSlotFromName("s5_summonersmiteduel");
                return;
            }

            Smite = _Player.GetSpellSlotFromName("summonersmite");
        }

        public static bool onCombo
        {
            get { return Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo; }
        }

        #endregion
    }
}