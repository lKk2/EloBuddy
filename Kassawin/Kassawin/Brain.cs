using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace Kassawin
{
    internal class Brain
    {
        public static void Init(EventArgs args)
        {
            if (Utils._Player.BaseSkinName.ToLower() != "kassadin") return;
            Bootstrap.Init(null);
            MenuX.GetMenu();
            Spells.GetSpells();
            Chat.Print("KassaWIN Loaded!", Color.Purple);
            Game.OnTick += OnTick;
            Interrupter.OnInterruptableSpell += Interrupterino;
            Gapcloser.OnGapcloser += GapCloserino;
            Orbwalker.OnPostAttack += Flags.AfterAttack;
            Drawing.OnDraw += DrawX.OnDraw;
            MenuX.SkinSelect.OnValueChange +=
                delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs a)
                {
                    Utils._Player.SetSkin(Utils._Player.ChampionName, a.NewValue);
                };
        }

        private static void OnTick(EventArgs args)
        {
            Potions();
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
                case Orbwalker.ActiveModes.JungleClear:
                    Flags.JungleClear();
                    break;
                case Orbwalker.ActiveModes.Flee:
                    Flags.Flee();
                    break;
            }
        }

        private static void Interrupterino(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (args.DangerLevel == DangerLevel.High &&
                sender.IsEnemy &&
                sender is AIHeroClient &&
                sender.Distance(Utils._Player) < Spells.Q.Range &&
                Spells.Q.IsReady())
            {
                Spells.Q.Cast(sender);
            }
        }

        private static void GapCloserino(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy &&
                sender is AIHeroClient &&
                sender.Distance(Utils._Player) < Spells.E.Range &&
                Spells.E.IsReady())
            {
                Spells.E.Cast(sender);
            }
        }

        private static void Potions()
        {
            if (Utils.isChecked(MenuX.Misc, "usePot") && !Utils._Player.IsInShopRange() &&
                !Utils._Player.HasBuff("recall"))
            {
                var hpPot = new Item(2003);
                var manaPot = new Item(2004);
                var flask = new Item(2041);
                if ((hpPot.IsReady() || flask.IsReady()) &&
                    (!Utils._Player.HasBuff("RegenerationPotion") || Utils._Player.HasBuff("ItemCrystalFlask")))
                {
                    if (Utils._Player.CountEnemiesInRange(700) > 0 &&
                        Utils._Player.Health + 200 < Utils._Player.MaxHealth)
                    {
                        if (Item.HasItem(hpPot.Id))
                        {
                            hpPot.Cast();
                        }
                        else
                        {
                            flask.Cast();
                        }
                    }
                    else if (Utils._Player.Health < Utils._Player.MaxHealth*0.6)
                    {
                        if (Item.HasItem(hpPot.Id))
                        {
                            hpPot.Cast();
                        }
                        else
                        {
                            flask.Cast();
                        }
                    }
                }
                if (manaPot.IsReady() && !Utils._Player.HasBuff("FlaskOfCrystalWater"))
                {
                    if (Utils._Player.Mana < Utils._Player.MaxMana*0.6)
                    {
                        manaPot.Cast();
                    }
                }
            }
        }
    }
}