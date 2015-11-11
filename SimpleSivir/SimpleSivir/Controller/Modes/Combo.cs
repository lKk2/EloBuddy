using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SimpleSivir.Helpers;
using SimpleSivir.Model;

namespace SimpleSivir.Controller.Modes
{
    internal class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null || !target.IsValid) return;
            if (Q.IsReady() && Config.Combo.UseQ)
            {
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                    Q.Cast(pred.CastPosition);
            }
            if (W.IsReady() &&
                _Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) > 0 &&
                Config.Combo.UseW)
            {
                W.Cast();
            }
            if (R.IsReady() && Config.Combo.UseR)
            {
                if (_Player.CountEnemiesInRange(800) > 2)
                {
                    R.Cast();
                }
                else if (target.IsValidTarget() && _Player.GetAutoAttackDamage(target)*2 > target.Health && !Q.IsReady() &&
                         target.CountEnemiesInRange(800) < 3)
                    R.Cast();
            }
        }
    }
}