using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;


namespace Kassawin
{
    class Spells
    {
        public static Spell.Targeted Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static Spell.Targeted Ignite;

        public static void GetSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 650);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Cone, (int)0.5f, int.MaxValue, 10);
            R = new Spell.Skillshot(SpellSlot.R, 700, SkillShotType.Circular, (int)0.5f, int.MaxValue, 150);
            if (Utils._Player.GetSpellSlotFromName("summonerdot") != SpellSlot.Unknown)
                Ignite = new Spell.Targeted(Utils._Player.GetSpellSlotFromName("summonerdot"), 550);
        }
    }
}
