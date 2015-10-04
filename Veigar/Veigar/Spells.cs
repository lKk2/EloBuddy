using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Veigar
{
    class Spells
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Targeted R;
        public static Spell.Targeted Ignite;

        public static void LoadSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, (int)0.25f, 2000, 70);
            Q.AllowedCollisionCount = 0;
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, (int)1.25f, 2000, 225);
            // spellspeed 2k?
            W.AllowedCollisionCount = int.MaxValue;
            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Circular, (int)0.5f, int.MaxValue, 40);
            E.AllowedCollisionCount = int.MaxValue;
            R = new Spell.Targeted(SpellSlot.R, 650);
            if (Brain._Player.GetSpellSlotFromName("summonerdot") != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(Brain._Player.GetSpellSlotFromName("summonerdot"), 550);
            }
        }
    }
}
