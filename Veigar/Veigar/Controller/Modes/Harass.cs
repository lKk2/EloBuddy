using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Veigar.Helpers;
using Veigar.Model;

namespace Veigar.Controller.Modes
{
    class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var t = AdvancedTargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (t == null || !t.IsValidTarget()) return;

            Orbwalker.ForcedTarget = t;

            if (E.IsReady() && MenuX.Modes.Harass.UseE && MenuX.Modes.Harass.MinMana <= _Player.ManaPercent)
            {
                var pred = E.GetPrediction(t);
                var unit = pred.UnitPosition;
                var okay = _Player.Distance(unit) <=
                           E.Range + Misc.GetArrivalTime(_Player.Distance(unit), E.CastDelay, E.Speed) * t.MoveSpeed;
                {
                    switch (MenuX.Modes.Harass.ModeE)
                    {
                        case 0:
                            E.Cast(pred.CastPosition);
                            return;
                        case 1:
                            E.Cast(t.Position.Shorten(_Player.Position, 150));
                            return;

                    }
                }
            }

            if (Q.IsReady() && MenuX.Modes.Harass.UseQ && MenuX.Modes.Harass.MinMana <= _Player.ManaPercent)
            {
                var pred = Q.GetPrediction(t);
                if (pred.HitChancePercent >= 70)
                    Q.Cast(pred.CastPosition);
           }
            if (W.IsReady() && MenuX.Modes.Harass.UseW && MenuX.Modes.Harass.MinMana <= _Player.ManaPercent)
            {
                var pred = W.GetPrediction(t);
                if (pred.HitChance >= HitChance.Immobile)
                    W.Cast(pred.CastPosition);
                else if (pred.HitChance >= HitChance.High)
                    W.Cast(pred.CastPosition);
            }

        }
    }
}
