using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace kTwitch2.Model
{
    internal class ItemManager : Model
    {

        public static void Init()
        {
            BTRK = new Item((int)ItemId.Blade_of_the_Ruined_King, 450);
            CutL = new Item((int)ItemId.Bilgewater_Cutlass, 450);
            Youmu = new Item((int)ItemId.Youmuus_Ghostblade);
            Potion = new Item((int)ItemId.Health_Potion);
        }

        public static void UseYomu()
        {
            var useYoumu = isChecked(ItemsMenu, "useYoumu");
            if (Youmu.IsOwned() && Youmu.IsReady() && useYoumu)
            {
                Youmu.Cast();
            }
        }

        public static void UseBtrk(AIHeroClient target)
        {
            var useBTRK = isChecked(ItemsMenu, "useBTRK");
            var myHP = getSliderValue(ItemsMenu, "myHP");
            var enemyHP = getSliderValue(ItemsMenu, "enemyHP");
            if (useBTRK && _Player.HealthPercent <= myHP && target.HealthPercent <= enemyHP)
            if (BTRK.IsOwned() || CutL.IsOwned())
            {
                if (target == null || !target.IsValidTarget()) return;
                if (BTRK.IsReady() || CutL.IsReady())
                {
                    BTRK.Cast(target);
                    CutL.Cast(target);
                }
            }
        }

        public static void UsePotions()
        {
            var usePOt = isChecked(ItemsMenu, "usePOT");
            if (!_Player.IsInShopRange() &&
                !_Player.HasBuff("recall") && usePOt)
            {
                if (Potion.IsReady() && !_Player.HasBuff("RegenerationPotion"))
                {
                    if (_Player.CountEnemiesInRange(700) > 0 && _Player.Health + 200 < _Player.MaxHealth)
                        Potion.Cast();
                    else if (_Player.Health < _Player.MaxHealth*0.6)
                        Potion.Cast();
                }
            }
        }

    }
}
