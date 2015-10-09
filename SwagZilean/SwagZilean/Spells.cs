using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace SwagZilean
{
    internal class Spells
    {
        public static Spell.Targeted E, R;
        public static Spell.Skillshot Q;
        public static Spell.Active W;

        public static void getSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Circular, (int) 0.30f, 2000, 210);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 700);
            R = new Spell.Targeted(SpellSlot.R, 900);
        }
    }
}