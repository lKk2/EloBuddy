using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace KayleBuddy
{
    class DamageLib
    {
        public static float QDamage(Obj_AI_Base target)
        {
            return Utils._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new[] { 60, 110, 160, 210, 260 }[Spells.Q.Level] + 0.6 * Utils._Player.FlatMagicDamageMod + Utils._Player.FlatPhysicalDamageMod));
        }
    }
}
