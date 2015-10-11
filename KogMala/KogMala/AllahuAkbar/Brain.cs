using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using KogMala.Modes;
using KogMala.Utils;

namespace KogMala.AllahuAkbar
{
    internal class Brain : Model
    {
        public static void Init()
        {
            Bootstrap.Init(null);
            Spells.getSpells();
            MenuX.getMenu();
            Game.OnTick += OnTick;
            Game.OnUpdate += OnUpdate;
            Items.setItems();
            Gapcloser.OnGapcloser += OnGapCloser;
            _Player.SetSkin(_Player.ChampionName, Misc.getSliderValue(MiscMenu, "skinSelect"));
            Drawing.OnDraw += Draws.OnDraw;
        }

        private static void OnTick(EventArgs args)
        {
            Items.useItems();
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo.useCombo();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    Harass.useHarass();
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    LaneClear.useClear();
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    JungleClear.useJG();
                    break;
            }
        }

        private static void OnUpdate(EventArgs args)
        {
            if (_Player.IsZombie && Misc.isChecked(MiscMenu, "autoPilot"))
            {
                var t = TargetSelector.GetTarget(2000, DamageType.Physical);
                if (t.IsValidTarget())
                    Player.IssueOrder(GameObjectOrder.MoveTo, t.ServerPosition);
            }
        }

        private static void OnGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy &&
                sender is AIHeroClient &&
                !sender.IsDead && !sender.IsZombie)
            {
                MananDamage.SetMana();
                if (E.IsReady() && _Player.Mana > Rmana + Emana)
                {
                    if (sender.IsValidTarget(E.Range))
                        E.Cast(sender);
                }
            }
        }
    }
}