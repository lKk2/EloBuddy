using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace SwagZilean
{
    internal class Brain
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Init(EventArgs args)
        {
            if (_Player.ChampionName.ToLower() != "zilean") return;
            Bootstrap.Init(null);
            Spells.getSpells();
            MenuX.getMenu();
            Chat.Print("Swaggggg Zileeeeean Loaded!", Color.DeepPink);
            Orbwalker.OnPreAttack += BeforeAttack;
            Gapcloser.OnGapcloser += OnGapCloser;
            Interrupter.OnInterruptableSpell += Interrupt;
            Game.OnTick += OnTick;
            Drawing.OnDraw += Drawings.OnDraw;
            _Player.SetSkin(_Player.ChampionName, 5);
            MenuX.SkinSelect.OnValueChange +=
                delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs aargs)
                {
                    _Player.SetSkin(_Player.ChampionName, aargs.NewValue);
                };
        }

        private static void OnTick(EventArgs args)
        {
            AutoR();
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Flags.Combo();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    Flags.Harass();
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    Flags.LaneClear();
                    break;
                case Orbwalker.ActiveModes.Flee:
                    Flags.Flee();
                    break;
            }
        }

        private static void BeforeAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            if (Utils.isChecked(MenuX.Misc, "Support") && target.Type == GameObjectType.obj_AI_Minion)
            {
                var allyinrage = EntityManager.Heroes.Allies.Count(x => !x.IsMe && x.Distance(_Player) <= 1200);
                if (allyinrage > 0)
                    args.Process = false;
            }
        }

        private static void OnGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy &&
                sender is AIHeroClient &&
                sender.Distance(_Player) <= Spells.E.Range &&
                Spells.E.IsReady() &&
                Utils.isChecked(MenuX.Misc, "gapCloser"))
            {
                Spells.E.Cast(sender);
            }
        }

        private static void Interrupt(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (args.DangerLevel == DangerLevel.High &&
                sender.IsEnemy &&
                sender is AIHeroClient &&
                sender.Distance(_Player) < Spells.Q.Range && Spells.Q.IsReady() &&
                Utils.isChecked(MenuX.Misc, "Interrupt"))
            {
                Spells.Q.Cast(sender);
                if (Spells.W.IsReady())
                {
                    Spells.W.Cast();
                    Spells.Q.Cast(sender);
                }
            }
        }

        public static void AutoR()
        {
            if (Spells.R.IsReady())
            {
                var whotoult = EntityManager.Heroes.Allies.Where(
                    x => !x.IsDead && !x.IsInShopRange() && !x.IsInvulnerable && !x.IsZombie &&
                         x.Distance(_Player) <= Spells.R.Range &&
                         Utils.isChecked(MenuX.UltMenu, "r" + x.ChampionName) &&
                         x.HealthPercent <= Utils.getSliderValue(MenuX.UltMenu, "rpct" + x.ChampionName)).ToList();
                var ally = whotoult.OrderBy(x => x.Health).FirstOrDefault();
                if (ally != null && _Player.CountEnemiesInRange(1000) > 0)
                    Spells.R.Cast(ally);
            }
        }
    }
}