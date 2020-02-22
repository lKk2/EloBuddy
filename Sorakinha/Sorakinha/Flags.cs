using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace Sorakinha
{
    internal static class Flags
    {
        #region Combo

        public static void Combo()
        {
            var autoW = Utils.isChecked(MenuX.Healing, "useW");
            if (autoW && Spells.W.IsReady())
            {
                Brain.AutoW();
            }
            if (Spells.R.IsReady())
            {
                Brain.AutoR();
            }
            var useQ = Utils.isChecked(MenuX.Combo, "useQCombo");
            var useE = Utils.isChecked(MenuX.Combo, "useECombo");
            var minMana = Utils.getSliderValue(MenuX.Combo, "minMcombo");

            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;
            if (useQ && Spells.Q.IsReady() && Utils._Player.ManaPercent >= minMana)
            {
                Spells.Q.Cast(target);
            }
            if (useE && Spells.E.IsReady() && Utils._Player.ManaPercent >= minMana)
            {
                Spells.E.Cast(target);
            }
        }

        #endregion

        #region Harass

        public static void Harass()
        {
            var autoW = MenuX.Healing["useW"].Cast<CheckBox>().CurrentValue;
            if (autoW && Spells.W.IsReady())
            {
                Brain.AutoW();
            }
            if (Spells.R.IsReady())
            {
                Brain.AutoR();
            }
            var useQ = Utils.isChecked(MenuX.Harass, "useQHarass");
            var useE = Utils.isChecked(MenuX.Harass, "useEHarass");
            var minMana = Utils.getSliderValue(MenuX.Harass, "minMharass");

            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;
            if (useQ && Spells.Q.IsReady() && Utils._Player.ManaPercent >= minMana &&
                Spells.Q.GetPrediction(target).HitChance >= Brain.getPred())
            {
                Spells.Q.Cast(target);
            }
            if (useE && Spells.E.IsReady() && Utils._Player.ManaPercent >= minMana &&
                Spells.E.GetPrediction(target).HitChance >= Brain.getPred())
            {
                Spells.E.Cast(target);
            }
        }

        #endregion

        #region Gap/Interrupter/SupMODE

        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            var useQ = Utils.isChecked(MenuX.Misc, "useQGapCloser");
            var useE = Utils.isChecked(MenuX.Misc, "useEGapCloser");
            if (useQ && Spells.Q.IsReady() && sender.IsEnemy && !sender.IsZombie)
            {
                Spells.Q.Cast(e.End);
            }
            if (useE && Spells.E.IsReady() && sender.IsEnemy && !sender.IsZombie)
            {
                Spells.E.Cast(e.End);
            }
        }

        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            var unit = sender;
            var spell = e;
            var useE = Utils.isChecked(MenuX.Misc, "eInterrupt");
            if (!useE || spell.DangerLevel != DangerLevel.High) return;
            if (!unit.IsValidTarget(Spells.E.Range)) return;
            if (!Spells.E.IsReady()) return;
            if (unit.IsAlly) return;
            Spells.E.Cast(unit);
        }


        public static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            var useAAminion = Utils.isChecked(MenuX.Misc, "AttackMinions");
            if (useAAminion && target.Type == GameObjectType.obj_AI_Minion)
            {
                var allyinrange = EntityManager.Heroes.Allies.Count(x => !x.IsMe && x.Distance(Utils._Player) <= 1200);
                if (allyinrange > 0)
                {
                    args.Process = false;
                }
            }
        }

        #endregion
    }
}
