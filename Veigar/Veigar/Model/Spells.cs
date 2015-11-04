using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Veigar.Model
{
    class Spells : Model
    {
        static Spells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 2000, 70) { AllowedCollisionCount = 1 };
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 1350, int.MaxValue, 225);
            E = new Spell.Skillshot(SpellSlot.E, 500, SkillShotType.Circular, 700, int.MaxValue, 80) { AllowedCollisionCount = int.MaxValue };   
            R = new Spell.Targeted(SpellSlot.R, 650);
            if (HasIgnite)
                Ignite = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
        }
        public static void Initialize() { }
    }
}
