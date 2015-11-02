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
    class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position, Q.Range).OrderBy(x => x.Health);


            if (W.IsReady() && MenuX.Modes.LaneClear.UseW &&
                _Player.ManaPercent >= MenuX.Modes.LaneClear.MinMana)
            {
                var wminions = Misc.GetBestCircularFarmLocation(minions.OrderByDescending(x => x.Health)
                   .Select(xm => xm.ServerPosition.To2D()).ToList(),
                    W.Width, W.Range);
                if (wminions.MinionsHit >= MenuX.Modes.LaneClear.MinMinion)
                {
                    W.Cast(wminions.Position.To3D());
                }
            }

            if (Q.IsReady() && MenuX.Modes.LaneClear.UseQ &&
                _Player.ManaPercent >= MenuX.Modes.LaneClear.MinMana)
            {
                var qminions = Misc.GetBestLineFarmLocation(minions
                    .Where(x => DamageLib.QDamage(x) >= x.Health)
                    .Select(xm => xm.ServerPosition.To2D()).ToList(),
                    Q.Width, Q.Range);
                if (qminions.MinionsHit >= 1)
                {
                    Q.Cast(qminions.Position.To3D());
                }
            }
        }
    }
}
