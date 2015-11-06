using System.Linq;
using EloBuddy.SDK;
using TooFatGragas.Helpers;
using TooFatGragas.Model;

namespace TooFatGragas.Controller.Modes
{
    internal class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var minions = EntityManager.MinionsAndMonsters.GetJungleMonsters(_Player.Position, Q.Range).ToList();
            if (!minions.Any()) return;

            var QJungle = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minions, Q.Width, (int) Q.Range);
            var EJungle = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, E.Width, (int) E.Range);

            if (Q.IsReady() && QJungle.HitNumber >= 1 && Barrel == null && Config.JungleClear.UseQ &&
                _Player.ManaPercent >= Config.JungleClear.MinMana)
            {
                Q.Cast(QJungle.CastPosition);
                Core.DelayAction(() => Q.Cast(QJungle.CastPosition), 500);
            }
            if (W.IsReady() && EJungle.HitNumber >= 1 && Config.JungleClear.UseW)
            {
                W.Cast();
            }
            if (E.IsReady() && EJungle.HitNumber >= 1 && Config.JungleClear.UseE &&
                _Player.ManaPercent >= Config.JungleClear.MinMana)
            {
                E.Cast(EJungle.CastPosition);
            }
        }
    }
}