using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace kTwitch2.Helpers
{
    class DmgLib : Model.Model
    {
        public static float EDamage(Obj_AI_Base target)
        {
            var buff = target.HasBuff("twitchdeadlyvenom");
            var bc = target.GetBuffCount("twitchdeadlyvenom");
            if (!buff || !E.IsLearned) return 0f;
            return  _Player.CalculateDamageOnUnit(target, DamageType.True,(float)
                (new[] { 15, 20, 25, 30, 35 }[E.Level -1] * bc +
                0.2 * _Player.FlatMagicDamageMod + 
                0.25 * _Player.FlatPhysicalDamageMod +
                new[] { 20, 35, 50, 65, 80 }[E.Level -1]));
        }
    }
}
