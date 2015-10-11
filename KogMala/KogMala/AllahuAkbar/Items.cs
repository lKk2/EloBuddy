using EloBuddy;
using EloBuddy.SDK;
using KogMala.Utils;

namespace KogMala.AllahuAkbar
{
    internal class Items : Model
    {
        public static void setItems()
        {
            BTRK = new Item(3153, 500);
            BILGE = new Item(3144, 500);
            Potion = new Item(2003);
        }

        public static void useItems()
        {
            var useBTRK = Misc.isChecked(ItemsMenu, "useBTRK");
            var myHP = Misc.getSliderValue(ItemsMenu, "myHP");
            var enemyHP = Misc.getSliderValue(ItemsMenu, "enemyHP");
            var usePOT = Misc.isChecked(ItemsMenu, "usePOT");

            if (Potion.IsReady() && !_Player.HasBuff("RegenerationPotion") && usePOT)
            {
                if (_Player.CountEnemiesInRange(700) > 0 && _Player.Health + 200 < _Player.MaxHealth)
                    Potion.Cast();
                else if (_Player.Health < _Player.MaxHealth*0.6)
                    Potion.Cast();
            }
            if (BTRK.IsOwned() || BILGE.IsOwned())
            {
                var t = TargetSelector.GetTarget(BTRK.Range, DamageType.Physical);
                if (t == null || !t.IsValidTarget()) return;
                if (useBTRK &&
                    _Player.HealthPercent <= myHP &&
                    t.HealthPercent <= enemyHP &&
                    (BTRK.IsReady() || BILGE.IsReady()))
                {
                    BTRK.Cast(t);
                    BILGE.Cast(t);
                }
            }
        }
    }
}