using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using TooFatGragas.Helpers;
using TooFatGragas.Model;

namespace TooFatGragas.Controller.Modes
{
    internal class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var target = AdvancedTargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (target == null || !target.IsValidTarget()) return;

            Orbwalker.ForcedTarget = target;

            if (E.IsReady() && Config.Harass.UseE &&
                _Player.ManaPercent >= Config.Harass.MinMana)
            {
                var pred = E.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                {
                    E.Cast(pred.CastPosition);
                }
            }

            if (Q.IsReady() && Barrel == null && Config.Combo.UseQ &&
                _Player.ManaPercent >= Config.Harass.MinMana)
            {
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                {
                    Q.Cast(pred.CastPosition);
                    Core.DelayAction(() => Q.Cast(pred.CastPosition), 500);
                }
            }
        }
    }
}