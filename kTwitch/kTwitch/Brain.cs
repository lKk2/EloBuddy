using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace kTwitch
{
    class Brain
    {
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static readonly float[] BaseDamage = { 20, 35, 50, 65, 80 };
        private static readonly float[] StackDamage = { 15, 20, 25, 30, 35 };
        private static readonly float[] MaxDamage = { 110, 155, 200, 245, 290 };
        private static Item BTRK1, BTRK2, YOMU;


        #region KS
        public static void KStiloso()
        {
            if (MenuX.MiscMenu["EKillsteal"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(enemy => enemy.IsValidTarget(1100)))
                {
                    if (CanKill(enemy))
                    {
                        Program.Spells[SpellSlot.E].Cast();
                    }
                }
            }
        }
        #endregion

        #region Combo
        public static void Combo()
        {
            BTRK1 = new Item((int)ItemId.Blade_of_the_Ruined_King);
            BTRK2 = new Item((int)ItemId.Bilgewater_Cutlass);
            YOMU = new Item((int)ItemId.Youmuus_Ghostblade);
            var target = TargetSelector2.GetTarget(Program.Spells[SpellSlot.W].Range, DamageType.Physical);
            if (target == null) return;
            if (MenuX.ComboMenu["useWCombo"].Cast<CheckBox>().CurrentValue)
            {
                if (target.IsValidTarget(Program.Spells[SpellSlot.W].Range) && Program.Spells[SpellSlot.W].IsReady())
                {
                    Program.Spells[SpellSlot.W].Cast(target);
                }
            }
            if (MenuX.ComboMenu["useRCombo"].Cast<CheckBox>().CurrentValue)
            {
                foreach (
                    var enemy in
                        ObjectManager.Get<AIHeroClient>()
                            .Where(enemy => enemy.Distance(_Player) <= Program.Spells[SpellSlot.R].Range))
                {
                    if (enemy.CountEnemiesInRange(850) >= MenuX.ComboMenu["minRCombo"].Cast<Slider>().CurrentValue)
                    {
                        Program.Spells[SpellSlot.R].Cast();
                    }
                }
            }
            if (target.HealthPercent <= 80 && (BTRK1.IsOwned() || BTRK2.IsOwned()) && (BTRK1.IsReady() || BTRK2.IsReady()))
            {
                BTRK1.Cast(target);
                BTRK2.Cast(target);
            }
            if (target.HealthPercent <= 75 && YOMU.IsOwned() && YOMU.IsReady())
            {
                YOMU.Cast();
            }

        }
        #endregion

        #region Harass
        public static void Harass()
        {
            var target = TargetSelector2.GetTarget(Program.Spells[SpellSlot.W].Range, DamageType.Physical);
            if (target == null) return;
            if (MenuX.HarassMenu["useWHarass"].Cast<CheckBox>().CurrentValue &&
                _Player.ManaPercent >= MenuX.HarassMenu["minWManaH"].Cast<Slider>().CurrentValue)
            {
                Program.Spells[SpellSlot.W].Cast(target);
            }
            if (MenuX.HarassMenu["autoEHarass"].Cast<CheckBox>().CurrentValue &&
                MenuX.HarassMenu["minEHarras"].Cast<Slider>().CurrentValue >= target.GetBuffCount("twitchdeadlyvenom"))
            {
                Program.Spells[SpellSlot.E].Cast();
            }
            
        }

        #endregion

        #region LaneClear
        /** TODO
        public static void LaneClear()
        {
            Chat.Print("FUI CHAMADO!!!");
            if (MenuX.FarmMenu["useWFarm"].Cast<CheckBox>().CurrentValue && Program.Spells[SpellSlot.W].IsReady())
            {
                var localtion =
                    ObjectManager.Get<Obj_AI_Minion>()
                        .Where(x => x.IsMinion());
                if (!localtion.Any())
                {
                    Chat.Print("Achei nenhum!!!!");
                    return;
                }
                else
                {
                    Chat.Print("Achei alguns aeHO!!!!");
                }
            }

            //  if (MenuX.FarmMenu["useEAmmo"].Cast<CheckBox>().CurrentValue && minion.IsEnemy)
            //&&
            //MenuX.FarmMenu["useEFarm"].Cast<Slider>().CurrentValue >= minion.CountEnemiesInRange(Program.Spells[SpellSlot.E].Range))
        }
        **/
        #endregion

        #region Calcs OP
        public static float GetRemainingPoisonDamageMinusRegeneration(Obj_AI_Base target)
        {
            var buff = target.GetBuff("twitchdeadlyvenom");
            if (buff == null) return 0f;
            return (float)(ObjectManager.Player.CalculateDamageOnUnit(target, DamageType.True, ((int)(buff.EndTime - Game.Time) + 1) * GetPoisonTickDamage() * buff.Count)) - ((int)(buff.EndTime - Game.Time)) * target.HPRegenRate;
        }

        private static int GetPoisonTickDamage()
        {
            if (ObjectManager.Player.Level > 16) return 6;
            if (ObjectManager.Player.Level > 12) return 5;
            if (ObjectManager.Player.Level > 8) return 4;
            if (ObjectManager.Player.Level > 4) return 3;
            return 2;
        }

        private static float GetPassiveAndActivateDamage(Obj_AI_Base target, int targetBuffCount = 0)
        {
            if (targetBuffCount == 0) return 0;
            return (float)GetRemainingPoisonDamageMinusRegeneration(target) + GetActivateDamage(target, targetBuffCount);
        }

        private static float GetActivateDamage(Obj_AI_Base target, int targetBuffCount = 0)
        {
            if (targetBuffCount == 0) return 0;
            return (float)ObjectManager.Player.CalculateDamageOnUnit(target, DamageType.Physical, Math.Min(MaxDamage[Program.Spells[SpellSlot.E].Level - 1] + ObjectManager.Player.TotalAttackDamage * 1.5f, targetBuffCount * (StackDamage[Program.Spells[SpellSlot.E].Level - 1] + ObjectManager.Player.TotalAttackDamage * 0.15f) + BaseDamage[Program.Spells[SpellSlot.E].Level - 1]));
        }


        public static bool CanKill(Obj_AI_Base target)
        {
            var targetBuffs = target.GetBuffCount("twitchdeadlyvenom");
            if (targetBuffs == 0) return false;
            return (float) GetPassiveAndActivateDamage(target, targetBuffs) > target.Health;
        }
    }
#endregion
}
