using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Sorakinha
{
    internal static class Spells
    {
        /**
       Declaring the Spells
       **/
        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Skillshot E;
        public static Spell.Active R;

        public static void getSpells()
        {
            // Spells Infos.
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, (int) 0.283f, 1100, (int) 210f);
            W = new Spell.Targeted(SpellSlot.W, 550);
            E = new Spell.Skillshot(SpellSlot.E, 925, SkillShotType.Circular, (int) 0.5f, 1750, (int) 70f);
            R = new Spell.Active(SpellSlot.R);
        }
    }
}