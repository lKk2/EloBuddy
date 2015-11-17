using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace Sorakinha
{
    internal static class Brain
    {
        private const int XOffset = 10;
        private const int YOffset = 20;
        private const int Width = 103;
        private const int Height = 8;

        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName.ToLower() != "soraka") return; // check if hero is not soraka 
            Bootstrap.Init(null);
            Spells.getSpells();
            MenuX.CallMeNigga(); // calls the menu!
            Chat.Print("Sorakinha Loaded!", Color.DeepPink);
            Game.OnTick += Game_OnTick;
            Orbwalker.OnPreAttack += Flags.Orbwalker_OnPreAttack;
            Gapcloser.OnGapcloser += Flags.Gapcloser_OnGapCloser;
            Interrupter.OnInterruptableSpell += Flags.Interrupter_OnInterruptableSpell;
            Drawing.OnDraw += Drawing_OnDraw;
            MenuX.SkinSelect.OnValueChange +=
                delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs a)
                {
                    Utils._Player.SetSkin(Utils._Player.ChampionName, a.NewValue);
                };
        }

        private static void Game_OnTick(EventArgs args)
        {
            var autoW = Utils.isChecked(MenuX.Healing, "useW");
            if (autoW && Spells.W.IsReady())
            {
                AutoW();
            }
            if (Spells.R.IsReady())
            {
                AutoR();
            }
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Flags.Combo();
                    return;
                case Orbwalker.ActiveModes.Harass:
                    Flags.Harass();
                    return;
                case Orbwalker.ActiveModes.None:
                    break;
                case Orbwalker.ActiveModes.LastHit:
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    break;
                case Orbwalker.ActiveModes.Flee:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            var hBar = Utils.isChecked(MenuX.Drawing, "drawH");
            if (hBar)
            {
                //Brain.DrawWbar();
                foreach (var pos in from hero in EntityManager.Heroes.Allies let pos = hero.HPBarPosition where !hero.IsDead && !hero.IsMe && hero.HealthPercent <= Utils.getSliderValue(MenuX.Healing, "wpct" + hero.ChampionName) select pos)
                {
                    // Brain.DrawWbar();
                    Drawing.DrawText(pos.X + 110, pos.Y - 5, Color.Tomato, "H");
                }
            }
            var QRange = Utils.isChecked(MenuX.Drawing, "drawQ");
            var ERange = Utils.isChecked(MenuX.Drawing, "drawE");
            if (QRange)
            {
                Drawing.DrawCircle(_Player.Position, Spells.Q.Range, Spells.Q.IsReady() ? Color.Aqua : Color.Red);
            }
            if (ERange)
            {
                Drawing.DrawCircle(_Player.Position, Spells.E.Range, Spells.E.IsReady() ? Color.Aqua : Color.Red);
            }
        }

        #region Auto R

        public static void AutoR()
        {
            var useR = Utils.isChecked(MenuX.Healing, "useR");
            if (!Spells.R.IsReady() && useR) return;
            if (ObjectManager.Get<AIHeroClient>().Where(x => x.IsAlly && x.IsValidTarget(float.MaxValue)).Select(x => (int) x.Health/x.MaxHealth*100).Select(friendHealth => new {friendHealth, health = Utils.getSliderValue(MenuX.Healing, "useRslider")}).Where(x => x.friendHealth <= x.health).Select(x => x.friendHealth).Any())
            {
                Spells.R.Cast();
            }
        }

        #endregion

        #region Auto W

        public static void AutoW()
        {
            var test = EntityManager.Heroes.Allies.Where(hero => !hero.IsMe && !hero.IsDead && !hero.IsInShopRange() && !hero.IsZombie && hero.Distance(_Player) <= Spells.W.Range && MenuX.Healing["w" + hero.ChampionName].Cast<CheckBox>().CurrentValue && hero.HealthPercent <= MenuX.Healing["wpct" + hero.ChampionName].Cast<Slider>().CurrentValue).ToList();
            var allytoheal = test.OrderBy(x => x.Health).FirstOrDefault(x => !x.IsInShopRange());
            if (allytoheal != null)
            {
                Spells.W.Cast(allytoheal);
            }
        }

        #endregion

        #region Calcs

        public static HitChance getPred()
        {
            var preDS = Utils.getSliderValue(MenuX.Harass, "predNeeded");
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

        #region Healing Bar Calcs

        private static float WHeal(Obj_AI_Base target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Magical, (float) (new[] {120, 150, 180, 210, 240}[Spells.W.Level] + 0.6*_Player.FlatMagicDamageMod));
        }

        public static void DrawWbar()
        {
            foreach (var unit in ObjectManager.Get<AIHeroClient>().Where(h => h.IsValid && h.IsHPBarRendered && h.IsAlly))
            {
                var barPos = unit.HPBarPosition;
                var healing = WHeal(unit);
                var pctgAfterHeal = Math.Max(0, unit.Health + healing)/unit.MaxHealth;
                var yPos = barPos.Y + YOffset;
                var xPosDamage = barPos.X + XOffset + Width*pctgAfterHeal;
                var xPosCurrentHp = barPos.X + XOffset + Width*unit.Health/unit.MaxHealth;

                if (healing > unit.Health)
                {
                    Drawing.DrawLine(xPosDamage, yPos, xPosDamage, yPos + Height, 2, Color.Lime);
                }
                var diffhp = xPosCurrentHp + xPosDamage;
                var pos1 = barPos.X + 9 + (107*pctgAfterHeal);
                for (var i = 0; i < diffhp; i++)
                {
                    Drawing.DrawLine(pos1 + i, yPos, pos1 + i, yPos + Height, 1, Color.Goldenrod);
                }
            }
        }

        #endregion
    }
}