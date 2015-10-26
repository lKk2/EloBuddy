using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using kTwitch2.Helpers;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    internal class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var t = AdvancedTargetSelector.GetTarget(RActive ? R.Range : W.Range, DamageType.Physical);
            if (t == null || !t.IsValidTarget()) return;

            Orbwalker.ForcedTarget = t;

            var useW = isChecked(HarassMenu, "harassW");
            var useE = isChecked(HarassMenu, "harassE");
            var hMode = getSliderValue(HarassMenu, "hMode");
            var minMana = getSliderValue(HarassMenu, "harassMana");

            if (W.IsReady() && useW &&
                _Player.ManaPercent >= minMana)
            {
                var pred = W.GetPrediction(t);
                if (pred.HitChance >= HitChance.High)
                {
                    W.Cast(pred.CastPosition);
                }
            }

            if (E.IsReady() && useE)
            {
                switch (hMode)
                {
                    case 0:
                        if (CanCastE)
                        {
                            E.Cast();
                        }
                        return;
                    case 1:
                        foreach (
                            var enemy in
                                EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget(E.Range) && e.IsEnemy &&
                                                                        e.IsVisible && !e.IsZombie && !e.IsDead &&
                                                                        e.HasBuff("twitchdeadlyvenom")))
                        {
                            if (DmgLib.EDamage(enemy) >= enemy.Health)
                            {
                                E.Cast();
                            }
                        }
                        return;
                }

            }
        }
    }
}
