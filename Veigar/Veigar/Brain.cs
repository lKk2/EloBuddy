using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace Veigar
{
    internal class Brain
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Init()
        {
            Bootstrap.Init(null);
            Spells.LoadSpells();
            MenuX.CallMenu();
            Chat.Print("Veigar^.^ Loaded", Color.Purple);
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawings.OnDraw;
            Interrupter.OnInterruptableSpell += Flags.Interrupter2_OnInterruptableTarget;
            Gapcloser.OnGapcloser += Flags.GapCloserino;
            Dash.OnDash += Flags.Unit_OnDash;

            _Player.SetSkin(_Player.ChampionName, Utils.getSliderValue(MenuX.Misc, "ChangeSkin"));
            MenuX.skinSelect.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
            {
                _Player.SetSkin(_Player.ChampionName, args.NewValue);
            };
        }

        private static void Game_OnTick(EventArgs args)
        {
            Flags.LastHit();   
            Potions();
            KillSteal();
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Flags.Combo();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    Flags.Harass();
                    break;
                case Orbwalker.ActiveModes.Flee:
                    Flags.Flee();
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    Flags.LaneClear();
                    break;
                    
            }
        }


        #region KS

        private static void KillSteal()
        {
            foreach (var target in HeroManager.Enemies.Where(
                x => x.IsValidTarget(700) && !x.HasBuffOfType(BuffType.Invulnerability)))
            {
                if (!target.IsDead) return;

                if (target.Health <= DamageLib.QDamage(target) && Spells.Q.IsReady() && Utils.isChecked(MenuX.KillSteal, "ksQ"))
                {
                    Spells.Q.Cast(target);
                }
                if (target.Health <= DamageLib.WDamage(target) && Spells.W.IsReady() && Utils.isChecked(MenuX.KillSteal, "ksW"))
                {
                    Spells.W.Cast(target);
                }
                if (target.Health <= DamageLib.RDamage(target) && Spells.R.IsReady() && Utils.isChecked(MenuX.KillSteal, "ksR"))
                {
                    Spells.R.Cast(target);
                }
            }
        }

        #endregion
     
        #region Potions

        private static void Potions()
        {
            if (Utils.isChecked(MenuX.Misc, "AutoPot") && !_Player.IsInShopRange() && !_Player.HasBuff("recall"))
            {
                var hpPot = new Item(2003);
                var manaPot = new Item(2004);
                var biscuit = new Item(2010);
                if ((hpPot.IsReady() || biscuit.IsReady()) &&
                    (!_Player.HasBuff("RegenerationPotion") || _Player.HasBuff("ItemMiniRegenPotion")))
                {
                    if (_Player.CountEnemiesInRange(700) > 0 && _Player.Health + 200 < _Player.MaxHealth)
                    {
                        if (Item.HasItem(hpPot.Id))
                        {
                            hpPot.Cast();
                        }
                        else
                        {
                            biscuit.Cast();
                        }
                    }
                    else if (_Player.Health < _Player.MaxHealth*0.6)
                    {
                        if (Item.HasItem(hpPot.Id))
                        {
                            hpPot.Cast();
                        }
                        else
                        {
                            biscuit.Cast();
                        }
                    }
                }
                if (manaPot.IsReady() && !_Player.HasBuff("FlaskOfCrystalWater"))
                {
                    if (_Player.Mana < _Player.MaxMana*0.6)
                    {
                        manaPot.Cast();
                    }
                }
            }
        }

        #endregion

 }
}
