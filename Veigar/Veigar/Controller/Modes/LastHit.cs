using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using Veigar.Helpers;
using Veigar.Model;

namespace Veigar.Controller.Modes
{
    internal class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            if (Q.IsReady() &&
                (!Orbwalker.IsAutoAttacking || !Orbwalker.CanAutoAttack) &&
                _Player.ManaPercent >= 20 &&
                MenuX.Modes.LastHit.UseQ &&
                _Player.ManaPercent >= MenuX.Modes.LastHit.MinMana)
            {
                var mFarm =
                    Misc.GetBestLineFarmLocation(
                        EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Distance(_Player) <= Q.Range)
                            .OrderBy(x => x.Health)
                            .Where(p => DamageLib.QDamage(p) >= p.Health)
                            .Select(q => q.ServerPosition.To2D())
                            .ToList(), Q.Width, Q.Range);
                if (mFarm.MinionsHit > 0)
                    Q.Cast(mFarm.Position.To3D());
            }
        }
    }
}

