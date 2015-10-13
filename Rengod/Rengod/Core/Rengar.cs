using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using Rengod.Modes;
using Rengod.Util;

namespace Rengod.Core
{
    internal class Rengar : Model
    {
        /*
        Init of my Rengo
        */

        public static void Init(EventArgs args)
        {
            if (_Player.BaseSkinName.ToLower() != "rengar") return;
            Spells.SetSpells();
            Items.setItems();
            MenuX.getMenu();
            Chat.Print("Rengod Loaded", Color.Purple);
            Game.OnTick += OnTick;
            Orbwalker.OnPostAttack += OnAfterAttack;
            Orbwalker.OnPreAttack += OnBeforeAttack;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Dash.OnDash += OnDash;
            Drawing.OnDraw += Drawings.OnDraw;
            _Player.SetSkinId(MenuX.SkinHax.CurrentValue);
        }

        /*
        OnTick
        */

        private static void OnTick(EventArgs args)
        {
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo.useCombo();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    Harass.useHarass();
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    LaneClear.useLaneClear();
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    JungleClear.useJungleClear();
                    break;
            }
            Items.useItems();
            Spells.Heal();
        }

        /*
        After Attack
        */

        private static void OnAfterAttack(AttackableUnit target, EventArgs args)
        {
            if (Ferocity == 5 && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo &&
                Q.IsReady() && target is AIHeroClient && target.IsValidTarget())
                Q.Cast();
        }

        /*
        Before Attack
        */

        private static void OnBeforeAttack(AttackableUnit target, EventArgs args)
        {
            if (target is AIHeroClient &&
                target.IsValidTarget() && Misc.getSliderValue(ComboMenu, "cPrio") == 1)
            {
                if (Ferocity <= 4 && _Player.IsInAutoAttackRange(target))
                    Q.Cast();
                if (Ferocity == 5 && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo &&
                    _Player.IsInAutoAttackRange(target))
                    Q.Cast();
                if (Ferocity == 5 && Q.IsReady() && Passive())
                    Q.Cast();
            }
        }

        /*
        OnDash
        */

        private static void OnDash(Obj_AI_Base sender, Dash.DashEventArgs args)
        {
            var target = TargetSelector.GetTarget(R.Range, DamageType.Physical); // 1500?
            if (target == null || !target.IsValidTarget()) return;
            if (sender.IsMe && onCombo)
            {
                var cType = Misc.getSliderValue(ComboMenu, "cPrio");
                if (Ferocity == 5)
                {
                    if (cType == 1 && _Player.IsDashing())
                        Casts.useQ(target);
                    if (cType == 0 && _Player.IsDashing())
                        Casts.useE(target);
                }
                if (Ferocity < 5)
                {
                    if (E.IsReady() && _Player.IsDashing())
                    {
                        Casts.useE(target);
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                    }
                    if (Q.IsReady() && _Player.IsDashing())
                    {
                        Casts.useQ(target);
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                    }
                    if (W.IsReady() && _Player.IsDashing())
                    {
                        Casts.useW(target);
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                        Items.useHydra();
                    }
                }
            }
        }

        /*
        Process Speall Cast
        */

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                if (args.SData.Name.ToLower() == "rengarr")
                {
                    Items.useYoumu();
                }
                if (args.SData.Name.Contains("Attack") && onCombo)
                {
                    EloBuddy.SDK.Core.DelayAction(Items.useHydra,
                        50 + (int) (_Player.AttackDelay*100) + Game.Ping/2 + 10);
                }
            }
        }
    }
}