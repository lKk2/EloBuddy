using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using TooFatGragas.Helpers;
using TooFatGragas.Model;

namespace TooFatGragas.Controller.Modes
{
    internal class Isac : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Config.Insec.getInsec;
        }

        public override void Execute()
        {
            var target = AdvancedTargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (target == null) return;

            Orbwalker.OrbwalkTo(Game.CursorPos);
            var eqpos = _Player.Position.Extend(target.Position, _Player.Distance(target) + 100);
            var iscapos = _Player.Position.Extend(target.Position, _Player.Distance(target) + 200).To3D();
            var away = _Player.Position.Extend(target.Position, _Player.Distance(target) + 300);

            if (target.IsFacing(_Player) == false && target.IsMoving && (target.Distance(iscapos) < 300))
            {
                R.Cast(away.To3D());
            }
            else if (target.IsFacing(_Player) && target.Distance(iscapos) < 300 && target.IsMoving)
            {
                R.Cast(eqpos.To3D());
            }
            else if (target.Distance(iscapos) < 300)
            {
                R.Cast(iscapos);
            }

            var pred = E.GetPrediction(target);
            if (pred.HitChance >= HitChance.High)
            {
                E.Cast(target.ServerPosition);
                Q.Cast(target.ServerPosition);
            }
        }
    }
}