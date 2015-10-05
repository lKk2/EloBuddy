using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Kassawin
{
    internal class Flags
    {
        private static int EBuffCount
        {
            get { return Utils._Player.GetBuffCount("forcepulsecounter"); }
        }

        private static float RMana
        {
            get { return Utils._Player.Spellbook.GetSpell(SpellSlot.R).SData.Mana; }
        }

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;

            if (target.IsValidTarget(Spells.Q.Range) &&
                Spells.Q.IsReady() &&
                Utils.isChecked(MenuX.Combo, "ComboQ"))
            {
                Spells.Q.Cast(target);
            }
            if (target.IsValidTarget(Spells.E.Range) &&
                Spells.E.IsReady() &&
                Utils.isChecked(MenuX.Combo, "ComboE"))
            {
                var pred = Spells.E.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                    Spells.E.Cast(target);
            }
            if (target.IsValidTarget(Spells.R.Range) &&
                Spells.R.IsReady() &&
                Utils.isChecked(MenuX.Combo, "ComboR"))
            {
                if (RMana < 400 &&
                    Utils._Player.CountEnemiesInRange(1400) <= Utils.getSliderValue(MenuX.Combo, "sliderR"))
                {
                    Spells.R.Cast(target.ServerPosition);
                }
                else if (EBuffCount >= 3 || DamageLib.RDamage(target) > target.Health || Spells.E.IsReady())
                {
                    Spells.R.Cast(target.ServerPosition);
                }
            }
            if (Spells.Ignite.IsReady() &&
                target.Health < Utils._Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite) &&
                Utils.isChecked(MenuX.Combo, "IgniteToKill"))
                Spells.Ignite.Cast(target);
        }

        public static void Harass()
        {
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;

            if (target.IsValidTarget(Spells.Q.Range) &&
                Spells.Q.IsReady() &&
                Utils.isChecked(MenuX.Harass, "HarassQ") &&
                Utils._Player.ManaPercent >= Utils.getSliderValue(MenuX.Harass, "manaPCTH"))
            {
                Spells.Q.Cast(target);
            }
            if (target.IsValidTarget(Spells.E.Range) &&
                Spells.E.IsReady() &&
                Utils.isChecked(MenuX.Harass, "HarassE") &&
                Utils._Player.ManaPercent >= Utils.getSliderValue(MenuX.Harass, "manaPCTH"))
            {
                var pred = Spells.E.GetPrediction(target);
                if (pred.HitChance >= HitChance.High)
                    Spells.E.Cast(target);
            }
        }

        public static void AfterAttack(AttackableUnit target, EventArgs args)
        {
            if (target != null &&
                target.IsEnemy &&
                !target.IsInvulnerable &&
                !target.IsDead &&
                target is AIHeroClient &&
                target.Distance(Utils._Player) <= Spells.W.Range)
            {
                if ((Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass ||
                     Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo) && Spells.W.IsReady())
                {
                    if (Utils.isChecked(MenuX.Combo, "ComboQ") || Utils.isChecked(MenuX.Harass, "HarassW"))
                        Spells.W.Cast();
                }
            }
        }

        public static void Flee()
        {
            var pos = Game.CursorPos;
            if (Spells.R.IsReady())
            {
                Spells.R.Cast(pos);
            }
        }

        public static void JungleClear()
        {
            var minions =
                EntityManager.GetJungleMonsters(Utils._Player.Position.To2D(), Spells.Q.Range)
                    .OrderByDescending(x => x.MaxHealth)
                    .First();
            if (Spells.Q.IsReady() &&
                minions.IsValidTarget(Spells.Q.Range) &&
                Utils.isChecked(MenuX.JungleClear, "JungleQ") &&
                Utils._Player.ManaPercent >= Utils.getSliderValue(MenuX.JungleClear, "manaPCTJ"))
                Spells.Q.Cast(minions);
            if (Spells.W.IsReady() &&
                minions.IsValidTarget(Spells.W.Range) &&
                Utils.isChecked(MenuX.JungleClear, "JungleW"))
                Spells.W.Cast();
            if (Spells.E.IsReady() &&
                minions.IsValidTarget(Spells.E.Range) &&
                Utils.isChecked(MenuX.JungleClear, "JungleE") &&
                Utils._Player.ManaPercent >= Utils.getSliderValue(MenuX.JungleClear, "manaPCTJ"))
                Spells.E.Cast(minions);
        }

        public static void LaneClear()
        {
            var minions = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy, Utils._Player.Position.To2D(),
                Spells.E.Range, true);


            if (Spells.Q.IsReady() && Utils.isChecked(MenuX.LaneClear, "LaneQ") &&
                Utils._Player.ManaPercent >= Utils.getSliderValue(MenuX.LaneClear, "manaPCTL"))
            {
                var qM = minions.FirstOrDefault(x => x.Health < DamageLib.QDamage(x));
                if (qM.IsValidTarget(Spells.Q.Range))
                    Spells.Q.Cast(qM);
            }
            if (Spells.W.IsReady() && Utils.isChecked(MenuX.LaneClear, "LaneW"))
            {
                var wM = minions.FirstOrDefault(x => x.Health < DamageLib.WDamage(x));
                if (wM.IsValidTarget(Spells.W.Range))
                    Spells.W.Cast();
            }
            if (Spells.E.IsReady() && Utils.isChecked(MenuX.LaneClear, "LaneE") &&
                Utils._Player.ManaPercent >= Utils.getSliderValue(MenuX.LaneClear, "manaPCTL"))
            {
                var cc = minions.Count;
                var eM = minions.FirstOrDefault(x => x.Health < DamageLib.EDamage(x));
                if (cc >= 3 &&
                    eM.IsValidTarget(Spells.E.Range) &&
                    eM != null)
                    Spells.E.Cast(eM.Position);
            }
        }
    }
}