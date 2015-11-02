using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using Veigar.Helpers;
using Veigar.Model;

namespace Veigar.Controller.Modes
{
    internal class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            Potions();
            if (Q.IsReady() &&
                (!Orbwalker.IsAutoAttacking || !Orbwalker.CanAutoAttack) &&
                MenuX.AutoQ.isKey && _Player.ManaPercent >= MenuX.AutoQ.MinMana)
            {
                var mFarm =
                    Misc.GetBestLineFarmLocation(
                        EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Distance(_Player) <= Q.Range)
                            .OrderBy(x => x.Health)
                            .Where(p => DamageLib.QDamage(p) >= p.Health)
                            .Select(q => q.ServerPosition.To2D())
                            .ToList(), Q.Width, Q.Range);
                if (mFarm.MinionsHit > 0)
                    Q.Cast(mFarm.Position.To3D());
            }
        }

        private static void Potions()
        {
            if (MenuX.Misc.UsePot && !_Player.IsInShopRange() && !_Player.HasBuff("recall"))
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
    }
}
