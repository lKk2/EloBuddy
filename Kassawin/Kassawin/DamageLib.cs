using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Kassawin
{
    class DamageLib
    {
        public static float QDamage(Obj_AI_Base target)
        {
            return Utils._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 80, 105, 130, 155, 180}[Spells.Q.Level] + 0.7 * Utils._Player.FlatMagicDamageMod
                    ));
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return Utils._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 40, 65, 90, 115, 140}[Spells.W.Level] + 0.6*Utils._Player.FlatMagicDamageMod
                    ));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Utils._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 80, 105, 130, 155, 180}[Spells.E.Level] + 0.7*Utils._Player.FlatMagicDamageMod
                    ));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Utils._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 80, 100, 120}[Spells.R.Level] + 0.2*Utils._Player.MaxMana
                    ));
        }

        public static float GetComboDmg(AIHeroClient target)
        {
            var damage = 0f;
            if (Spells.Q.IsReady() && target.IsValidTarget(Spells.Q.Range))
                damage += QDamage(target);
            if (Spells.W.IsReady())
                damage += WDamage(target);
            if (Spells.E.IsReady() && target.IsValidTarget(Spells.E.Range))
                damage += EDamage(target);
            if (Spells.R.IsReady() && target.IsValidTarget(Spells.R.Range))
                damage += RDamage(target);
            damage += Utils._Player.GetAutoAttackDamage(target, true)*2;
            return damage;
        }
    }
}
