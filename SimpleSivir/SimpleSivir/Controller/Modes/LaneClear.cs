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
            var qMinions = EntityManager.MinionsAndMonsters.GetLineFarmLocation(EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position, Q.Range, true), Q.Width, (int) Q.Range);
            if (Q.IsReady() && qMinions.HitNumber >= Config.LaneClear.MinMinions &&
                _Player.ManaPercent >= Config.LaneClear.MinMana && Config.LaneClear.UseQ)
            {
                Q.Cast(qMinions.CastPosition);
            }
            if (W.IsReady() && EntityManager.MinionsAndMonsters.GetLaneMinions().Count(a => a.Distance(_Player.Position) <= _Player.GetAutoAttackRange()) >= Config.LaneClear.MinMinions &&
                Config.LaneClear.UseW &&
                Config.LaneClear.MinMana < _Player.ManaPercent)
            {
                W.Cast();
            }
        }
    }
}