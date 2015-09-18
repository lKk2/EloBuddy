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
    class Brain
    {
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        public static float GetDynamicRange()
        {
            if (Program.Q.IsReady())
            {
                return Program.Q.Range;
            }
            return _Player.GetAutoAttackRange();
        }

        public static void Combo()
        {
            if (_Player.IsDead) return;
            var target = TargetSelector.GetTarget(GetDynamicRange() + 100, DamageType.Magical);
            if (target == null) return;

            if (Program.ComboMenu["useQCombo"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && target.IsValidTarget(Program.Q.Range))
            {
                Program.Q.Cast(target);
            }
            else if (Program.ComboMenu["useWCombo"].Cast<CheckBox>().CurrentValue && Program.W.IsReady() &&
                _Player.HealthPercent >= 30 && target.Distance(_Player) <= Program.E.Range)
            {
                Program.W.Cast(_Player);
            }
            else if (Program.ComboMenu["useECombo"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() &&
                target.Distance(_Player) <= Program.E.Range)
            {
                Program.E.Cast();
            }
        }

        public static void Harass()
        {
            if (_Player.IsDead) return;
            var target = TargetSelector.GetTarget(GetDynamicRange() + 100, DamageType.Magical);
            if (target == null) return;

            if (Program.HarassMenu["useQHarass"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && target.IsValidTarget(Program.Q.Range))
            {
                Program.Q.Cast(target);
            }
            else if (Program.HarassMenu["useEHarass"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                Program.E.Cast();
            }
        }

        public static void WaveClear()
        {
            if (_Player.IsDead) return;
            var minions =
                ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsEnemy && _Player.Distance(m) <= Program.E.Range);
            if (minions.Any() && Program.FarmMenu["useEWave"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                Program.E.Cast();
            }
        }

        public static void UltManager()
        {
            if (_Player.IsDead || !Program.R.IsReady()) return;

            if (Program.UltMenu["useRSelf"].Cast<CheckBox>().CurrentValue &&
                _Player.HealthPercent <= Program.UltMenu["MinHPR"].Cast<Slider>().CurrentValue)
            {
                if (Program.W.IsReady()) Program.W.Cast(_Player);
                Program.R.Cast(_Player);
            }
        }

        public static void LastHit()
        {
            if (_Player.IsDead) return;
            if (Program.FarmMenu["useQFarm"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
            {
                var minion = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(a => a.IsEnemy && a.Health <= QDamage(a));
                if (minion == null) return;
                Program.Q.Cast(minion);
            }
        }


        public static float QDamage(Obj_AI_Base target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new[] { 60, 110, 160, 210, 260 }[Program.Q.Level] + 0.6 * _Player.FlatMagicDamageMod + _Player.FlatPhysicalDamageMod));
        }
    }
}
