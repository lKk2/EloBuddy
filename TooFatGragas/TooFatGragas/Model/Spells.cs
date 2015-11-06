using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace TooFatGragas.Model
{
    internal class Spells : Model
    {
        static Spells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Circular, 250, 1300, 275)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 950, SkillShotType.Linear, 0, 1200, 200);
            R = new Spell.Skillshot(SpellSlot.R, 1050, SkillShotType.Circular, 250, 1800, 375);
        }

        public void Init()
        {
        }
    }
}