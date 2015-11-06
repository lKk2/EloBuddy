using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using TooFatGragas.Helpers;
using TooFatGragas.Model;

namespace TooFatGragas.Controller.Modes
{
    internal class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = AdvancedTargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (target == null || !target.IsValidTarget()) return;

            Orbwalker.ForcedTarget = target;

            if (W.IsReady() && E.IsInRange(target) && E.IsReady() && Config.Combo.UseW)
            {
                W.Cast();
            }

            if (E.IsReady() && Config.Combo.UseE)
            {
                if (_Player.HasBuff("GragasWAttackBuff") || target.IsValidTarget(_Player.GetAutoAttackRange()))
                {
                    Player.IssueOrder(GameObjectOrder.AutoAttack, target);
                }
                var pred = E.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                {
                    E.Cast(pred.CastPosition);
                }
            }

            if (Q.IsReady() && Barrel == null && Config.Combo.UseQ)
            {
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                {
                    Q.Cast(pred.CastPosition);
                }
            }

            if (R.IsReady() && _Player.GetSpellDamage(target, SpellSlot.R) >= target.Health && Config.Combo.UseR)
                // adicionar mais modos :P
            {
                R.Cast(target.Position.Extend(_Player.Position, 100).To3D());
            }
        }
    }
}