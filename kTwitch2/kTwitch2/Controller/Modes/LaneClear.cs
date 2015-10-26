using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EloBuddy.SDK;
using kTwitch2.Helpers;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var useW = isChecked(LaneClearMenu, "luseW");
            var useE = isChecked(LaneClearMenu, "luseE");
            var minE = getSliderValue(LaneClearMenu, "lminE");
            var minMana = getSliderValue(LaneClearMenu, "lMana");
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                W.Range);

            if (W.IsReady() && useW &&
                _Player.ManaPercent >= minMana)
            {
                var wfarm = Misc.GetBestCircularFarmLocation(minions.Where(x => x.Distance(_Player) <= W.Range).Select(xm => xm.ServerPosition.To2D()).ToList(), W.Width, W.Range);
                if (wfarm.MinionsHit >= 3)
                {
                    W.Cast(wfarm.Position.To3D());
                }
            }
            if (E.IsReady() && useE)
            {
                var eminions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                    _Player.Position, E.Range);
                var count = eminions.Count(m => DmgLib.EDamage(m) >= m.Health);
                if (count >= minE)
                    E.Cast();
            }
        }
    }
}
