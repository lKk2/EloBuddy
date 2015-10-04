using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Veigar
{
    class DamageLib
    {
        public static float RDamage(Obj_AI_Base target)
        {
            return Brain._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new[] { 250, 375, 500 }[Spells.R.Level - 1] + 1.2 * Brain._Player.FlatMagicDamageMod
                    ));
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return Brain._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new[] { 120, 170, 220, 270, 320 }[Spells.W.Level - 1] + 1.0 * Brain._Player.FlatMagicDamageMod
                    ));
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Brain._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new[] { 80, 125, 170, 215, 260 }[Spells.Q.Level - 1] + 0.6 * Brain._Player.FlatMagicDamageMod
                    ));
        }
    }
}
