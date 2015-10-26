using System;
using System.Linq;
using EloBuddy.SDK;
using kTwitch2.Helpers;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    internal class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var minMana = getSliderValue(JungleClearMenu, "jMana");
            var useW = isChecked(JungleClearMenu, "juseW");
            var useE = isChecked(JungleClearMenu, "juseE");
            var m =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(_Player.Position, W.Range)
                    .OrderByDescending(x => x.Health)
                    .ToList();
            if (!m.Any()) return;

            if (W.IsReady() && _Player.ManaPercent >= minMana && useW)
            {
                W.Cast(m.First().ServerPosition);
            }
            if (E.IsReady() && useE)
            {
                var mk = m.Where(x => x.Health + (x.PercentHealingAmountMod/2) < DmgLib.EDamage(x));
                if (mk.Any())
                {
                    E.Cast();
                }
            }
        }
    }
}
