using System.Linq;
using EloBuddy.SDK;
using TooFatGragas.Helpers;
using TooFatGragas.Model;

namespace TooFatGragas.Controller.Modes
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
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                    Q.Range).ToList();

            var qFarm = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minions, Q.Width, (int) Q.Range);

            if (qFarm.HitNumber >= 2 && Barrel == null && Config.LaneClear.UseQ &&
                _Player.ManaPercent >= Config.LaneClear.MinMana)
            {
                Q.Cast(qFarm.CastPosition);
                Core.DelayAction(() => Q.Cast(qFarm.CastPosition), 500);
            }
            var eFarm = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, E.Width, (int) E.Range);

            if (eFarm.HitNumber >= 2 && Config.LaneClear.UseE &&
                _Player.ManaPercent >= Config.LaneClear.MinMana)
            {
                E.Cast(eFarm.CastPosition);
            }
        }
    }
}