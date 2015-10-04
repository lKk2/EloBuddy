using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace Veigar
{
    class Flags
    {
        #region Combo

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;
            if (Utils.isChecked(MenuX.Combo, "useRCombo"))
                Casts.RCast(target);
            if (Utils.isChecked(MenuX.Combo, "useECombo"))
                Casts.ECast(target);
            if (Utils.isChecked(MenuX.Combo, "useQCombo"))
                Casts.QCast(target);
            if (Utils.isChecked(MenuX.Combo, "useWCombo"))
            {
                if (Utils.isChecked(MenuX.Combo, "useWComboS"))
                    Casts.WTest();
                else
                {
                    Casts.WCast(target);
                }
            }
            if (Utils.isChecked(MenuX.Combo, "useIGCombo") && Spells.Ignite.IsReady() &&
                target.Health < Utils._Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite))
            {
                Spells.Ignite.Cast(target);
            }
        }

        #endregion

        #region Harass

        public static void Harass()
        {
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;
            if (Utils.isChecked(MenuX.Harass, "useQH") && Utils.getSliderValue(MenuX.Harass, "minManaH") <= Utils._Player.ManaPercent)
                Casts.QCast(target);
            if (Utils.isChecked(MenuX.Harass, "useEH") && Utils.getSliderValue(MenuX.Harass, "minManaH") <= Utils._Player.ManaPercent)
                Casts.ECast(target);
            if (Utils.isChecked(MenuX.Harass, "useWH") && Utils.getSliderValue(MenuX.Harass, "minManaH") <= Utils._Player.ManaPercent)
            {
                if (Utils.isChecked(MenuX.Harass, "useWHS"))
                    Casts.WTest();
                else
                {
                    Casts.WCast(target);
                }
            }
        }

        #endregion

        #region Auto Last Hit Q

        public static void LastHit()
        {
            if (!Spells.Q.IsLearned) return;
            if (MenuX.LastHit["farmQActive"].Cast<KeyBind>().CurrentValue
               && !Utils._Player.IsRecalling
               && !Utils._Player.IsDead)
            {
                if (Utils.getSliderValue(MenuX.LastHit, "farmSlider") <= Utils._Player.ManaPercent)
                {
                    foreach (var m in ObjectManager.Get<Obj_AI_Minion>().Where(
                        x => x.IsEnemy && x.Distance(Utils._Player) <= Spells.Q.Range
                        ).OrderBy(m => m.MaxHealth))
                    {
                        if (m.IsDead || !m.IsValidTarget(Spells.Q.Range)) return;
                        var pred = Spells.Q.GetPrediction(m);
                        if (m.Health <= DamageLib.QDamage(m))
                            Spells.Q.Cast(pred.CastPosition);
                    }
                }
            }
        }

        #endregion

        #region Flee

        public static void Flee()
        {
            var target = TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);
            if (target == null) return;
            Casts.ECast(target);
        }

        #endregion

        #region LaneClear

        public static void LaneClear()
        {
            if (Spells.Q.IsReady() && Utils.isChecked(MenuX.LaneClear, "useQL") &&
                Utils.getSliderValue(MenuX.LaneClear, "minML") <= Utils._Player.ManaPercent)
            {
                foreach (var m in ObjectManager.Get<Obj_AI_Minion>().Where(
                    x => x.IsEnemy && x.Distance(Utils._Player) <= Spells.Q.Range
                    ).OrderBy(m => m.MaxHealth))
                {
                    if (m.IsDead || !m.IsValidTarget(Spells.Q.Range)) return;
                    var pred = Spells.Q.GetPrediction(m);
                    if (m.Health <= DamageLib.QDamage(m))
                        Spells.Q.Cast(pred.CastPosition);
                }
            }
            var minions = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy, Utils._Player.Position.To2D(), Spells.Q.Range,
                true);
            if (minions.Count > 3 && Spells.W.IsReady() && Utils.isChecked(MenuX.LaneClear, "useWL") &&
                Utils.getSliderValue(MenuX.LaneClear, "minML") <= Utils._Player.ManaPercent)
            {
                foreach (var minion in minions.Where(x => x.IsValidTarget(Spells.W.Range + Spells.W.Width)))
                {
                    var pred = Spells.W.GetPrediction(minion);
                    if (pred.HitChancePercent >= 70 && DamageLib.WDamage(minion) > minion.Health)
                    {
                        Spells.W.Cast(pred.CastPosition);
                    }
                }
            }
        }

        #endregion

        #region Custom Events

        // TODO Teste on Dash !
        public static void Unit_OnDash(Obj_AI_Base sender, Dash.DashEventArgs args)
        {
            if (sender.IsEnemy &&
                sender is AIHeroClient &&
                Spells.E.IsReady() &&
                args.EndPos.Distance(Utils._Player.Position) < Spells.E.Range &&
                Utils.isChecked(MenuX.Misc, "EDash"))
            {
                var target = Spells.E.GetPrediction(sender);
                var pos = target.CastPosition;
                if (pos.IsValid())
                Spells.E.Cast(pos.Extend(sender.Position, 375).To3D());
            }
        }

        public static void GapCloserino(Obj_AI_Base sender,Gapcloser.GapcloserEventArgs args)
        {
            if (Spells.E.IsReady() &&
                args.End.Distance(Utils._Player.Position) < Spells.E.Range &&
                sender is AIHeroClient &&
                sender.IsEnemy &&
                Utils.isChecked(MenuX.Misc, "EGap"))
            {
                var target = Spells.E.GetPrediction(sender);
                var pos = target.CastPosition;
                if (pos.IsValid())
                    Spells.E.Cast(pos.Extend(sender.Position, 375).To3D());
            }
        }

        public static void Interrupter2_OnInterruptableTarget(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (Spells.E.IsReady() &&
                sender.Distance(Utils._Player) < Spells.E.Range &&
                sender is AIHeroClient &&
                sender.IsEnemy &&
                Utils.isChecked(MenuX.Misc, "EInterrupt"))
            {
                var target = Spells.E.GetPrediction(sender);
                var pos = target.CastPosition;
                if (pos.IsValid())
                    Spells.E.Cast(pos.Extend(sender.Position, 375).To3D());
            }
        }

        #endregion
    }
}

