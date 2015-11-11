using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SimpleSivir.Helpers;
using SimpleSivir.Model;

namespace SimpleSivir.Controller.Modes
{
    internal class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null || !target.IsValid) return;
            if (Q.IsReady() && Config.Harass.UseQ &&
                _Player.ManaPercent >= Config.Harass.MinMana)
            {
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                {
                    Q.Cast(pred.CastPosition);
                }
            }
            if (W.IsReady()
                && _Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) > 0 && Config.Harass.UseW &&
                _Player.ManaPercent >= Config.Harass.MinMana)
            {
                W.Cast();
            }
        }
    }
}