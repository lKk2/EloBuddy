using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Rengod.Core;
using Rengod.Util;

namespace Rengod.Modes
{
    internal class LaneClear : Model
    {
        public static void useLaneClear()
        {
            var useQ = Misc.isChecked(LaneMenu, "LaneQ");
            var useW = Misc.isChecked(LaneMenu, "LaneW");
            var useE = Misc.isChecked(LaneMenu, "LaneE");

            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position, W.Range)
                    .FirstOrDefault();

            if (minions == null) return;

            if (_Player.Spellbook.IsAutoAttacking) return;

            if (Q.IsReady() && _Player.CanMove && useQ)
                Q.Cast();
            if (W.IsReady() && _Player.Distance(minions) <= W.Range/3 && useW)
            {
                Items.useHydra();
                W.Cast();
            }
            if (E.IsReady() &&
                _Player.GetSpellDamage(minions, SpellSlot.E) > minions.Health &&
                _Player.Distance(minions) <= E.Range && useE)
            {
                E.Cast(minions.Position);
            }
        }
    }
}