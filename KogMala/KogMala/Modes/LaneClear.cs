using System.Linq;
using EloBuddy.SDK;
using KogMala.AllahuAkbar;
using KogMala.Utils;

namespace KogMala.Modes
{
    internal class LaneClear : Model
    {
        public static void useClear()
        {
            var useW = Misc.isChecked(LaneClearMenu, "clearW");
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                W.Range);

            if (W.IsReady() && useW && minions.Count() >= 3)
            {
                W.Cast();
            }
        }
    }
}