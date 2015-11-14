using System.Linq;
using EloBuddy.SDK;
using SimpleSivir.Helpers;
using SimpleSivir.Model;

namespace SimpleSivir.Controller.Modes
{
    internal class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position).ToList();
            var qFarm = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, Q.Width, (int) Q.Range);
            if (qFarm.HitNumber > 0 && Q.IsReady() &&
                Config.JungleClear.UseQ &&
                Config.JungleClear.MinMana < _Player.ManaPercent)
            {
                Q.Cast(qFarm.CastPosition);
            }
            if (W.IsReady() && minions.Count >= 2 &&
                Config.JungleClear.UseW &&
                Config.JungleClear.MinMana < _Player.ManaPercent)
            {
                W.Cast();
            }
        }
    }
}