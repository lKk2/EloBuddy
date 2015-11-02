using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Veigar.Helpers
{
    class DamageLib : Model.Model
    {
        public static float GetTotalDamage(AIHeroClient t)
        {
            //AA
            var damage = Player.Instance.GetAutoAttackDamage(t);
            // Q
            if (Q.IsReady())
                damage += QDamage(t);
            // W
            if (W.IsReady())
                damage += WDamage(t);
            // R
            if (R.IsReady())
                damage += RDamage(t);
            return damage;
        }

        public static float QDamage(Obj_AI_Base t)
        {
            return _Player.CalculateDamageOnUnit(t, DamageType.Magical, (float)
                (new[] {0, 80, 125, 170, 215, 260}[Q.Level] +
                 0.6*_Player.FlatMagicDamageMod));
        }

        public static float WDamage(Obj_AI_Base t)
        {
            return _Player.CalculateDamageOnUnit(t, DamageType.Magical, (float) 
                (new[] {0, 120, 170, 220, 270, 320}[W.Level] +
                + 1.0 * _Player.FlatMagicDamageMod));
        }
        public static float RDamage(Obj_AI_Base t)
        {
            return _Player.CalculateDamageOnUnit(t, DamageType.Magical, (float)
                (new[] {0, 250, 375, 500}[R.Level] +
                0.8*t.FlatMagicDamageMod +
                1.0 *_Player.FlatMagicDamageMod));

        }
    }
}
