using EloBuddy;
using EloBuddy.SDK;
using Rengod.Core;
using Rengod.Util;

namespace Rengod.Modes
{
    internal class Harass : Model
    {
        public static void useHarass()
        {
            var useQ = Misc.isChecked(HarassMenu, "HarassQ");
            var useW = Misc.isChecked(HarassMenu, "HarassW");
            var useE = Misc.isChecked(HarassMenu, "HarassE");

            var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
            if (target == null || !target.IsValidTarget()) return;

            var cType = Misc.getSliderValue(HarassMenu, "hPrio");

            if (Ferocity == 5)
            {
                if (cType == 0)
                    Casts.useE(target);
                else if (cType == 1)
                    Casts.useQ(target);
            }
            if (Ferocity < 5)
            {
                if (useQ)
                    Casts.useQ(target);

                if (RengarR)
                    return;
                if (useE)
                    Casts.useE(target);

                if (useW)
                    Casts.useW(target);
            }
        }
    }
}