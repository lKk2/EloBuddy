using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace SimpleSivir.Model
{
    internal class Spells : Model
    {
        static Spells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1245, SkillShotType.Linear, (int) 0.25, 1030, 90)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R, 1000);
        }

        public static void Initiliaze()
        {
        }

        public static float GetTotalDmg(AIHeroClient target)
        {
            var damage = Player.Instance.GetAutoAttackDamage(target);
            if (Q.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.Q);
            return damage;
        }
    }
}