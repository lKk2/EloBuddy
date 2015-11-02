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
    internal class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var t = AdvancedTargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (t == null || !t.IsValidTarget()) return;

            Orbwalker.ForcedTarget = t;

            if (E.IsReady() && MenuX.Modes.Combo.UseE)
            {
                var pred = E.GetPrediction(t);
                var unit = pred.UnitPosition;
                var okay = _Player.Distance(unit) <=
                           E.Range + Misc.GetArrivalTime(_Player.Distance(unit), E.CastDelay, E.Speed)*t.MoveSpeed;
                if (okay)
                {
                    switch (MenuX.Modes.Combo.ModeE)
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

            if (Q.IsReady() && MenuX.Modes.Combo.UseQ)
            {
                var pred = Q.GetPrediction(t);
                if (pred.HitChancePercent >= 70)
                    Q.Cast(pred.CastPosition);
            }

            if (W.IsReady() && MenuX.Modes.Combo.UseW)
            {
                var pred = W.GetPrediction(t);
                if (pred.HitChance >= HitChance.Immobile)
                    W.Cast(pred.CastPosition);
                else if (pred.HitChance >= HitChance.High)
                    W.Cast(pred.CastPosition);
            }



            if (R.IsReady() && MenuX.Modes.Combo.UseR)
            {
                if (DamageLib.RDamage(t) >= t.Health)
                {
                    Player.CastSpell(SpellSlot.R, t);
                }
            }

            if (_Player.GetSummonerSpellDamage(t, DamageLibrary.SummonerSpells.Ignite) >= t.Health && MenuX.Modes.Combo.UseIg &&
                        _Player.Distance(t) <= 0x258)
            {
                _Player.Spellbook.CastSpell(Ignite, t);
            }
        }
    }
}
