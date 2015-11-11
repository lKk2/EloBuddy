using EloBuddy;
using EloBuddy.SDK;

namespace SimpleSivir.Model
{
    public abstract class Model
    {
        /*
        Spells 
        */
        public static Spell.Skillshot Q { get; set; }
        public static Spell.Active W { get; set; }
        public static Spell.Active E { get; set; }
        public static Spell.Active R { get; set; }

        /*
        Misc
        */

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
    }
}