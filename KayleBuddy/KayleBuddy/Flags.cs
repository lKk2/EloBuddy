using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace KayleBuddy
{
    class Flags
    {
        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;

            if (Utils.isChecked(MenuX.ComboMenu, "useQCombo") &&
                Spells.Q.IsReady() &&
                target.IsValidTarget(Spells.Q.Range))
            {
                Spells.Q.Cast(target);
            }
            if (Utils.isChecked(MenuX.ComboMenu, "useWCombo") &&
                Spells.W.IsReady() &&
                Utils._Player.HealthPercent >= 30 &&
                target.Distance(Utils._Player) <= Spells.E.Range)
            {
                Spells.W.Cast(Utils._Player);
            }
            if (Utils.isChecked(MenuX.ComboMenu, "useECombo") &&
                Spells.E.IsReady() &&
                target.Distance(Utils._Player) <= Spells.E.Range)
            {
                Spells.E.Cast();
            }
        }

        public static void Harass()
        {
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null) return;

            if (Utils.isChecked(MenuX.HarassMenu, "useQHarass") &&
                Spells.Q.IsReady() &&
                target.IsValidTarget(Spells.Q.Range) &&
                Utils.getSliderValue(MenuX.HarassMenu, "manaHarass") <= Utils._Player.ManaPercent)
            {
                Spells.Q.Cast(target);
            }
            if (Utils.isChecked(MenuX.HarassMenu, "useEHarass") &&
                Spells.E.IsReady() &&
                Utils.getSliderValue(MenuX.HarassMenu, "manaHarass") <= Utils._Player.ManaPercent)
            {
                Spells.E.Cast();
            }
        }

        public static void WaveClear()
        {
            var minions =
                ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsEnemy &&
                Utils._Player.Distance(m) <= Spells.E.Range);
            if (minions.Any() &&
                Utils.isChecked(MenuX.FarmMenu, "useEWave") &&
                Spells.E.IsReady() &&
                Utils.getSliderValue(MenuX.FarmMenu, "manaFarm") <= Utils._Player.ManaPercent)
            {
                Spells.E.Cast();
            }
        }

        public static void JungleClear()
        {
            var jgminions = EntityManager.GetJungleMonsters(Utils._Player.Position.To2D(), 1000, true);
            var m = jgminions.OrderByDescending(x => x.MaxHealth);
            if (Spells.E.IsReady() &&
                m.Any() &&
                Utils.isChecked(MenuX.JungleMenu, "useEJungle") &&
                Utils.getSliderValue(MenuX.JungleMenu, "manaJungle") <= Utils._Player.ManaPercent)
            {
                Spells.E.Cast();
            }
            if (Spells.Q.IsReady() &&
               m.Any() &&
               Utils.isChecked(MenuX.JungleMenu, "useQJungle") &&
               Utils.getSliderValue(MenuX.JungleMenu, "manaJungle") <= Utils._Player.ManaPercent)
            {
                Spells.Q.Cast(m.First());
            } 
        }

        public static void LastHit()
        {
            if (Utils.isChecked(MenuX.FarmMenu, "useQFarm") &&
                Spells.Q.IsReady() &&
                Utils.getSliderValue(MenuX.FarmMenu, "manaFarm") <= Utils._Player.ManaPercent)
            {
                var minion = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.IsEnemy && a.Health <= DamageLib.QDamage(a));
                if (minion == null) return;
                Spells.Q.Cast(minion);
            }
        }

    }
}
