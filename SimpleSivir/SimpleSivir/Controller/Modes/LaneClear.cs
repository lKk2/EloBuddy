using System.Linq;
using EloBuddy.SDK;
using SimpleSivir.Helpers;
using SimpleSivir.Model;

namespace SimpleSivir.Controller.Modes
{
    internal class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position).ToList();
            if (!minions.Any()) return;
            var qMinions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, Q.Width, (int) Q.Range);
            if (Q.IsReady() &&
                qMinions.HitNumber >= 2 &&
                Config.LaneClear.UseQ &&
                Config.LaneClear.MinMana <= _Player.ManaPercent)
            {
                Q.Cast(qMinions.CastPosition);
            }
            if (W.IsReady() && minions.Count >= 2 &&
                Config.LaneClear.UseW &&
                Config.LaneClear.MinMana <= _Player.ManaPercent)
            {
                W.Cast();
            }
        }
    }
}