using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace kTwitch2.Model
{
    public abstract class Model
    {
        /*
        Globals 
        */
        

        /* 
        Spells
        */
        public static Spell.Active Q { get; set; }
        public static Spell.Skillshot W { get; set; }
        public static Spell.Active E { get; set; }
        public static Spell.Active R { get; set; }

        /*
        Items
        */
        public static Item BTRK { get; set; }
        public static Item CutL { get; set; }
        public static Item Youmu { get; set; }
        public static Item Potion { get; set; }

        /* 
        Menu
        */
        public static Menu TwitchMenu, ComboMenu, HarassMenu, LaneClearMenu, JungleClearMenu, ItemsMenu, DrawingsMenu, MiscMenu;

        /*
        Utils
        */
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }

        public static bool RActive
        {
            get { return _Player.HasBuff("TwitchFullAutomatic"); }
        }

        public static bool CanCastE { get; set; }

        public static bool isChecked(Menu obj, string value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }

        public static int getSliderValue(Menu obj, string value)
        {
            return obj[value].Cast<Slider>().CurrentValue;
        }
    }
}
