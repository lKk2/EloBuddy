using EloBuddy;
using EloBuddy.SDK;

namespace TooFatGragas.Model
{
    public abstract class Model
    {
        /* Spells */
        public static Spell.Skillshot Q { get; set; }
        public static Spell.Active W { get; set; }
        public static Spell.Skillshot E { get; set; }
        public static Spell.Skillshot R { get; set; }

        /* Barrel */
        public static GameObject Barrel { get; set; }


        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
    }
}