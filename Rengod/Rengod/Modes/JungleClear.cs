using System.Linq;
using EloBuddy.SDK;
using Rengod.Core;
using Rengod.Util;

namespace Rengod.Modes
{
    internal class JungleClear : Model
    {
        public static void useJungleClear()
        {
            var useQ = Misc.isChecked(JungleMenu, "JungleQ");
            var useW = Misc.isChecked(JungleMenu, "JungleW");
            var useE = Misc.isChecked(JungleMenu, "JungleE");
            var minions =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(_Player.Position, W.Range + 100, true)
                    .OrderByDescending(x => x.MaxHealth)
                    .FirstOrDefault();
            if (minions == null) return;

            if (Q.IsReady() && useQ)
                Q.Cast();
            if (W.IsReady() && _Player.Distance(minions) <= 450 && useW)
            {
                W.Cast();
                Items.useHydra(minions);
            }

            if (E.IsReady() &&
                _Player.Distance(minions) <= E.Range && useE && !Passive())
            {
                E.Cast(minions.Position);
            }
        }
    }
}