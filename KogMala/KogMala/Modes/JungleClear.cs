using System.Linq;
using EloBuddy.SDK;
using KogMala.AllahuAkbar;
using KogMala.Utils;

namespace KogMala.Modes
{
    internal class JungleClear : Model
    {
        public static void useJG()
        {
            var useW = Misc.isChecked(JungleMenu, "jungleW");
            var useR = Misc.isChecked(JungleMenu, "jungleR");
            var useMana = Misc.getSliderValue(JungleMenu, "jungleS");
            var minion =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(_Player.Position, W.Range, true)
                    .OrderByDescending(x => x.Health)
                    .FirstOrDefault();
            if (minion == null) return;
            if (useW && W.IsReady() && minion.Distance(_Player) <= W.Range &&
                _Player.ManaPercent >= useMana)
                W.Cast();
            if (useR && R.IsReady() && minion.Distance(_Player) <= R.Range &&
                _Player.ManaPercent >= useMana)
                R.Cast(minion.ServerPosition);
        }
    }
}