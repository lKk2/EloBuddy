using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Sorakinha
{
    class Brain
    {
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }

        #region Combo

        public static void Combo()
        {
            var autoW = MenuX.Healing["useW"].Cast<CheckBox>().CurrentValue;
            if (autoW && Program.W.IsReady())
            {
                AutoW();
            }
            if (Program.R.IsReady())
            {
                AutoR();
            }
            var useQ = MenuX.Combo["useQCombo"].Cast<CheckBox>().CurrentValue;
            var useE = MenuX.Combo["useECombo"].Cast<CheckBox>().CurrentValue;
            var minMana = MenuX.Combo["minMcombo"].Cast<Slider>().CurrentValue;

            var target = TargetSelector.GetTarget(Program.Q.Range, DamageType.Magical);
            if (target == null) return;
            if (useQ && Program.Q.IsReady() && _Player.ManaPercent >= minMana)
            {
                Program.Q.Cast(target);
            }
            if (useE && Program.E.IsReady() && _Player.ManaPercent >= minMana)
            {
                Program.E.Cast(target);
            }
        }
        #endregion

        #region Harass
        public static void Harass()
        {
            var autoW = MenuX.Healing["useW"].Cast<CheckBox>().CurrentValue;
            if (autoW && Program.W.IsReady())
            {
                AutoW();
            }
            if (Program.R.IsReady())
            {
                AutoR();
            }
            var useQ = MenuX.Harass["useQHarass"].Cast<CheckBox>().CurrentValue;
            var useE = MenuX.Harass["useEHarass"].Cast<CheckBox>().CurrentValue;
            var minMana = MenuX.Harass["minMharass"].Cast<Slider>().CurrentValue;
          
            var target = TargetSelector.GetTarget(Program.Q.Range, DamageType.Magical);
            if (target == null) return;
            if (useQ && Program.Q.IsReady() && _Player.ManaPercent >= minMana && Program.Q.GetPrediction(target).HitChance >= getPred())
            {
                Program.Q.Cast(target);
                //Chat.Print("HitChange: " + preDS);
            }
            if (useE && Program.E.IsReady() && _Player.ManaPercent >= minMana && Program.E.GetPrediction(target).HitChance >= getPred())
            {
                Program.E.Cast(target);
            }
        }

        #endregion

        #region Auto R

        public static void AutoR()
        {
            var useR = MenuX.Healing["useR"].Cast<CheckBox>().CurrentValue;
            if (!Program.R.IsReady() && useR) return;
                if (
                    ObjectManager.Get<AIHeroClient>()
                        .Where(x => x.IsAlly && x.IsValidTarget(float.MaxValue))
                        .Select(x => (int) x.Health/x.MaxHealth*100)
                        .Select(
                            friendHealth =>
                                new {friendHealth, health = MenuX.Healing["useRslider"].Cast<Slider>().CurrentValue})
                        .Where(x => x.friendHealth <= x.health)
                        .Select(x => x.friendHealth)
                        .Any()
                        )
                {
                    Program.R.Cast();
                }
            }

        #endregion

        #region Auto W
        public static void AutoW()
        {
            var test = HeroManager.Allies.Where(
                hero => !hero.IsMe && !hero.IsDead && !hero.IsInShopRange()
                        && !hero.IsZombie &&
                        hero.Distance(_Player) <= Program.W.Range &&
                        MenuX.Healing["w" + hero.ChampionName].Cast<CheckBox>().CurrentValue &&
                        hero.HealthPercent <= MenuX.Healing["wpct" + hero.ChampionName].Cast<Slider>().CurrentValue
                ).ToList();
            var allytoheal = test.OrderBy(x => x.Health).FirstOrDefault(x => !x.IsInShopRange());
            if (allytoheal != null)
            {
                Program.W.Cast(allytoheal);
            }

        }
        #endregion

        #region Calcs
        public static HitChance getPred()
        {
            var preDS = MenuX.Harass["predNeeded"].Cast<Slider>().CurrentValue;
            switch (preDS)
            {
                case 1:
                    return HitChance.Low;
                case 2: 
                    return HitChance.Medium;
                case 3: 
                    return HitChance.High;
            }
            return HitChance.Unknown;
        }
        #endregion

        #region Gap/Interrupter
        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            var useQ = MenuX.Misc["useQGapCloser"].Cast<CheckBox>().CurrentValue;
            var useE = MenuX.Misc["useEGapCloser"].Cast<CheckBox>().CurrentValue;
            if (useQ && Program.Q.IsReady())
            {
                Program.Q.Cast(sender);
            }
            if (useE && Program.E.IsReady())
            {
                Program.E.Cast(sender);
            }
        }

        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            var unit = sender;
            var spell = e;
            var useE = MenuX.Misc["eInterrupt"].Cast<CheckBox>().CurrentValue;
            if (!useE || spell.DangerLevel != DangerLevel.High) return;
            if (!unit.IsValidTarget(Program.E.Range)) return;
            if (!Program.E.IsReady()) return;
            Program.E.Cast(unit);


        }
        #endregion

        public static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            var useAAminion = MenuX.Misc["AttackMinions"].Cast<CheckBox>().CurrentValue;
            if (useAAminion && target.Type == GameObjectType.obj_AI_Minion)
            {
                var allyinrange = HeroManager.Allies.Count(x => !x.IsMe && x.Distance(_Player) <= 1200);
                if (allyinrange > 0)
                {
                    args.Process = false;
                }
            }
        }
    }
}
