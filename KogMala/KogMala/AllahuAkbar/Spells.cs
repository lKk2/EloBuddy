using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace KogMala.AllahuAkbar
{
    internal class Spells : Model
    {
        public static void getSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 980, SkillShotType.Linear, (int) 0.25f, 2000, 50);
            Q.AllowedCollisionCount = 0;
            W = new Spell.Active(SpellSlot.W, 1000);
            E = new Spell.Skillshot(SpellSlot.E, 1200, SkillShotType.Linear, (int) 0.25f, 1400, 120);
            R = new Spell.Skillshot(SpellSlot.R, 1800, SkillShotType.Circular, (int) 1.5f, int.MaxValue, 200);
            R.AllowedCollisionCount = int.MaxValue;
        }
    }
}