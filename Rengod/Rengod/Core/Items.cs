using EloBuddy;
using EloBuddy.SDK;
using Rengod.Util;

namespace Rengod.Core
{
    internal class Items : Model
    {
        public static void setItems()
        {
            /*
            Items Inits
            */

            Youmu = new Item((int) ItemId.Youmuus_Ghostblade);
            Cutl = new Item((int) ItemId.Bilgewater_Cutlass, 450);
            Btrk = new Item((int) ItemId.Blade_of_the_Ruined_King, 450);
            Tiamat = new Item((int) ItemId.Tiamat_Melee_Only, 400);
            Hydra = new Item((int) ItemId.Ravenous_Hydra_Melee_Only, 400);
            Potion = new Item((int) ItemId.Health_Potion);
        }

        public static void useHydra(Obj_AI_Base target)
        {
            var useH = Misc.isChecked(ItemsMenu, "useHydra");
            if (Tiamat.IsOwned() || Hydra.IsOwned() && useH)
            {
                if ((Tiamat.IsReady() || Hydra.IsReady()) && _Player.Distance(target) <= Hydra.Range)
                {
                    Tiamat.Cast();
                    Hydra.Cast();
                }
            }
        }

        public static void useHydraNot()
        {
            var useH = Misc.isChecked(ItemsMenu, "useHydra");
            if (Tiamat.IsOwned() || Hydra.IsOwned() && useH)
            {
                if ((Tiamat.IsReady() || Hydra.IsReady()))
                {
                    Tiamat.Cast();
                    Hydra.Cast();
                }
            }
        }
    
        public static void useYoumu()
        {
            var useY = Misc.isChecked(ItemsMenu, "useYoumu");
            if (Youmu.IsOwned() && Youmu.IsReady() && useY)
            {
                Youmu.Cast();
            }
        }

        public static void useItems()
        {
            var useBTRK = Misc.isChecked(ItemsMenu, "useBTRK");
            var myHP = Misc.getSliderValue(ItemsMenu, "myHP");
            var enemyHP = Misc.getSliderValue(ItemsMenu, "enemyHP");
            var usePOT = Misc.isChecked(ItemsMenu, "usePOT");

            if (Potion.IsReady() && !_Player.HasBuff("RegenerationPotion") && usePOT && !_Player.IsInShopRange())
            {
                if (_Player.CountEnemiesInRange(700) > 0 && _Player.Health + 200 < _Player.MaxHealth)
                    Potion.Cast();
                else if (_Player.Health < _Player.MaxHealth*0.6)
                    Potion.Cast();
            }

            if (Btrk.IsOwned() || Cutl.IsOwned())
            {
                var t = TargetSelector.GetTarget(Btrk.Range, DamageType.Physical);
                if (t == null || !t.IsValidTarget()) return;
                if (useBTRK &&
                    _Player.HealthPercent <= myHP &&
                    t.HealthPercent <= enemyHP &&
                    (Btrk.IsReady() || Cutl.IsReady()))
                {
                    Btrk.Cast(t);
                    Cutl.Cast(t);
                }
            }
        }
    }
}